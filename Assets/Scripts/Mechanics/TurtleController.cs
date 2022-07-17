using System;
using UnityEngine;

namespace Mechanics
{
    public class TurtleController : MonoBehaviour
    {
        [SerializeField] private Transform LowestPoint;
        [SerializeField] private float MoveSpeed = 0.5f;
        
        private Rigidbody _rb;
        private bool _isBlow;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (transform.position.y > LowestPoint.position.y && !_isBlow)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, LowestPoint.position, MoveSpeed * Time.deltaTime);
            }
            
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