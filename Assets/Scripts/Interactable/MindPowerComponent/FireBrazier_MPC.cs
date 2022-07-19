using System;
using System.Collections;
using UnityEngine;

namespace Interactable.MindPowerComponent
{
    public class FireBrazier_MPC : global::MindPowerComponent
    {
        [SerializeField] private GameObject FireVFX;
        [SerializeField] private ParticleSystem TriggerVFX;
        [SerializeField] private FireBrazier FireBrazier;
        [SerializeField] private float LitTime = 5f;

        bool soundPlayed;

        private bool _isFirstLit = true;
        
        private void Start()
        {
            FireVFX.SetActive(false);
        }
        private void Update()
        {
            if (FireBrazier.IsFire && !soundPlayed)
            {
                AkSoundEngine.PostEvent("Campfire", gameObject);
                soundPlayed = true;
            }
            else if(!FireBrazier.IsFire)
            {
                AkSoundEngine.StopAll(gameObject);
                soundPlayed = false;
            }
        }
        public override void MindPowerTrigger()
        {
            StopAllCoroutines();
            FireBrazier.IsFire = true;
            FireVFX.SetActive(true);
            StartCoroutine(Burn());

            if (_isFirstLit)
            {
                TriggerVFX.Play();
                _isFirstLit = false;
            }
        }

        private IEnumerator Burn()
        {
            yield return new WaitForSeconds(LitTime);

            FireBrazier.IsFire = false;
            FireVFX.SetActive(false);
            _isFirstLit = true;
        }
    }
}