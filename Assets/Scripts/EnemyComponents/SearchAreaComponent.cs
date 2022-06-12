using System;
using UnityEngine;

namespace EnemyComponents
{
    public class SearchAreaComponent : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.15f);
            Gizmos.DrawSphere(transform.position, transform.localScale.x / 2);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GetComponentInParent<EnemyAIController>().FindPlayer(other.gameObject);
                Debug.Log("Find " + other.name + "...");
            }
        }
    }
}