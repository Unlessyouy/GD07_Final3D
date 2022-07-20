using System;
using UnityEngine;

namespace Interactable.MindPowerComponent
{
    public class RainbowBridge_MPC : EventMPCBase
    {
        [SerializeField] private float MindPowerDistance = 2.7f;
        [SerializeField] private ParticleSystem GrowUpVFX;
        [SerializeField] private GameObject SetActive;
        
        private bool _isActivated;

        private void Start()
        {
            SetActive.SetActive(false);
        }

        public override void OnMentalPowerActivate(Transform sonTransform)
        {
            var distance = Vector3.Distance(sonTransform.position, transform.position);

            if (distance <= MindPowerDistance)
            {
                if (_isActivated)
                {
                    SetActive.SetActive(false);
                    _isActivated = false;
                }
                else
                {
                    GrowUp();
                }
            }
        }

        private void GrowUp()
        {
            if (GrowUpVFX)
            {
                GrowUpVFX.Play();
            }

            SetActive.SetActive(true);
            _isActivated = true;
        }
    }
}