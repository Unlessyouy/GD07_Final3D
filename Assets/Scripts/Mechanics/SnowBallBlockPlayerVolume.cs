using System;
using UnityEngine;

namespace Mechanics
{
    public class SnowBallBlockPlayerVolume : MonoBehaviour
    {
        [SerializeField] private Transform FollowObject;

        private void Update()
        {
            transform.position = FollowObject.position;
        }
    }
}