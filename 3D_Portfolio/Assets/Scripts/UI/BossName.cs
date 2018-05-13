using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossName : MonoBehaviour
{
    public RectTransform tfName;
    public Text txtName;
    private float fDestX;

    private void Awake()
    {
        fDestX = tfName.position.x;
        Vector3 vStartPos = tfName.transform.position;
        vStartPos.x = fDestX - 300.0f;
        tfName.position = vStartPos;

        StartCoroutine(DelayDestroy());
    }

    private void Update()
    {
        if (tfName.transform.position.x >= fDestX) return;
        
        Vector3 vPos = tfName.transform.position;
        vPos.x += Time.deltaTime * 500.0f;
        tfName.position = vPos;
    }

    public void SetName(string name)
    {
        txtName.text = name;
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(2.0f);

        Destroy(this.gameObject);
    }
}
