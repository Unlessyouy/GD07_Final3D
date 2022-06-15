using System;
using UnityEngine;

namespace Mechanics
{
    public class OneWayPlatform : MonoBehaviour
    {
        private BoxCollider _meCollider;
        private BoxCollider _checkCollider;
        
        private void Start()
        {
            _meCollider = GetComponent<BoxCollider>();
            _checkCollider = gameObject.AddComponent<BoxCollider>();
            _checkCollider.size = _meCollider.size * 1.2f;
            _checkCollider.center = _meCollider.center;
            _checkCollider.isTrigger = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (Physics.ComputePenetration(_meCollider, transform.position, transform.rotation,
                    other, other.transform.position, other.transform.rotation,
                    out var direction, out var distance))
            {
                var dot = Vector3.Dot(transform.up, direction);

                if (dot < 0)
                {
                    Physics.IgnoreCollision(_meCollider, other, false);
                }
                else
                {
                    Physics.IgnoreCollision(_meCollider, other, true);
                }
            }
        }
    }
}