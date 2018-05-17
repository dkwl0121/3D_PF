using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_SCENE_NO
{
    INVALID = (-1),
    TITLE,
    ROBBY,
    TOWN,
    DUNGEON,
    LOADING,
    MAX
}

public enum E_CHARACTER_TYPE
{
    PLAYER = -2,
    INVALID = (-1),
    MUSHROOM_01,
    MUSHROOM_02,
    MUSHROOM_03,
    STONE_MONSTER,
    PUMPKIN,
    MAX
}

public enum E_DUNGEON_NO
{
    INVALID = (-1),
    DUNGEON_01,
    DUNGEON_02,
    DUNGEON_03,
    MAX
}

public enum E_PLAYER_ATTACK_NO
{
    INVALID = (-1),
    DEFAULT,
    SKILL_01,
    SKILL_02,
    SKILL_03,
    MAX
}

public enum E_LAYER_TYPE
{
    PLAYER = 8,
    ENEMY
}

public enum E_CAMERA_CTRL_TYPE
{
    DEFAULT,
    STRAIGHT,
    SHAKE,
    SHOW_BOSS
}

public enum E_SHOP_TAB
{
    WEAPON,
    ITEM
}

public enum E_REINFORCE_TAB
{
    WEAPON,
    STRENGTH
}

public enum E_INVENTORY_TAB
{
    WEAPON,
    ITEM
}

public enum E_QUEST_LIST
{
    INVALID = (-1),
    FIND_DUNGEON,
    RED_MUSHROOM,
    BUY_HP_POSION,
    STONE_MONSTER,
    REINFORCE,
    PUMPKIN,
    MAX
}

public enum E_BGM_SOUND_LIST
{
    INVALID = (-1),
    ROBBY,
    TOWN,
    DUNGEON,
    BOSS,
    MAX
}

public enum E_EFT_SOUND_LIST
{
    INVALID = (-1),
    //== 플레이어 ==
    PLAYER_ATT,
    PLAYER_SKILL_01,
    PLAYER_SKILL_02,
    PLAYER_SKILL_03,
    PLAYER_DIE,
    PLAYER_LEVEL_UP,
    // == 에너미 ==
    ENEMY_ATT,
    ENEMY_SKILL,
    ENEMY_DIE,
    ENEMY_BOSS,             // 카메라 줌 일 때 사운드
    // == 기타 소리 ==
    SELECT,                 // 탭버튼, 아이템선택, 로비신에서 버튼 선택, 플러스버튼
    COIN,
    POPUP,
    // == 게임 ==
    WIN,
    LOSE,
    MAX
}