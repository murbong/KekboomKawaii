using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KekboomKawaii.Models
{
    public enum EnchantElement
    {
        None,
        Physic,
        Thunder,
        Ice,
        Fire,
        Superpower
    }

    public enum EnchantType
    {
        None,
        Atk,
        Def,
        Crit
    }


    public class EnchantProperty
    {
        public string Name { get; set; }
        public float Value { get; set; }

        public string Titan { get; set; }
        public EnchantElement Element { get; set; } = EnchantElement.None;
        public EnchantType Type { get; set; } = EnchantType.None;

        public int Priority { get; set; } = 0;

        public bool isEnchantMult = false;

        public bool isExtraUp = false;
        public string DisplayValue
        {
            get
            {
                if (Name.Contains("Mult") || Name.Contains("FinalCrit"))
                {
                    if (Name.Contains("ExtraUp"))
                    {
                        return $"{Value * 100:F2}(E)%";
                    }
                    return $"{Value * 100:F2}%";

                }
                return Value.ToString("F2");
            }
        }

        public string DisplayPropertyImage
        {
            get
            {
                if (Name.Contains("ThunderAtk"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_atk_dian.png";
                else if (Name.Contains("CommonAtk"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_atk.png";
                else if (Name.Contains("IceAtk"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_atk_bing.png";
                else if (Name.Contains("PhyAtk"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_atk_physic.png";
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
                else if (Name.Contains("SuperpowerAtk"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_yineng.png";
                else if (Name.Contains("SuperpowerDef"))
                    return $@"pack://application:,,,/Resources/Enchant/icon_def_yineng.png";
                else if (Name.Contains("uncommon"))
                {
                    if (Titan.Contains("CommonAtkAggravatingInjury"))
                    {
                        return $@"pack://application:,,,/Resources/Enchant/Red/icon_pugongzengshang.png";
                    }
                    else if (Titan.Contains("EvadeAtkAggravatingInjury"))
                    {
                        return $@"pack://application:,,,/Resources/Enchant/Red/icon_shanAzs.png";
                    }
                    else if (Titan.Contains("SkillAtkAggravatingInjury"))
                    {
                        return $@"pack://application:,,,/Resources/Enchant/Red/icon_skillzengshang.png";
                    }
                    else if (Titan.Contains("ChangeSkillAtkAggravatingInjury"))
                    {
                        return $@"pack://application:,,,/Resources/Enchant/Red/icon_lianxiezs.png";
                    }
                    else if (Titan.Contains("CommonAggravatingInjury"))
                    {
                        return $@"pack://application:,,,/Resources/Enchant/Red/icon_create_increases_damage.png";
                    }
                    else if (Titan.Contains("CommonReduceInjuries"))
                    {
                        return $@"pack://application:,,,/Resources/Enchant/Red/icon_reduce_receivedamage.png";
                    }
                    else if (Titan.Contains("FinalRigidity"))
                    {
                        return $@"pack://application:,,,/Resources/Enchant/Red/icon_heal.png";
                    }
                    else if (Titan.Contains("SlowMult"))
                    {
                        return $@"pack://application:,,,/Resources/Enchant/Red/icon_chihuan.png";
                    }
                    else if (Titan.Contains("CommonWeaknessIncreaseInjury"))
                    {
                        return $@"pack://application:,,,/Resources/Enchant/Red/icon_ruodianzengshang.png";
                    }
                    else if (Titan.Contains("CommonDamageRecoverHPMult"))
                    {
                        return $@"pack://application:,,,/Resources/Enchant/Red/icon_xixue.png";
                    }
                    else if (Titan.Contains("AutoRestoreHPMult"))
                    {
                        return $@"pack://application:,,,/Resources/Enchant/Red/icon_huifu.png";
                    }
                    else if (Titan.Contains("CommonBlockMult"))
                    {
                        return $@"pack://application:,,,/Resources/Enchant/Red/icon_block.png";
                    }
                }
                return "";
            }
        }

        public string DisplayBackgroundColor => Titan.Equals("EquipmentTupo") ? "Yellow" : "White";

        public EnchantProperty()
        {

        }
        public EnchantProperty(string name, float value,string titan)
        {
            Name = name;
            Value = value;
            Titan = titan;

            if (Name.Contains("uncommon"))
            {
                Priority += 10;
            }

            if (Name.Contains("Mult") || Name.Contains("FinalCrit"))
            {
                isEnchantMult = true;
                Priority++;
            }
            if (Name.Contains("ExtraUp"))
            {
                isExtraUp = true;
                Priority--;
            }
            if (Name.Contains("Atk"))
            {
                Type = EnchantType.Atk;
                Priority++;
            }
            else if (Name.Contains("Crit"))
            {
                Type = EnchantType.Crit;
            }
            else if (Name.Contains("Def"))
            {
                Type = EnchantType.Def;
                Priority--;
            }

            if (Name.Contains("Thunder"))
            {
                Element = EnchantElement.Thunder;
                Priority++;
            }
            else if (Name.Contains("Ice"))
            {
                Element = EnchantElement.Ice;
                Priority++;
            }
            else if (Name.Contains("Fire"))
            {
                Element = EnchantElement.Fire;
                Priority++;
            }
            else if (Name.Contains("Phy"))
            {
                Element = EnchantElement.Physic;
                Priority++;
            }
        }
    }
}
