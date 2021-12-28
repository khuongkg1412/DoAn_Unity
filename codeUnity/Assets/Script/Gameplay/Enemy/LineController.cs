
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LineController : MonoBehaviour
{
    //[Range(0, 50)]
    public int segments = 50;
    //[Range(0, 5)]
    public float xradius = 300;
    //[Range(0, 5)]
    public float yradius = 300;
    LineRenderer line;

    [System.Obsolete]
    void Start()
    {
        //Get the LineRenderer component
        line = gameObject.GetComponent<LineRenderer>();

        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        CreatePoints();
    }

    void CreatePoints()
    {
        float x;
        float y;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }
}
