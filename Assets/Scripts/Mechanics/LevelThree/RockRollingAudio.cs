using System;
using UnityEngine;

namespace Mechanics.LevelThree
{
    public class RockRollingAudio : MonoBehaviour
    {
        [SerializeField] AK.Wwise.Event RockRolling;

        private void OnEnable()
        {
            RockRolling.Post(gameObject);
        }
    }
}