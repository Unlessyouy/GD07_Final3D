using System;
using UnityEngine;

namespace Mechanics
{
    public class FireBrazierComponent : MonoBehaviour
    {
        [Range(0, 1)] [SerializeField] private float WarmSpeed = 0.25f;

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<WarmableComponent>())
            {
                Debug.Log("HI I will warm you!");
                other.GetComponent<WarmableComponent>().WarmSelf(WarmSpeed * Time.deltaTime);
            }
        }
    }
}