using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomControl : IChracter
{
    private float fFreeMoveDist;
    private Vector3 vFreePos = Vector3.zero;

    private new void Awake()
    {
        base.Awake();   // 부모의 Awake() 호출

        fFreeMoveDist = 4.0f;
        fSearchDist = 4.0f;
        fAttackDist = 1.0f;
        navMesh.stoppingDistance = fAttackDist;

        fMoveSpeed = 0.5f;
        navMesh.speed = fMoveSpeed;

        strEnemyName = Util.Tag.PLAYER;
    }

    private void OnEnable()
    {
        navMesh.ResetPath();
        attackFunc.ResetAttack();
        vDestPos = this.transform.position;
        vFreePos = vDestPos + new Vector3(Random.Range(-fFreeMoveDist, fFreeMoveDist), 0, Random.Range(-fFreeMoveDist, fFreeMoveDist));
    }
    
    private void Update()
    {
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
                navMesh.SetDestination(objTarget.transform.position);
        }

        else
        {
            // 자유영역을 넘어가면 원위치로 돌아가기
            if ((vDestPos - this.transform.position).magnitude > fFreeMoveDist)
            {
                navMesh.SetDestination(vDestPos);
            }
            // 자유영역이면
            else
            {
                // 자유영역 근처라면 다시 셋팅
                if ((vFreePos - this.transform.position).magnitude < 1.0f)
                {
                    vFreePos = vDestPos + new Vector3(Random.Range(-fFreeMoveDist, fFreeMoveDist), 0, Random.Range(-fFreeMoveDist, fFreeMoveDist));
                }
                navMesh.SetDestination(vFreePos);
            }

            // 이동 값이 있을 때 (오토 - 네브 메쉬)
            if (navMesh.path.corners.Length > 1)
            {
                Vector3 lookDir = navMesh.path.corners[1] - navMesh.path.corners[0];
                lookDir.y = 0.0f;
                this.transform.rotation = Quaternion.LookRotation(lookDir.normalized, Vector3.up);
            }
        }

        // 이동 값 설정
        fCurrMoveSpeed = navMesh.velocity.magnitude;
        anim.SetFloat(Util.AnimParam.MOVE_SPEED, fCurrMoveSpeed / fMoveSpeed);
    }
}
