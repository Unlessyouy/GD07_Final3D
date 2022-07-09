using System;
using EventClass;
using Systems;
using UnityEngine;

namespace Mechanics
{
    public class SnowBallBlockPlayerVolume : MonoBehaviour
    {
        [SerializeField] private Transform FollowObject;
        [SerializeField] private Rigidbody Rigidbody;

        private void Update()
        {
            transform.position = FollowObject.position;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(Rigidbody.velocity.sqrMagnitude);
            if (Rigidbody.velocity.sqrMagnitude >= 10)
            {
                NewEventSystem.Instance.Publish(new GameEndEvent(true));
            }
        }
    }
}