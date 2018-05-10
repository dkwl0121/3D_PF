using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMonsterControl : EnemyControl
{
    private new void Awake()
    {
        base.Awake();   // 부모의 Awake() 호출
        
        HPbar.fMoveY = 3.0f;
        HPbar.fScale = 0.6f;

        fFreeMoveDist = 8.0f;
        fSearchDist = 6.0f;
        fAttackDist = 2.0f;
        navMesh.stoppingDistance = fAttackDist;

        fMoveSpeed = 2.0f;
        navMesh.speed = fMoveSpeed;
    }
}
