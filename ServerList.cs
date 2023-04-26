using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KekboomKawaii
{

    public static class ServerList
    {
        private static string[] serverDNSList = {
            "ht1.pwrdoverseagame.com",
            "ht2.pwrdoverseagame.com",
            //"ht3.pwrdoverseagame.com",
            //"ht4.pwrdoverseagame.com",
            //"ht5.pwrdoverseagame.com",
            //"ht6.pwrdoverseagame.com",
            //"ht7.pwrdoverseagame.com",

            /*"htk1.pwrdoverseagame.com",
            "htk2.pwrdoverseagame.com",
            "htk3.pwrdoverseagame.com",
            "htk4.pwrdoverseagame.com", 전투 udp 서버 사용 X*/


        };
        private static string[] serverIPList;
        static ServerList()
        {
            serverIPList = serverDNSList.Select(dns => Dns2IP(dns)).ToArray();
        }

        public static bool IsSourceIsServer(string source)
        {
            return serverIPList.Contains(source);
        }
        private static string Dns2IP(string dns)
        {
            return Dns.GetHostAddresses(dns)[0].ToString();
        }
    }
}
