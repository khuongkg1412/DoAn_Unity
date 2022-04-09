using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    //Setting up Enemy
    public VirusBoss2 virus;

    /*
HP Enemy
*/
    GameObject gamePlay;
    //Cureent Health Point
    float currentHP;
    //Max Health Point
    float maxHP, maxHPsize;
    public GameObject HealthBar;
    Rigidbody2D rd;
    bool isWalking, isSearching, isAttack = false;
    float waitTime = 0.5f, walkTime = 2f, walkCounter, waitCounter, waitToRush = 1f, turnToSearching = 1f;
    int walkDirection;
    Vector3 reachPosition;
    private void Start()
    {
        virus = new VirusBoss2();
        setNumeral();
        gameObject.GetComponent<Virus_Numeral>().settingNumeral(virus);
        rd = GetComponent<Rigidbody2D>();
        isWalking = false;
        waitCounter = waitTime;
        walkCounter = walkTime;
        ChooseDirection();
        isSearching = true;
    }
    public void setNumeral()
    {
        maxHP = virus.returnSPD();
        currentHP = maxHP;
        maxHPsize = HealthBar.transform.localScale.x;
    }

    private void Update()
    {
        updateHP();
        if (isSearching)
        {
            Movement();
            searchForAttack();
        }
        else
        {
            Attack();
        }
    }

    void updateHP()
    {
        if (virus.isDead)
        {
            virus.reviveLife();
            maxHP = virus.returnSPD();
            currentHP = maxHP;
            //Instantiate(gameObject, transform.position, Quaternion.identity);
        }
        HealthBar.transform.localScale = new Vector3((virus.returnSPD() / maxHP) * maxHPsize, HealthBar.transform.transform.localScale.y, HealthBar.transform.transform.localScale.z);
    }
    void searchForAttack()
    {
        Transform targetPlayer = GameObject.FindWithTag("Player").transform;
        if (Vector3.Distance(transform.position, targetPlayer.position) < 700f)
        {
            Debug.Log("Found");
            reachPosition = targetPlayer.position;
            isSearching = false;
            rd.velocity = Vector2.zero;
            isAttack = false;
        };
    }
    void Attack()
    {
        waitToRush -= Time.deltaTime;
        if (waitToRush <= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, reachPosition, 1000f * Time.deltaTime);
            isAttack = true;
        }
        if (isAttack)
        {
            turnToSearching -= Time.deltaTime;
            if (turnToSearching <= 0)
            {
                turnToSearching = 0.5f;
                isSearching = true;
                waitToRush = 1.5f;
            }
        }
    }

    void Movement()
    {
        if (isWalking)
        {
            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    rd.velocity = new Vector2(0, virus.returnSPD());
                    break;
                case 1:
                    rd.velocity = new Vector2(virus.returnSPD(), 0);
                    break;
                case 2:
                    rd.velocity = new Vector2(0, -virus.returnSPD());
                    break;
                case 3:
                    rd.velocity = new Vector2(-virus.returnSPD(), 0);
                    break;
            }
            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            rd.velocity = Vector2.zero;
            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }
    }
    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            virus.getDamage(other.gameObject.GetComponent<Bullet>().dameGiven);
        }
    }
}
