using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Image joystickBackground;

    private Image joystickTouch;

    [HideInInspector]
    public Vector3 InputDir;

    [HideInInspector]
    public bool shoot;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Vector2.zero;
        if (
            RectTransformUtility
                .ScreenPointToLocalPointInRectangle(joystickBackground
                    .rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out position)
        )
        {
            position.x =
                (position.x / joystickBackground.rectTransform.sizeDelta.x);
            position.y =
                (position.y / joystickBackground.rectTransform.sizeDelta.y);

            float x =
                (joystickBackground.rectTransform.pivot.x == 1f)
                    ? position.x * 2 + 1
                    : position.x * 2 - 1;
            float y =
                (joystickBackground.rectTransform.pivot.x == 1f)
                    ? position.y * 2 + 1
                    : position.y * 2 - 1;

            InputDir = new Vector3(x, y, 0);
            InputDir =
                (InputDir.magnitude > 1) ? InputDir.normalized : InputDir;
        }

        joystickTouch.rectTransform.anchoredPosition =
            new Vector3(InputDir.x *
                (joystickBackground.rectTransform.sizeDelta.x / 2.5f),
                InputDir.y *
                (joystickBackground.rectTransform.sizeDelta.y / 2.5f));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag (eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputDir = Vector3.zero;
        joystickTouch.rectTransform.anchoredPosition = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        joystickBackground = GetComponent<Image>();
        joystickTouch = transform.GetChild(0).GetComponent<Image>();
        InputDir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
