using System;
using Character;
using UnityEngine;

namespace Environment.Colliders
{
    public class NavMeshOffLinkOverlap : MonoBehaviour
    {

        [SerializeField] private GameObject NavLinkB;

        private float DistanceBetweenTriggers;

        private void Start()
        {
            DistanceBetweenTriggers = Vector3.Distance(transform.position, NavLinkB.transform.position);
            Debug.Log(DistanceBetweenTriggers);
        }

        private void OnTriggerEnter(Collider other)
        {
            MovementComponent movement = other.GetComponent<MovementComponent>();
            
            if (!movement) return;
            Debug.Log("Player Triggered");

            Vector3 direction = movement.MoveDirection.normalized * DistanceBetweenTriggers * 10f;
            Vector3 newPosition = other.transform.position + direction;
            
            movement.SetDestination(newPosition);
        }
    }
}
