using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAttackFunction : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    public AnimatorStateInfo animState;

    [HideInInspector] public IChracterControl CharacterCtrl;

    [HideInInspector] public bool isAttack = false;

    protected string[] arrAnimParam;

    public E_LAYER_TYPE eEnemyLayer;

    protected bool isAttDelay;

    protected float fDelayAtt;
    
    protected void Awake()
    {
        isAttDelay = false;
    }
    
    public void ResetAttack()
    {
        if (arrAnimParam == null) return;

        isAttack = false;
        
        for (int i = 0; i < arrAnimParam.Length; ++i)
        {
            anim.SetBool(arrAnimParam[i], false);
        }
    }
    
    protected void StartAttack(int index)
    {
        ResetAttack();
        isAttack = true;
        anim.SetBool(arrAnimParam[index], true);
        
        StartCoroutine(DelayAtt());
    }

    protected void StartEffect(int index)
    {
        
    }

    protected void StartDamage(int index)
    {

    }

    public void AutoAttack()
    {
        // 어텍 중이거나 어텍 딜레이 중이거나 죽었으면
        if (isAttack || isAttDelay || CharacterCtrl.Stat.isDead) return;

        StartAttack(Random.Range(0, arrAnimParam.Length));
    }

    private IEnumerator DelayAtt()
    {
        isAttDelay = true;

        yield return new WaitForSeconds(fDelayAtt);

        isAttDelay = false;
    }
}
