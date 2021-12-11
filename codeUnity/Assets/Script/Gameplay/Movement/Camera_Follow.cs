using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform followTransform;

    public BoxCollider2D mapBounds;

    private float xMin,
            xMax,
            yMin,
            yMax;

    private float camY,
            camX;

    private float camOrthsize;

    private float cameraRatio;

    private Camera mainCam;

    private void Start()
    {
        xMin = mapBounds.bounds.min.x;
        xMax = mapBounds.bounds.max.x;
        yMin = mapBounds.bounds.min.y;
        yMax = mapBounds.bounds.max.y;
        mainCam = GetComponent<Camera>();
        camOrthsize = mainCam.orthographicSize;
        cameraRatio = camOrthsize * mainCam.aspect;
        //cameraRatio = 1000;
        Debug.Log("Bound X Max-" + xMax + " minX-" +xMin);
        Debug.Log("Bound Y Max-" + yMax + " minY-" +yMin);
        Debug.Log("cameraRatio "+ cameraRatio);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        camY =
            Mathf
                .Clamp(followTransform.position.y,
                yMin + camOrthsize,
                yMax - camOrthsize);
        camX =
            Mathf
                .Clamp(followTransform.position.x,
                xMin + cameraRatio,
                xMax - cameraRatio);
        this.transform.position =
            new Vector3(camX, camY, this.transform.position.z);
    }
}
