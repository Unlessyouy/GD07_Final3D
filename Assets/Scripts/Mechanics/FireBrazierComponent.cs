using System;
using UnityEngine;

namespace Mechanics
{
    public class FireBrazierComponent : MonoBehaviour
    {
        [Range(0, 1)] [SerializeField] private float WarmSpeed = 0.25f;

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 1, 0, 0.25f);
            Gizmos.DrawSphere(transform.position, transform.localScale.x);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<WarmableComponent>() && !other.GetComponent<WarmableComponent>().IsWarmed)
            {
                other.GetComponent<WarmableComponent>().WarmSelf(WarmSpeed * Time.deltaTime);
            }
        }
    }
}