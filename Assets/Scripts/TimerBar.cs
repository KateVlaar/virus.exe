using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.UI;

public class TimerBar : FillBar
{
    [SerializeField] private Slider slider;

//    [SerializeField] private Slider closeSlider;
   // Event to call when timer times out
   private UnityEvent onTimerComplete;

    private void Awake()
    {
        if (slider == null)
        {
            slider = transform.parent.GetComponent<Slider>();
        }
    }

   // Create a property to handle the slider's value
   public new float CurrentValue {
       get {
           return base.CurrentValue;
       }
       set {
           // If value exceeds timer value, invoke the time out function
           if (value >= slider.maxValue)
              onTimerComplete.Invoke();

           // Remove overfill
           base.CurrentValue = value % slider.maxValue;
           //Only if displaying the remaining time in countdown as text
       }
   }
   
    // Start is called before the first frame update
    void Start()
    {
        // if (onTimerComplete == null)
        //     onTimerComplete = new UnityEvent();
        onTimerComplete.AddListener(OnTimerComplete);        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentValue += 0.0153f;
    }

    void OnTimerComplete() {
        Debug.Log("Timer Complete");
        slider.parent.Find("DraggableWindow").SendMessage("CloseWindow", adWindowCanvas);
    }
}
