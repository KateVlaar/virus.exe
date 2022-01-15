using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Responsible for spawning virus popups
///

public class PopupManager : MonoBehaviour
{
    bool _shouldSpawn = false;

    GameObject adWindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldSpawn)
        {
            // TODO: Algorithm for spawning ads
            Debug.Log("Spawn ad algo not implemented");
            _shouldSpawn = false;
        }
    }

    void spawnAds(bool shouldSpawn)
    {
        _shouldSpawn = shouldSpawn;
    }
}
