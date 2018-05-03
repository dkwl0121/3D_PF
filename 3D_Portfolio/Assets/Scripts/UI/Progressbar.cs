using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour
{
    [HideInInspector] public GameObject Target;
    [HideInInspector] public float fMoveY;
    [HideInInspector] public float fScale;

    //private Camera CanvasCamera;
    RectTransform CanvasRect;
    private GameObject objProgressbar;
    private Image imgBar;
    
    private void Awake()
    {
        GameObject objCanvas = Instantiate(Resources.Load("UI/Canvas Progressbar") as GameObject, Target.transform);
        //CanvasCamera = objCanvas.GetComponent<Canvas>().worldCamera;
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

        Vector3 Pos = Target.transform.position;
        Pos.y += fMoveY;

        // == 스크린 좌표로 변경 ==
        Vector3 screenPos = Camera.main.WorldToViewportPoint(Pos);
        screenPos.x *= CanvasRect.rect.width;
        screenPos.y *= CanvasRect.rect.height;
        objProgressbar.transform.position = screenPos;
        objProgressbar.transform.localScale = new Vector3(fScale, fScale, fScale);

        //Vector3 uiPos = Camera.main.WorldToScreenPoint(Pos);
        //float fScale = Mathf.InverseLerp(0, 1.0f, 1.0f - screenPos.z);
        //fScale = Mathf.Clamp(fScale, 0.0f, 1.0f);
        //objProgressbar.transform.localScale = new Vector3(fScale, fScale, fScale);

        //Vector3 Pos = Target.transform.position;
        //Pos.y += fMoveY;

        //Vector3 uiPos = Camera.main.WorldToScreenPoint(Pos);

        //if (uiPos.z >= 0.0f)
        //{
        //    objProgressbar.SetActive(true);

        ////     거리에 따른 사이즈 조절
        ////    float fScale = uiCamera.scaledPixelWidth - uiPos.z / uiCamera.scaledPixelWidth;
        ////    fScale = Mathf.Clamp(fScale, 0.0f, 1.0f);

        //    float fScale = Mathf.InverseLerp(0, 1.0f, 1.0f - uiPos.z);
        //    fScale = Mathf.Clamp(fScale, 0.0f, 1.0f);

        //    objProgressbar.transform.localScale = new Vector3(fScale, fScale, fScale);

        //    uiPos.z = 0.0f;
        //    objProgressbar.transform.position = tf.InverseTransformPoint(uiPos);// tf.TransformVector(uiPos);//.TransformPoint(uiPos);
        //}

        //else
        //{
        //    objProgressbar.SetActive(false);
        //}
    }
}
