using KekboomKawaii.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using System.IO;

namespace KekboomKawaii.ViewModels
{
    public class PlayerViewModel : ViewModelBase
    {
        PlayerData playerData;

        private RelayCommand saveCurrentPlayerCommand;

        public ICommand SaveCurrentPlayerCommand => saveCurrentPlayerCommand ?? (saveCurrentPlayerCommand = new RelayCommand(SaveCurrentPlayer));

        public void SaveCurrentPlayer(object sender)
        {
            File.WriteAllText($@"Player/{UID}_{PlayerName}_{DateTime.Now.ToString("yyyyMMdd")}.json", JsonConvert.SerializeObject(playerData,Formatting.Indented));
        }

        public string CurentPosition => playerData.CurentPosition;
        public string PlayerName => playerData.PlayerName;
        public string UID => playerData.UID;
        public string UUID
        {
            get
            {
                int[] ints = (int[])GetValue("uid");
                return $"{ints[1]}{ints[0]}";
            }
        }
        public string Title => GetValue("EquippingTitle").ToString();
        public string GuildName => GetValue("GuildName").ToString();
        public GuildPostEnum GuildPost => (GuildPostEnum)GetValue("GuildPost");
        public GenderEnum Gender => (GenderEnum)GetValue("RoleInfoSex");
        public float SuperpowerAttack => float.TryParse(GetValue("SuperpowerAtk").ToString(), out float value) ? value : 0.0f;
        public float ThunderAtk => float.TryParse(GetValue("ThunderAtk").ToString(), out float value) ? value : 0.0f;
        public float IceAtk => float.TryParse(GetValue("IceAtk").ToString(), out float value) ? value : 0.0f;
        public float FireAtk => float.TryParse(GetValue("FireAtk").ToString(), out float value) ? value : 0.0f;
        public float PhysicalAttack => float.TryParse(GetValue("PhysicalAttack").ToString(), out float value) ? value : 0.0f;
        public float HP => float.TryParse(GetValue("MaxHP").ToString(), out float value) ? value : 0.0f;
        public float Critical => float.TryParse(GetValue("Crit").ToString(), out float value) ? value : 0.0f;
        public float CriticalRatio => float.TryParse(GetValue("GetCritMult").ToString(), out float value) ? value : 0.0f;
        public int Level => (int)GetValue("level");

        private object GetValue(string key)
        {
            if (playerData.KeyData.TryGetValue(key, out object value))
            {
                return value;

            }
            return new object();
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
                var list = new List<Equipment>();

                for (var i = 0; i < 20; i++)
                {
                    if (playerData.KeyData.TryGetValue($"Equipment_{i}", out object val))
                    {
                        list.Add(new Equipment((string)val));
                    }
                }
                return list;

            }
        }

        public string DisplayedAvatar => $@"pack://application:,,,/Resources/Avatar/{GetValue("AvatarId")}.png";
        public string DisplayedAvatarFrame => $@"pack://application:,,,/Resources/AvatarFrame/{GetValue("AvatarFrameId")}.png";
        public string DisplayedSuppressor => $@"pack://application:,,,/Resources/Suppressor/{GetValue("ShenGeLevel")}.png";
        public string DisplayedGender => $@"pack://application:,,,/Resources/Gender/{(Gender == GenderEnum.Female ? "Female" : "Male")}.png";

        public int GS => (int)GetValue("BattleStrengthScore");

        public PlayerViewModel() { }

        public PlayerViewModel(PlayerData playerData)
        {
            this.playerData = playerData;
        }
    }
}
