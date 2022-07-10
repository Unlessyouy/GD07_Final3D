using System;
using EventClass;
using Systems;
using UnityEngine;

namespace Mechanics
{
    public class KillCharacterVolume : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<BasicControl>())
            {
                NewEventSystem.Instance.Publish(new GameEndEvent(true));
            }
        }
    }
}