
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAttackFunction : IAttackFunction
{
    private new void Awake()
    {
        base.Awake();

        fDelayAtt = 3.0f;

        arrAnimParam = new string[(int)E_PLAYER_ATTACK_NO.DEFAULT + 1];
        arrAnimParam[(int)E_PLAYER_ATTACK_NO.DEFAULT] = Util.AnimParam.DEFAULT_ATTACK;
    }

    public new void StartDamage(int index)
    {
        if (index < (int)E_PLAYER_ATTACK_NO.DEFAULT || index >= (int)E_PLAYER_ATTACK_NO.MAX) return;

        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.ENEMY_ATT);

        Collider[] cols = null;
        switch ((E_PLAYER_ATTACK_NO)index)
        {
            case E_PLAYER_ATTACK_NO.DEFAULT:
                {
                    float size = ((float)index * 0.3f) + 1;

                    Vector3 Pos = this.transform.position + (this.transform.up * size) + (this.transform.forward * size);

                    cols = Physics.OverlapSphere(Pos, size);
                }
                break;
        }

        // 충돌 된 적이 없으면 리턴
        if (cols == null) return;

        for (int i = 0; i < cols.Length; ++i)
        {
            if (cols[i].gameObject.layer == (int)eEnemyLayer)
            {
                CommonFunction.CauseDamage(cols[i].gameObject, CharacterCtrl.GetAttackValue());
            }
        }
    }
}