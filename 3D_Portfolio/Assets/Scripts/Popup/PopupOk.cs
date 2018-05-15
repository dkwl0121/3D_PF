using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupOk : MonoBehaviour
{
    public Text txtDescription;

    public void SetDescription(string str)
    {
        txtDescription.text = str;
    }

    public void ClickOk()
    {
        Destroy(this.gameObject);
    }
}
