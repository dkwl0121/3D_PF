using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public Text txtContent;

    private bool isStart;

    private void Awake()
    {
        isStart = false;
        // 모든 자식 비활성화
        for (int i = 0; i < this.transform.childCount; ++i)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // 퀘스트가 생겼다면
        if (!isStart && PlayerManager.Instace.CurrQuestNo >= 0)
        {
            isStart = true;
            // 모든 자식 활성화
            for (int i = 0; i < this.transform.childCount; ++i)
            {
                this.transform.GetChild(i).gameObject.SetActive(true);
            }

            GameManager.Instace.QuestChange = true;
        }

        // 모든 퀘스트가 완료 되었을 때
        if (PlayerManager.Instace.CurrQuestNo >= QuestDBManager.Instace.GetMaxQuestNo())
        {
            Destroy(this.gameObject);
        }

        // 퀘스트를 셋팅 해야 할 때
        else if (isStart && GameManager.Instace.QuestChange)
        {
            SetQuest(PlayerManager.Instace.ClearQuest, QuestDBManager.Instace.GetQuestContent(PlayerManager.Instace.CurrQuestNo));
            
            GameManager.Instace.QuestChange = false;
        }

    }

    private void SetQuest(bool isClear, string str)
    {
        txtContent.text = "Q : " + str;
        if (isClear)
            txtContent.color = Color.yellow;
        else
            txtContent.color = Color.white;
    }
}
