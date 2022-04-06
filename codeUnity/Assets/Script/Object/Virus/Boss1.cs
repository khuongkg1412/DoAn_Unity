using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    //Setting up Enemy
    public Enemy virus;

    /*
    HP Enemy
    */
    GameObject gamePlay;
    //Cureent Health Point
    float currentHP;
    //Max Health Point
    float maxHP, maxHPsize;
    public GameObject HealthBar;
    Vector3 updWard;
    Vector3 downWard;
    private void Start()
    {
        //Set virus type for boss virus
        virus = new VirusBoss();
        setNumeral();
        gameObject.GetComponent<Virus_Numeral>().settingNumeral(new VirusBoss());
        gamePlay = GameObject.Find("Canvas");
        updWard = new Vector3(transform.position.x, transform.position.y + 300, transform.position.z);
        downWard = new Vector3(transform.position.x, transform.position.y - 300, transform.position.z);
    }
    private float shootTimer;
    [SerializeField] private float coolDownTime;
    private void Update()
    {
        moveUpDown();
        TimeShooting();
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
            GetComponent<Animator>().Play("New Animation");
            for (int i = 0; i < 5; i++)
            {
                switch (i)
                {
                    case 0:
                        Shoot(firePoint1);
                        break;
                    case 1:
                        Shoot(firePoint2);
                        break;
                    case 2:
                        Shoot(firePoint3);
                        break;
                    case 3:
                        Shoot(firePoint4);
                        break;
                    case 4:
                        Shoot(firePoint5);
                        break;
                }
            }
        }
    }

    public void setNumeral()
    {
        maxHP = virus.returnHP();
        currentHP = maxHP;
        maxHPsize = HealthBar.transform.localScale.x;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        /*
        Enemy HP
        */
        if (other.gameObject.tag == "Bullet")
        {
            currentHP -= other.gameObject.GetComponent<Bullet>().dameGiven;
            if (currentHP > 0)
            {
                HealthBar.transform.localScale = new Vector3((currentHP / maxHP) * maxHPsize, HealthBar.transform.transform.localScale.y, HealthBar.transform.transform.localScale.z);
            }
            else
            {
                currentHP = 0;
                HealthBar.transform.localScale = new Vector3(0, HealthBar.transform.transform.localScale.y, HealthBar.transform.transform.localScale.z);

                GameObject.FindWithTag("Player").GetComponent<Player_Controller>().Character.setScore(1000f);
                gamePlay.GetComponent<Game_Boss>().isGameOver = true;
                gamePlay.GetComponent<Game_Boss>().isVictory = true;

                Destroy(gameObject);
            }
        }
    }
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint1, firePoint2, firePoint3, firePoint4, firePoint5;
    void Shoot(Transform firePoint)
    {
        //Creating bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<Bullet>().setPositionStartShooting(firePoint);
        bullet.GetComponent<Bullet>().rangeShooting = 1000f;
        //Pull bullet out at fire point
        rb.AddForce(firePoint.up * 1500f, ForceMode2D.Impulse);
    }
    bool reachUpper = false, reachLowwe = true;
    public void moveUpDown()
    {
        if (reachUpper)
        {
            //Downward Move
            transform.position = Vector3.MoveTowards(transform.position, downWard, virus.returnSPD() * Time.deltaTime);
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), downWard) == 0)
            {
                reachUpper = false;
                reachLowwe = true;
            }
        }
        else if (reachLowwe)
        {
            //Upward Move
            transform.position = Vector3.MoveTowards(transform.position, updWard, virus.returnSPD() * Time.deltaTime);
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), updWard) == 0)
            {
                reachUpper = true;
                reachLowwe = false;
            }
        }
    }

}
