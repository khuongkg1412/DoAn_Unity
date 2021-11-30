using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class joystick_move : MonoBehaviour
{
    public GameObject joystick;

    public GameObject joystickBG;

    public Vector2 joystickVec;

    private Vector2 joystickTouchPos;

    private Vector2 joystickOriginalPos;

    private float joystickRadius;

    // Start is called before the first frame update
    void Start()
    {
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius =
            joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    public void PointerDown()
    {
        Debug.Log("Pointer Down 3");

        //joystick.transform.position = new VecInput.mousePosition;
        joystick.transform.position =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100);
        joystickBG.transform.position =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100);
        joystickTouchPos =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100);
        // joystick1.position =
        //     new Vector3(Input.mousePosition.x, Input.mousePosition.y,100);
        // joystick2.position =
        //     new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100);
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;
        float joystickDis = Vector2.Distance(dragPos, joystickTouchPos);
        if (joystickDis < joystickRadius)
        {
            joystick.transform.position =
                joystickTouchPos + joystickVec * joystickDis;
            joystick.transform.position =
                new Vector3(joystick.transform.position.x,
                    joystick.transform.position.y,
                    100);
        }
        else
        {
            joystick.transform.position =
                joystickTouchPos + joystickVec * joystickRadius;
            joystick.transform.position =
                new Vector3(joystick.transform.position.x,
                    joystick.transform.position.y,
                    100);
        }
    }

    public void PointerUp()
    {
        Debug.Log("Pointer Up");
        joystickVec = Vector2.zero;
        joystick.transform.position =
            new Vector3(joystickOriginalPos.x, joystickOriginalPos.y, 100);
        Debug.Log("Pointer Up X: " + joystickOriginalPos.x);
        joystickBG.transform.position =
            new Vector3(joystickOriginalPos.x, joystickOriginalPos.y, 100);
    }
}
