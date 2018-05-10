using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton
{
    public GameObject objRoot = null;
    public GameObject objText;
    public Image imgFront;
    public Text text;
    public float fMaxCount;
    public float fCurrCount;

    public AttackButton() { }
}

public class PlayerAttackFunction : IAttackFunction
{
    public GameObject objAttackBtn;

    private GameObject[] arrEffect;

    private AttackButton[] stAttackBtn;

    private bool isGizsmos = false;
    private E_PLAYER_ATTACK_NO currAttackNo = E_PLAYER_ATTACK_NO.INVALID;
    private Vector3 DamagePos;
    private float DamageRadius;
    private Vector3 DamageSize;
    private Matrix4x4 DamageMat;

    private new void Awake()
    {
        base.Awake();

        fDelayAtt = 0.0f;

        arrAnimParam = new string[(int)E_PLAYER_ATTACK_NO.MAX];
        arrAnimParam[(int)E_PLAYER_ATTACK_NO.DEFAULT] = Util.AnimParam.DEFAULT_ATTACK;
        arrAnimParam[(int)E_PLAYER_ATTACK_NO.SKILL_01] = Util.AnimParam.SKILL_01;
        arrAnimParam[(int)E_PLAYER_ATTACK_NO.SKILL_02] = Util.AnimParam.SKILL_02;
        arrAnimParam[(int)E_PLAYER_ATTACK_NO.SKILL_03] = Util.AnimParam.SKILL_03;

        stAttackBtn = new AttackButton[(int)E_PLAYER_ATTACK_NO.MAX];
        SetAttackBtn((int)E_PLAYER_ATTACK_NO.DEFAULT, "DefaultAttack");
        SetAttackBtn((int)E_PLAYER_ATTACK_NO.SKILL_01, "Skill_01");
        SetAttackBtn((int)E_PLAYER_ATTACK_NO.SKILL_02, "Skill_02");
        SetAttackBtn((int)E_PLAYER_ATTACK_NO.SKILL_03, "Skill_03");
        
        arrEffect = new GameObject[(int)E_PLAYER_ATTACK_NO.MAX];
        arrEffect[(int)E_PLAYER_ATTACK_NO.DEFAULT]
            = Instantiate(Resources.Load(Util.ResourcePath.PT_DEFAULT) as GameObject, this.transform);
        arrEffect[(int)E_PLAYER_ATTACK_NO.SKILL_01]
            = Instantiate(Resources.Load(Util.ResourcePath.PT_SKILL_01) as GameObject, this.transform);
        arrEffect[(int)E_PLAYER_ATTACK_NO.SKILL_02]
            = Instantiate(Resources.Load(Util.ResourcePath.PT_SKILL_02) as GameObject, this.transform);
        arrEffect[(int)E_PLAYER_ATTACK_NO.SKILL_03]
            = Instantiate(Resources.Load(Util.ResourcePath.PT_SKILL_03) as GameObject, this.transform);

        for (int i = 0; i < arrEffect.Length; ++i)
        {
            arrEffect[i].GetComponent<ParticleSystem>().Stop();
        }
    }

    private void OnEnable()
    {
        // 레벨에 따른 버튼 활성화 셋팅
        SetAttButtonEnable();
    }

    private void SetAttackBtn(int index, string btnName)
    {
        stAttackBtn[index] = new AttackButton();

        if (objAttackBtn.transform.Find(btnName) == null) return;

        stAttackBtn[index].objRoot = objAttackBtn.transform.Find(btnName).gameObject;
        stAttackBtn[index].objText = stAttackBtn[index].objRoot.transform.Find("Text").gameObject;
        stAttackBtn[index].imgFront = stAttackBtn[index].objRoot.transform.Find("Front").GetComponent<Image>();
        stAttackBtn[index].text = stAttackBtn[index].objText.GetComponent<Text>();
        stAttackBtn[index].fMaxCount = index * 5;
        stAttackBtn[index].fCurrCount = 0;
    }

    public void SetAttButtonEnable()
    {
        int level = PlayerManager.Instace.GetCurrLevel();

        for (int i = 1; i < stAttackBtn.Length; ++i)
        {
            stAttackBtn[i].objRoot.SetActive(false);
        }

        if (level >= 6)
        {
            for (int i = 1; i < (int)E_PLAYER_ATTACK_NO.SKILL_03 + 1; ++i)
            {
                stAttackBtn[i].objRoot.SetActive(true);
            }
        }
        else if (level >= 4)
        {
            for (int i = 1; i < (int)E_PLAYER_ATTACK_NO.SKILL_02 + 1; ++i)
            {
                stAttackBtn[i].objRoot.SetActive(true);
            }
        }
        else if (level >= 2)
        {
            for (int i = 1; i < (int)E_PLAYER_ATTACK_NO.SKILL_01 + 1; ++i)
            {
                stAttackBtn[i].objRoot.SetActive(true);
            }
        }
    }

    private void Update()
    {
        CoolTimeUpdate();

        // 죽었거나 퍼포먼스 중이면
        if (CharacterCtrl.Stat.isDead || GameManager.Instace.Performance)
        {
            for (int i = 0; i < stAttackBtn.Length; ++i)
            {
                if (stAttackBtn[i].objRoot == null
                    || stAttackBtn[i].objRoot.activeSelf == false)  // 버튼이 비활성화 되있으면
                    continue;
                
                stAttackBtn[i].imgFront.gameObject.SetActive(false);
            }

            return;
        }

        // 공격 여부에 따라 버튼(front) 활성화 설정
        for (int i = 0; i < stAttackBtn.Length; ++i)
        {
            if (stAttackBtn[i].objRoot == null
                || stAttackBtn[i].objRoot.activeSelf == false)  // 버튼이 비활성화 되있으면
                continue;

            if (isAttack && stAttackBtn[i].objText.activeSelf == false)
                stAttackBtn[i].imgFront.gameObject.SetActive(false);
            else
                stAttackBtn[i].imgFront.gameObject.SetActive(true);
        }
        
        KeyControl();
    }

    private void KeyControl()
    {
        // 키보드 공격
        if (Input.GetKeyDown(KeyCode.Alpha1))
            StartAttack((int)E_PLAYER_ATTACK_NO.DEFAULT);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            StartAttack((int)E_PLAYER_ATTACK_NO.SKILL_01);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            StartAttack((int)E_PLAYER_ATTACK_NO.SKILL_02);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            StartAttack((int)E_PLAYER_ATTACK_NO.SKILL_03);
    }

    private void CoolTimeUpdate()
    {
        for (int i = 0; i < stAttackBtn.Length; ++i)
        {
            if (stAttackBtn[i].objRoot == null
                || stAttackBtn[i].objRoot.activeSelf == false  // 버튼이 비활성화 되있거나
                || stAttackBtn[i].objText.activeSelf == false)  // 카운트가 비활성화 되있으면
                continue;

            stAttackBtn[i].fCurrCount -= Time.deltaTime;

            if (stAttackBtn[i].fCurrCount <= 0)
            {
                stAttackBtn[i].fCurrCount = 0;
                stAttackBtn[i].objText.SetActive(false);
            }

            else
            {
                stAttackBtn[i].text.text = ((int)stAttackBtn[i].fCurrCount + 1).ToString();
            }

            stAttackBtn[i].imgFront.fillAmount = 1.0f - (stAttackBtn[i].fCurrCount / stAttackBtn[i].fMaxCount);
        }
    }

    public new void AutoAttack()
    {
        if (isAttack) return;

        List<int> AutoList = new List<int>();

        for (int i = 0; i < stAttackBtn.Length; ++i)
        {
            if (stAttackBtn[i].objRoot != null
                || stAttackBtn[i].objRoot.activeSelf == true  // 버튼이 활성화 되있거나
                || stAttackBtn[i].objText.activeSelf == false)  // 카운트가 비활성화 되있으면
            {
                AutoList.Add(i);
            }
        }

        if (AutoList.Count > 0)
            StartAttack(Random.Range(0, AutoList.Count));
    }

    public new void StartAttack(int index)
    {
        if (isAttack || stAttackBtn[index].objRoot.activeSelf == false  // 버튼이 비활성화 되있거나
            || stAttackBtn[index].fCurrCount > 0)           // 카운트가 끝나지 않았으면
            return;

        float fMana = CharacterCtrl.GetManaValue() * index;

        if (CharacterCtrl.Stat.fCurrMP < fMana) return;

        CharacterCtrl.Stat.fCurrMP -= fMana;

        stAttackBtn[index].objText.SetActive(true);
        stAttackBtn[index].fCurrCount = stAttackBtn[index].fMaxCount;
        stAttackBtn[index].text.text = ((int)stAttackBtn[index].fCurrCount + 1).ToString();
        
        base.StartAttack(index);
    }

    public new void StartEffect(int index)
    {
        if (index < (int)E_PLAYER_ATTACK_NO.DEFAULT || index >= (int)E_PLAYER_ATTACK_NO.MAX) return;

        ControlCamera(index);
       
        arrEffect[index].GetComponent<ParticleSystem>().Stop();
        arrEffect[index].GetComponent<ParticleSystem>().Play();
    }

    private void ControlCamera(int index)
    {
        switch ((E_PLAYER_ATTACK_NO)index)
        {
            case E_PLAYER_ATTACK_NO.SKILL_01:
                {
                    Camera.main.GetComponent<CameraControl>().CameraCtrlStart(E_CAMERA_CTRL_TYPE.STRAIGHT, 1.0f, 2.0f);
                }
                break;
            case E_PLAYER_ATTACK_NO.SKILL_02:
                {
                    Camera.main.GetComponent<CameraControl>().CameraCtrlStart(E_CAMERA_CTRL_TYPE.SHAKE, 1.0f, 0.05f);
                }
                break;
            case E_PLAYER_ATTACK_NO.SKILL_03:
                {
                    Camera.main.GetComponent<CameraControl>().CameraCtrlStart(E_CAMERA_CTRL_TYPE.SHAKE, 1.5f, 0.1f);
                }
                break;
        }
    }

    public new void StartDamage(int index)
    {
        if (index < (int)E_PLAYER_ATTACK_NO.DEFAULT || index >= (int)E_PLAYER_ATTACK_NO.MAX) return;

        Collider[] cols = null;

        switch ((E_PLAYER_ATTACK_NO)index)
        {
            case E_PLAYER_ATTACK_NO.DEFAULT:
                {
                    DamageRadius = ((float)index * 0.5f) + 1;

                    DamagePos = this.transform.position + (this.transform.up * DamageRadius) + (this.transform.forward * DamageRadius);

                    cols = Physics.OverlapSphere(DamagePos, DamageRadius);
                }
                break;
            case E_PLAYER_ATTACK_NO.SKILL_01:
                {
                    float size = ((float)index * 0.5f) + 1;

                    DamagePos = (this.transform.up * size * 0.5f) + (this.transform.forward * size);
                    DamageSize = new Vector3(size, size, size * 2.0f);
                    DamageMat = transform.localToWorldMatrix;

                    // OverlapBox()에 사이즈는 0.5사이즈를 넣어야 생각하는 사이즈가 나옴.
                    cols = Physics.OverlapBox(this.transform.position + DamagePos, DamageSize * 0.5f, transform.rotation);
                }
                break;
            case E_PLAYER_ATTACK_NO.SKILL_02:
                {
                    float size = ((float)index * 0.5f) + 1;

                    DamagePos = (this.transform.up * size * 0.5f) + (this.transform.forward * size * 1.5f);
                    DamageSize = new Vector3(size * 1.5f, size, size * 3.0f);
                    DamageMat = transform.localToWorldMatrix;

                    cols = Physics.OverlapBox(this.transform.position + DamagePos, DamageSize * 0.5f, transform.rotation);
                }
                break;
            case E_PLAYER_ATTACK_NO.SKILL_03:
                {
                    DamageRadius = ((float)index);

                    DamagePos = this.transform.position;

                    cols = Physics.OverlapSphere(DamagePos, DamageRadius);
                }
                break;
        }

        // 충돌 된 적이 없으면 리턴
        if (cols == null) return;

        for (int i = 0; i < cols.Length; ++i)
        {
            if (cols[i].gameObject.layer == (int)eEnemyLayer)
            {
                CommonFunction.CauseDamage(cols[i].gameObject, CharacterCtrl.GetAttackValue() * (index + 1));
            }
        }

        currAttackNo = (E_PLAYER_ATTACK_NO)index;
        StartCoroutine(DelayDamageGizmos((index * 0.5f) + 1));
    }
    
    private IEnumerator DelayDamageGizmos(float seconds)
    {
        isGizsmos = true;

        yield return new WaitForSeconds(seconds);

        isGizsmos = false;
    }

    void OnDrawGizmos()
    {
        if (!isGizsmos) return;

        Gizmos.color = Color.yellow;

        switch (currAttackNo)
        {
            case E_PLAYER_ATTACK_NO.DEFAULT:
                {
                    Gizmos.DrawWireSphere(DamagePos, DamageRadius);
                }
                break;
            case E_PLAYER_ATTACK_NO.SKILL_01:
                {
                    Gizmos.matrix = DamageMat;
                    Gizmos.DrawWireCube(DamagePos, DamageSize);
                }
                break;
            case E_PLAYER_ATTACK_NO.SKILL_02:
                {
                    Gizmos.matrix = DamageMat;
                    Gizmos.DrawWireCube(DamagePos, DamageSize);
                }
                break;
            case E_PLAYER_ATTACK_NO.SKILL_03:
                {
                    Gizmos.DrawWireSphere(DamagePos, DamageRadius);
                }
                break;
        }
    }
}
