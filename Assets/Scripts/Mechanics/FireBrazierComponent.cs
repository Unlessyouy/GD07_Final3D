using System;
using UnityEngine;

namespace Mechanics
{
    public class FireBrazierComponent : MonoBehaviour
    {
        public bool IsFire { get; set; }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 1, 0, 0.25f);
            Gizmos.DrawSphere(transform.position, transform.localScale.x);
        }
    }
}