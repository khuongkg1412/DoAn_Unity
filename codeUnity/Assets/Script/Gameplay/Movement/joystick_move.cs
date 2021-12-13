using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class joystick_move : MonoBehaviour
{
    [SerializeField]
    private Transform circle;

    [SerializeField]
    private Transform outerCircle;

    [SerializeField]
    private Camera mainCamera;

    private Vector2 joystickTouchPos;

    private int leftTouch = 99;

    private float joystickRadius;

    public Vector2 joystickVec;

    private void Start()
    {
        joystickRadius =
            outerCircle.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    public void pointerDown()
    {
        circle.transform.position = getTouchPosition(Input.mousePosition);
        joystickTouchPos = Input.mousePosition;
    }

    public void pointerUp()
    {
        joystickVec = Vector2.zero;
        circle.transform.position = outerCircle.transform.position;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;

        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance( getTouchPosition(dragPos) ,getTouchPosition(joystickTouchPos));

        if (joystickDist < joystickRadius)
        {
            circle.transform.position = getTouchPosition(joystickTouchPos + joystickVec * joystickDist)
                ;

            circle.transform.position =
                new Vector3(circle.transform.position.x,
                    circle.transform.position.y,
                    10);
        }
        else
        {
            circle.transform.position = getTouchPosition( joystickTouchPos + joystickVec * joystickRadius);

            circle.transform.position =
                new Vector3(circle.transform.position.x,
                    circle.transform.position.y,
                    10);
        }
    }

    Vector3 getTouchPosition(Vector2 touchPosition)
    {
        Vector3 vector =
            mainCamera
                .ScreenToWorldPoint(new Vector3(touchPosition.x,
                    touchPosition.y,
                    transform.position.z));
        return vector;
    }
}
