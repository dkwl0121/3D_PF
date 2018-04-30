﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum E_SCENE_NO
{
    INVALID = (-1),
    TITLE,
    ROBBY,
    TOWN,
    SHOP,
    DUNGEON,
    MAX
}

enum E_CHARACTER_TYPE
{
    INVALID = (-1),
    PLAYER,
    ENEMY_01,
    ENEMY_02,
    BOSS,
    NPC,
    MAX
}

enum E_CHARACTER_STAT
{
    INVALID = (-1),
    IDLE,
    RUN,
    ATTACK,
    SKILL_01,
    SKILL_02,
    JUMP,
    MAX
}

enum E_PLAYER_ATTACK_NO
{
    INVALID = (-1),
    DEFAULT,
    SKILL_01,
    SKILL_02,
    SKILL_03,
    MAX
}

enum E_ENEMY_TYPE
{
    INVALID = (-1),
    MUSHROOM_01,
    MUSHROOM_02,
    MUSHROOM_03,
    MAX
}