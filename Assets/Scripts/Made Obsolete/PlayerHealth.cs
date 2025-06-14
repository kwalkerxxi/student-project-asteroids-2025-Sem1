using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] string asteroidTagToDetect = "Asteroid";
    public int livesCount = 5;
    public TextMeshProUGUI textMeshProHealthNumber;
    public float damageDelay = 1.0f;
    private bool tempinvincible = false;
    private bool cheatInvincible = false;
    public GameObject pauseButton;
    InputAction invincibilityAction;
    [SerializeField] GameObject hitParticle;
    [SerializeField] float particleLifetime = 0.5f;
    [SerializeField] float belowPlayerPosition = -2f;

    [SerializeField] GameObject gameOverScreen;

    private void Start()
    {
        invincibilityAction = InputSystem.actions.FindAction("Invincibility");
    }

    void Update()
    {
        if (invincibilityAction.WasPressedThisFrame())
        {
            cheatInvincible = !cheatInvincible;
            Debug.Log("Invincibility: " + cheatInvincible);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(asteroidTagToDetect))
        {
            DeductLife();
        }
    }

    void DeductLife()
    {
        if (tempinvincible == false & cheatInvincible == false)
        {
            Debug.Log("Hit!" + livesCount);

            livesCount -= 1;

            TemporaryInvincibility();
            TriggerHitParticle();

            if (livesCount == 0)
            {
                Debug.Log("Game Over");
                CallGameOver();
            }
        }
    }
    void TemporaryInvincibility()
    {
        if (tempinvincible) return;
        tempinvincible = true;
        StartCoroutine(DamageDelay());
    }
    void TriggerHitParticle()
    {
        if (hitParticle != null)
        {
            GameObject particle = Instantiate(hitParticle, transform.position, Quaternion.identity);
            particle.transform.SetParent(transform);
            particle.transform.localPosition = new Vector3(0, belowPlayerPosition, 0);
            Destroy(particle, particleLifetime);
        }
    }

    void UpdateLivesText()
    {
        textMeshProHealthNumber.text = livesCount.ToString();
    }

    private IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(damageDelay);
        tempinvincible = false;
    }

    public HighScore highScoreScript; //Added for the highscore script to update/check score - [Added by Sharnez - HighScoreScript]
    public OffScreenSpawner scoreScript; //Added for the score part of the script to get final score - [Added by Sharnez - HighScoreScript]
    void CallGameOver()
    {
        highScoreScript.CheckHighScore(scoreScript.GetScore()); //Added for the highscore script
                                                       //which checks if player got new highscore - [Added by Sharnez - HighScoreScript]
        gameOverScreen.SetActive(true);
        pauseButton.SetActive(false);
        Destroy(GameObject.FindWithTag("Player"));
    }
}
