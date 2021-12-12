using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float boundX;

    private float boundY;

    public float dameGiven;

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
        Destroy (gameObject);
    }
}
