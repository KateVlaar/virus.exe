using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWindow : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform dragWindow;
    [SerializeField] private Canvas canvas;

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
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging!");
        dragWindow.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragWindow.SetAsLastSibling();
    }
}
