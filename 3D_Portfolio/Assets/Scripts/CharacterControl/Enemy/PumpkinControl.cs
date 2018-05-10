using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinControl : EnemyControl
{
    private new void Awake()
    {
        base.Awake();   // 부모의 Awake() 호출
        
        HPbar.fMoveY = 3.5f;
        HPbar.fScale = 1.0f;

        fFreeMoveDist = 0.0f;
        fSearchDist = 8.0f;
        fAttackDist = 3.0f;
        navMesh.stoppingDistance = fAttackDist;

        fMoveSpeed = 3.0f;
        navMesh.speed = fMoveSpeed;
    }
}
