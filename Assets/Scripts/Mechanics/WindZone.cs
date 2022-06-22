using System;
using UnityEngine;

namespace Mechanics
{
    public class WindZone : MonoBehaviour
    {
        [SerializeField] private float Intensity;
        [SerializeField] private Vector3 Direction;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0,1,0,0.25f);
            Gizmos.DrawCube(transform.position, transform.localScale);
            Gizmos.DrawLine(transform.position, transform.position + Direction * 5f);
            
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<WindBlowable>())
            {
                var otherRb = other.GetComponent<Rigidbody>();
                if (otherRb.velocity.sqrMagnitude > 0)
                {
                    Debug.Log("Wind");
                    otherRb.AddForce(Direction * Intensity * Time.deltaTime * 1000f);
                }
            }
        }
    }
}
