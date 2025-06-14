using UnityEngine;
using UnityEngine.InputSystem;
using LaylaSyed;

namespace LaylaSyed
{
    /// <summary>
    /// This script handles all of the aspects of the player input, including shooting. It has alot of things you can tweak to make sure the movement is responsive and feels good to play.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] float minSpeed = 1f;
        [SerializeField] float speedIncreaseRate = 1f;
        [SerializeField] float speedDecelerationRate = 2f;
        [SerializeField] float maxSpeed = 2f;
        [SerializeField] float smoothingFactor = 10f;  // Smoothing factor for how quickly it caps speed

        [SerializeField] float maxVelocity = 10f;
        [SerializeField] float rotationSpeed = 10f;
        [SerializeField] float rotationDampingFactor = 0.95f;
        [SerializeField] float speedDampingFactor = 0.99f;
        [SerializeField] float minimumThresholdBeforeZeroed = 0.1f;
        //These are all values for tweaking the minutiae of the movement feels and behaves

        private bool isMoving = false;

        private Vector2 moveInputValue;


        Rigidbody cachedRigidbody;

        [SerializeField] GameObject projectileToSpawn;
        GameObject projectileHolder;
        [SerializeField] float projectileSpawnOffset = 2f;
        [SerializeField] float fireRate = 1f;
        private bool isProjectileBeingSpawned = false;
        private float lastBulletFired = -Mathf.Infinity;
        //Values for shooting
        private void Start()
        {
            cachedRigidbody = GetComponent<Rigidbody>();
            projectileHolder = new GameObject("Projectiles");
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            moveInputValue = context.ReadValue<Vector2>();
            isMoving = moveInputValue.magnitude > 0f;
        }
        void Update()
        {
            CheckFireRate();
            MovePlayer();
            if(isMoving)
            {
                IncreaseSpeedOverTime();
            }
            else
            {
                DecelerateSpeed();
            }
        }


        public void MovePlayer()
        {
            Vector3 movement = new Vector3(moveInputValue.x, 0f, moveInputValue.y);

            
            Vector3 worldMovement = movement.normalized * speed;
            cachedRigidbody.AddForce(worldMovement, ForceMode.VelocityChange);

            if(cachedRigidbody.linearVelocity.magnitude > maxVelocity)
            {
                //    //cachedRigidbody.linearVelocity = Vector3.zero;
                //    //worldMovement = Vector3.zero;
                //    speed = 0.01f;

                Vector3 newVelocity = cachedRigidbody.linearVelocity.normalized * maxVelocity;
                cachedRigidbody.linearVelocity = Vector3.Lerp(cachedRigidbody.linearVelocity, newVelocity, Time.deltaTime * smoothingFactor);
            }


            if(isMoving)
            {
                Quaternion targetRotation = Quaternion.LookRotation(worldMovement);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            cachedRigidbody.angularVelocity *= rotationDampingFactor;
            if(cachedRigidbody.angularVelocity.magnitude < minimumThresholdBeforeZeroed)
            {
                cachedRigidbody.angularVelocity = Vector3.zero;
            }

            cachedRigidbody.linearVelocity *= speedDampingFactor;
            if(cachedRigidbody.linearVelocity.magnitude < minimumThresholdBeforeZeroed)
            {
                cachedRigidbody.linearVelocity = Vector3.zero;
            }



        }
        public void IncreaseSpeedOverTime()
        {
            speed += speedIncreaseRate * Time.deltaTime;
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        }
        public void DecelerateSpeed()
        {
            speed = Mathf.Max(speed - speedDecelerationRate * Time.deltaTime, minSpeed);
        }
        public void ShootInput(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                ShootPressed();
            }
            else if(context.canceled)
            {
                ShootReleased();
            }
        }
        private void ShootPressed()
        {
            isProjectileBeingSpawned = true;
        }
        private void ShootReleased()
        {
            isProjectileBeingSpawned = false;
        }
        public void SpawnProjectile()
        {
            Vector3 spawnPosition = transform.position + transform.forward * projectileSpawnOffset;
            Instantiate(projectileToSpawn, spawnPosition, transform.rotation, projectileHolder.transform);
        }
        private void CheckFireRate()
        {
            if(isProjectileBeingSpawned && Time.time > lastBulletFired + fireRate)
            {
                lastBulletFired = Time.time;
                SpawnProjectile();
            }
        }
    }
}