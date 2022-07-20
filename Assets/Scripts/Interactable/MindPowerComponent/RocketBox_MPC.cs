using System;
using System.Collections;
using UnityEngine;

namespace Interactable.MindPowerComponent
{
    public class RocketBox_MPC : EventMPCBase
    {
        [SerializeField] private float MindPowerDistance = 2.7f;
        [SerializeField] private ParticleSystem GrowUpVFX;

        [Header("Time")] [SerializeField] private float MoveY;
        [SerializeField] private float MoveTime = 2f;

        private bool _isActivated;
        private bool _isReachMaxPoint;
        [SerializeField] private Transform MaxPoint;
        
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (IsGrounded())
            {
                _isActivated = false;
                _isReachMaxPoint = false;
            }
        }

        private void FixedUpdate()
        {
            // if (!_isReachMaxPoint && (transform.position.y >= MaxPoint.position.y))
            // {
            //     StartCoroutine(StartStatic());
            // }
            //
            if (!_isReachMaxPoint && _rb.velocity.y <= -0.3f)
            {
                StartCoroutine(StartStatic());
            }
        }

        public override void OnMentalPowerActivate(Transform sonTransform)
        {
            if (_isActivated) return;
            
            var distance = Vector3.Distance(sonTransform.position, transform.position);

            if (distance <= MindPowerDistance)
            {
                _isActivated = true;
                _rb.AddForce(Vector3.up * (MoveY * 100000f));

                if (GrowUpVFX)
                {
                    GrowUpVFX.Play();
                }
            }
        }

        private bool IsGrounded()
        {
            if (Physics.Raycast(transform.position, -transform.up, out var hitInfo, 0.255f))
            {
                if (hitInfo.collider.CompareTag("Terrain") && hitInfo.collider.gameObject != gameObject)
                {
                    if (_rb.velocity.y < 0)
                    {
                        _rb.velocity = Vector3.zero; 
                    }
                    return true;
                }
            }

            return false;
        }

        // private void OnCollisionEnter(Collision collision)
        // {
        //     if (collision.gameObject.CompareTag("Terrain"))
        //     {
        //         _isActivated = false;
        //         _isReachMaxPoint = false;
        //     }
        // }

        private IEnumerator StartStatic()
        {
            _isReachMaxPoint = true;
            _rb.isKinematic = true;
            _rb.velocity = Vector3.zero;

            yield return new WaitForSeconds(MoveTime);

            _rb.isKinematic = false;
        }
    }
}