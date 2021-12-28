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
            if (waiToFolllow >= 0.5f)
            {
                waiToFolllow = 0;
                //Stop Enemy Moving After Beeing Hit
                Rigidbody2D rd = gameObject.GetComponent<Rigidbody2D>();
                rd.velocity = Vector2.zero;
                //Allow it follow player or Citizen
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
                other.gameObject.GetComponent<Citizen_Helping>().isSicked = true;
            }
        }
    }
}
