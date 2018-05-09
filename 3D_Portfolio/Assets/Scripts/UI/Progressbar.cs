using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour
{
    [HideInInspector] public GameObject Target;
    [HideInInspector] public float fMoveY;
    [HideInInspector] public float fScale;
    
    RectTransform CanvasRect;
    private GameObject objProgressbar;
    private Image imgBar;
    
    private void Awake()
    {
        GameObject objCanvas = Instantiate(Resources.Load(Util.ResourcePath.UI_PROGRESSBAR) as GameObject, Target.transform);
        CanvasRect = objCanvas.GetComponent<RectTransform>();
        objProgressbar = objCanvas.transform.Find("HP").gameObject;
        imgBar = objProgressbar.transform.FindChildByRecursive("Gauge").gameObject.GetComponent<Image>();
    }

    public void SetUI(float max, float curr)
    {
        imgBar.fillAmount = curr / max;
    }

    private void Update()
    {
        if (Target == null) return;

        // == 스크린 좌표로 변경 ==
        Vector3 Pos = Target.transform.position;
        Pos.y += fMoveY;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(Pos);
        if (screenPos.z >= 0.0f && screenPos.z <= 20.0f)
        {
            objProgressbar.SetActive(true);
            objProgressbar.transform.position = screenPos;
            objProgressbar.transform.localScale = new Vector3(fScale, fScale, fScale);
        }
        else
        {
            objProgressbar.SetActive(false);
        }

        //Vector3 Pos = Target.transform.position;
        //Pos.y += fMoveY;

        //Vector3 uiPos = Camera.main.WorldToScreenPoint(Pos);

        //if (uiPos.z >= 0.0f)
        //{
        //    objProgressbar.SetActive(true);

        //    // 거리에 따른 사이즈 조절
        //    //float fScale = uiCamera.scaledPixelWidth - uiPos.z / uiCamera.scaledPixelWidth;
        //    //fScale = Mathf.Clamp(fScale, 0.0f, 1.0f);

        //    float scale = Mathf.InverseLerp(0, 1.0f, 1.0f - uiPos.z);
        //    scale = Mathf.Clamp(fScale, 0.0f, 1.0f);

        //    objProgressbar.transform.localScale = new Vector3(scale, scale, scale);

        //    uiPos.z = 0.0f;
        //    objProgressbar.transform.position = Camera.main.ScreenToWorldPoint(uiPos);
        //}

        //else
        //{
        //    objProgressbar.SetActive(false);
        //}
    }
}
