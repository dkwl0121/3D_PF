using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobbyPlayerControl : MonoBehaviour
{
    public Animator anim;
    public float fChangeCount;
    private float fCurrCount = 0;
    
    private void Update()
    {
        fCurrCount += Time.deltaTime;

        if (fCurrCount > fChangeCount)
        {
            fCurrCount = 0.0f;

            int nRanNum = Random.Range(0, 5);
            string str;

            if (nRanNum == 0)
                str = "Pose_01";
            else if (nRanNum == 1)
                str = "Pose_02";
            else if (nRanNum == 2)
                str = "Pose_03";
            else if (nRanNum == 3)
                str = "Pose_04";
            else return;

            anim.SetTrigger(str);
        }
    }
}