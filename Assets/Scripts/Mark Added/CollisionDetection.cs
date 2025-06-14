using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    CombinedPlayerHealth combinedPlayerHealthScript;
    [SerializeField] string asteroidTagToDetect = "Asteroid";

    private void Awake()
    {
        combinedPlayerHealthScript = GameObject.FindAnyObjectByType<CombinedPlayerHealth>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(asteroidTagToDetect))
        {
            combinedPlayerHealthScript.DeductLife(gameObject);
        }
    }

}
