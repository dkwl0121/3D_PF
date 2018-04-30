using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAttackFunction : IAttackFunction
{
    private new void Awake()
    {
        base.Awake();

        arrAnimParam = new string[(int)E_PLAYER_ATTACK_NO.DEFAULT + 1];
        arrAnimParam[(int)E_PLAYER_ATTACK_NO.DEFAULT] = Util.AnimParam.DEFAULT_ATTACK;
    }
}
