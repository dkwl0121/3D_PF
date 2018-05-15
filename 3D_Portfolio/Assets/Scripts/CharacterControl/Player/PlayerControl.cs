using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : IChracterControl
{
    public GameObject objWeaponParent = null;
    public JoyStickControl joystick;
    public GameObject AutoButton;

    private PlayerProgressbar StatBar;
    
    private bool isAuto = false;
    private bool isCoolTime = false;
    
    private new void Awake()
    {
        base.Awake();   // 부모의 Start() 호출

        StatBar = GetComponent<PlayerProgressbar>();
        
        fSearchDist = 4.0f;
        fAttackDist = 2.0f;
        navMesh.stoppingDistance = fAttackDist;

        fMoveSpeed = 4.0f;
        navMesh.speed = fMoveSpeed;

        eCharType = E_CHARACTER_TYPE.PLAYER;
        eEnemyLayer = E_LAYER_TYPE.ENEMY;
        attackFunc.eEnemyLayer = eEnemyLayer;

        Stat = PlayerManager.Instace.LoadData();
    }

    private new void OnEnable()
    {
        base.OnEnable();

        AllStop();
        AutoButton.SetActive(true);
    }

    private void AllStop()
    {
        joystick.OnPointerUp(null);
        fCurrMoveSpeed = 0.0f;
        navMesh.ResetPath();
        attackFunc.ResetAttack();
    }

    private void OnDestroy()
    {
        PlayerManager.Instace.SaveData();
    }

    private void Update()
    {
        if (Stat.isDead)
        {
            AutoButton.SetActive(false);

            return;
        }

        // 무기 체크!!
        CheckWeapon();

        // 레벨 업 체크!!
        CheckLevelUp();

        // 움직이면 안 될 때
        if (GameManager.Instace.NoMove)
        {
            AllStop();
            return;
        }

        // 씬에 따라 오토 버튼 활성화 설정
        if (SceneCtrlManager.Instace.CurrSceneNo == E_SCENE_NO.DUNGEON)
        {
            AutoButton.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            AutoOff();
            AutoButton.transform.parent.gameObject.SetActive(false);
        }

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

        // 체력 채우기
        if (!isCoolTime)
            StartCoroutine(UpStat());
    }
    
    private IEnumerator UpStat()
    {
        isCoolTime = true;

        yield return new WaitForSeconds(Stat.fCoolTime);

        isCoolTime = false;

        if (!Stat.isDead && this.gameObject)
        {
            if (Stat.fCurrHP < Stat.fMaxHP)
            {
                Stat.fCurrHP += Stat.fUpHp;
                Stat.fCurrHP = Mathf.Clamp(Stat.fCurrHP, 0, Stat.fMaxHP);
            }
            if (Stat.fCurrMP < Stat.fMaxMP)
            {
                Stat.fCurrMP += Stat.fUpMp;
                Stat.fCurrMP = Mathf.Clamp(Stat.fCurrMP, 0, Stat.fMaxMP);
            }
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
                fCurrMoveSpeed -= fMoveSpeed * 2.0f * Time.deltaTime;
                fCurrMoveSpeed = Mathf.Clamp(fCurrMoveSpeed, 0.0f, fMoveSpeed);
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

        fCurrMoveSpeed = fDistance * fMoveSpeed;
        
        transform.Translate(Vector3.forward * fCurrMoveSpeed * Time.deltaTime);
    }

    private void CheckWeapon()
    {
        // 무기를 바꿔야 한다면
        if (PlayerManager.Instace.ChangeWeapon)
        {
            // 자식이 있다면 지운다
            if (objWeaponParent.transform.childCount > 0)
                Destroy(objWeaponParent.transform.GetChild(0).gameObject);

            GameObject weapon = Resources.Load(
                WeaponDBManager.Instace.GetArrWeaponInfo()[PlayerManager.Instace.CurrWeapon].Weaponloadpath) as GameObject;
            Instantiate(weapon, objWeaponParent.transform);
        }
    }

    private void CheckLevelUp()
    {
        if (!PlayerManager.Instace.Levelup) return;

        PlayerManager.Instace.Levelup = false;

        ((PlayerAttackFunction)attackFunc).SetAttButtonEnable();
        GameObject objMessage = Instantiate(Resources.Load(Util.ResourcePath.UI_MESSAGE)) as GameObject;
        objMessage.GetComponent<Message>().SetText(Color.green, LevelDBManager.Instace.GetLevelUpTip(Stat.nLevel));
    }
    
    private void AutoOn()
    {
        isAuto = true;
        AutoButton.SetActive(false);
        navMesh.ResetPath();
    }

    public void AutoOff()
    {
        isAuto = false;
        AutoButton.SetActive(true);
        navMesh.velocity = Vector3.zero;
        navMesh.ResetPath();
    }

    public void TouchAutoButton()
    {
        if (isAuto)
            AutoOff();
        else
            AutoOn();
    }

    public void SetDestPos(Vector3 Pos)
    {
        vDestPos = Pos;
    }
}
