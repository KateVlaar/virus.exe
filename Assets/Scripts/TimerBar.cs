using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimerBar : FillBar
{
   // Event to call when timer times out
   public UnityEvent onTimerComplete;
   public bool timerUp = false;

   // Create a property to handle the slider's value
   public new float CurrentValue {
       get {
           return base.CurrentValue;
       }
       set {
            if (slider == null)
            {
                slider = transform.parent.GetComponentInChildren<Slider>();

            }// If value exceeds timer value, invoke the time out function
                if (slider && value >= slider.maxValue && !timerUp)
            {
                onTimerComplete.Invoke();
                base.CurrentValue = slider.maxValue;
                timerUp = true;
            }
            else if (slider)
            {
                // Remove overfill
                base.CurrentValue = value % slider.maxValue;
            }

           //Only if displaying the remaining time in countdown as text
       }
   }
   
    // Start is called before the first frame update
    void Start()
    {
        if (onTimerComplete == null)
            onTimerComplete = new UnityEvent();
        onTimerComplete.AddListener(OnTimerComplete);        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentValue += 0.00153f;
    }

    public void OnTimerComplete() {
        Debug.Log("Timer Complete");
        Destroy(this);
    }
}
