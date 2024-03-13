using KekboomKawaii.Tools;
using Newtonsoft.Json;

namespace KekboomKawaii.Models
{
    public class TOFPacket : ITCPReadable
    {
        [JsonIgnore] public int HeaderLength { get; set; }
        [JsonIgnore] public int ClassLength { get; set; }
        [JsonIgnore] public int Header2Length { get; set; }
        [JsonIgnore] public int Class2Length { get; set; }
        [JsonIgnore] public uint Flag { get; set; }

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
