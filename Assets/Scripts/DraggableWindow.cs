using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableWindow : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform dragWindow;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button closeButton;
    [SerializeField] private Slider slider;
    public enum windowTypes
    {
        Normal,
        Glitch,
        Slow,
        Ice,
        Fire
    }

    public bool timerUp = false;

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

        if (slider == null)
        {
            slider = transform.parent.GetComponentInChildren<Slider>();

            TimerBar t = slider.GetComponent<TimerBar>();

            t.onTimerComplete.AddListener(TimerComplete);
        }
    }

    void Update()
    {
    }

    public void setCanvas(Canvas canvas)
    {
        _canvas = canvas;
    }
    
    public void setWindowType(float difficulty)
    {
        windowTypes windowType;
        //if (Random.value > 0.1)
        if (false)
        {
            windowType = windowTypes.Normal;
        }
        else
        {
            windowType = (windowTypes)Random.Range(1, 4);
        }
        switch (windowType)
        {
            case windowTypes.Normal:
                GetComponent<Image>().color = new Color32(255,255,255,100);
                break;
            case windowTypes.Glitch:
                GetComponent<Image>().color = new Color32(203,66,245,100);
                break;
            case windowTypes.Slow:
                GetComponent<Image>().color = new Color32(245, 230, 66,100);
                break;
            case windowTypes.Ice:
                GetComponent<Image>().color = new Color32(31, 179, 237,100);
                break;
            case windowTypes.Fire:
                GetComponent<Image>().color = new Color32(245, 34, 55,100);
                break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (timerUp)
        {
            return;
        }

        dragWindow.anchoredPosition += eventData.delta / _canvas.scaleFactor;

        var UIHierarchyParent = transform.parent.parent.parent.GetComponentsInChildren<Transform>();
        var TaskBarPanel = UIHierarchyParent.Where(k => k.transform.name == "Taskbar").FirstOrDefault();
        float TaskBarHeight = TaskBarPanel.localScale.y;

        var x = Mathf.Clamp(dragWindow.anchoredPosition.x, ((Screen.width/2) * -1) + (dragWindow.rect.width / 2), (Screen.width/2) - (dragWindow.rect.width / 2));
        var y = Mathf.Clamp(dragWindow.anchoredPosition.y, ((Screen.height/2) * -1) + (dragWindow.rect.height / 2) + TaskBarHeight, (Screen.height/2) - (dragWindow.rect.height / 2));

        dragWindow.anchoredPosition = new Vector2(x, y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragWindow.SetAsLastSibling();
    }

    void CloseWindow()
    {
        GameObject.Find("PopupManager").SendMessage("DecrementOpenCounter");
        GameObject.Find("PopupManager").SendMessage("IncrementClosedCounter");
        Destroy(this);
        Destroy(transform.parent.gameObject);
    }

    void TimerComplete()
    {
        closeButton.onClick.RemoveAllListeners();
        timerUp = true;
    }
}


