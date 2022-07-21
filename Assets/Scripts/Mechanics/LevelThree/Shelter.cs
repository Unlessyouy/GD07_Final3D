using System;
using UnityEngine;

namespace Mechanics.LevelThree
{
    public class Shelter : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            var player = other.GetComponent<PlayerWarmAmount>();

            if (player)
            {
                player.IsInShelter = true;
                player.IsInWind = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<PlayerWarmAmount>();

            if (player)
            {
                player.IsInShelter = false;
            }
        }
    }
}