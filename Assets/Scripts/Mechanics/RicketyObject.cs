using System;
using EnemyComponents;
using UnityEngine;

namespace Mechanics
{
    public class RicketyObject : TriggerObject
    {
        public override void OnActivated()
        {
            base.OnActivated();

            GetComponent<Rigidbody>().useGravity = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<EnemyCollision>())
            {
                other.GetComponent<EnemyCollision>().DestroySelf();
            }
        }
    }
}