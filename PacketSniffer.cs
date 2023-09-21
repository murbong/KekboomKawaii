using KekboomKawaii.Models;
using KekboomKawaii.Tools;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace KekboomKawaii
{
    class StreamBuffer
    {
        public const int maxSize = 16384;
        public int size = default;
        public byte[] stream = new byte[maxSize];

    }
    public class PacketSniffer
    {
        public readonly IList<LivePacketDevice> AllDevices = LivePacketDevice.AllLocalMachine;

        private LivePacketDevice selectedDevice;
        private StreamBuffer streamBuffer = new StreamBuffer();

        private PacketCommunicator communicator = null;

        public event Action<UserChat> UserChatEvent;

        public event Action<PlayerData> PlayerDataEvent;

        private List<string> LastTenChatChecksum = new List<string>();


        public void Select(int index)
        {
            if (AllDevices.Count == 0)
            {
                return;
            }
            selectedDevice = AllDevices[index];
        }

        public Task RunAsync()
        {
            return Task.Run(() => Run());
        }

        public void Run()
        {
            if (selectedDevice == null) return;

            using (communicator = selectedDevice.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                if (communicator.DataLink.Kind != DataLinkKind.Ethernet) return;

                using (var filter = communicator.CreateFilter("ip and tcp"))
                {
                    communicator.SetFilter(filter);
                }
                var result = communicator.ReceivePackets(0, packetHandler);

                if (result == PacketCommunicatorReceiveResult.BreakLoop) return;
            }
        }
        public void Stop()
        {
            if (communicator != null)
            {
                communicator.Break();
            }
        }
        private void packetHandler(Packet packet)
        {
            var ip = packet.Ethernet.IpV4;
            var tcp = ip.Tcp;


            if (ServerList.IsSourceIsServer(ip.Source.ToString()))
            {
                TcpAssembly(tcp.Payload.ToArray(), tcp.PayloadLength);
            }
        }
        private void TcpAssembly(byte[] stream, int size)
        {
            try
            {
                if (size > 0)
                {
                    stream.BlockCopy(0, streamBuffer.stream, streamBuffer.size, size);
                    streamBuffer.size += size;
                    do
                    {
                        var check = ParseAssembly(streamBuffer.stream, streamBuffer.size);
                        if (check == 0)//1460
                        {
                            return;
                        }
                        if (check < 0 || check > streamBuffer.size)//별난놈
                        {
                            streamBuffer.size = 0;
                            return;
                        }
                        streamBuffer.stream.BlockCopy(check, streamBuffer.stream, 0, streamBuffer.size - check);
                        streamBuffer.size -= check;
                    }
                    while (streamBuffer.size != 0);// 패킷헤더가 여러개가 한꺼번에 들어오는걸 처리함.
                }
            }
            catch
            {

            }

        }
        private int ParseAssembly(byte[] stream, int size)
        {
            if (size < 2)//걸러
            {
                return 0;
            }
            using (MemoryStream input = new MemoryStream(stream))
            using (BinaryReader bin = new BinaryReader(input))
            {
                int headerLength = bin.ReadInt32() + 4;
                if (headerLength < 8 || headerLength > 16384)//걸르자.
                {
                    return -1;

                }
                if (size < headerLength)//분할되서 날라올때.
                {
                    return 0;
                }
                if (size > headerLength)
                {
                    return -1;
                }
                ParsePayload(stream, headerLength);
                return headerLength;
            }
        }
        private void ParsePayload(byte[] stream, int size)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(stream, 0, size))
                using (TCPBinaryReader br = new TCPBinaryReader(ms))
                {
                    Debug.WriteLine(size);
                    TOFPacket tofpacket = new TOFPacket();

                    tofpacket.Read(br);

                    if (tofpacket.Flag == 0x6A9)
                    {
                        br.BaseStream.Position = 0;
                        UserChat userChat = new UserChat();
                        userChat.Read(br);

                        if (!LastTenChatChecksum.Contains(userChat.Checksum))
                        {
                            LastTenChatChecksum.Add(userChat.Checksum);
                            UserChatEvent?.Invoke(userChat);

                            if (LastTenChatChecksum.Count > 10)
                            {
                                LastTenChatChecksum.Clear();
                            }
                        }
                    }
                    else if (tofpacket.Flag == 0x140)
                    {
                        br.BaseStream.Position = 0;
                        PlayerData playerData = new PlayerData();
                        playerData.Read(br);
                        PlayerDataEvent?.Invoke(playerData);

                    }
                }
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);
            }


        }
    }
}
