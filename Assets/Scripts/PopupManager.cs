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
    public float spawnFrequency; // Number of game ticks to spawn windows
    private float spawnCountdown; // The elapsed time
    public GameObject adWindow;
    public Canvas adWindowCanvas;

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
}
