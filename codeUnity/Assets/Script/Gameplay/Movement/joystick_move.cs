using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class joystick_move : MonoBehaviour
{
    //Joystick controller
    [SerializeField]
    private Transform circle;

    //Joystick background
    [SerializeField]
    private Transform outerCircle;

    //Main camera
    [SerializeField]
    private Camera mainCamera;

    //Touching point
    private Vector2 joystickTouchPos;

    //Radius of joystick
    private float joystickRadius;

    //Vector of dragPos and Touchpoint
    public Vector2 joystickVec;

    private void Start()
    {
        //Get Radius of joystick
        joystickRadius =
            outerCircle.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    //Event is triggered when joystick been pressed down
    public void pointerDown()
    {
        //Set joystick controller to
        circle.transform.position = getTouchPosition(Input.mousePosition);

        //Get touch position
        joystickTouchPos = Input.mousePosition;
    }

    //Event is triggered when joystick been pressed up
    public void pointerUp()
    {
        //Player no more moving
        joystickVec = Vector2.zero;

        //Set joystick controller back to origin position
        circle.transform.position = outerCircle.transform.position;
    }

    //Event is triggered when joystick been dragging
    public void Drag(BaseEventData baseEventData)
    {

        //Get draging point
        PointerEventData pointerEventData = baseEventData as PointerEventData;

        //Convert it to Vector
        Vector2 dragPos = pointerEventData.position;

        //Find the Vector from the touch position to drag
        joystickVec = (dragPos - joystickTouchPos).normalized;

        //Distance dragging
        float joystickDist = Vector2.Distance( getTouchPosition(dragPos) ,getTouchPosition(joystickTouchPos));

        //Check dragging distance and radius of joystick
        if (joystickDist < joystickRadius)
        {
            //Set joystick controller to the draggin position
            circle.transform.position = getTouchPosition(joystickTouchPos + joystickVec * joystickDist)
                ;

            //Change the Z-axis that it would be visible on camera
            circle.transform.position =
                new Vector3(circle.transform.position.x,
                    circle.transform.position.y,
                    10);
        }
        else if(joystickDist >= joystickRadius)
        {
            //Set joystick controller to the draggin position
            circle.transform.position = getTouchPosition( joystickTouchPos + joystickVec * joystickRadius);

            //Change the Z-axis that it would be visible on camera
            circle.transform.position =
                new Vector3(circle.transform.position.x,
                    circle.transform.position.y,
                    10);
        }
    }

    //Get vector following the camera position
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
