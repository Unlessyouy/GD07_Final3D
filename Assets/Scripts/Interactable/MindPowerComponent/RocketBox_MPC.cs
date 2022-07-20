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
            if (Physics.Raycast(transform.position, -transform.up, out var hitInfo, 0.4f))
            {
                if (hitInfo.collider.CompareTag("Terrain") && hitInfo.collider.gameObject != gameObject)
                {
                    return true;
                }
            }

            return false;
        }
    }
}