using UnityEngine;
using TMPro;


public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField] GameObject nextAsteroid;
    [SerializeField] int subAsteroidsToSpawn = 2;
    
    [SerializeField] string tagToDetect = "Projectile";
    [SerializeField] float scoreIncreaseValue = 10f;

    public OffScreenSpawner spawnerScript;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagToDetect))
        {
            Destroy(collision.gameObject);
            spawnerScript.ReduceItemCount();
            SplitAsteroid();
        }
    }
    void SplitAsteroid()
    {
        spawnerScript.UpdateScore(scoreIncreaseValue);

        if (nextAsteroid)
        {
            for (int i = 0; i < subAsteroidsToSpawn; i++)
            {
                GameObject smallerAsteroid = Instantiate(nextAsteroid, gameObject.transform.position, Quaternion.Euler(0,Random.Range(0,360),0));

                if (smallerAsteroid.GetComponent<AsteroidBehaviour>())
                {
                    smallerAsteroid.GetComponent<AsteroidBehaviour>().spawnerScript = spawnerScript;
                }
            }
        }

        Destroy(gameObject);
    }
}

