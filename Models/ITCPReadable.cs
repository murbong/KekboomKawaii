using KekboomKawaii.Tools;

namespace KekboomKawaii.Models
{
    interface ITCPReadable
    {
        void Read(TCPBinaryReader reader);
    }
}
