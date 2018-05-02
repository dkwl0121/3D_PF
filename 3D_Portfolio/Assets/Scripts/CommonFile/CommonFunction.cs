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
    }
}
