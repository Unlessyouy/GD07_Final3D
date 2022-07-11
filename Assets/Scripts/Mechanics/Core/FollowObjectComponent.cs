using System;
using UnityEngine;

namespace Mechanics
{
    public class FollowObjectComponent : MonoBehaviour
    {
        [SerializeField] private Transform FollowTransform;

        private void Update()
        {
            if (FollowTransform.gameObject.activeSelf)
            {
                transform.position = FollowTransform.position;
            }
        }
    }
}