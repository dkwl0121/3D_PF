using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgressbar : MonoBehaviour
{
    public Text txtLevel;
    public Image imgHpMainGauge;
    public Image imgHPBackGauge;
    public Text txtHpValue;
    public Image imgMpGauge;
    public Image imgExpGauge;

    private void Awake()
    {
        // 만약에 HP가 적은 상태일 때 깍이는 모습으로 연출 될 수 있기 때문에
        imgHPBackGauge.fillAmount = 0.0f;
    }

    public void Update()
    {
        CharacterStat stat = PlayerManager.Instace.Stat;

        txtLevel.text = stat.nLevel.ToString();
        imgHpMainGauge.fillAmount = stat.fCurrHP / stat.fMaxHP;
        txtHpValue.text = stat.fCurrHP.ToString() + " / " + stat.fMaxHP.ToString();
        imgMpGauge.fillAmount = stat.fCurrMP / stat.fMaxMP;
        imgExpGauge.fillAmount = stat.fCurrExp / stat.fMaxExp;

        // 백그라운드 HP 게이지가 메인 HP 게이지보다 크면 천천히 줄어들게!
        if (imgHPBackGauge.fillAmount > imgHpMainGauge.fillAmount)
        {
            imgHPBackGauge.fillAmount -= Time.deltaTime * 0.2f;

            imgHPBackGauge.fillAmount = imgHPBackGauge.fillAmount < imgHpMainGauge.fillAmount ? imgHpMainGauge.fillAmount : imgHPBackGauge.fillAmount;
        }
        else
        {
            imgHPBackGauge.fillAmount = imgHpMainGauge.fillAmount;
        }
    }
}
