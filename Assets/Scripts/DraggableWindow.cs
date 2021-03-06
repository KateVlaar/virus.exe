using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static PopupManager;

public class DraggableWindow : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform dragWindow;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button closeButton;
    [SerializeField] private Sprite testSprite;
    [SerializeField] private Slider slider;

    public bool timerUp = false;
    private windowTypes activeEffect = windowTypes.Slow;
    private int numIceClicks = 0;
    public Button iceButton;

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

        var UIHierarchyButton = GetComponent<Image>().transform.parent.GetComponentsInChildren<Button>();
        iceButton = UIHierarchyButton.Where(k => k.transform.name == "IceLayerButton").FirstOrDefault();
        iceButton.onClick.AddListener(BreakIce);
    }

    void Update()
    {
        if (activeEffect == windowTypes.Ice)
        {



        }
    }

    public void setCanvas(Canvas canvas)
    {
        _canvas = canvas;
    }
    
    public void setWindowType(PopupManager.windowTypes windowType)
    {
        GameObject adSpace = transform.GetChild(0).gameObject;
        //if (Random.value > 0.1)
        if (false)
        {
            windowType = windowTypes.Normal;
        }
        else
        {
            windowType = (windowTypes)Random.Range(1, 4);
        }

        var UIHierarchyParent = GetComponent<Image>().transform.GetComponentsInChildren<Image>();
        var ProgressBarFill = UIHierarchyParent.Where(k => k.transform.name == "Fill").FirstOrDefault();

        switch (windowType)
        {
            case PopupManager.windowTypes.Normal:
                GetComponent<Image>().color = new Color32(255,255,255,255);
                ProgressBarFill.color = new Color32(222, 222, 222, 255);
                activeEffect = windowTypes.Normal;
                Destroy(iceButton.gameObject);

                break;
            case PopupManager.windowTypes.Glitch:
                GetComponent<Image>().color = new Color32(203,66,245,255);
                ProgressBarFill.color = new Color32(159, 37, 196, 255);
                Destroy(iceButton.gameObject);

                SetGlitchEffect();
                break;
            case PopupManager.windowTypes.Slow:
                GetComponent<Image>().color = new Color32(245, 230, 66,255);
                ProgressBarFill.color = new Color32(209, 193, 17, 255);
                Destroy(iceButton.gameObject);

                SetSlowEffect();
                break;
            case PopupManager.windowTypes.Ice:
                GetComponent<Image>().color = new Color32(31, 179, 237,255);
                ProgressBarFill.color = new Color32(10, 142, 194, 255);
                SetIceEffect();
                break;
            case PopupManager.windowTypes.Fire:
                GetComponent<Image>().color = new Color32(245, 34, 55,255);
                ProgressBarFill.color = new Color32(181, 18, 34, 255);
                Destroy(iceButton.gameObject);

                break;
        }
    }
    
    public void setWindowSprite(Sprite windowSprite)
    {
        GameObject adSpace = transform.GetChild(0).gameObject;
        adSpace.GetComponent<Image>().sprite = windowSprite;
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
        closeButton.interactable = false;
        timerUp = true;
    }

    void SetSlowEffect()
    {
        activeEffect = windowTypes.Slow;

    }

    void SetGlitchEffect()
    {
        activeEffect = windowTypes.Glitch;

    }

    void SetIceEffect()
    {
        activeEffect = windowTypes.Ice;

        var UIHierarchyParentImage = GetComponent<Image>().transform.parent.GetComponentsInChildren<Image>();
        var IceSheetImage = UIHierarchyParentImage.Where(k => k.transform.name == "IceLayerButton").FirstOrDefault();
        IceSheetImage.color = new Color32(31, 179, 237, 191);
    }

    void BreakIce()
    {
        // ice button should maybe be a prefab and only spawn for ice windows but not enough time so all windows hit this
        if (activeEffect != windowTypes.Ice)
        {
            return;
        }

        numIceClicks++;

        var UIHierarchyParentImage = GetComponent<Image>().transform.parent.GetComponentsInChildren<Image>();
        var IceSheetImage = UIHierarchyParentImage.Where(k => k.transform.name == "IceLayerButton").FirstOrDefault();

        switch (numIceClicks)
        {
            case 1:
                IceSheetImage.color = new Color32(31, 179, 237, 127);
                break;
            case 2:
                IceSheetImage.color = new Color32(31, 179, 237, 63);
                break;
            case 3:
                IceSheetImage.color = new Color32(31, 179, 237, 0);
                Destroy(iceButton.gameObject);
                activeEffect = windowTypes.Normal;
                break;
        }
    }
}


