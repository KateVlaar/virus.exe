using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    // UI References
    // Start is called before the first frame update
    public Slider slider;
    // Do we need a counter in the Fillbar? public Text displayText;

    // Create property to handle the slider's value
    private float currentValue = 0f;
    public float CurrentValue {
        get {
            return currentValue;
        }
        set {
            currentValue = value;
            slider.value = currentValue;
            //displayText.text = (slider.value * 100).ToString("0.00") + "%";
            //Displays the percent to completion
            //If used, need to be changed to remaining time
        }
    }

    // Init current value
    void Start()
    {
       //  CurrentValue = 0f;
    }

    // Update is called once per frame
    void Update()
    {
       //CurrentValue += 0.0043f;
       // Debugging the incrementing of fill bar
    }
}
