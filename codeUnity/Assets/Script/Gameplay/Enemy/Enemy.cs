using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Player target to enemy move forward
    public Transform target, target2;

    //Distance from player to enemy
    //public Vector3 distancePlayer = new Vector3(10,10,0);
    //The orginal position of enemy, after out of range with player, enemy would comeback here
    [SerializeField]
    Transform originalPos;

    //Speed to Move
    private float speed = 200f;

    //Range to Move
    private float range = 300f;

    //Decide whether enemy is following player
    public bool isFollow = true;

    //Time before enemy continute follow player after a collision
    float waiToFolllow = 0f;

    //Dame enemy give to player
    public float dameGiven = 10f;

    private void Update()
    {
        //Stop virus follow player for a secend
        if (!isFollow)
        {
            //Count to follow
            waiToFolllow += Time.deltaTime;
            if (waiToFolllow >= 1f)
            {
                waiToFolllow = 0;
                isFollow = true;
            }
        }
        else
        {
            //Follow if in range
            if (distanceToPlayer())
            {
                followPlayer();
            }
            else if (distanceToCitizen())
            {
                followCitizen();
            }
            //Out range then comeback to home position
            else
            {
                comeBackPos();
            }
        }

    }

    bool distanceToPlayer()
    {
        if (Vector3.Distance(target.position, transform.position) <= range)
        {
            return true;
        }
        return false;
    }

    bool distanceToCitizen()
    {
        if (Vector3.Distance(target2.position, transform.position) <= range)
        {
            return true;
        }
        return false;
    }
    //Comeback home position
    public void comeBackPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, originalPos.position, speed * Time.deltaTime);
    }

    //Following p;ayer
    public void followPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void followCitizen()
    {
        transform.position = Vector3.MoveTowards(transform.position, target2.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isFollow = false;
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Citizen")
        {
            //Hit the Player
            Rigidbody2D rd = gameObject.GetComponent<Rigidbody2D>();
            rd.AddForce(gameObject.transform.position, ForceMode2D.Impulse);

            if (other.gameObject.tag == "Citizen")
            {
                other.gameObject.GetComponent<Citizen_Helping>().isSicked = true;
            }
        }
    }
}
