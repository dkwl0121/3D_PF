using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    public Image imgFade;
    public float fFadeSecond;
    private float fDuration = 0.0f;

    private void Awake()
    {
        SceneCtrlManager.Instace.Setup();
    }

    private void Update()
    {
        fDuration += Time.deltaTime;

        Color color = Color.black;

        if (fDuration < fFadeSecond * 0.3f)
            color.a = 1.0f - (fDuration / fFadeSecond * 0.3f);
        else if (fDuration < fFadeSecond * 0.7f)
            color.a = 0.0f;
        else if (fDuration < fFadeSecond)
            color.a = 1.0f - (fFadeSecond - fDuration) / (fFadeSecond * 0.3f);
        else
            SceneCtrlManager.Instace.ChangeScene(E_SCENE_NO.ROBBY);

        imgFade.color = color;
    }
}
