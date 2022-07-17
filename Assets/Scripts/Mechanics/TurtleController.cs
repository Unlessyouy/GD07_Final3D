using System;
using UnityEngine;

namespace Mechanics
{
    public class TurtleController : MonoBehaviour
    {
        private Rigidbody _rb;
        private bool _isBlow;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!_isBlow && _rb.velocity.sqrMagnitude != 0)
            {
                _rb.velocity = Vector3.zero;
            }
        }

        public void BlowByWater(Vector3 velocity)
        {
            _rb.velocity = velocity;
            _isBlow = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<WaterPump>())
            {
                _isBlow = false;
            }
        }
    }
}