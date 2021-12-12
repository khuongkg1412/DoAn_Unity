using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class joystick_move : MonoBehaviour
{
    // //Joystick controller
    // public GameObject joystick;
    // //Joystick background
    // public GameObject joystickBG;
    // public Vector2 joystickVec;
    // private Vector2 joystickTouchPos;
    // [SerializeField]
    // private Transform joystickOriginalPos;
    // private float joystickRadius;
    // public Camera cameraMain;
    // // Start is called before the first frame update
    // void Start()
    // {
    //     joystickRadius =
    //         joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
    // }
    // public void PointerDown()
    // {
    //     joystick.transform.position =
    //         new Vector3(cameraMain.ScreenToWorldPoint(Input.mousePosition).x,
    //             cameraMain.ScreenToWorldPoint(Input.mousePosition).y,
    //             10);
    //     joystickBG.transform.position =
    //         new Vector3(cameraMain.ScreenToWorldPoint(Input.mousePosition).x,
    //             cameraMain.ScreenToWorldPoint(Input.mousePosition).y,
    //             10);
    //     joystickTouchPos =
    //         new Vector3(cameraMain.ScreenToWorldPoint(Input.mousePosition).x,
    //             cameraMain.ScreenToWorldPoint(Input.mousePosition).y,
    //             10);
    // }
    // public void Drag(BaseEventData baseEventData)
    // {
    //     PointerEventData pointerEventData = baseEventData as PointerEventData;
    //     Vector2 dragPos =
    //         new Vector2(cameraMain
    //                 .ScreenToWorldPoint(pointerEventData.position)
    //                 .x,
    //             cameraMain.ScreenToWorldPoint(pointerEventData.position).y);
    //    // Debug.Log("joystickTouchPos: " + joystickTouchPos);
    //     joystickVec = (dragPos - joystickTouchPos).normalized;
    //     Debug.Log("joystickVec :" + joystickVec);
    //     float joystickDis = Vector2.Distance(dragPos, joystickTouchPos);
    //     //Debug.Log("joystickDis :" + joystickDis);
    //     //Debug.Log("joystickRadius :" + joystickRadius);
    //     if (joystickDis < joystickRadius)
    //     {
    //         Debug.Log("Keo nho hon: ");
    //         joystick.transform.position =
    //             joystickTouchPos + joystickVec * joystickDis;
    //         // joystick.transform.position =
    //         //     new Vector3(cameraMain
    //         //             .ScreenToWorldPoint(joystick.transform.position)
    //         //             .x -
    //         //         Screen.width,
    //         //         cameraMain
    //         //             .ScreenToWorldPoint(joystick.transform.position)
    //         //             .y -
    //         //         Screen.height,
    //         //         10);
    //         joystick.transform.position =
    //             new Vector3(joystick.transform.position.x,
    //                 joystick.transform.position.y,
    //                 10);
    //     }
    //     else
    //     {
    //         Debug.Log("Keo dai hon");
    //         joystick.transform.position =
    //             joystickTouchPos + joystickVec * joystickRadius;
    //         // joystick.transform.position =
    //         //     joystickTouchPos + joystickVec * joystickDis;
    //         joystick.transform.position =
    //             new Vector3(joystick.transform.position.x,
    //                 joystick.transform.position.y,
    //                 10);
    //     }
    // }
    // public void PointerUp()
    // {
    //     joystickVec = Vector2.zero;
    //     joystick.transform.position = joystickOriginalPos.position;
    //     joystickBG.transform.position = joystickOriginalPos.position;
    // }
    [SerializeField]
    private Transform circle;

    [SerializeField]
    private Transform outerCircle;

    [SerializeField]
    private Camera mainCamera;

    private Vector2 startingPoint;

    private int leftTouch = 99;

    private float range;

    public Vector2 joystickVec;

    private void Start() {
        range = outerCircle.GetComponent<RectTransform>().sizeDelta.y / 4;
    }
    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            Vector2 touchPos = getTouchPosition(t.position); // * -1 for perspective cameras
            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x > Screen.width / 2)
                {
                    // shootBullet();
                }
                else
                {
                    leftTouch = t.fingerId;
                    startingPoint = touchPos;
                }
            }
            else if (t.phase == TouchPhase.Moved && leftTouch == t.fingerId)
            {
                Vector2 offset = touchPos - startingPoint;
                Debug.Log("Moving :" + offset);
                joystickVec = (touchPos - startingPoint).normalized;
                Vector2 direction = Vector2.ClampMagnitude(offset, range);

                // moveCharacter(direction);
                circle.transform.position =
                    new Vector3( outerCircle.transform.position.x + direction.x,
                         outerCircle.transform.position.y + direction.y,
                        10);
            }
            else if (t.phase == TouchPhase.Ended && leftTouch == t.fingerId)
            {
                joystickVec = Vector2.zero;
                leftTouch = 99;
                circle.transform.position =
                    new Vector3(outerCircle.transform.position.x,
                        outerCircle.transform.position.y,
                        10);
            }
            ++i;
        }
    }
    Vector2 getTouchPosition(Vector2 touchPosition)
    {
        return mainCamera
            .ScreenToWorldPoint(new Vector3(touchPosition.x,
                touchPosition.y,
                transform.position.z));
    }
}
