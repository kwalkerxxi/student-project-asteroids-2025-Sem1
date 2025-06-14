using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
/// <summary>
/// Spawns items outside the screen edges and moves them toward the center, with a gradually increasing spawn rate.
/// </summary>
public class OffScreenSpawner : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Camera mainCamera;
  
    [SerializeField] private float initialSpawnDelay = 1f;
    private float currentSpawnDelay;

    [SerializeField] private float minimumSpawnDelay = 0.2f;
    [SerializeField] private float spawnAcceleration = 0.01f;

    [SerializeField] private int maximumItemCountAllowed = 10;
    private int currentItemCount = 0;

    private float spawnTimer;

    private float score;

    [SerializeField] TextMeshProUGUI scoreTextBox;

    public HighScore highScoreScript; //Added for the highscore script - [Added by Sharnez - HighScore Script]

    GameObject enemyHolder;

    [Header("Frustrum Spawning Related - Unused")]
    [SerializeField] private float spawnDistanceFromCamera = 20f;
    [SerializeField] private float spawnDistanceOutsideView = 0.05f;

    public void UpdateScore(float scoreIncreaseValue)
    {
        score += scoreIncreaseValue;
        scoreTextBox.SetText("" + score);
    }


    //Below added for the highscore script and so I can access the score.This just allows for other script PlayerHealth to
    //have access to the current score - [Added by Sharnez - HighScore Script]
    public float GetScore()
    {
        return score;
    }


    private enum ScreenSide
    {
        Top,
        Bottom,
        Left,
        Right
    }

    void Start()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        enemyHolder = new GameObject("Enemies Spawned");

        //centerOfScreenPosition = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, mainCamera.nearClipPlane + spawnDistanceFromCamera));

        currentSpawnDelay = initialSpawnDelay;
    }

    void Update()
    {
        if(currentItemCount >= maximumItemCountAllowed)
        {
            return;
        }

        if(PlayerJoinHandler.playerIndex < 0)
        {
            return;
        }

        spawnTimer += Time.deltaTime;

        if(spawnTimer >= currentSpawnDelay)
        {
            spawnTimer -= spawnTimer;
            currentSpawnDelay = Mathf.Max(minimumSpawnDelay, currentSpawnDelay - spawnAcceleration);
            SpawnItemFromRandomSide();
        }
    }

    public void ReduceItemCount()
    {
        currentItemCount--;
    }

    private void SpawnItemFromRandomSide()
    {
        currentItemCount++;
        ScreenSide side = (ScreenSide)Random.Range(0, 4);
        //Vector3 screenSpawnPosition = GetSpawnPosition(side);
        //Vector3 worldSpawnPosition = mainCamera.ViewportToWorldPoint(screenSpawnPosition);
        Vector3 worldSpawnPosition = RandomPointInsideAndOutsideCameraView.GetRandomPointOutsideCameraOnPlaneAtZeroY(mainCamera);
        worldSpawnPosition.y = 0;


        GameObject item = Instantiate(itemPrefab, worldSpawnPosition, Quaternion.Euler(0, Random.Range(0, 360), 0), enemyHolder.transform);


        if(item.GetComponent<AsteroidBehaviour>())
        {
            item.GetComponent<AsteroidBehaviour>().spawnerScript = this;
        }
    }

    private Vector3 GetSpawnPosition(ScreenSide side)
    {
        switch(side)
        {
            case ScreenSide.Top:
                return new Vector3(Random.value, 1f + spawnDistanceOutsideView, mainCamera.nearClipPlane + spawnDistanceFromCamera);
            case ScreenSide.Bottom:
                return new Vector3(Random.value, -spawnDistanceOutsideView, mainCamera.nearClipPlane + spawnDistanceFromCamera);
            case ScreenSide.Left:
                return new Vector3(-spawnDistanceOutsideView, Random.value, mainCamera.nearClipPlane + spawnDistanceFromCamera);
            case ScreenSide.Right:
                return new Vector3(1f + spawnDistanceOutsideView, Random.value, mainCamera.nearClipPlane + spawnDistanceFromCamera);
            default:
                return Vector3.zero;
        }
    }
}
