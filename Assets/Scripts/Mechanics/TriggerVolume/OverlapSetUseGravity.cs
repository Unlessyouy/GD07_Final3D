using System;
using UnityEngine;

namespace Mechanics.TriggerVolume
{
    public class OverlapSetUseGravity : MonoBehaviour
    {
        [SerializeField] private Rigidbody UseRigidbody;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<BasicControl>())
            {
                UseRigidbody.useGravity = true;
                UseRigidbody.isKinematic = false;
            }
        }
    }
}