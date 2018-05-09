using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    private Text txtValue;
    private float fDuration;

    private void Awake()
    {
        txtValue = transform.Find("Text").GetComponent<Text>();
        txtValue.enabled = false;
    }

    public void SetText(Color color, string str)
    {
        txtValue.enabled = true;
        txtValue.text = str;
        color.a = 0;
        txtValue.color = color;
        fDuration = 0.0f;

        StartCoroutine(DelayDestroy());
    }

    private void Update()
    {
        fDuration += Time.deltaTime;
        Color color = txtValue.color;
        if (fDuration < 0.5f)
            color.a = fDuration * 2.0f;
        else if (fDuration < 1.5f)
            color.a = 1;
        else
            color.a = 1.0f - ((fDuration - 1.5f) * 2.0f);
        txtValue.color = color;
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(2.0f);

        Destroy(this.gameObject);
    }
}
