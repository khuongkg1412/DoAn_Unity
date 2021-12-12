using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public Vector2 screenBounds;

    private float objectWidth;

    private float objectHeight;

    private float boundX = 2085f;

    private float boundY = 2185f;


    public Vector2 getScreenBounds(){
        return screenBounds;
    }
        public float getBoundX(){
        return boundX;
    }
        public float getBoundY(){
        return boundY;
    }
    // Use this for initialization
    void Start()
    {
        screenBounds = new Vector3( boundX, boundY, 10);
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x =
            Mathf
                .Clamp(viewPos.x,
                screenBounds.x * -1 + objectWidth,
                screenBounds.x - objectWidth);
        viewPos.y =
            Mathf
                .Clamp(viewPos.y,
                screenBounds.y * -1 + objectHeight,
                screenBounds.y - objectHeight);
        transform.position = viewPos;
    }
}
