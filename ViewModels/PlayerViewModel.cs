using KekboomKawaii.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace KekboomKawaii.ViewModels
{


    public class PlayerViewModel : ViewModelBase
    {
        PlayerData playerData;

        private RelayCommand saveCurrentPlayerCommand;

        public ICommand SaveCurrentPlayerCommand => saveCurrentPlayerCommand ?? (saveCurrentPlayerCommand = new RelayCommand(SaveCurrentPlayer));

        public void SaveCurrentPlayer(object sender)
        {
            File.WriteAllText($@"Player/{UID}_{PlayerName}.json", JsonConvert.SerializeObject(playerData, Formatting.Indented));
            //File.WriteAllText($@"Player/{UID}_{PlayerName}_{DateTime.Now.ToString("yyyyMMdd")}.json", JsonConvert.SerializeObject(playerData, Formatting.Indented));
        }

        public string CurentPosition => playerData.CurentPosition;
        public string PlayerName => playerData.PlayerName;
        public string UID => playerData.UID;
        public string UUID
        {
            get
            {
                ulong uint64 = (ulong)GetValue("uid");
                return $"{uint64 >> 32}{(uint)uint64}";
            }
        }

        public string LastConnectTime => new DateTime((long)(ulong)GetValue("OfflineMoment")).ToShortDateString();
        public string Title => GetValue("EquippingTitle").ToString();
        public string GuildName => GetValue("GuildName").ToString();
        public string GuildPost => Global.GuildPostDictionary[(GuildPostEnum)GetValue("GuildPost")];
        public GenderEnum Gender => (GenderEnum)GetValue("RoleInfoSex");

        #region Equipment Property
        public float SuperpowerAttackBase => float.TryParse(GetValue("SuperpowerAtkDisplayBase").ToString(), out float value) ? value : 0.0f;
        public float ThunderAtkBase => float.TryParse(GetValue("ThunderAtkDisplayBase").ToString(), out float value) ? value : 0.0f;
        public float IceAtkBase => float.TryParse(GetValue("IceAtkDisplayBase").ToString(), out float value) ? value : 0.0f;
        public float FireAtkBase => float.TryParse(GetValue("FireAtkDisplayBase").ToString(), out float value) ? value : 0.0f;
        public float PhysicAttackBase => float.TryParse(GetValue("PhyAtkDisplayBase").ToString(), out float value) ? value : 0.0f;
        public float SuperpowerAttack => float.TryParse(GetValue("SuperpowerAtk").ToString(), out float value) ? value : 0.0f;
        public float ThunderAtk => float.TryParse(GetValue("ThunderAtk").ToString(), out float value) ? value : 0.0f;
        public float IceAtk => float.TryParse(GetValue("IceAtk").ToString(), out float value) ? value : 0.0f;
        public float FireAtk => float.TryParse(GetValue("FireAtk").ToString(), out float value) ? value : 0.0f;
        public float PhysicAttack => float.TryParse(GetValue("PhysicalAttack").ToString(), out float value) ? value : 0.0f;
        public float HP => float.TryParse(GetValue("MaxHP").ToString(), out float value) ? value : 0.0f;
        public float Critical => float.TryParse(GetValue("Crit").ToString(), out float value) ? value : 0.0f;
        public float CriticalRatio => float.TryParse(GetValue("GetCritMult").ToString(), out float value) ? value : 0.0f;
        public float FinalCrit => float.TryParse(GetValue("FinalCrit").ToString(), out float value) ? value : 0.0f;
        public int Level => (int)GetValue("level");

        public EnchantElement PrimaryElement
        {
            get
            {
                var AttackArr = new float[] { ThunderAtk, IceAtk, FireAtk, PhysicAttack };

                var MaxAttack = AttackArr.OrderByDescending(a => a).First();

                if (MaxAttack == ThunderAtk) return EnchantElement.Thunder;
                else if (MaxAttack == IceAtk) return EnchantElement.Ice;
                else if (MaxAttack == FireAtk) return EnchantElement.Fire;
                else if (MaxAttack == PhysicAttack) return EnchantElement.Physic;
                else return EnchantElement.None;
            }
        }

        private object GetValue(string key)
        {
            if (playerData.KeyData.TryGetValue(key, out object value))
            {
                return value;

            }
            return new object();
        }

        public int Compare(PlayerViewModel y)
        {
            return this.PlayerName.CompareTo(y.PlayerName);
        }

        public List<Weapon> WeaponList
        {
            get
            {
                var list = new List<Weapon>();

                for (var i = 0; i < 3; i++)
                {
                    if (playerData.KeyData.TryGetValue($"Weapon_{i}", out object val))
                    {
                        list.Add(new Weapon((string)val));
                    }
                }
                return list;

            }
        }

        public List<Equipment> EquipmentList
        {
            get
            {

                var AttackArr = new float[] { ThunderAtkBase, IceAtkBase, FireAtkBase, PhysicAttackBase };

                var MaxAttackBase = AttackArr.OrderByDescending(a => a).First();

                var list = new List<Equipment>();

                for (var i = 0; i < 20; i++)
                {
                    if (playerData.KeyData.TryGetValue($"Equipment_{i}", out object val))
                    {
                        list.Add(new Equipment((string)val, MaxAttackBase, PrimaryElement));
                    }
                }

                return list.OrderBy(x => x.GetAttackValue(PrimaryElement)).OrderByDescending(x => x.Priority).ToList();

            }
        }

        public BitmapImage DisplayedAvatar
        {
            get
            {
                return Global.GetImage(GetValue("AvatarId").ToString(), ImageEnum.Avatar);
            }
        }
        public BitmapImage DisplayedAvatarFrame
        {
            get
            {
                return Global.GetImage(GetValue("AvatarFrameId").ToString(), ImageEnum.AvatarFrame);
            }
        }
        public string DisplayedSuppressor => $@"pack://application:,,,/Resources/Suppressor/{GetValue("ShenGeLevel")}.png";
        public string DisplayedGender => $@"pack://application:,,,/Resources/Gender/{(Gender == GenderEnum.Female ? "Female" : "Male")}.png";

        public int GS => (int)GetValue("BattleStrengthScore");

        #endregion


        public PlayerViewModel() { }

        public PlayerViewModel(PlayerData playerData)
        {
            this.playerData = playerData;

            SaveCurrentPlayer(this);
        }
    }
}
