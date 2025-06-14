using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class RandomPointOnSpline : MonoBehaviour
{
    public SplineContainer splineContainer;

    void Start()
    {
        // Pick a random point on the spline
        Vector3 randomPoint = GetRandomPointOnSpline();
        Debug.Log("Random Point on Spline: " + randomPoint);
    }

    Vector3 GetRandomPointOnSpline()
    {
        //Vector3 pointOnSpline = Vector3.zero;
        //if(splineContainer.Evaluate(0, UnityEngine.Random.value, out float3 position, out float3 tangent, out float3 upVector))
        //{

        //}

        return splineContainer.EvaluatePosition(0, UnityEngine.Random.value);
    }
}
