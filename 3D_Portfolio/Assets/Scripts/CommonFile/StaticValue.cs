using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class NumValue
    {
        public static readonly int INVALID_NUMBER = (-1); //잘못된 값
    }
    public static class Tag
    {
        public static readonly string PLAYER = "Player";
        public static readonly string MUSHROOM = "Mushroom";
        public static readonly string STONE_MONSTER = "StoneMonster";
        public static readonly string PUMPKIN = "Pumpkin";
        public static readonly string PLAY_PACK = "PlayPack";
        public static readonly string MINI_MAP_CAMERA = "MiniMapCamera";
    }
    public static class AnimParam
    {
        public static readonly string MOVE_SPEED = "MoveSpeed";
        public static readonly string DEFAULT_ATTACK = "Default_Attack";
        public static readonly string SKILL_01 = "Skill_01";
        public static readonly string SKILL_02 = "Skill_02";
        public static readonly string SKILL_03 = "Skill_03";
        public static readonly string DAMAGE = "Damage";
        public static readonly string DEATH = "Death";
    }
    public static class ResourcePath
    {
        public static readonly string MUSHROOM_01 = "Enemy/Mushroom_1";
        public static readonly string MUSHROOM_02 = "Enemy/Mushroom_2";
        public static readonly string MUSHROOM_03 = "Enemy/Mushroom_3";
        public static readonly string STONE_MONSTER = "Enemy/StoneMonster";
        public static readonly string PUMPKIN = "Enemy/Pumpkin";

        public static readonly string DB_LEVEL = "DB/Level";
        public static readonly string DB_ENEMY = "DB/Enemy";
        public static readonly string DB_DUNGEON = "DB/Dungeon";
        public static readonly string DB_WEAPON = "DB/Weapon";
        public static readonly string DB_ITEM = "DB/Item";

        public static readonly string UI_PROGRESSBAR = "UI/Canvas Progressbar";
        public static readonly string UI_DAMAGE = "UI/Canvas Damage";
        public static readonly string UI_MESSAGE = "UI/Canvas Message";
        public static readonly string UI_BOSS_NAME = "UI/BossName";

        public static readonly string PT_DEFAULT = "Particle/Default_Attack";
        public static readonly string PT_SKILL_01 = "Particle/Skill_01";
        public static readonly string PT_SKILL_02 = "Particle/Skill_02";
        public static readonly string PT_SKILL_03 = "Particle/Skill_03";
        public static readonly string PT_PUMPKIN_ATTACK = "Particle/Pumpkin_Attack";
        public static readonly string PT_PUMPKIN_SKILL_01 = "Particle/Pumpkin_Skill_01";

        public static readonly string POPUP_DUNGEON = "Popup/DungeonPopup";
        public static readonly string POPUP_WIN = "Popup/WinPopup";
        public static readonly string POPUP_LOSE = "Popup/LosePopup";
        public static readonly string POPUP_EXIT = "Popup/ExitPopup";
        public static readonly string POPUP_SHOP = "Popup/ShopPopup";
        public static readonly string POPUP_REINFORCE = "Popup/ReinforcePopup";
        public static readonly string POPUP_INVENTORY = "Popup/InventoryPopup";
        public static readonly string POPUP_STATS = "Popup/StatsPopup";
        public static readonly string POPUP_SETTING = "Popup/SettingPopup";
        public static readonly string POPUP_OK = "Popup/OkPopup";
        
        public static readonly string IMAGE_TAB_ACTIVE = "Image/Tab_Active";
        public static readonly string IMAGE_TAB_INACTIVE = "Image/Tab_Inactive";

        public static readonly string ICON_SLOT = "Icon/Slot";

        public static readonly string PLAY_PACK = "PlayPack";
    }

    public static class Message
    {
        public static readonly string NO_MONEY = "You don't have enough money.";
        public static readonly string HAD_WEAPON = "You already have this weapon.";
    }
}
