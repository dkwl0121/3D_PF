using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;       // 네브메쉬를 사용하기 위한 네임스페이스 추가

public class CharacterStat
{
    public int nLevel = 1;
    public float fMaxHP = 0;
    public float fCurrHP = 0;
    public float fMaxMP = 0;
    public float fCurrMP = 0;
    public float fMaxExp = 0;
    public float fCurrExp = 0;
    public float fAtt = 0;
    public float fDef = 0;
    public float fStr = 0;
    public float fDex = 0;
    public float fInt = 0;
    public float fUpHp = 0;
    public float fUpMp = 0;
    public float fCoolTime = 0;
    public bool isDead = false;
    public int nMoney = 0;
}

[RequireComponent(typeof(NavMeshAgent))]
public class IChracterControl : MonoBehaviour
{
    public E_CHARACTER_TYPE eCharType;
    //private E_CHARACTER_STAT eCharStat;

    public CharacterStat Stat;

    protected NavMeshAgent navMesh = null;
    
    protected Animator anim = null;                     // 애니메이터
    protected AnimatorStateInfo animState;              // 애니메이터 스텟

    protected IAttackFunction attackFunc = null;        // 공격 함수 클래스
    
    protected Vector3 vDestPos;
    protected Quaternion qDestRot;
    protected GameObject objTarget = null;              // 타겟

    protected float fSearchDist = 0;
    protected float fAttackDist = 0;

    protected E_LAYER_TYPE eEnemyLayer;
    
    protected float fCurrMoveSpeed = 0.0f;
    protected float fMoveSpeed = 0;

    protected void Awake()
    {
        Stat = new CharacterStat();

        navMesh = this.GetComponent<NavMeshAgent>();
        // 자동 회전을 하지 않도록 한다.
        navMesh.updateRotation = false;

        anim = this.GetComponentInChildren<Animator>();
        animState = anim.GetCurrentAnimatorStateInfo(0);
        attackFunc = this.GetComponentInChildren<IAttackFunction>();
        attackFunc.anim = anim;
        attackFunc.animState = animState;
        attackFunc.CharacterCtrl = this;
    }

    protected void OnEnable()
    {
        Stat.isDead = false;
        anim.enabled = true;
        if (eCharType != E_CHARACTER_TYPE.PLAYER)
        {
            Stat.fCurrHP = Stat.fMaxHP;
            Stat.fCurrMP = Stat.fMaxMP;
        }
    }

    protected void CheckEnemy()
    {
        Vector3 Pos = this.transform.position;
        objTarget = null;

        List<GameObject> listTarget = new List<GameObject>();

        Collider[] cols = Physics.OverlapSphere(Pos, fSearchDist);
        for (int i = 0; i < cols.Length; ++i)
        {
            if (cols[i].gameObject.layer == (int)eEnemyLayer
                && cols[i].gameObject.activeSelf
                && !cols[i].gameObject.GetComponent<IChracterControl>().Stat.isDead)
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

    public float GetAttackValue()
    {
        float fAttValue = Stat.fAtt + (Stat.fInt * 0.5f);

        return fAttValue;
    }

    public float GetManaValue()
    {
        float fManaValue = 3 * Stat.nLevel;

        return fManaValue;
    }
    
    public void GetDamage(float fDamge)
    {
        if (Stat.isDead) return;

        // 민첩에 의해 회피율이 올라감.
        if (Random.Range(0, 100) == Stat.fDex - 1) return;

        fDamge -= Stat.fDef;

        if (fDamge <= 0)
            fDamge = 0;
        else
        {
            if (fDamge > Stat.fCurrHP)
                fDamge = Stat.fCurrHP;

            Stat.fCurrHP -= fDamge;

            if (!attackFunc.isAttack)
                anim.SetTrigger(Util.AnimParam.DAMAGE);

            if (Stat.fCurrHP <= 0)
            {  
                StartDie();
            }
        }

        // 깍이는 데미지 값 보여주기
        if (eCharType == E_CHARACTER_TYPE.PLAYER)
            DamageValuePool.Instace.ShowDamage(this.transform.position, -fDamge, true);
        else
            DamageValuePool.Instace.ShowDamage(this.transform.position, fDamge, false);
    }

    private void StartDie()
    {
        if (eCharType != E_CHARACTER_TYPE.PLAYER)
        {
            PlayerManager.Instace.AddExp(Stat.fMaxExp);
            PlayerManager.Instace.AddMoney(Stat.nMoney);
        }
        Stat.isDead = true;
        attackFunc.ResetAttack();
        anim.SetTrigger(Util.AnimParam.DEATH);
    }

    public void FnishDie()
    {
        anim.enabled = false;

        StartCoroutine(DelayDie());
    }

    private IEnumerator DelayDie()
    {
        yield return new WaitForSeconds(2.0f);

        this.gameObject.SetActive(false);
    }
}