using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupStory : MonoBehaviour
{
    public GameObject objNpcTab;
    public GameObject objPlayerTab;
    public Text txtContent;

    private int nCurrStoryNo;           // 현재 스토리 진행 넘버
    private int nCurrStoryIndex;        // 현재 팝업에 보여지고 있는 스토리의 배열 인덱스

    private storyData CurrStoryData;    // 현재 팝업에 보여지고 있는 스토리 데이터

    private bool isClearQuest = false;
    private bool isFinishQuest = false;

    public void Awake()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.POPUP);

        GameManager.Instace.NoMove = true;

        objNpcTab.SetActive(false);
        objPlayerTab.SetActive(false);

        nCurrStoryNo = PlayerManager.Instace.CurrStoryNo;
        
        // 스토리 진행이 다 끝났다면
        if (nCurrStoryNo >= StoryDBManager.Instace.GetMaxStoryNo())
        {
            isFinishQuest = true;
            objNpcTab.SetActive(true);
            objPlayerTab.SetActive(false);
            txtContent.text = "Summer Land를 구해주셔서 항상 감사하게 생각하고 있답니다!!";

            return;
        }

        nCurrStoryIndex = StoryDBManager.Instace.GetStartIndex(nCurrStoryNo);

        CurrStoryData = StoryDBManager.Instace.GetStoryData(nCurrStoryIndex);

        // 현재 스토리를 진행하기 위한 퀘스트를 클리어 했다면
        if (PlayerManager.Instace.CurrQuestNo == CurrStoryData.Questno && PlayerManager.Instace.ClearQuest)
        {
            // 완료 퀘스트 보상 지급
            if (PlayerManager.Instace.CurrQuestNo >= 0)
            {
                PlayerManager.Instace.AddMoney(QuestDBManager.Instace.GetCoin(PlayerManager.Instace.CurrQuestNo));
            }
            isClearQuest = true;
            ShowNextStory();
        }
        else
        {
            isClearQuest = false;
            objNpcTab.SetActive(true);
            objPlayerTab.SetActive(false);
            txtContent.text = "현재 퀘스트를 완료하세요.";
        }
    }

    public void OnDestroy()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.POPUP);

        GameManager.Instace.NoMove = false;
    }

    public void Update()
    {
        // 스크린을 클릭하거나, 스크린 터치를 시작 했다면
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            // 퀘스트 클리어 상태이고, 스토리진행이 끝나지 않았을 때
            if (isClearQuest && !isFinishQuest)
            {
                // 스토리가 끝났다면
                if (CurrStoryData.Finish)
                {
                    GameManager.Instace.QuestChange = true;

                    // 스토리에 따른 퀘스트로 새로 설정.
                    PlayerManager.Instace.CurrQuestNo = CurrStoryData.Questno;
                    PlayerManager.Instace.ClearQuest = false;
                    // 스토리 넘버 증가.
                    ++PlayerManager.Instace.CurrStoryNo;
                    // 모든 스토리가 끝난 경우라면(퀘스트도 끝난 상태)
                    if (PlayerManager.Instace.CurrStoryNo >= StoryDBManager.Instace.GetMaxStoryNo())
                    {
                        GameObject.Find("TownScene").GetComponent<TownScene>().SetSummerMap();
                    }

                    FinishStory();
                }
                else
                {
                    ShowNextStory();
                }
            }
            
            // 퀘스트 클리어 상태가 아니거나, 스토리 진행이 끝났을 때
            else
            {
                FinishStory();
            }
        }
    }
    
    private void ShowNextStory()
    {
        CurrStoryData = StoryDBManager.Instace.GetStoryData(nCurrStoryIndex);

        // 플레이어 대화라면
        if (CurrStoryData.Isplayer)
        {
            objNpcTab.SetActive(false);
            objPlayerTab.SetActive(true);
        }
        // NPC 대화라면
        else
        {
            objNpcTab.SetActive(true);
            objPlayerTab.SetActive(false);
        }
        txtContent.text = CurrStoryData.Content;

        // 스토리 인덱스 증가!
        ++nCurrStoryIndex;
    }

    private void FinishStory()
    {
        Destroy(this.gameObject);
    }
}
