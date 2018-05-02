using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomControl : IChracterControl
{
    private float fFreeMoveDist;
    private Vector3 vFreePos = Vector3.zero;

    private bool isStartCort;

    private new void Awake()
    {
        base.Awake();   // 부모의 Awake() 호출

        fFreeMoveDist = 4.0f;
        fSearchDist = 4.0f;
        fAttackDist = 1.0f;
        navMesh.stoppingDistance = fAttackDist;

        fMoveSpeed = 0.5f;
        navMesh.speed = fMoveSpeed;
        
        eEnemyLayer = E_LAYER_TYPE.PLAYER;
        attackFunc.eEnemyLayer = eEnemyLayer;

        isStartCort = false;

        SetStat();
    }

    private new void OnEnable()
    {
        base.OnEnable();

        if (navMesh)
            navMesh.ResetPath();
        if (attackFunc)
            attackFunc.ResetAttack();
        vDestPos = this.transform.position;
        vFreePos = vDestPos + new Vector3(Random.Range(-fFreeMoveDist, fFreeMoveDist), 0, Random.Range(-fFreeMoveDist, fFreeMoveDist));
    }
    
    private void Update()
    {
        if (isDead) return;

        if (attackFunc.isAttack)
        {
            fCurrMoveSpeed = 0.0f;
            navMesh.ResetPath();
        }
        else
        {
            AutoControl();
        }
    }

    private void SetStat()
    {
        EnemyDBManager.Instace.SetStat(Stat, eCharType);
    }

    private void AutoControl()
    {
        // 주변 에너미 체크
        CheckEnemy();

        // 에너미가 주변에 있으면 쫓아감
        if (objTarget != null && objTarget.activeSelf)
        {
            // 에너미 방향으로 회전
            Vector3 Dir = objTarget.transform.position - this.transform.position;
            Vector3 normalDir = Dir.normalized;
            normalDir.y = 0.0f;
            transform.rotation = Quaternion.LookRotation(Dir);

            // 공격범위에 있으면 공격
            if (Dir.magnitude < fAttackDist)
            {
                attackFunc.AutoAttack();
            }
            else
            {
                navMesh.SetDestination(objTarget.transform.position);
            }
        }

        else
        {
            // 자유영역을 넘어가면 원위치로 돌아가기
            if ((vDestPos - this.transform.position).magnitude > fFreeMoveDist)
            {
                navMesh.SetDestination(vDestPos);
            }
            // 자유영역이고, 코루틴이 시작된게 아니라면
            else if (!isStartCort)
            {
                navMesh.SetDestination(vFreePos);
            }

            // 이동 값이 있을 때 (오토 - 네브 메쉬) // 도착지점에 도달하지 않았을 때
            if (navMesh.path.corners.Length > 1 
                && (navMesh.destination - this.transform.position).magnitude > navMesh.stoppingDistance)
            {
                Vector3 lookDir = navMesh.path.corners[1] - navMesh.path.corners[0];
                lookDir.y = 0.0f;
                this.transform.rotation = Quaternion.LookRotation(lookDir.normalized, Vector3.up);

                // 장애물로 인해 앞으로 가지 못하는 상황이면
                if (navMesh.velocity.magnitude <= 0.0f)
                {
                    SetFreePos();
                }
            }

            // 이동 값이 없고, 코루틴이 시작된게 아니라면
            else if (!isStartCort)
            {
                navMesh.ResetPath();
                StartCoroutine(DelayFreePos());
            }
        }
        
        // 이동 값 설정
        fCurrMoveSpeed = navMesh.velocity.magnitude;
        anim.SetFloat(Util.AnimParam.MOVE_SPEED, fCurrMoveSpeed / fMoveSpeed);
    }

    private IEnumerator DelayFreePos()
    {
        isStartCort = true;

        yield return new WaitForSeconds(3.0f);

        isStartCort = false;
        if (!attackFunc.isAttack && !isDead
            && (vDestPos - this.transform.position).magnitude <= fFreeMoveDist) // 자유영역 안에 있다면
            SetFreePos();
    }

    private void SetFreePos()
    {
        float fDist = fFreeMoveDist * 0.5f;
        vFreePos = vDestPos + new Vector3(Random.Range(-fDist, fDist), 0, Random.Range(-fDist, fDist));
        navMesh.SetDestination(vFreePos);
    }
}
