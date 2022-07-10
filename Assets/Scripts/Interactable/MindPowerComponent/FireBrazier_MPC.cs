using System;
using System.Collections;
using UnityEngine;

namespace Interactable.MindPowerComponent
{
    public class FireBrazier_MPC : global::MindPowerComponent
    {
        [SerializeField] private GameObject FireVFX;
        [SerializeField] private FireBrazier FireBrazier;
        [SerializeField] private float LitTime = 5f;

        private void Start()
        {
            FireVFX.SetActive(false);
        }

        public override void MindPowerTrigger()
        {
            StopAllCoroutines();
            FireBrazier.IsFire = true;
            FireVFX.SetActive(true);
            StartCoroutine(Burn());
        }

        private IEnumerator Burn()
        {
            yield return new WaitForSeconds(LitTime);

            FireBrazier.IsFire = false;
            FireVFX.SetActive(false);
        }
    }
}