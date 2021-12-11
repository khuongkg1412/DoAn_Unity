using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float dameGiven;

    private void Start() {
        dameGiven = 10f;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy (gameObject);
    }
}
