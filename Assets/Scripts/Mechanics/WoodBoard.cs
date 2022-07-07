using System;
using UnityEngine;

namespace Mechanics
{
    public class WoodBoard : MonoBehaviour
    {
        [SerializeField] private float CriticalVelocityToDestroy = 0.1f;
        
        private void OnTriggerEnter(Collider other)
        {
            var woodBoardDestroyer = other.GetComponent<WoodBoardDestroyer>();
            if (woodBoardDestroyer && woodBoardDestroyer.GetWoodBoardVelocity() >= CriticalVelocityToDestroy)
            {
                gameObject.SetActive(false);
            }
        }
    }
}