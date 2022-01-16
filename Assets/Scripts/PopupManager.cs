using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

///
/// Responsible for spawning virus popups
///

public class PopupManager : MonoBehaviour
{
    private bool _shouldSpawn = false;
    public float spawnFrequency = 5000.0f; // The time until the next window will spawn
    private float spawnCountdown; // The elapsed time
    public GameObject adWindow;

    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        spawnCountdown = spawnFrequency; // Preset spawn timer to desired amount
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldSpawn)
        {
            // Algorithm for spawning ads
            if (spawnCountdown > 0)
            {
                spawnCountdown -= Time.deltaTime;
            }
            else
            {
                SpawnWindow();
            }
        }
    }

    void spawnAds(bool shouldSpawn)
    {
        _shouldSpawn = shouldSpawn;
    }

    void SpawnWindow()
    {
        Vector2 randomPosition = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
        Instantiate(adWindow, randomPosition, quaternion.identity);
        spawnCountdown = spawnFrequency;
    }
}
