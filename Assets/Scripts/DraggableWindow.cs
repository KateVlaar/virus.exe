using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableWindow : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform dragWindow;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        if (dragWindow == null)
        {
            dragWindow = transform.parent.GetComponent<RectTransform>();
        }

        if (canvas == null)
        {
            Transform testCanvasTransform = transform.parent;
            while (testCanvasTransform != null)
            {
                canvas = testCanvasTransform.GetComponent<Canvas>();
                if (canvas != null)
                {
                    break;
                }
                testCanvasTransform = testCanvasTransform.parent;
            }
        }

        if (closeButton == null)
        {
            closeButton = transform.GetComponentInChildren<Button>();
            closeButton.onClick.AddListener(CloseWindow);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragWindow.anchoredPosition += eventData.delta / canvas.scaleFactor;
        var x = Mathf.Clamp(dragWindow.anchoredPosition.x, ((Screen.width/2) * -1) + (dragWindow.rect.width) / 2, (Screen.width/2) - (dragWindow.rect.width / 2));
        var y = Mathf.Clamp(dragWindow.anchoredPosition.y, ((Screen.height/2) * -1) + (dragWindow.rect.height) / 2, (Screen.height/2) - (dragWindow.rect.height / 2));

        dragWindow.anchoredPosition = new Vector2(x, y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragWindow.SetAsLastSibling();
    }

    void CloseWindow()
    {
        Destroy(this);
        Destroy(transform.parent.gameObject);
    }
}
