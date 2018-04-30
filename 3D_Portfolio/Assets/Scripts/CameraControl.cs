using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float PosY;
    public float PosZ;
    public float RotX;

    private GameObject objPlayer;
    private Vector3 vPlusPos;
    private Vector3 vRotation;

    private void Awake()
    {
        vPlusPos = new Vector3(0, PosY, PosZ);
        vRotation = new Vector3(RotX, 0, 0);
        objPlayer = GameObject.FindGameObjectWithTag(Util.Tag.PLAYER);
        this.transform.position = objPlayer.transform.position + vPlusPos;
        this.transform.eulerAngles = vRotation;
    }

    private void Update()
    {
        this.transform.position = objPlayer.transform.position + vPlusPos;

        // 플레이어 앞에 있는(가리는) 오브젝트 비활성화 시키기 -> 콜리더가 있는 것만 레이 캐스트가 됨....
        //CheckFrontObject();
    }

    private void CheckFrontObject()
    {
        RaycastHit[] rayHit = Physics.RaycastAll(this.transform.position, this.transform.forward);
        
        for (int i = 0; i < rayHit.Length; ++i)
        {
            if (!rayHit[i].transform.CompareTag(Util.Tag.PLAYER)
                && rayHit[i].distance < (this.transform.position - objPlayer.transform.position).magnitude)
                rayHit[i].transform.gameObject.SetActive(false);
        }
    }
}
