using KekboomKawaii.Tools;

using System;
using System.Diagnostics;

namespace KekboomKawaii.Models
{
    public class UserChat : TOFPacket
    {
        public int ChatID { get; set; }
        public ChatClassFlag ChatClass { get; set; }
        public int AtUID { get; set; }
        public int AtServer { get; set; }
        public string Violation { get; set; }
        public string Message { get; set; }
        public string Checksum { get; set; }
        public string Token { get; set; }
        public string AppropLevel { get; set; }
        public int Level { get; set; }
        public GenderEnum Gender { get; set; }
        public int UID { get; set; }
        public int Server { get; set; }
        public string SuppressorLevel { get; set; }
        public string AvatarFrame { get; set; }
        public string Avatar { get; set; }
        public string ChatBubble { get; set; }
        public string Title { get; set; }
        public string NickName { get; set; }
        public override void Read(TCPBinaryReader reader)
        {
            base.Read(reader);

            reader.ReadBytes(4);
            reader.ReadBytes(reader.ReadInt32());

            ChatID = reader.ReadInt32();

            reader.ReadBytes(0x10);

            var chatClass = (ChatClassEnum)reader.ReadInt32();
            ChatClass = (ChatClassFlag)Enum.Parse(typeof(ChatClassFlag), chatClass.ToString());


            var param = reader.ReadInt32();

            if (param == 0x10) // mention
            {
                AtUID = reader.ReadInt32();
                AtServer = reader.ReadInt32();

                reader.ReadBytes(4);
            }
            else if (param == 0x08)
            {
                reader.ReadBytes(4);
                Violation = reader.ReadString();
            }
            else
            {
                reader.ReadBytes(8);
            }

            Message = reader.ReadString();
            Checksum = reader.ReadString();
            AppropLevel = reader.ReadString();

            reader.ReadBytes(0x1a);

            var len = reader.ReadInt16() - 16; // idk what is this

            reader.ReadBytes(8);

            Level = reader.ReadInt32();
            Gender = (GenderEnum)reader.ReadInt32();

            reader.ReadBytes(len);

            len = reader.ReadInt32();

            UID = reader.ReadInt32();
            Server = reader.ReadInt32();

            reader.ReadBytes(len - 0x10 - 8);

            SuppressorLevel = reader.ReadString();

            Token = reader.ReadString();

            AvatarFrame = reader.ReadString();

            Avatar = reader.ReadString();

            ChatBubble = reader.ReadString();

            Title = reader.ReadString();

            NickName = reader.ReadString();

            Debug.WriteLine(ChatClass.ToString());

        }
    }
}
