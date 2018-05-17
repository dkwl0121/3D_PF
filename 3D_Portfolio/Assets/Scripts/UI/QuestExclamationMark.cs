using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestExclamationMark : MonoBehaviour
{
    private void Update()
    {
        // 모든 퀘스트가 완료 되었을 때
        if (PlayerManager.Instace.CurrQuestNo >= QuestDBManager.Instace.GetMaxQuestNo())
        {
            Destroy(this.gameObject);
        }
        else if (PlayerManager.Instace.ClearQuest)
        {
            for (int i = 0; i < this.transform.childCount; ++i)
            {
                this.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < this.transform.childCount; ++i)
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

    }
}
