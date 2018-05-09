using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageValue : MonoBehaviour
{
    private RectTransform CanvasRect;
    private GameObject objText;
    private Text txtValue;

    private Vector3 Target;
    private float fSpeed = 2.0f;
    
    private void Awake()
    {
        CanvasRect = this.GetComponent<RectTransform>();
        objText = this.transform.Find("Value").gameObject;
        txtValue = objText.GetComponent<Text>();
    }

    private void Update()
    {
        if (Target == null) return;

        Target.y += fSpeed * Time.deltaTime;

        // == 스크린 좌표로 변경 ==
        Vector3 screenPos = Camera.main.WorldToScreenPoint(Target);
        objText.transform.position = screenPos;
    }

    public void SetInfo(Vector3 pos, float damage, Color color)
    {
        Target = pos;
        txtValue.text = damage.ToString();
        txtValue.color = color;
    }
}
