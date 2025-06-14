using UnityEngine;

public class MoveTowardsRandomPoints : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 5f;
    void Start()
    {
        mainCamera = Camera.main;
        targetPosition = RandomPointInsideAndOutsideCameraView.GetRandomPointInsideCameraOnPlaneAtZeroY(mainCamera);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Calculate the direction to the target (ignoring Y if you want horizontal rotation only)
        Vector3 direction = (targetPosition - transform.position).normalized;
        if(direction != Vector3.zero) // avoid zero-vector rotation
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if(Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if(Random.value <= 0.5f)
            {
                targetPosition = RandomPointInsideAndOutsideCameraView.GetRandomPointInsideCameraOnPlaneAtZeroY(mainCamera);
            }
            else
            {
                targetPosition = RandomPointInsideAndOutsideCameraView.GetRandomPointOutsideCameraOnPlaneAtZeroY(mainCamera);
            }
        }
    }
}
