using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HideTutorial : MonoBehaviour, IClickable
{
    // Start is called before the first frame update
    private bool tutorialIsOpen = false;
    void Start()
    {
        GetComponent<Renderer>().enabled = tutorialIsOpen;
        GetComponent<Renderer>().GetComponentInChildren<Button>().onClick.AddListener(Click);
        
        var UIHierarchyButton = GetComponent<Renderer>().transform.parent.GetComponentsInChildren<Button>();
        var tutOpenButton = UIHierarchyButton.Where(k => k.transform.name == "TutorialOpenButton").FirstOrDefault();
        
        tutOpenButton.onClick.AddListener(Click);

        if (!tutorialIsOpen)
        {
            GetComponent<Renderer>().GetComponentInChildren<Button>().onClick.RemoveListener(Click);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        Debug.Log("Bruh");
        tutorialIsOpen = !tutorialIsOpen;
        GetComponent<Renderer>().enabled = tutorialIsOpen;
    }
}
