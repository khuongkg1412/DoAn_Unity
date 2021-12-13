using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    //Joystick Controller
    public joystick_move joystickMove;

    public float runSpeed;

    private Rigidbody2D rb2d;

    public Animator animator;

    public Camera cam;

    public bool isShoot = true;

    Vector2 mousePos;

    public Transform firePoint;

    [SerializeField]
    public GameObject bulletPrefab;

    public float bulletSpeed = 1000;

    [SerializeField]
    private float coolDownTime = 0.5f;

    private float shootTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Shoot()
    {
        //Increase shooterTimer
        shootTimer += Time.deltaTime;
        //Shooting every time shootTimer reaches the coolDownTime
        if (shootTimer > coolDownTime)
        {
            //Reset time shooter
            shootTimer = 0f;

            //Creating bullet
            GameObject bullet =
                Instantiate(bulletPrefab,
                firePoint.position,
                firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            //Pull bullet out at fire point
            rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    // run animation for player movement
    void Update()
    {
       animator.SetFloat("Horizontal", joystickMove.joystickVec.x);
       animator.SetFloat("Vertical", joystickMove.joystickVec.y);
       animator.SetFloat("Speed", joystickMove.joystickVec.sqrMagnitude);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (isShoot)
        {
            Shoot();
        }
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
            Vector2 lookDir = mousePos - rb2d.position;
            float angle =
                Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 270f;
          //  rb2d.rotation = angle;
        }
    }
}
