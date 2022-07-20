using System;
using UnityEngine;

namespace Mechanics.LevelThree
{
    public class RocketParentComponent : MonoBehaviour
    {
        [SerializeField] private Transform Parent;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<RocketMoveable>()) return;
            other.transform.parent = Parent;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.GetComponent<RocketMoveable>()) return;
            other.transform.parent = null;
        }
    }
}