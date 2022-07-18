using System;
using UnityEngine;

namespace EnemyComponents
{
    public class AnglerFishSearchArea : MonoBehaviour
    {
        private AnglerFishController _anglerFishController;

        private void Start()
        {
            _anglerFishController = GetComponentInParent<AnglerFishController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherPlayer = other.GetComponent<BasicControl>();
            if (otherPlayer && !_anglerFishController.IsPlayerHideInBush)
            {
                if (otherPlayer.IsHideInCoral)
                {
                    return;
                }
                _anglerFishController.FindPlayer(otherPlayer);
            }
        }
    }
}