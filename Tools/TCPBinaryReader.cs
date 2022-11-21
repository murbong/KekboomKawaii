using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KekboomKawaii.Tools
{
    public class TCPBinaryReader : BinaryReader
    {
        public TCPBinaryReader(Stream input) : base(input)
        {
        }
        public TCPBinaryReader(Stream input, Encoding encoding) : base(input, encoding) { }
        public override string ReadString()
        {
            int length = ReadInt32();
            int cnt = ceilingToNext(length, 0x04);
            byte[] buffer = ReadBytes(length);

            ReadBytes(cnt - length);
            return Encoding.UTF8.GetString(buffer);
        }

        public List<int> ReadIntList()
        {
            var list = new List<int>();
            int cnt = ReadInt32();
            for (int i = 0; i < cnt; i++)
            {
                list.Add(ReadInt32());
            }
            return list;
        }

        private int ceilingToNext(int n, int a)
        {
            return a * (1 + (n / a));
            //return (int)Math.Ceiling(n / (a * 1.0)) * a;
        }
    }
}
