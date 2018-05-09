using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float PosY;
    public float PosZ;
    public float RotX;

    private GameObject objTarget;
    private GameObject objPlayer;
    private Vector3 vPlusPos;
    private Vector3 vRotation;

    private E_CAMERA_CTRL_TYPE eCtrlType;
    private float fCtrlDuration;
    private float fCurrDuration;
    private float fMaxDist;
    private bool isShake = false;

    private void Start()
    {
        vPlusPos = new Vector3(0, PosY, PosZ);
        vRotation = new Vector3(RotX, 0, 0);
        GameObject objPlayPack = GameObject.FindGameObjectWithTag(Util.Tag.PLAY_PACK);
        objPlayer = objPlayPack.transform.Find(Util.Tag.PLAYER).gameObject;
        objTarget = objPlayer;
        this.transform.position = objTarget.transform.position + vPlusPos;
        this.transform.eulerAngles = vRotation;
        eCtrlType = E_CAMERA_CTRL_TYPE.DEFAULT;
    }

    private void Update()
    {
        switch (eCtrlType)
        {
            case E_CAMERA_CTRL_TYPE.DEFAULT:
                {
                    this.transform.position = objTarget.transform.position + vPlusPos;
                }
                break;
            case E_CAMERA_CTRL_TYPE.STRAIGHT:
                {
                    fCurrDuration += Time.deltaTime;

                    float fCurrDist = (fMaxDist * 2.0f) * (fCurrDuration / fCtrlDuration);  // 보간
                    // 왕복을 구현해야 하기 때문에!
                    if (fCurrDist > fMaxDist)
                    {
                        fCurrDist = (fMaxDist * 2.0f) - fCurrDist;

                        // 도착지점 에서는 잠시 멈춤
                        if (fCurrDist > fMaxDist * 0.5f)
                            fCurrDist = fMaxDist;
                        else
                            fCurrDist *= 2.0f;
                    }

                    Vector3 vPos = objTarget.transform.position + (objTarget.transform.forward * fCurrDist);
                    this.transform.position = vPos + vPlusPos;

                    if (fCurrDuration >= fCtrlDuration)
                        eCtrlType = E_CAMERA_CTRL_TYPE.DEFAULT;
                }
                break;
            case E_CAMERA_CTRL_TYPE.SHAKE:
                {
                    fCurrDuration += Time.deltaTime;

                    if (!isShake)
                    {
                        Vector3 vPos = objTarget.transform.position
                            + (transform.right * Random.Range(-fMaxDist, fMaxDist))
                            + (transform.up * Random.Range(-fMaxDist, fMaxDist));
                        this.transform.position = vPos + vPlusPos;

                        StartCoroutine(DelayShake());
                    }

                    if (fCurrDuration >= fCtrlDuration)
                        eCtrlType = E_CAMERA_CTRL_TYPE.DEFAULT;
                }
                break;
        }
        
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

    // 수동 컨트롤
    public void CameraCtrlStart(E_CAMERA_CTRL_TYPE eType, float fDuration, float fDist)
    {
        eCtrlType = eType;
        fCtrlDuration = fDuration;
        fCurrDuration = 0.0f;
        fMaxDist = fDist;
        isShake = false;
    }

    private IEnumerator DelayShake()
    {
        isShake = true;

        yield return new WaitForSeconds(0.05f);

        isShake = false;
    }
}
