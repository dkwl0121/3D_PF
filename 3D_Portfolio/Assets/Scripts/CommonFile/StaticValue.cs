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
    }

    //public static class AnimStateTag
    //{
    //    public static readonly string MOVE = "Move";
    //    public static readonly string ATTACK = "Attack";
    //}
}
