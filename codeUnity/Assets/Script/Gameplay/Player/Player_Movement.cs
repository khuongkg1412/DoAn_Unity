using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
    /* 
        Moving Part
    */
    //Joystick controller (core of joystick)
    //Joystick object
    public Joystick moveJoystick;

    //Velocity movement for player
    public Vector2 moveVelocity;

    //Rigid body of Player
    private Rigidbody2D myBody;

    //Movement speed of player
    public float speed = 300f;

    /* 
       Shooting Part
    */
    //The fire point, when bullet come out
    public Transform firePoint;

    //Bullet Prefab
    [SerializeField]
    public GameObject bulletPrefab;

    //Bullet speed
    public float bulletSpeed = 1000;

    //Time CD for every shot
    [SerializeField]
    private float coolDownTime = 0.5f;

    //Time Player need to wait for next shot
    private float shootTimer;

    //Shoot Joystick object
    public Joystick shootJoystick;

    //Detemin people can shoot or not
    public bool canShoot;

    /*
    Camera Main
    */
    public Camera cameraMain;

    /*
     Help People
    */
    //public GameObject citizen;
    private void Awake()
    {
        //Getting RigidBody
        myBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Shooting after time
        TimeShooting();

        //Detect citizen
        DetectCitizen();
        //Rotation by the touch in joystick shooting
        if (GetComponent<Player_HP>().currentHP > 0)
        {
            Rotation();
        }
    }

    private void FixedUpdate()
    {
        //Movement by the touch in joystick movement
        if (GetComponent<Player_HP>().currentHP > 0)
        {
            Movement();
        }
    }

    /*
        Method for movement of player
    */
    void Movement()
    {
        //Getting hortizontal and vertical axis means x,y and store in vector
        Vector2 moveInput =
            new Vector2(Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical"));

        //If joystick has been dragging, then move the player
        if (moveJoystick.InputDir != Vector3.zero)
        {
            //Direction dragging
            moveInput = moveJoystick.InputDir;

            //Velocity for dragging
            moveVelocity = moveInput.normalized * speed;

            //Move the Player by the Velocity* Time
            myBody
                .MovePosition(myBody.position + moveVelocity * Time.deltaTime);
        }
    }

    /*
        Rotation Player by the touch in Shooting Joystick
    */
    void Rotation()
    {
        //Get the touching position on screen
        Vector2 dir =
            cameraMain.ScreenToWorldPoint(Input.mousePosition) -
            transform.position;

        //Calculate the angle of rotation
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;

        //If joystick has been dragging, then rotate the player
        if (shootJoystick.InputDir != Vector3.zero)
        {
            //Calculate the angle of rotation
            angle =
                Mathf
                    .Atan2(shootJoystick.InputDir.y, shootJoystick.InputDir.x) *
                Mathf.Rad2Deg +
                90;

            //Rotation
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation =
                Quaternion
                    .Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
        }
    }

    /*
    Shooting timer countdown
    */
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

    /*
     Shooting action
    */
    void Shooting()
    {
        //Creating bullet
        GameObject bullet =
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        //Pull bullet out at fire point
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }

    public void DetectCitizen()
    {
        float range = 150f;
        Button button = GameObject.Find("HelpButton").GetComponent<Button>();
        bool check = false;
        GameObject[] citizen = GameObject.FindGameObjectsWithTag("Citizen");
        foreach (var i in citizen)
        {
            if (
                Vector2.Distance(i.transform.position, transform.position) <=
                range &&
                citizen != null
            )
            {
                check = true;
                //button.interactable = true;
                //button.GetComponent<Citizen_Healing>().setCitizenObject(i);
            }
        }
        if (!check)
        {
            button.GetComponent<Citizen_Healing>().disableCitizenObject();
            button.interactable = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Button button = GameObject.Find("HelpButton").GetComponent<Button>();
        if (other.gameObject.tag == "Citizen")
        {
            button.interactable = true;
            button
                .GetComponent<Citizen_Healing>()
                .setCitizenObject(other.gameObject);
        }
        else
        {
            button.interactable = false;
        }
    }
}
