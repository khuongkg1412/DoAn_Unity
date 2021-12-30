
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LineController : MonoBehaviour
{
    [Range(0, 500)]
    int segments = 500;
    [Range(0, 100)]
    float xradius = 100;
    [Range(0, 100)]
    float yradius = 100;
    LineRenderer line;

    [System.Obsolete]
    void Start()
    {
        //Get the LineRenderer component
        line = gameObject.GetComponent<LineRenderer>();

        Color c1 = new Color(1f, 0f, 0f, 0.75f);
        line.SetColors(c1, c1);
        line.SetWidth(5f, 5f);
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
