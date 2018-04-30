using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;       // 네브메쉬를 사용하기 위한 네임스페이스 추가

public class IChracter : MonoBehaviour
{
    private E_CHARACTER_TYPE eCharType;
    private E_CHARACTER_STAT eCharStat;

    protected NavMeshAgent navMesh = null;
    
    protected Animator anim = null;                     // 애니메이터
    protected AnimatorStateInfo animState;              // 애니메이터 스텟

    protected IAttackFunction attackFunc = null;        // 공격 함수 클래스

    protected CapsuleCollider BodyCol = null;           // 몸 콜리더
    protected SphereCollider AttackCol = null;          // 공격 콜리더

    protected Vector3 vDestPos;
    protected GameObject objTarget = null;              // 타겟

    protected float fSearchDist = 0;
    protected float fAttackDist = 0;

    protected string strEnemyName = "";
    
    protected int nLevel = 1;

    protected float fMaxHP = 0;
    protected float fCurrHP = 0;
    protected float fMaxMP = 0;
    protected float fCurrMP = 0;
    protected float fMaxExp = 0;
    protected float fCurrExp = 0;
    protected float fAtt = 0;
    protected float fDef = 0;
    protected float fStr = 0;
    protected float fDex = 0;
    protected float fInt = 0;

    protected float fUpHp = 0;
    protected float fUpMp = 0;
    protected float fCoolTime = 0;

    protected float fCurrMoveSpeed = 0.0f;
    protected float fMoveSpeed = 0;

    protected void Awake()
    {
        navMesh = this.GetComponent<NavMeshAgent>();
        // 자동 회전을 하지 않도록 한다.
        navMesh.updateRotation = false;

        anim = this.GetComponentInChildren<Animator>();
        animState = anim.GetCurrentAnimatorStateInfo(0);
        attackFunc = this.GetComponentInChildren<IAttackFunction>();
        attackFunc.anim = anim;
        attackFunc.animState = animState;
        
        BodyCol = this.GetComponent<CapsuleCollider>();
        AttackCol = this.GetComponentInChildren<SphereCollider>();
    }
    
    protected void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<Collision>().ist

        //// 어텍 콜리더가 충돌난건지 체크 !!!!
        //if (AttackCol.enabled && AttackCol && other.CompareTag(strEnemyName))
        //{
        //    other.gameObject.GetComponent<IChracter>().GetDamage(1);

        //    //// 플레이어일 경우
        //    //if (other.gameObject.GetComponent<PlayerControl>() != null)
        //    //{

        //    //}
        //    //// 머쉬룸일 경우
        //    //else if (other.gameObject.GetComponent<MushroomControl>() != null)
        //    //{

        //    //}
        //}
    }

    protected float GetAttackValue()
    {
        return Util.NumValue.INVALID_NUMBER;
    }

    protected void GetDamage(float fDamage)
    {

    }

    protected void CheckEnemy()
    {
        Vector3 Pos = this.transform.position;
        objTarget = null;

        List<GameObject> listTarget = new List<GameObject>();

        Collider[] cols = Physics.OverlapSphere(Pos, fSearchDist);
        for (int i = 0; i < cols.Length; ++i)
        {
            if (cols[i].gameObject.CompareTag(strEnemyName))
            {
                listTarget.Add(cols[i].gameObject);
            }
        }

        if (listTarget.Count > 0)
        {
            float minDist = float.MaxValue;
            for (int i = 0; i < listTarget.Count; ++i)
            {
                float currDist = (Pos - listTarget[i].transform.position).magnitude;
                if (currDist < minDist)
                {
                    objTarget = listTarget[i];
                    minDist = currDist;
                }
            }
        }
    }
}