using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    public joystick_move joystickMove;

    public float runSpeed;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,Camera.main.transform.position.z));
        rb2d = GetComponent<Rigidbody2D>();
        
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (joystickMove.joystickVec.y != 0)
        {
            rb2d.velocity =
                new Vector2(joystickMove.joystickVec.x * runSpeed,
                    joystickMove.joystickVec.y * runSpeed);
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
        ;
        
      
    }
}
