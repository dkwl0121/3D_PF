
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinAttackFunction : IAttackFunction
{
    private GameObject[] arrEffect;

    private new void Awake()
    {
        base.Awake();

        fDelayAtt = 3.0f;

        arrAnimParam = new string[(int)E_PLAYER_ATTACK_NO.SKILL_01 + 1];
        arrAnimParam[(int)E_PLAYER_ATTACK_NO.DEFAULT] = Util.AnimParam.DEFAULT_ATTACK;
        arrAnimParam[(int)E_PLAYER_ATTACK_NO.SKILL_01] = Util.AnimParam.SKILL_01;

        arrEffect = new GameObject[(int)E_PLAYER_ATTACK_NO.SKILL_01 + 1];
        arrEffect[(int)E_PLAYER_ATTACK_NO.DEFAULT]
            = Instantiate(Resources.Load(Util.ResourcePath.PT_PUMPKIN_ATTACK) as GameObject, this.transform);
        arrEffect[(int)E_PLAYER_ATTACK_NO.SKILL_01]
            = Instantiate(Resources.Load(Util.ResourcePath.PT_PUMPKIN_SKILL_01) as GameObject, this.transform);

        for (int i = 0; i < arrEffect.Length; ++i)
        {
            arrEffect[i].GetComponent<ParticleSystem>().Stop();
        }
    }

    public new void AutoAttack()
    {
        // 어텍 중이거나 어텍 딜레이 중이거나 죽었으면
        if (isAttack || isAttDelay || CharacterCtrl.Stat.isDead) return;

        StartAttack(Random.Range(0, arrAnimParam.Length));  // 펌킨의 스타트 어택을 실행!!
    }

    public new void StartAttack(int index)
    {
        float fMana = CharacterCtrl.GetManaValue() * index;

        if (CharacterCtrl.Stat.fCurrMP < fMana) return;

        CharacterCtrl.Stat.fCurrMP -= fMana;

        base.StartAttack(index);
    }

    public new void StartEffect(int index)
    {
        if (index < (int)E_PLAYER_ATTACK_NO.DEFAULT || index >= (int)E_PLAYER_ATTACK_NO.MAX) return;
        
        arrEffect[index].GetComponent<ParticleSystem>().Stop();
        arrEffect[index].GetComponent<ParticleSystem>().Play();
    }

    public new void StartDamage(int index)
    {
        if (index < (int)E_PLAYER_ATTACK_NO.DEFAULT || index >= (int)E_PLAYER_ATTACK_NO.MAX) return;

        Collider[] cols = null;
        switch ((E_PLAYER_ATTACK_NO)index)
        {
            case E_PLAYER_ATTACK_NO.DEFAULT:
                {
                    float size = 2;

                    Vector3 Pos = this.transform.position + (this.transform.up * size) + (this.transform.forward * size);

                    cols = Physics.OverlapSphere(Pos, size);
                }
                break;
            case E_PLAYER_ATTACK_NO.SKILL_01:
                {
                    float size = 3;

                    Vector3 Pos = this.transform.position;

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
                CommonFunction.CauseDamage(cols[i].gameObject, CharacterCtrl.GetAttackValue() * (index + 1));
            }
        }
    }
}