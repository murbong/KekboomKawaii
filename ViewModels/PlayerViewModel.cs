using KekboomKawaii.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KekboomKawaii.ViewModels
{
    public class PlayerViewModel : ViewModelBase
    {
        PlayerData playerData;

        public string CurentPosition => playerData.CurentPosition;
        public string PlayerName => playerData.PlayerName;
        public string UID => playerData.UID;
        public string Title => (string)playerData.KeyData["EquippingTitle"];
        public string GuildName => (string)playerData.KeyData["GuildName"];
        public GuildPostEnum GuildPost => (GuildPostEnum)playerData.KeyData["GuildPost"];
        public GenderEnum Gender => (GenderEnum)playerData.KeyData["RoleInfoSex"];
        public float SuperpowerAttack => (float)playerData.KeyData["SuperpowerAtk"];
        public float ThunderAtk => (float)playerData.KeyData["ThunderAtk"];
        public float IceAtk => (float)playerData.KeyData["IceAtk"];
        public float FireAtk => (float)playerData.KeyData["FireAtk"];
        public float PhysicalAttack => (float)playerData.KeyData["PhysicalAttack"];
        public float HP => (float)playerData.KeyData["MaxHP"];
        public float Critical => (float)playerData.KeyData["Crit"];
        public float CriticalRatio => (float)playerData.KeyData["GetCritMult"];
        public int Level => (int)playerData.KeyData["level"];




        public List<Weapon> WeaponList
        {
            get
            {
                var list = new List<Weapon>();

                for(var i = 0; i < 3; i++)
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

                for (var i = 0; i < 12; i++)
                {
                    if (playerData.KeyData.TryGetValue($"Equipment_{i}", out object val))
                    {
                        list.Add(new Equipment((string)val));
                    }
                }
                return list;

            }
        }

        public string DisplayedAvatar => $@"pack://application:,,,/Resources/Avatar/{playerData.KeyData["AvatarId"]}.png";
        public string DisplayedAvatarFrame => $@"pack://application:,,,/Resources/AvatarFrame/{playerData.KeyData["AvatarFrameId"]}.png";
        public string DisplayedSuppressor => $@"pack://application:,,,/Resources/Suppressor/{playerData.KeyData["ShenGeLevel"]}.png";
        public string DisplayedGender => $@"pack://application:,,,/Resources/Gender/{(Gender == GenderEnum.Female ? "Female" : "Male")}.png";

        public int GS => (int)playerData.KeyData["BattleStrengthScore"];

        public PlayerViewModel() { }

        public PlayerViewModel(PlayerData playerData)
        {
            this.playerData = playerData;
        }
    }
}
