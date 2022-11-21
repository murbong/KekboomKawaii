using KekboomKawaii.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KekboomKawaii.Models
{
    public class TOFPacket : ITCPReadable
    {
        public int HeaderLength { get; set; }
        public int ClassLength { get; set; }
        public int Header2Length { get; set; }
        public int Class2Length { get; set; }
        public uint Flag { get; set; }

        public virtual void Read(TCPBinaryReader reader)
        {
            HeaderLength = reader.ReadInt32();
            ClassLength = reader.ReadInt32();

            reader.ReadBytes(ClassLength + 4);

              Header2Length = reader.ReadInt32();
            Class2Length = reader.ReadInt32();

            reader.ReadBytes(Class2Length + 4);

            Flag = reader.ReadUInt32();
        }
    }
}
