using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hoey
{
	/// <summary>
	/// Author: Mark Hoey
	/// Description: This script demonstrates how to ... in Unity
	/// </summary>
    [RequireComponent(typeof(Rigidbody))]
	public class PushProjectile : MonoBehaviour 
	{
        Rigidbody objectRigidbody;
        [SerializeField] private float forwardPower = 3;
        [SerializeField] private float upPower = 5;
        [SerializeField] bool effectedByGravity = true;
        [SerializeField] float lifeTime = 2f;
        
        void Start () 
		{
            objectRigidbody = this.gameObject.GetComponent<Rigidbody>();
            objectRigidbody.useGravity = effectedByGravity;

            objectRigidbody.AddRelativeForce((Vector3.forward * forwardPower) + (Vector3.up * upPower), ForceMode.Impulse);

            Destroy(this.gameObject, lifeTime);
		}

        
    }
}