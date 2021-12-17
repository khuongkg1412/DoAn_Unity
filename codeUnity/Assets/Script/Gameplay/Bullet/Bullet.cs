using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float boundX;

    private float boundY;

    public float dameGiven;

    public GameObject hitEffect;
    private void Start()
    {
        dameGiven = 10f;
        boundX = GameObject.Find("Player").GetComponent<Boundary>().getBoundX();
        boundY = GameObject.Find("Player").GetComponent<Boundary>().getBoundY();
    }

    private void Update()
    {
        if(transform.position.x < - boundX || transform.position.y < - boundY ||
           transform.position.x >   boundX || transform.position.y >   boundY ){
            Destroy (gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        effect.transform.position = new Vector3(effect.transform.position.x,effect.transform.position.y, 1);
        Destroy(effect,0.5f);
        Destroy (gameObject);
    }
}
