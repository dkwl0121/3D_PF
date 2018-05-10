using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomControl : EnemyControl
{
    private new void Awake()
    {
        base.Awake();   // 부모의 Awake() 호출

        HPbar.fMoveY = 2.0f;
        HPbar.fScale = 0.5f;

        fFreeMoveDist = 8.0f;
        fSearchDist = 6.0f;
        fAttackDist = 2.0f;
        navMesh.stoppingDistance = fAttackDist;

        fMoveSpeed = 1.5f;
        navMesh.speed = fMoveSpeed;
    }
}
