using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class joystick_move : MonoBehaviour
{
    //Joystick controller
    public GameObject joystick;

    //Joystick background
    public GameObject joystickBG;

    public Vector2 joystickVec;

    private Vector2 joystickTouchPos;

    [SerializeField]
    private Transform joystickOriginalPos;

    private float joystickRadius;

    public Camera cameraMain;

    // Start is called before the first frame update
    void Start()
    {
        joystickRadius =
            joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    public void PointerDown()
    {
        joystick.transform.position =
            new Vector3(cameraMain.ScreenToWorldPoint(Input.mousePosition).x,
                cameraMain.ScreenToWorldPoint(Input.mousePosition).y,
                10);

        joystickBG.transform.position =
            new Vector3(cameraMain.ScreenToWorldPoint(Input.mousePosition).x,
                cameraMain.ScreenToWorldPoint(Input.mousePosition).y,
                10);

        joystickTouchPos =
            new Vector3(cameraMain.ScreenToWorldPoint(Input.mousePosition).x,
                cameraMain.ScreenToWorldPoint(Input.mousePosition).y,
                10);
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;

        Vector2 dragPos =
            new Vector2(cameraMain
                    .ScreenToWorldPoint(pointerEventData.position)
                    .x,
                cameraMain.ScreenToWorldPoint(pointerEventData.position).y);
        
       // Debug.Log("joystickTouchPos: " + joystickTouchPos);
        joystickVec = (dragPos - joystickTouchPos).normalized;

        Debug.Log("joystickVec :" + joystickVec);

        float joystickDis = Vector2.Distance(dragPos, joystickTouchPos);
        //Debug.Log("joystickDis :" + joystickDis);
        //Debug.Log("joystickRadius :" + joystickRadius);


        if (joystickDis < joystickRadius)
        {
            Debug.Log("Keo nho hon: ");
            joystick.transform.position =
                joystickTouchPos + joystickVec * joystickDis;

            // joystick.transform.position =
            //     new Vector3(cameraMain
            //             .ScreenToWorldPoint(joystick.transform.position)
            //             .x -
            //         Screen.width,
            //         cameraMain
            //             .ScreenToWorldPoint(joystick.transform.position)
            //             .y -
            //         Screen.height,
            //         10);
            joystick.transform.position =
                new Vector3(joystick.transform.position.x,
                    joystick.transform.position.y,
                    10);
        }
        else
        {
            Debug.Log("Keo dai hon");
            joystick.transform.position =
                joystickTouchPos + joystickVec * joystickRadius;
            // joystick.transform.position =
            //     joystickTouchPos + joystickVec * joystickDis;
            joystick.transform.position =
                new Vector3(joystick.transform.position.x,
                    joystick.transform.position.y,
                    10);
        }
    }

    public void PointerUp()
    {
        joystickVec = Vector2.zero;

        joystick.transform.position = joystickOriginalPos.position;

        joystickBG.transform.position = joystickOriginalPos.position;
    }
}
