using System;
using Interactable.CommonInteractableObjects;
using UnityEngine;

namespace Mechanics
{
    public class CrackedPlatform : MonoBehaviour
    {
        [SerializeField] private int DestroyCount = 3;
        [SerializeField] private float VelocityToCrack = 10f;
        [SerializeField] private Transform Seaweed;
        [SerializeField] private float Distance = 0.25f;

        private bool _canCollisionWithRock = true;

        private void OnCollisionEnter(Collision collision)
        {
            var rock = collision.gameObject.GetComponent<OnlyFatherInteractRock>();

            if (!rock) return;

            if (!_canCollisionWithRock) return;
            
            if (rock.GetRockVelocitySqrMag() < VelocityToCrack)
            {
                return;
            }
            
            OnCollisionRock(rock);
            
            if (DestroyCount <= 0)
            {
                DestroySelf();
            }
        }

        private void OnCollisionRock(OnlyFatherInteractRock rock)
        {
            rock.DestroySelf();
            
            DestroyCount--;
            
            Seaweed.position -= Vector3.up * Distance;
        }

        private void DestroySelf()
        {
            _canCollisionWithRock = false;
        }
    }
}