using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RobbyPlayerRotationCtrl : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private float fPrevX;
    public GameObject objPlayer;

    public void OnPointerDown(PointerEventData eventData)
    {
        fPrevX = eventData.position.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float fRot = fPrevX - eventData.position.x;
        objPlayer.transform.Rotate(Vector3.up * fRot);
        fPrevX = eventData.position.x;
    }
}
