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
        public List<EnchantProperty> Properties { get; set; }

        public string DisplayEquipmentImage
        {
            get
            {
                var reg = new Regex(@"([0-9])");
                var result = reg.Replace(Name, "");
                return $@"pack://application:,,,/Resources/Equipment/{result}.png";
            }
        }


        public Equipment()
        {
            Properties = new List<EnchantProperty>();
        }
        // core_OS_blue#0#1,ThunderAtkAdded;2,20.000000;|1,ThunderDefAdded;2,61.000000;|1,CommonAtkAdded;2,15.000000;#0#

        //shawl_orange#29#1,CommonAtkAdded;2,679.502686;|1,IceDefAdded;2,1366.897705;|1,ThunderDefAdded;2,215.000000;|1,MaxHealthAdded;2,4125.000000;#5#

        public Equipment(string rawEquipment) : this()
        {

            //var reg = new Regex(@"(\w+)#(\d+)#(\d+),(\w+);([\d,]*\.?\d*);\|(\d+),(\w+);([\d,]*\.?\d*);\|(\d+),(\w+);([\d,]*\.?\d*);\|(\d+),(\w+);([\d,]*\.?\d*);#(\d+)#");
            var reg = new Regex(@"(\w+)#(\d+)#(.+)#(\d+)#");

            var reg2 = new Regex(@"(\d+),(\w+);(\d+),(\d+\.*\d*);");

            var resultCollection = reg.Matches(rawEquipment)[0].Groups;

            Name = resultCollection[1].Value;

            Enchant = int.Parse(resultCollection[2].Value);

            var properties = resultCollection[3].Value.Split('|');

            foreach (var property in properties)
            {
                var collection = reg2.Matches(property)[0].Groups;

                Properties.Add(new EnchantProperty(collection[2].Value, float.Parse(collection[4].Value)));
            }

            Star = int.Parse(resultCollection[4].Value);
        }
    }
}
