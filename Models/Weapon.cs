using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KekboomKawaii.Models
{
    public class Weapon
    {

        public string Name { get; set; }
        public int Enchant { get; set; }
        public int Level { get; set; }
        public int Star { get; set; }
        public int MaxLevel { get; set; }
        public int CurrentEnchant { get; set; }
        public string DisplayWeaponImage=> $@"pack://application:,,,/Resources/Weapon/{Name}.png";
        public Weapon()
        {

        }

        public Weapon(string rawWeapon) // Dkatana_ice#0##150#&&2:150:14
        {
            var reg = new Regex(@"(.+)#(\d+)##(\d+)#&&(\d+):(\d+):(\d+)");

            var resultCollection = reg.Matches(rawWeapon)[0].Groups;

            Name = resultCollection[1].Value;

            Enchant = int.Parse(resultCollection[2].Value);
            Level = int.Parse(resultCollection[3].Value);
            Star = int.Parse(resultCollection[4].Value);
            MaxLevel = int.Parse(resultCollection[5].Value);
            CurrentEnchant = int.Parse(resultCollection[6].Value);
        }
    }
}
