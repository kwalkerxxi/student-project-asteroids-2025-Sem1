using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class PlayerJoinHandler : MonoBehaviour
{
    // Reference to PlayerInputManager
    public PlayerInputManager playerInputManager;

    static public int playerIndex = -1;

    [SerializeField] private Color[] playerColors = new Color[] { Color.white, Color.yellow, Color.cyan, Color.green };


    [SerializeField] private GameObject playerJoinDisplay;
    GameObject dynamicJoinText;

    [SerializeField] private SplineContainer splineContainer;

    private GameObject playerHolder;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();

        dynamicJoinText = Instantiate(playerJoinDisplay);

        playerHolder = new GameObject("Players Spawned");
    }

    #region Subscription and Unsubscription when using "Invoke C Sharp Events" as its notification behaviour
    //*********************************************************************************************************************
    /// <summary>
    /// Use these if <see cref="PlayerInputManager"/> is using "Invoke C Sharp Events" as its notification behaviour
    /// </summary>
    private void OnEnable()
    {
        playerInputManager.onPlayerLeft += OnPlayerJoined;
        playerInputManager.onPlayerLeft += OnPlayerLeft;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerLeft -= OnPlayerJoined;
        playerInputManager.onPlayerLeft -= OnPlayerLeft;
    }
    //*********************************************************************************************************************
    #endregion

    /// <summary>
    /// Automatically works if <see cref="PlayerInputManager"/> is using "Send Messages" as its notification behaviour
    /// </summary>
    /// <param name="playerInput"></param>
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        //Debug.Log("Player joined: " + playerInput.playerIndex);
        playerIndex++;

        playerInput.transform.position = splineContainer.EvaluatePosition(0, UnityEngine.Random.value);
        playerInput.transform.SetParent(playerHolder.transform);
        SetJoinTextVisibility();

        SetOutlineColourOnPlayer(playerInput);


        CombinedPlayerHealth combinedPlayerHealth = GameObject.FindAnyObjectByType<CombinedPlayerHealth>();
        combinedPlayerHealth.AddToMaximumLives();
        combinedPlayerHealth.AddLife();
    }

    /// <summary>
    /// Automatically works if <see cref="PlayerInputManager"/> is using "Send Messages" as its notification behaviour
    /// </summary>
    /// <param name="playerInput"></param>
    private void OnPlayerLeft(PlayerInput playerInput)
    {
        //Debug.Log("Removed Player");
        playerIndex--;
    }

    public void ResetPlayerIndex()
    {
        playerIndex = -1;
    }

    private void SetOutlineColourOnPlayer(PlayerInput playerInput)
    {
        Color colorToAssign = Color.white;
        if(playerIndex < playerColors.Length)
        {
            colorToAssign = playerColors[playerIndex];
        }
        else
        {
            colorToAssign = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f);
        }

        playerInput.GetComponentInChildren<Outline>().OutlineColor = colorToAssign;
        
        //Deprecated technique
        //playerInput.GetComponentInChildren<ParticleSystem>().startColor = colorToAssign;

        ParticleSystem particleSystem = playerInput.GetComponentInChildren<ParticleSystem>();

        // Access the MainModule and set the startColor
        ParticleSystem.MainModule mainModule = particleSystem.main;
        mainModule.startColor = colorToAssign;

        ParticleSystemRenderer renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        Material particleMaterial = renderer.material;
        particleMaterial.SetColor("_EmissionColor", colorToAssign);
        particleMaterial.EnableKeyword("_EMISSION");
    }

    private void SetJoinTextVisibility()
    {
        if(playerIndex >= 0)
        {
            dynamicJoinText.SetActive(false);
        }
        else
        {
            dynamicJoinText.SetActive(true);
        }
    }


}
