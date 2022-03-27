using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lr_Testing : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] private Transform[] points;
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void setUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }

    private void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }
}