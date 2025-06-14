using UnityEngine;

public class RandomPointInsideAndOutsideCameraView : MonoBehaviour
{
    // Reference to the camera, can be set in the inspector
    [SerializeField] private Camera mainCamera; 

    //void Start()
    //{
    //    for(int i = 0; i < 300; i++)
    //    {
    //        Vector3 randomPoint = GetRandomPointInsideCameraOnPlaneAtZeroY(mainCamera);
    //        randomPoint = GetRandomPointOutsideCameraOnPlaneAtZeroY(mainCamera);
    //        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //        cube.transform.position = randomPoint;
    //    }
    //}

    public static Vector3 GetRandomPointInsideCameraOnPlaneAtZeroY(Camera camera)
    {
        // Generate a random point in the camera's viewport space (x and y between 0 and 1)
        float randomX = Random.Range(0f, 1f);
        float randomY = Random.Range(0f, 1f);

        // Convert the random viewport coordinates to world space at a far distance along the camera's view direction
        Vector3 viewportPoint = new Vector3(randomX, randomY, camera.farClipPlane);
        Vector3 worldPoint = camera.ViewportToWorldPoint(viewportPoint);

        // Create a ray from the camera towards the world point
        Ray ray = new Ray(camera.transform.position, (worldPoint - camera.transform.position).normalized);

        // Plane at y = 0 (this is the plane we want the ray to hit)
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        // Raycast to the plane at y = 0 and check for intersection
        float distance;
        if(groundPlane.Raycast(ray, out distance))
        {
            // Get the point of intersection with the plane
            Vector3 hitPoint = ray.GetPoint(distance);

            // Return the hit point as the random point on the plane
            return hitPoint;
        }

        // If raycast fails, return a default point (this shouldn't normally happen)
        return Vector3.zero;
    }

    public static Vector3 GetRandomPointOutsideCameraOnPlaneAtZeroY(Camera camera)
    {
        // Determine if the point should be outside on the x-axis or y-axis
        bool outsideXAxis = Random.Range(0f, 1f) > 0.5f;

        // Pick random distance multiplier for the offset to make sure the point is outside
        float outsideDistance = Random.Range(0.1f, 0.3f); // Adjust distance multiplier for how far outside

        float randomX = 0f;
        float randomY = 0f;

        // Randomly choose which axis to push the point outside
        if(outsideXAxis)
        {
            // Random x point just outside the left or right side of the screen
            randomX = Random.Range(0f, 1f) < 0.5f ? -outsideDistance : 1 + outsideDistance; // Negative or positive to go outside
            randomY = Random.Range(0f, 1f); // Random y within the viewport
        }
        else
        {
            // Random y point just outside the top or bottom side of the screen
            randomY = Random.Range(0f, 1f) < 0.5f ? -outsideDistance : 1 + outsideDistance; // Negative or positive to go outside
            randomX = Random.Range(0f, 1f); // Random x within the viewport
        }

        // Convert the random viewport coordinates to world space at a far distance along the camera's view direction
        Vector3 viewportPoint = new Vector3(randomX, randomY, camera.farClipPlane);
        Vector3 worldPoint = camera.ViewportToWorldPoint(viewportPoint);

        // Create a ray from the camera towards the world point
        Ray ray = new Ray(camera.transform.position, (worldPoint - camera.transform.position).normalized);

        // Plane at y = 0 (this is the plane we want the ray to hit)
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        // Raycast to the plane at y = 0 and check for intersection
        float distance;
        if(groundPlane.Raycast(ray, out distance))
        {
            // Get the point of intersection with the plane
            Vector3 hitPoint = ray.GetPoint(distance);

            // Return the hit point as the random point on the plane
            return hitPoint;
        }

        // If raycast fails, return a default point (this shouldn't normally happen)
        return Vector3.zero;
    }
}
