using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
 //Player target to enemy move forward
    public Transform target;

    //Distance from player to enemy

    //public Vector3 distancePlayer = new Vector3(10,10,0);
    //The orginal position of enemy, after out of range with player, enemy would comeback here
    [SerializeField]
    Transform originalPos;

    //Speed to Move
    [SerializeField]
    private float speed = 50f;

    //Range to Move
    [SerializeField]
    private float range = 900f;

    //Decide whether enemy is following player
    public bool isFollow = true;

    //Time before enemy continute follow player after a collision
    float waiToFolllow = 0.5f;

    //Dame enemy give to player
    public float dameGiven = 10f;

    private void Update()
    {
        //Stop virus follow player for a secend
        if (!isFollow)
        {
            //Count to follow

            waiToFolllow -= Time.deltaTime;
            if (waiToFolllow <= 0)
            {
                isFollow = true;
            }
        }

        //Follow if in range
        if (
            Vector3.Distance(target.position, transform.position) <= range &&
            isFollow
        )
        {
            followPlayer();
        }
        else //Out range then comeback to home position
        if (Vector3.Distance(target.position, transform.position) >= range)
        {
            comeBackPos();
        }
    }

    //Comeback home position
    public void comeBackPos()
    {
        transform.position =
            Vector3
                .MoveTowards(transform.position,
                originalPos.position,
                speed * Time.deltaTime);

        // //Make Enemy Follow Player After Comeback to Position
        // if (transform.position == originalPos.position)
        // {
        //     isFollow = true;
        // }
    }

    //Following p;ayer
    public void followPlayer()
    {
        transform.position =
            Vector3
                .MoveTowards(transform.position,
                target.position,
                speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isFollow = false;
        if (other.gameObject.tag == "Bullet")
        {
            Destroy (gameObject);
        }
        else if (other.gameObject.tag == "Player")
        {
            //Hit the Player
            Rigidbody2D rd =  gameObject.GetComponent<Rigidbody2D>();
            rd.AddForce(gameObject.transform.position,ForceMode2D.Impulse);
        }
        // else if(other.gameObject.tag == "Player"){
        //     Debug.Log("ComeBack");
        //     // Renderer render =
        //     //     other.gameObject.GetComponentInChildren<Renderer>();
        //     //     Debug.Log("Enable: "+ render.enabled);
        //     // if(render.enabled == false){
        //         comeBackPos();
        //     // }
        // }
    }
}
