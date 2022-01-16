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
    public int difficulty; // From 1 (easiest) to 10 (hardest)

    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        spawnCountdown = spawnFrequency; // Preset spawn timer to desired amount
        openCounter = 0;
        closedCounter = 0;
        difficulty = 1;
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
        spawnCountdown = spawnFrequency;
    }

    // Increment open window counter when window is opened
    public void IncrementOpenCounter()
    {
        openCounter++;
        CheckWindowCount(openCounter);
    }
    // Increment closed window counter when window is closed
    public void IncrementClosedCounter()
    {
        closedCounter++;
    }
    
    // Increment difficulty when window is closed
    public void IncrementDifficulty()
    {
        difficulty++;
    }

    // Decrement open window counter when window is closed
    public void DecrementOpenCounter()
    {
        openCounter--;
    }
    
    // Check how many windows are open - if too many are up, end the game
    public void CheckWindowCount(int openCounter)
    {
        if (openCounter >= 10)
        {
            _shouldSpawn = false;
        }
    }
}
