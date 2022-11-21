using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KekboomKawaii.Models
{
    public class Equipment
    {

        public string Name { get; set; }
        public int Enchant { get; set; }
        public int Star { get; set; }
        public Dictionary<string, float> Properties { get; set; }

        public Equipment()
        {
            Properties = new Dictionary<string, float>();
        }
        public Equipment(string rawEquipment) : this() //shawl_orange#29#1,CommonAtkAdded;2,679.502686;|1,IceDefAdded;2,1366.897705;|1,ThunderDefAdded;2,215.000000;|1,MaxHealthAdded;2,4125.000000;#5#
        {

            var reg = new Regex(@"(\w+)#(\d+)#(\d+),(\w+);([\d,]*\.?\d*);\|(\d+),(\w+);([\d,]*\.?\d*);\|(\d+),(\w+);([\d,]*\.?\d*);\|(\d+),(\w+);([\d,]*\.?\d*);#(\d+)#");

            var resultCollection = reg.Matches(rawEquipment)[0].Groups;

            Name = resultCollection[1].Value;

            Enchant = int.Parse(resultCollection[2].Value);

            for (var i = 0; i < 4; i++)
            {
                Properties.Add(resultCollection[4 + i * 3].Value, float.Parse(resultCollection[5 + i * 3].Value));
            }

            Star = int.Parse(resultCollection[15].Value);
        }
    }
}
