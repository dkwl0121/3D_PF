using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Color DefaultColor;

    [HideInInspector] public int nIndex;

    private bool isSelect;
    public bool Select { get { return isSelect; } }
    
    private void Awake()
    {
        DefaultColor = this.GetComponent<Image>().color;
        isSelect = false;
    }
    
    // 슬롯 선택
    public void SelectOn()
    {
        isSelect = true;
        this.GetComponent<Image>().color = Color.yellow;
    }

    // 슬롯 선택 해제
    public void SelectOff()
    {
        isSelect = false;
        this.GetComponent<Image>().color = DefaultColor;
    }
}
