using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PheobiNash;

/// <summary>
/// Moves the attached object toward a specified target position.
/// </summary>
public class MoveTowardsCenter : MonoBehaviour
{
    private Vector3 targetPosition;
    public float moveSpeed = 3f;

    public void Initialize(Vector3 centerPosition) => targetPosition = centerPosition;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}