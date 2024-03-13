using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace KekboomKawaii.Models
{
    public class Equipment
    {
        // Fire #ffa286
        // Physic #e6d6ab
        // Thunder #d9b1f3
        // Ice #b0e9ff

        public static Regex reg = new Regex(@"(\w+)#(\d+)#(.+)#(\d+)#");

        public static Regex reg2 = new Regex(@"(\d+),(\w+);(\d+),(\d+\.*\d*);(\d+),(\w+);");

        public string Name { get; set; }
        public int Enchant { get; set; }
        public int Star { get; set; }
        public int Priority { get; set; }

        private float maxAttackBase;

        private EnchantElement playerElement;
        public List<EnchantProperty> Properties { get; set; }

        public float TotalElementDamage
        {
            get
            {
                return GetAttackValue(GetEnchantElement);
            }
        }

        public float GetExtraDamage
        {
            get
            {
                return GetExtraValue(GetEnchantElement);
            }
        }

        public string DisplayEquipmentImage
        {
            get
            {
                var reg = new Regex(@"([0-9])");
                var result = reg.Replace(Name, "");
                return $@"pack://application:,,,/Resources/Equipment/{result}.png";
            }
        }

        public EnchantElement GetEnchantElement
        {
            get
            {
                var first = Properties.Where(prop => prop.Type == EnchantType.Atk && prop.Element != EnchantElement.None)
                    .Select(a => new { a.Name, Value = a.Value * (a.isEnchantMult ? 10000 : 1) * (a.isExtraUp ? 0.72 : 1), a.Element })
                    .OrderByDescending(a => a.Value).FirstOrDefault();
                if (first == null) return EnchantElement.None;
                return first.Element;
            }
        }

        public float GetAttackValue(EnchantElement element)
        {
            return Properties.Where(prop => prop.Type == EnchantType.Atk && (prop.Element == EnchantElement.None || prop.Element == element) && prop.isEnchantMult == false).Sum(a => a.Value);
        }

        public float GetExtraValue(EnchantElement element)
        {
            bool condition(EnchantProperty prop) => prop.Element == element && prop.isEnchantMult == true && prop.Type == EnchantType.Atk;

            var val = Properties.Any(condition);
            if (val == false) return 0;
            return Properties.First(condition).Value * maxAttackBase;

        }

        public Equipment()
        {
            Properties = new List<EnchantProperty>();
        }
        // core_OS_blue#0#1,ThunderAtkAdded;2,20.000000;|1,ThunderDefAdded;2,61.000000;|1,CommonAtkAdded;2,15.000000;#0#

        //shawl_orange#29#1,CommonAtkAdded;2,679.502686;|1,IceDefAdded;2,1366.897705;|1,ThunderDefAdded;2,215.000000;|1,MaxHealthAdded;2,4125.000000;#5#

        public Equipment(string rawEquipment, float maxAttackBase, EnchantElement playerElement) : this()
        {
            this.maxAttackBase = maxAttackBase;
            this.playerElement = playerElement;

            //var reg = new Regex(@"(\w+)#(\d+)#(\d+),(\w+);([\d,]*\.?\d*);\|(\d+),(\w+);([\d,]*\.?\d*);\|(\d+),(\w+);([\d,]*\.?\d*);\|(\d+),(\w+);([\d,]*\.?\d*);#(\d+)#");

            var resultCollection = reg.Matches(rawEquipment)[0].Groups;

            Name = resultCollection[1].Value;

            Enchant = int.Parse(resultCollection[2].Value);

            var properties = resultCollection[3].Value.Split('|');

            foreach (var property in properties)
            {
                var collection = reg2.Matches(property)[0].Groups;

                Properties.Add(new EnchantProperty(collection[2].Value, float.Parse(collection[4].Value), collection[6].Value));
            }

            Properties = Properties.OrderByDescending(a => a.Value).OrderByDescending(a => a.Priority).ToList();

            Star = int.Parse(resultCollection[4].Value);

            if (Name.Contains("core") || Name.Contains("exoskeleton") || Name.Contains("reactor") || Name.Contains("visor"))
            {
                Priority++;
            }

            if (Name.Contains("_red"))
            {
                Priority += 10;
            }
            else if (Name.Contains("_orangeTupo"))
            {
                Priority += 5;
            }


            this.playerElement = playerElement;
        }
    }
}
