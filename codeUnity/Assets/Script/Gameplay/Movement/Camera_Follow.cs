using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform followTransform;

    public BoxCollider2D mapBounds;

    private float

            xMin,
            xMax,
            yMin,
            yMax;

    private float

            camY,
            camX;

    private float camOrthsize;

    private float cameraRatio;

    private Camera mainCam;

    private Vector3 smoothPos;

    public float smoothSpeed = 0.5f;

    public float offSetX, offSetY;

    private void Start()
    {
        CameraSetting();
    }

    public void CameraSetting()
    {
        xMin = mapBounds.bounds.min.x;
        xMax = mapBounds.bounds.max.x;
        yMin = mapBounds.bounds.min.y;
        yMax = mapBounds.bounds.max.y;
        mainCam = GetComponent<Camera>();
        camOrthsize = mainCam.orthographicSize;
        cameraRatio = camOrthsize * mainCam.aspect;
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
        smoothPos =
            Vector3
                .Lerp(this.transform.position,
                new Vector3(camX + offSetX, camY + offSetY, this.transform.position.z),
                smoothSpeed);
        this.transform.position = smoothPos;
    }
}
