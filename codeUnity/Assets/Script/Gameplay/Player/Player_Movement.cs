using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    /* 
        Moving Part
    */
    public Joystick moveJoystick;

    public Vector2 moveVelocity;

    private Rigidbody2D myBody;

    public float speed = 300f;

    /* 
       Shooting Part
    */
    public Transform firePoint;

    [SerializeField]
    public GameObject bulletPrefab;

    public float bulletSpeed = 1000;

    [SerializeField]
    private float coolDownTime = 0.5f;

    private float shootTimer;

    public Joystick shootJoystick;

    public bool canShoot;

    /*
    Camera Main
    */
    public Camera cameraMain;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        TimeShooting();
        if (GetComponent<Player_HP>().currentHP > 0)
        {
            Rotation();
        }
    }

    private void FixedUpdate()
    {
        if (GetComponent<Player_HP>().currentHP > 0)
        {
            Movement();
        }
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
            myBody
                .MovePosition(myBody.position + moveVelocity * Time.deltaTime);
        }
    }

    void Rotation()
    {
        Vector2 dir =
            cameraMain.ScreenToWorldPoint(Input.mousePosition) -
            transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;

        if (shootJoystick.InputDir != Vector3.zero)
            angle =
                Mathf
                    .Atan2(shootJoystick.InputDir.y, shootJoystick.InputDir.x) *
                Mathf.Rad2Deg +
                90;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        transform.rotation =
            Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
    }

    void TimeShooting()
    {
        //Increase shooterTimer
        shootTimer += Time.deltaTime;

        //Shooting every time shootTimer reaches the coolDownTime
        if (shootTimer > coolDownTime)
        {
            //Reset time shooter
            shootTimer = 0f;
            Shooting();
        }
    }

    void Shooting()
    {
        //Creating bullet
        GameObject bullet =
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        //Pull bullet out at fire point
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }
}
