using System;
using UnityEngine;

namespace Mechanics.LevelThree
{
    public class PushObject : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            var player = other.gameObject.GetComponent<BasicControl>();
            if (player)
            {
                player.PushObject();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var player = other.gameObject.GetComponent<BasicControl>();
            if (player)
            {
                player.StopPushObject();
            }
        }
    }
}