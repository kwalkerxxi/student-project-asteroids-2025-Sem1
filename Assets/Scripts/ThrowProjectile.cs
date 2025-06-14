using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Hoey
{
    public class ThrowProjectile : MonoBehaviour
    {
        [SerializeField] GameObject projectileToSpawn;
        [SerializeField] float secondsToWait = 0.5f;
        [SerializeField] float spawnOffset = 10f;
        private bool isProjectileBeingSpawned = false;

        public void SpawnProjectile()
        {
            if (!isProjectileBeingSpawned)
            {
                isProjectileBeingSpawned = true;
                Vector3 spawnPosition = transform.position + transform.forward * spawnOffset;
                Instantiate(projectileToSpawn, transform.position, transform.rotation);
                StartCoroutine(ResetSpawnStatus());
            }
        }
        private IEnumerator ResetSpawnStatus()
        {
            yield return new WaitForSeconds(secondsToWait);
            isProjectileBeingSpawned = false;
        }
    }
}