using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float boundX = 2085f;

    private float boundY = 2185f;

    public float dameGiven;

    private Transform positionStartShooting;

    private float rangeShooting;

    public GameObject hitEffect;
    private void Start()
    {
        rangeShooting = 200f;
        dameGiven = 10f;
    }

    private void Update()
    {
        checkBoundary();
        checkDistanceShooting();
    }

    private void checkBoundary()
    {
        if (transform.position.x < -boundX || transform.position.y < -boundY ||
           transform.position.x > boundX || transform.position.y > boundY)
        {
            Destroy(gameObject);
        }
    }

    private void bulletDistroy()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        effect.transform.position = new Vector3(effect.transform.position.x, effect.transform.position.y, 1);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
    }
    public void setPositionStartShooting(Transform position)
    {
        positionStartShooting = position;
    }

    public void checkDistanceShooting()
    {
        if (Vector2.Distance(positionStartShooting.position, transform.position) > rangeShooting)
        {
            bulletDistroy();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        bulletDistroy();
    }
}
