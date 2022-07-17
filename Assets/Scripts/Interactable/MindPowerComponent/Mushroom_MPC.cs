using System;
using System.Collections;
using EventClass;
using Systems;
using UnityEngine;

namespace Interactable.MindPowerComponent
{
    public class Mushroom_MPC : EventMPCBase
    {
        [SerializeField] private float MindPowerDistance = 4f;
        [SerializeField] private ParticleSystem ActivatedVFX;
        [SerializeField] private ParticleSystem BounceVFX;

        [Header("Time")] [SerializeField] private float ResetTime = 5f;
        [SerializeField] private float WaitForResetTime = 1f;

        [Header("Force")] [SerializeField] private float BounceForce = 50f;
        
        private bool _isActivated;
        
        public override void OnMentalPowerActivate(Transform sonTransform)
        {
            if (_isActivated)
            {
                return;
            }
            
            var distance = Vector3.Distance(sonTransform.position, transform.position);

            if (distance <= MindPowerDistance)
            {
                ActivateSelf();
            }
        }

        private void ActivateSelf()
        {
            if (ActivatedVFX)
            {
               ActivatedVFX.Play();
            }

            _isActivated = true;
            
            StartCoroutine(ResetMushroom());
        }

        private IEnumerator ResetMushroom()
        {
            yield return new WaitForSeconds(WaitForResetTime);

            yield return new WaitForSeconds(ResetTime);
            
            _isActivated = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!_isActivated)
            {
                return;
            }
            
            var player = collision.gameObject.GetComponent<BasicControl>();

            if (player && player.GetRigidbodyVelocity().y <= 0f)
            {
                player.IsInBounce = true;
                BounceVFX.Play();
                player.SetRigidbodyVelocity(Vector3.zero);
                player.AddForceToRigidbody(BounceForce * Vector3.up, ForceMode.Acceleration);
                StartCoroutine(ResetPlayerBounceTime(player));
            }
        }

        private IEnumerator ResetPlayerBounceTime(BasicControl player)
        {
            yield return new WaitForSeconds(0.2f);

            player.IsInBounce = false;
        }
    }
}