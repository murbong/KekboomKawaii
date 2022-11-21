using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KekboomKawaii.Models
{
    public class EnchantProperty
    {


        public string Name { get; set; }
        public float Value { get; set; }

        public string DisplayValue { get
            {
                if (Name.Contains("Mult"))
                {
                    return $"{Value * 100}%";
                }
                return Value.ToString("F2");
            } 
        }

        public string DisplayPropertyImage
        {
            get
            {
                if(Name.Contains("ThunderAtk"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_atk_dian.png";
                else if (Name.Contains("CommonAtk"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_atk.png";
                else if (Name.Contains("IceAtk"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_atk_bing.png";
                else if (Name.Contains("PhyAtk"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_atk.png";
                else if (Name.Contains("FireAtk"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_atk_huo.png";

                else if (Name.Contains("PhyDef"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_def.png";
                else if (Name.Contains("ElementDef"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_resist_all.png";
                else if (Name.Contains("IceDef"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_def_bing.png";
                else if (Name.Contains("FireDef"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_def_huo.png";
                else if (Name.Contains("ThunderDef"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_def_dian.png";
                else if (Name.Contains("MaxHealth"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_hp.png";
                else if (Name.Contains("Crit"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_baoji.png";
                return "";
            }
        }

        public EnchantProperty()
        {

        }
        public EnchantProperty(string name, float value)
        {
            Name = name;
            Value= value;
        }
    }
}
