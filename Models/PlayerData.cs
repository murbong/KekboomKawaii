using KekboomKawaii.Tools;
using PcapDotNet.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace KekboomKawaii.Models
{
    [Serializable]
    public class PlayerData : TOFPacket
    {


        public string CurentPosition { get; set; }
        public string PlayerName { get; set; }
        public string Unknown { get; set; }
        public string UID { get; set; }
        public Dictionary<string, object> KeyData { get; set; }

        public PlayerData()
        {
            KeyData = new Dictionary<string, object>();
        }

        public override void Read(TCPBinaryReader reader)
        {
            base.Read(reader);

            reader.ReadBytes(4);
            var length = reader.ReadInt32();
            if (length == 0x10)
            {
                return;
            }

            reader.ReadBytes(length);

            reader.ReadBytes(0x1c);

            CurentPosition = reader.ReadString();
            PlayerName = reader.ReadString();
            Unknown = reader.ReadString();
            UID = reader.ReadString();


            reader.ReadBytes(reader.ReadInt32());
            reader.ReadBytes(4);

            var list = reader.ReadIntList();

            for (var i = 0; i < 200; i++)
            {
                reader.ReadBytes(4);
                var type = reader.ReadInt32();
                var bodyLength = reader.ReadInt32();
                var trailLength = reader.ReadInt32();

                reader.ReadBytes(trailLength);

                if (type == 0x1000000) // int string
                {
                    int value = 0;
                    if (trailLength > 0x4) reader.BaseStream.Position -= 4;
                    if (bodyLength != 0xc)
                    {
                        value = reader.ReadInt32();
                    }
                    if (bodyLength - trailLength > 0x10) reader.BaseStream.Position += 4;
                    var key = reader.ReadString();

                    if (key == "level") reader.BaseStream.Position += 8;

                    KeyData.Add(key, value);

                }
                else if (type == 0xB000000) // bool string
                {
                    var boolean = false;

                    if (bodyLength - trailLength > 0x8)
                    {
                        boolean = reader.ReadInt32() == 1 ? true : false;
                    }
                    if (bodyLength - trailLength > 0x10) reader.BaseStream.Position += 4;

                    var key = reader.ReadString();

                    KeyData.Add(key, boolean);

                }
                else if (type == 0x3000000) // uint64 string
                {
                    ulong int64 = 0;

                    if (bodyLength != 0xc)
                    {
                        int64 = reader.ReadUInt64();
                    }
                    if (bodyLength - trailLength > 0x10) reader.BaseStream.Position += 4;

                    var key = reader.ReadString();

                    KeyData.Add(key, int64);
                }
                else if (type == 0x6000000) // string string
                {
                    var whatisthis = reader.ReadInt32();
                    var value = reader.ReadString();
                    var key = reader.ReadString();

                    if (key == "NameReportID")
                    {
                        reader.BaseStream.Position += 0x28;
                        break;
                    }
                    KeyData.Add(key, value);

                }
                else if (type == 0x8000000) // float string
                {
                    float value = 0f;

                    if (bodyLength != 0xc)
                    {
                        value = reader.ReadSingle();
                    }
                    var key = reader.ReadString();
                    KeyData.Add(key, value);
                }
                else
                {
                    break;
                }
            }

            var name = reader.ReadString();


            foreach (var kv in KeyData)
            {
                var key = kv.Key;
                var value = kv.Value;

                Console.WriteLine($"{key} : {value}");
            }
        }
    }
}
