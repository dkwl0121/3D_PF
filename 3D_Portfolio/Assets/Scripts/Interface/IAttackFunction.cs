using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAttackFunction : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    public AnimatorStateInfo animState;

    [HideInInspector] public bool isAttack = false;

    protected string[] arrAnimParam;
    
    protected void Awake()
    {
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
        anim.SetBool(arrAnimParam[index], true);
        isAttack = true;
    }

    protected void StartEffect()
    {
        // 이펙트 콜리더로 어텍할지 박스콜리더로 할지 
    }

    public void AttackColOn(int index)
    {
        //float size = ((float)index * 0.5f) + 1;

        //Vector3 Pos = this.transform.position + (this.transform.up * size) + (this.transform.forward * size);

        //List<GameObject> listTarget = new List<GameObject>();

        //Collider[] cols = Physics.OverlapSphere(Pos, size);
        //for (int i = 0; i < cols.Length; ++i)
        //{
        //    if (cols[i].gameObject.CompareTag(Util.Tag.ENEMY))
        //    {
        //        listTarget.Add(cols[i].gameObject);
        //    }
        //}
    }

    public void AutoAttack()
    {
        if (isAttack) return;

        StartAttack(Random.Range(0, arrAnimParam.Length));
    }
}
