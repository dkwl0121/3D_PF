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

    //// 보간
    //public static Vector3 GetInterpolate(Vector3 v1, Vector3 v2, float fT)
    //{
    //    Vector3 vResult = Vector3.zero;

    //    //vResult = (v1 - v2) * fT;

    //    return vResult;
    //}
}
