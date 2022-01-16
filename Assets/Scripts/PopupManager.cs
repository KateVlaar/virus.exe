using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private float spawnCountdown; // The elapsed time
    
    public GameObject adWindow;
    public Canvas adWindowCanvas;

    public float spawnFrequency; // Number of game ticks to spawn windows
    public int openCounter;
    public int closedCounter;
    public float difficulty; // From 0.0 (easiest) to 0.9 (hardest)

    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        spawnCountdown = spawnFrequency; // Preset spawn timer to desired amount
        openCounter = 0;
        closedCounter = 30; // TODO: Change from 30 to 0 (this was for debugging)
        difficulty = 0.0f;
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
                IncrementOpenCounter();
            }
        }
    }

    void spawnAds(bool shouldSpawn)
    {
        _shouldSpawn = shouldSpawn;
    }

    void SpawnWindow()
    {
        float widthBuffer = 50.0f; // width is not wide enough for some reason
        RectTransform adWindowTransform = (RectTransform)adWindow.transform;

        var UIHierarchyParent = adWindowCanvas.transform.parent.GetComponentsInChildren<Transform>();
        var TaskBarPanel = UIHierarchyParent.Where(k => k.transform.name == "Taskbar").FirstOrDefault();
        float TaskBarHeight = TaskBarPanel.localScale.y;

        float minX = ((Screen.width / 2) * -1) + (Mathf.Abs(adWindowTransform.rect.width) / 2) - widthBuffer;
        float maxX = (Screen.width / 2) - (Mathf.Abs(adWindowTransform.rect.width) / 2) + widthBuffer;
        float minY = ((Screen.height / 2) * -1) + (Mathf.Abs(adWindowTransform.rect.height) / 2) + TaskBarHeight;
        float maxY = (Screen.height / 2) - (Mathf.Abs(adWindowTransform.rect.height) / 2);

        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject newAdWindow = Instantiate(adWindow, randomPosition, quaternion.identity);
        newAdWindow.transform.SetParent(adWindowCanvas.transform, false);
        newAdWindow.transform.Find("TitleBar").SendMessage("setCanvas", adWindowCanvas);
        newAdWindow.transform.Find("TitleBar").SendMessage("setWindowType", difficulty);
        spawnCountdown = spawnFrequency;
    }

    // Increment open window counter when window is opened
    public void IncrementOpenCounter()
    {
        openCounter++;
        CheckOpenWindowCount(openCounter);
    }
    // Increment closed window counter when window is closed
    public void IncrementClosedCounter()
    {
        closedCounter++;
        CheckClosedWindowCount(closedCounter);
    }
    
    // Increment difficulty according to given parameter
    public void IncrementDifficulty(float newDifficulty, float newSpawnFrequency)
    {
        difficulty = newDifficulty;
        spawnFrequency = newSpawnFrequency;
    }

    // Decrement open window counter when window is closed
    public void DecrementOpenCounter()
    {
        openCounter--;
    }
    
    // Check how many windows are open - if too many are up, end the game
    public void CheckOpenWindowCount(int openCounter)
    {
        if (openCounter >= 10)
        {
            _shouldSpawn = false;
        }
    }
    
    // Check how many windows have been closed - adjust the difficulty as necessary
    public void CheckClosedWindowCount(int closedCounter)
    {
        if (closedCounter > 10)
        {
            IncrementDifficulty(0.1f, 4.0f);
        }
        if (closedCounter > 20)
        {
            IncrementDifficulty(0.2f, 4.0f);
        }
        if (closedCounter > 30)
        {
            IncrementDifficulty(0.3f, 3.0f);
        }
        if (closedCounter > 40)
        {
            IncrementDifficulty(0.4f, 3.0f);
        }
        if (closedCounter > 50)
        {
            IncrementDifficulty(0.5f, 2.0f);
        }
        if (closedCounter > 60)
        {
            IncrementDifficulty(0.6f, 2.0f);
        }
        if (closedCounter > 70)
        {
            IncrementDifficulty(0.7f, 1.0f);
        }
        if (closedCounter > 80)
        {
            IncrementDifficulty(0.8f, 1.0f);
        }
        if (closedCounter > 90)
        {
            IncrementDifficulty(0.9f, 1.0f);
        }
    }
}
