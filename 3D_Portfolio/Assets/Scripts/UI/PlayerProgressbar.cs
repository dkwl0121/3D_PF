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
    
    public void SetStatUI(CharacterStat stat)
    {
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
