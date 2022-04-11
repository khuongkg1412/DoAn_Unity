using UnityEngine;
using UnityEngine.UI;
public class SafeArea : MonoBehaviour
{
    public GameObject canvas;
    RectTransform rectTransform;
    Rect safeArea;
    Vector2 minAnchor;
    Vector2 maxAnchor;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        safeArea = Screen.safeArea;
        minAnchor = safeArea.position;
        maxAnchor = minAnchor + safeArea.size;

        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        rectTransform.anchorMin = minAnchor;
        rectTransform.anchorMax = maxAnchor;
        //Check screen width size greater than 1500 on portrait orientation
        if (Screen.width > 1500 && (Screen.orientation == ScreenOrientation.Portrait))
        {
            canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1100, 600);
        }
        else
        {
            canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(800, 600);
        }

    }



}
