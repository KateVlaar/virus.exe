using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableWindow : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform dragWindow;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        if (dragWindow == null)
        {
            dragWindow = transform.parent.GetComponent<RectTransform>();
        }

        if (closeButton == null)
        {
            closeButton = transform.GetComponentInChildren<Button>();
            closeButton.onClick.AddListener(CloseWindow);
        }
    }

    public void setCanvas(Canvas canvas)
    {
        _canvas = canvas;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragWindow.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragWindow.SetAsLastSibling();
    }

    void CloseWindow()
    {
        Destroy(this);
        Destroy(this.transform.parent.gameObject);
    }
}
