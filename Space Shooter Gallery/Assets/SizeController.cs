using UnityEngine;

public class SizeController : MonoBehaviour {

    public GameObject canvas;

    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (canvas != null && rectTransform != null)
        {
            RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
            if (canvasRectTransform != null)
            {
                rectTransform.sizeDelta = new Vector2(canvasRectTransform.sizeDelta.x, canvasRectTransform.sizeDelta.y);
            }
        }
    }
}
