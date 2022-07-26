using System;
using UnityEngine;

namespace Mechanics.LevelTwo
{
    public class FearLight : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<JellyFishLight>())
            {
                transform.localScale = Vector3.one / 2f;
            }
        }
    }
}