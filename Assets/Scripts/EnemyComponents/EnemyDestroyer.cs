using System;
using UnityEngine;

namespace EnemyComponents
{
    public class EnemyDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<EnemyCollision>())
            {
                other.GetComponent<EnemyCollision>().DestroySelf();
            }
        }
    }
}