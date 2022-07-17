using System;
using CharacterControl;
using Mechanics;
using Systems;
using UnityEngine;

namespace Interactable.CommonInteractableObjects
{
    public class SeaweedMeshCollider : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            var player = other.GetComponent<BasicControl>();
            if (player)
            {
                player.isInOcean = true;
                if (player as CompanionControl)
                {
                    SynchronousControlSingleton.Instance.IsInteractWithOceanObject = true;
                }

                player.GetComponent<WindBlowable>().IsBlowable = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<BasicControl>();
            if (player)
            {
                player.GetComponent<WindBlowable>().IsBlowable = true;
                if (player as CompanionControl)
                {
                    player.isInOcean = false;
                    SynchronousControlSingleton.Instance.IsInteractWithOceanObject = false;
                }
            }
        }
    }
}