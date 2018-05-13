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