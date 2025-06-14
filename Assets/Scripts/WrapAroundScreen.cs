using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class WrapAroundScreen : MonoBehaviour
{
    [SerializeField] private Camera cameraToDetectWrapping;
   
    private Vector3 newPos = new Vector2(-99, -99);
    private Vector3 currentScreenPosition;
    private bool WarpSpot = false;

    void Start()
    {
        if (cameraToDetectWrapping == null)
        {
            cameraToDetectWrapping = Camera.main;
        }
    }

    void Update()
    {
        currentScreenPosition = cameraToDetectWrapping.WorldToViewportPoint(transform.position);
        newPos = currentScreenPosition;

        if (currentScreenPosition.y > 1.05f)
        {
            newPos.y = -0.025f;
            WarpSpot = true;
        }
        else if (currentScreenPosition.y < -0.05f)
        {
            newPos.y = 1.025f;
            WarpSpot = true;
        }
        if (currentScreenPosition.x > 1.05f)
        {
            newPos.x = -0.025f;
            WarpSpot = true;
        }
        else if (currentScreenPosition.x < -0.05f)
        {
            newPos.x = 1.025f;
            WarpSpot = true;
        }

        if (WarpSpot)
        {
            Vector3 wrapSpot = cameraToDetectWrapping.ViewportToWorldPoint(newPos);
            wrapSpot.y = 0;
            transform.position = wrapSpot;
            WarpSpot = false;
        }
    }
}





