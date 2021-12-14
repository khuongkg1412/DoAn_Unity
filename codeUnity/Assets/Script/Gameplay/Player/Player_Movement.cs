using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Joystick moveJoystick;

    public Vector3 moveVelocity;

    private Rigidbody2D myBody;

    public float speed = 300f;

    private void Awake()
    {
    }

    void Movement()
    {
        Vector2 moveInput =
            new Vector2(Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical"));

        if (moveJoystick.InputDir != Vector3.zero)
        {
            moveInput = moveJoystick.InputDir;

            moveVelocity = moveInput.normalized * speed;
        }
    }
}
