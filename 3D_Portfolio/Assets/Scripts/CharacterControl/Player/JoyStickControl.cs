using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickControl : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Range(0f, 2f)] public float handleLimit = 1f;

    [HideInInspector] public Vector2 inputVector = Vector2.zero;

    Vector2 joystickPosition = Vector2.zero;
    private Camera cam = new Camera();

    public RectTransform background;
    public RectTransform handle;

    private bool isClick;

    public float Horizontal { get { return inputVector.x; } }
    public float Vertical { get { return inputVector.y; } }

    private float fDistance;
    public float Distance { get { return fDistance; } }

    private void Awake()
    {
        isClick = false;
        joystickPosition = RectTransformUtility.WorldToScreenPoint(cam, background.position);
    }

    private void OnDisable()
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isClick) return;
        Vector2 direction = eventData.position - joystickPosition;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        fDistance = inputVector.magnitude;
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClick = true;
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isClick = false;
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}
