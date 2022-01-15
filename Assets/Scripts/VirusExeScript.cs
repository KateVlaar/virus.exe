using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Script for detecting virus.exe being clicked
///

public class VirusExeScript : MonoBehaviour, IClickable
{
    bool _gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (!_gameStarted)
        {
            /* Send message to VirusManager to start spawning virus windows */
            GameObject.Find("PopupManager").SendMessage("spawnAds", true);
            _gameStarted = true;
        }
    }
}
