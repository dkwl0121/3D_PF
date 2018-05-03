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

    private AttackButton[] stAttackBtn;

    private new void Awake()
    {
        base.Awake();

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
    
    private void Update()
    {
        CoolTimeUpdate();

        // 죽었으면
        if (CharacterCtrl.Stat.isDead)
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

        if (CharacterCtrl.Stat.fCurrMP >= fMana)
        {
            CharacterCtrl.Stat.fCurrMP -= fMana;
        }

        stAttackBtn[index].objText.SetActive(true);
        stAttackBtn[index].fCurrCount = stAttackBtn[index].fMaxCount;
        stAttackBtn[index].text.text = ((int)stAttackBtn[index].fCurrCount + 1).ToString();
        
        base.StartAttack(index);
    }

    public new void StartEffect(int index)
    {
        if (index < (int)E_PLAYER_ATTACK_NO.DEFAULT || index >= (int)E_PLAYER_ATTACK_NO.MAX) return;

        ControlCamera(index);
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
                    float size = ((float)index * 0.5f) + 1;

                    Vector3 Pos = this.transform.position + (this.transform.up * size) + (this.transform.forward * size);

                    cols = Physics.OverlapSphere(Pos, size);
                }
                break;
            case E_PLAYER_ATTACK_NO.SKILL_01:
                {
                    float size = ((float)index * 0.5f) + 1;

                    Vector3 Pos = this.transform.position + (this.transform.up * size) + (this.transform.forward * size);

                    cols = Physics.OverlapSphere(Pos, size);
                }
                break;
            case E_PLAYER_ATTACK_NO.SKILL_02:
                {
                    float size = ((float)index * 0.5f) + 1;

                    Vector3 Pos = this.transform.position + (this.transform.up * size) + (this.transform.forward * size);

                    cols = Physics.OverlapSphere(Pos, size);
                }
                break;
            case E_PLAYER_ATTACK_NO.SKILL_03:
                {
                    float size = ((float)index * 0.5f) + 1;

                    Vector3 Pos = this.transform.position + (this.transform.up * size) + (this.transform.forward * size);

                    cols = Physics.OverlapSphere(Pos, size);
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
    }
}
