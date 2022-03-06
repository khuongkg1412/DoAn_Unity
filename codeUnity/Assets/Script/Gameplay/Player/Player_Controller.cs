using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    //Getting Canvas
    GameObject canvas;

    Slider HealthBar;

    Text HPText;

    //Create a new object for player
    public Character Character;

    NumeralStruct originNumeral;
    /* 
      Moving Part
  */
    //Joystick controller (core of joystick)
    //Joystick object
    Joystick moveJoystick;

    //Velocity movement for player
    Vector2 moveVelocity;

    //Rigid body of Player
    private Rigidbody2D myBody;

    /* 
       Shooting Part
    */
    //The fire point, when bullet come out
    public Transform firePoint;

    //Bullet Prefab
    [SerializeField] public GameObject bulletPrefab;

    //Bullet speed
    float bulletSpeed = 1000;

    //Time CD for every shot
    [SerializeField] private float coolDownTime;

    //Time Player need to wait for next shot
    private float shootTimer;

    //Shoot Joystick object
    Joystick shootJoystick;

    /*
    Camera Main
    */
    Camera cameraMain;

    // Start is called before the first frame update
    void Start()
    {
        settingCharacter();
        //Let Player shoot and move fistly
        Character.setShoot(true);
        Character.setMove(true);
        //Setting object for Game object
        canvas = GameObject.Find("Canvas");
        HealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        HPText = GameObject.Find("HPText").GetComponent<Text>();
        moveJoystick = GameObject.Find("MoveJoystick").GetComponent<Joystick>();
        shootJoystick = GameObject.Find("ShootJoystick").GetComponent<Joystick>();
        cameraMain = GameObject.Find("Camera").GetComponent<Camera>();
        //Getting RigidBody
        myBody = GetComponent<Rigidbody2D>();
        //Set max HP to slider
        HealthBar.maxValue = Character.returnHP();
        //Set Atack speed
        coolDownTime = Character.returnATKSPD();
    }

    // Update is called once per frame
    void Update()
    {
        //Update the HP of Player
        updateProcessHP();
        //U[date the Movement of Player including Moving, shooting and helping citizen
        updateMovement();
    }
    void settingCharacter()
    {
        // Debug.Log("Before Setting Player " + Player_DataManager.Instance.Player.numeral.HP_Numeral);
        // Debug.Log("Before Setting Charac " + Player_DataManager.Instance.playerCharacter.returnHP());
        //Create Character for gameplay
        // originNumeral = Player_DataManager.Instance.playerCharacter.returnNumeral();
        Character = new Character(Player_DataManager.Instance.playerCharacter.returnNumeral());
        // Debug.Log("After Setting Player " + Player_DataManager.Instance.Player.numeral.HP_Numeral);
        // Debug.Log("After Setting Charac " + Player_DataManager.Instance.playerCharacter.returnHP());
    }
    void updateMovement()
    {
        if (Character.isShoot())
        {
            //Shooting after time
            TimeShooting();
        }

        //Detect citizen
        DetectCitizen();

        //Rotation by the touch in joystick shooting
        if (!Character.isPlayerDead())
        {
            Rotation();
        }
        //Movement by the touch in joystick movement
        if (!Character.isPlayerDead() && Character.isMove())
        {
            Movement();
        }
    }
    void updateProcessHP()
    {
        //Update Text and health bar by the current Health from the object Player
        HealthBar.value = Character.returnHP();
        HPText.text = (HealthBar.value + " / " + HealthBar.maxValue);
        //Cheeck the player is dead
        if (Character.isPlayerDead())
        {
            if (canvas.GetComponent<Game_Boss>() != null)
            {
                canvas.GetComponent<Game_Boss>().GameOVer();
            }
            //Split up by 2 modes : Story Mode and Tutorial
            else if (canvas.GetComponent<Game_Start>() != null)
            {
                canvas.GetComponent<Game_Start>().GameOVer();
            }
            else
            {
                canvas.GetComponent<Game_Tutorial>().GameOVer();
            }
            //Player dead function
            Invoke("playerDead", 1f);
        }
    }
    //Player are dead
    void playerDead()
    {
        //Destroy the game object
        gameObject.SetActive(false);
    }
    private void getDamage()
    {
        /*
            Player Get Hurt then stop the player movement
        */
        Character.setMove(false);
        Invoke("allowMoving", 0.5f);
    }
    /*
        Allow player to move after get damage
    */
    void allowMoving()
    {
        Character.setMove(true);
    }
    /*
        Collision Happend
    */
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Get damage from enemy
        if (other.gameObject.tag == "Enemy")
        {
            Character.getDamage(other.gameObject.GetComponent<Virus_Numeral>().virusNumeral.ATK_Numeral);
            getDamage();
        }
        //Hit by Bullet from Enemy
        if (other.gameObject.tag == "Bullet")
        {
            Character.getDamage(other.gameObject.GetComponent<Bullet>().dameGiven);
            getDamage();
        }
        //Help Citizen
        Button button = GameObject.Find("HelpButton").GetComponent<Button>();
        if (other.gameObject.tag == "Citizen")
        {
            button.interactable = true;
            button.GetComponent<Citizen_Healing>().setCitizenObject(other.gameObject);
        }
        else
        {
            button.interactable = false;
        }
    }
    ////////////////////////////

    /*
        Method for movement of player
    */
    void Movement()
    {
        //Getting hortizontal and vertical axis means x,y and store in vector
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //If joystick has been dragging, then move the player
        if (moveJoystick.InputDir != Vector3.zero)
        {
            //Direction dragging
            moveInput = moveJoystick.InputDir;

            //Velocity for dragging
            moveVelocity = moveInput.normalized * Character.returnSPD();
            //Move the Player by the Velocity* Time
            myBody.MovePosition(myBody.position + moveVelocity * Time.deltaTime);
        }
    }

    /*
        Rotation Player by the touch in Shooting Joystick
    */
    void Rotation()
    {
        //Get the touching position on screen
        Vector2 dir = cameraMain.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        //Calculate the angle of rotation
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;

        //If joystick has been dragging, then rotate the player
        if (shootJoystick.InputDir != Vector3.zero)
        {
            //Calculate the angle of rotation
            angle = Mathf.Atan2(shootJoystick.InputDir.y, shootJoystick.InputDir.x) * Mathf.Rad2Deg + 90;

            //Rotation
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<Bullet>().setPositionStartShooting(firePoint);

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
            if (Vector2.Distance(i.transform.position, transform.position) <= range && citizen != null)
            {
                check = true;
            }
        }
        if (!check)
        {
            button.GetComponent<Citizen_Healing>().disableCitizenObject();
            button.interactable = false;
        }
    }

}
