using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy (gameObject);
    }

    private void FixedUpdate()
    {
        if (
            gameObject.transform.position.x > 580f ||
            gameObject.transform.position.x < -580f
        )
        {
            Destroy (gameObject);
        }
        else if (
            gameObject.transform.position.y > 1020f ||
            gameObject.transform.position.y < -1020f
        )
        {
            Destroy (gameObject);
        }
    }
}
