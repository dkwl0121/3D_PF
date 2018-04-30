using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : IChracter
{
    public GameObject objWeaponParent;
    public JoyStickControl joystick;
    public GameObject AutoButton;
    
    private bool isAuto = false;
    
    private new void Awake()
    {
        base.Awake();   // 부모의 Awake() 호출
        
        GameObject weapon = Resources.Load("Weapon/Staff_01") as GameObject;
        Instantiate(weapon, objWeaponParent.transform);

        fSearchDist = 4.0f;
        fAttackDist = 2.0f;
        navMesh.stoppingDistance = fAttackDist;

        fMoveSpeed = 4.0f;
        navMesh.speed = fMoveSpeed;

        strEnemyName = Util.Tag.ENEMY;

        LoadData();
        
        //====================================================================================
        vDestPos = GameObject.Find("Dest").transform.position;
    }

    private void OnDestroy()
    {
        SaveData();
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
            MoveControl();

            if (isAuto)
                AutoControl();

            // 이동 값 설정
            anim.SetFloat(Util.AnimParam.MOVE_SPEED, fCurrMoveSpeed / fMoveSpeed);
        }
    }
    
    private void MoveControl()
    {
        // 이동 값이 있을 때 (조이스틱)
        if ((joystick.Horizontal != 0.0f || joystick.Vertical != 0.0f))
        {
            AutoOff();

            Move(new Vector3(joystick.Horizontal, 0, joystick.Vertical), joystick.Distance);
        }
        // 이동 값이 있을 때 (키보드)
        else if ((Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f))
        {
            AutoOff();
            
            Vector3 vInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            Move(vInput, vInput.magnitude);
        }
        // 이동 값이 없을 때
        else if (!isAuto)
        {
            if (fCurrMoveSpeed > 0.0f)
            {
                fCurrMoveSpeed -= Time.deltaTime * fMoveSpeed;
                Mathf.Clamp(fCurrMoveSpeed, 0.0f, fMoveSpeed);
                transform.Translate(Vector3.forward * fCurrMoveSpeed * Time.deltaTime);
            }
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
                ((PlayerAttackFunction)attackFunc).AutoAttack();
            else
                navMesh.SetDestination(objTarget.transform.position);
        }

        else
        {
            // 이동 값이 있을 때 (오토 - 네브 메쉬)
            if (navMesh.path.corners.Length > 1)
            {
                // 이동 값 재설정 (타겟을 따라가다 타겟이 죽었을 경우)
                if (navMesh.destination != vDestPos)
                    navMesh.SetDestination(vDestPos);

                Vector3 lookDir = navMesh.path.corners[1] - navMesh.path.corners[0];
                lookDir.y = 0.0f;
                this.transform.rotation = Quaternion.LookRotation(lookDir.normalized, Vector3.up);
            }
            // 이동 값이 없을 때
            else
                navMesh.SetDestination(vDestPos);
        }

        fCurrMoveSpeed = navMesh.velocity.magnitude;
    }

    private void Move(Vector3 vLookRot, float fDistance)
    {
        // 이동 방향으로 회전
        transform.rotation = Quaternion.LookRotation(vLookRot, Vector3.up);

        // 이동
        if (fCurrMoveSpeed < fMoveSpeed)
        {
            fCurrMoveSpeed += Time.deltaTime * 2.0f;
            Mathf.Clamp(fCurrMoveSpeed, 0.0f, fMoveSpeed);
        }

        transform.Translate(Vector3.forward * fDistance * fCurrMoveSpeed * Time.deltaTime);
    }

    private void AutoOn()
    {
        isAuto = true;
        AutoButton.SetActive(false);
        navMesh.ResetPath();
    }

    private void AutoOff()
    {
        isAuto = false;
        AutoButton.SetActive(true);
        navMesh.ResetPath();
    }

    public void TouchAutoButton()
    {
        if (isAuto)
            AutoOff();
        else
            AutoOn();
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey("nLevel"))
        {
            nLevel = PlayerPrefs.GetInt("nLevel");
            fCurrHP = PlayerPrefs.GetInt("fCurrHP");
            fCurrMP = PlayerPrefs.GetInt("fCurrMP");
            fCurrExp = PlayerPrefs.GetInt("fCurrExp");
        }
        else
        {
            nLevel = 1;
            fCurrHP = fMaxHP;
            fCurrMP = fMaxMP;
        }

        // 레벨에 따른 스탯 설정
        fMaxHP = LevelManager.Instace.GetHpByLevel(nLevel);
        fMaxMP = LevelManager.Instace.GetMpByLevel(nLevel);
        fMaxExp = LevelManager.Instace.GetExpByLevel(nLevel);
        fAtt = LevelManager.Instace.GetAttByLevel(nLevel);
        fDef = LevelManager.Instace.GetDefByLevel(nLevel);
        fStr = LevelManager.Instace.GetStrByLevel(nLevel);
        fDex = LevelManager.Instace.GetDexByLevel(nLevel);
        fInt = LevelManager.Instace.GetIntByLevel(nLevel);
        fUpHp = LevelManager.Instace.GetUpHpByLevel(nLevel);
        fUpMp = LevelManager.Instace.GetUpMpByLevel(nLevel);
        fCoolTime = LevelManager.Instace.GetCoolTimeByLevel(nLevel);
    }

    private void SaveData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("nLevel", nLevel);
        PlayerPrefs.SetInt("fCurrHP", (int)fCurrHP);
        PlayerPrefs.SetInt("fCurrMP", (int)fCurrMP);
        PlayerPrefs.SetInt("fCurrExp", (int)fCurrExp);
        
        PlayerPrefs.Save();
    }
}
