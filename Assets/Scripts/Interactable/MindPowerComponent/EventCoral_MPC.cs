using System;
using System.Collections;
using EventClass;
using Systems;
using UnityEngine;

namespace Interactable.MindPowerComponent
{
    public class EventCoral_MPC : EventMPCBase
    {
        [SerializeField] private float MindPowerDistance = 2.7f;
        [SerializeField] private ParticleSystem GrowUpVFX;
        [SerializeField] private Transform Mesh;
        [Header("Time")] [SerializeField] private float ResetTime = 5f;
        [SerializeField] private float WaitForResetTime = 1f;
        [SerializeField] private Vector3 GrowUpScale;

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
                GrowUp();
            }
        }

        private void GrowUp()
        {
            if (GrowUpVFX)
            {
                GrowUpVFX.Play();
            }

            StartCoroutine(StartGrowUp());
            
            _isActivated = true;
        }

        private IEnumerator StartGrowUp()
        {
            var timer = 0f;
            var currentMeshScale = Mesh.localScale;
            while (timer <= 1f)
            {
                timer += Time.deltaTime;
                Mesh.localScale = Vector3.Lerp(currentMeshScale, GrowUpScale, timer);
                yield return null;
            }
            
            yield return StartCoroutine(ResetCoral());
        }

        private IEnumerator ResetCoral()
        {
            yield return new WaitForSeconds(WaitForResetTime);
            var timer = 0f;
            var currentMeshScale = Mesh.localScale;
            while (timer <= 1f)
            {
                timer += Time.deltaTime / ResetTime;
                Mesh.localScale = Vector3.Lerp(currentMeshScale, Vector3.one, timer);
                yield return null;
            }

            _isActivated = false;
            yield return null;
        }


        private void OnTriggerEnter(Collider other)
        {
            if(!_isActivated) return;
            var playerCharacter = other.GetComponent<BasicControl>();
            if (playerCharacter)
            {
                NewEventSystem.Instance.Publish(new PlayerHideEvent(playerCharacter, true));
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if(!_isActivated) return;
            var playerCharacter = other.GetComponent<BasicControl>();
            if (playerCharacter)
            {
                playerCharacter.IsHideInCoral = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(!_isActivated) return;
            var playerCharacter = other.GetComponent<BasicControl>();
            if (playerCharacter)
            {
                playerCharacter.IsHideInCoral = false;
                NewEventSystem.Instance.Publish(new PlayerHideEvent(playerCharacter, false));
            }
        }
    }
}