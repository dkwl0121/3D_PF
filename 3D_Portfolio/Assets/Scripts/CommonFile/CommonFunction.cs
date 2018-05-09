using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFunction
{
    public static void CauseDamage(GameObject objEneny, float fDamge)
    {
        if (objEneny.CompareTag(Util.Tag.PLAYER))
        {
            PlayerControl ctrl = objEneny.GetComponent<PlayerControl>();
            if (ctrl != null)
                ctrl.GetDamage(fDamge);
        }
        else if (objEneny.CompareTag(Util.Tag.MUSHROOM))
        {
            MushroomControl ctrl = objEneny.GetComponent<MushroomControl>();
            if (ctrl != null)
                ctrl.GetDamage(fDamge);
        }
        else if (objEneny.CompareTag(Util.Tag.STONE_MONSTER))
        {
            StoneMonsterControl ctrl = objEneny.GetComponent<StoneMonsterControl>();
            if (ctrl != null)
                ctrl.GetDamage(fDamge);
        }
        else if (objEneny.CompareTag(Util.Tag.PUMPKIN))
        {
            PumpkinControl ctrl = objEneny.GetComponent<PumpkinControl>();
            if (ctrl != null)
                ctrl.GetDamage(fDamge);
        }
    }

    public static void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://google.com");
#else
        Application.Quit();
#endif
    }

    //// 코사인함수를 활용한 보간
    //public static float GetCosInterpolate(float fLenght, float fT)
    //{
    //    return fLenght * (Mathf.Sin(fT * Mathf.PI) + 1.0f) * 0.5f;    //cos()값이  -1 ~ 1 사이의 숫자가 나옴 0 ~ 1 사이값으로 변경 후 계산!
    //}

    //// 보간
    //public static float GetInterpolate(float fLenght, float fT)
    //{
    //    return fLenght * fT;
    //}
}
