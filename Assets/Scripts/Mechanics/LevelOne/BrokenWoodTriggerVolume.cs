using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.LevelOne
{
    public class BrokenWoodTriggerVolume : MonoBehaviour
    {
        [SerializeField] private List<Rigidbody> meshRigidbodies = new List<Rigidbody>();
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<BasicControl>())
            {
                foreach (var meshRigidbody in meshRigidbodies)
                {
                    meshRigidbody.isKinematic = false;
                }
            }
        }
    }
}
