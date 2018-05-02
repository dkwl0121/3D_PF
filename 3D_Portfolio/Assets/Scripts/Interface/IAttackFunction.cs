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
        isAttDelay = true;

        isAttack = true;
        ResetAttack();
        anim.SetBool(arrAnimParam[index], true);
        
        StartCoroutine(DelayAtt());
    }

    protected void StartEffect(int index)
    {
        
    }

    protected void StartDamage(int index)
    {

    }

    //public void AttackColOn(int index)
    //{
    //    //float size = ((float)index * 0.5f) + 1;

    //    //Vector3 Pos = this.transform.position + (this.transform.up * size) + (this.transform.forward * size);

    //    //List<GameObject> listTarget = new List<GameObject>();

    //    //Collider[] cols = Physics.OverlapSphere(Pos, size);
    //    //for (int i = 0; i < cols.Length; ++i)
    //    //{
    //    //    if (cols[i].gameObject.CompareTag(Util.Tag.ENEMY))
    //    //    {
    //    //        listTarget.Add(cols[i].gameObject);
    //    //    }
    //    //}
    //}

    public void AutoAttack()
    {
        // 어텍 중이거나 어텍 딜레이 중이면
        if (isAttack || isAttDelay) return;

        if (!CharacterCtrl.isDead)
            StartAttack(Random.Range(0, arrAnimParam.Length));
    }

    private IEnumerator DelayAtt()
    {
        yield return new WaitForSeconds(3.0f);

        isAttDelay = false;
    }
}
