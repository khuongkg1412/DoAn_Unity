using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public Animator anim;
    //Setting up Enemy
    public Enemy virus;
    //Player targetPlayer to enemy move forward
    public GameObject[] targetCitizen;
    public Transform targetPlayer;
    //The orginal position of enemy, after out of range with player, enemy would comeback here
    [SerializeField]
    Transform originalPos;

    //Decide whether enemy is following player
    public bool isFollow = true;

    //Time before enemy continute follow player after a collision
    float waiToFolllow = 0f;

    /*
    HP Enemy
    */
    GameObject gamePlay;
    //Cureent Health Point
    float currentHP;

    //Max Health Point
    float maxHP;

    public GameObject HealthBar;

    float maxHPsize;
    Vector3 updWard;
    Vector3 downWard;
    private void Start()
    {
        virus = new Enemy();
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
        maxHP = virus.numeral.HP_Numeral;
        currentHP = maxHP;
        maxHPsize = HealthBar.transform.localScale.x;
    }
    bool distanceToPlayer()
    {

        if (Vector3.Distance(targetPlayer.position, transform.position) <= virus.detectRange)
        {
            return true;
        }
        return false;
    }

    GameObject distanceToCitizen()
    {
        //Find all citizen
        targetCitizen = GameObject.FindGameObjectsWithTag("Citizen");
        if (targetCitizen.Length > 0)
        {
            if (targetCitizen[0] != null)
            {
                //Set minimum value for fisrt object in list of obejcts citizens
                float minimumRange = Vector3.Distance(targetCitizen[0].transform.position, transform.position);
                GameObject target = targetCitizen[0];
                //Go throught list and check distance from enemy to citizens
                foreach (var i in targetCitizen)
                {
                    if (i != null)
                    {
                        //Set Value when new minimun range is found
                        if (Vector3.Distance(i.transform.position, transform.position) < minimumRange)
                        {
                            //Set mimum range value and object
                            minimumRange = Vector3.Distance(i.transform.position, transform.position);
                            target = i;
                        }
                    }
                }
                //If mimum range is in range following then return that target
                if (minimumRange <= virus.detectRange)
                {
                    return target;
                }
            }

        }

        //else return null
        return null;
    }
    //Comeback home position
    public void comeBackPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, originalPos.position, virus.numeral.SPD_Numeral * Time.deltaTime);
    }

    //Following p;ayer
    public void followPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, virus.numeral.SPD_Numeral * Time.deltaTime);
    }

    public void followCitizen(GameObject citizen)
    {
        transform.position = Vector3.MoveTowards(transform.position, citizen.transform.position, virus.numeral.SPD_Numeral * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //When being hit, enemy cannot move around
        isFollow = false;
        // force is how forcefully we will push the player away from the enemy.
        float force = 10000;
        // If the object we hit is the enemy
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Citizen")
        {
            // Calculate Angle Between the collision point and the player
            Vector2 dir = other.contacts[0].point - (Vector2)transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force.
            // This will push back the player
            Rigidbody2D rd = gameObject.GetComponent<Rigidbody2D>();
            rd.AddForce(dir * force);
            //Hit citizen , then decrease HP from zitizen
            if (other.gameObject.tag == "Citizen")
            {
                other.gameObject.GetComponent<Citizen_HP>().isSicked = true;
            }
        }

        /*
        Enemy HP
        */
        if (other.gameObject.tag == "Bullet")
        {

            currentHP -= other.gameObject.GetComponent<Bullet>().dameGiven;
            Debug.Log("currentHP " + currentHP);
            if (currentHP > 0)
            {
                Debug.Log("Still live");
                HealthBar.transform.localScale = new Vector3((currentHP / maxHP) * maxHPsize, HealthBar.transform.transform.localScale.y, HealthBar.transform.transform.localScale.z);
            }
            else
            {
                Debug.Log("Dead");
                currentHP = 0;
                HealthBar.transform.localScale =
                    new Vector3(0,
                        HealthBar.transform.transform.localScale.y,
                        HealthBar.transform.transform.localScale.z);
                if (gamePlay.GetComponent<Game_Boss>() != null)
                {
                    GameObject.FindWithTag("Player").GetComponent<Player_Controller>().Character.score += 1000;
                    gamePlay.GetComponent<Game_Boss>().isGameOver = true;
                    gamePlay.GetComponent<Game_Boss>().isVictory = true;
                }
                else if (gamePlay.GetComponent<Game_Start>() != null)
                {
                    GameObject.FindWithTag("Player").GetComponent<Player_Controller>().Character.score += 10;
                    gamePlay.GetComponent<Game_Start>().UpdateEnemyNumber(1);
                }
                else
                {
                    gamePlay.GetComponent<Game_Tutorial>().UpdateScore(10f);
                    gamePlay.GetComponent<Game_Tutorial>().UpdateEnemyNumber(1);
                }
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
            transform.position = Vector3.MoveTowards(transform.position, downWard, virus.numeral.SPD_Numeral * Time.deltaTime);
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), downWard) == 0)
            {
                reachUpper = false;
                reachLowwe = true;
            }
        }
        else if (reachLowwe)
        {
            //Upward Move
            transform.position = Vector3.MoveTowards(transform.position, updWard, virus.numeral.SPD_Numeral * Time.deltaTime);
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), updWard) == 0)
            {
                reachUpper = true;
                reachLowwe = false;
            }
        }
    }

}
