using System;
using EventClass;
using Systems;
using UnityEngine;

namespace Mechanics
{
    public class IceCone : MonoBehaviour
    {
        [SerializeField] private GameObject InstantiateGo;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<BasicControl>())
            {
                NewEventSystem.Instance.Publish(new GameEndEvent(true));
            }
            
            if (other.CompareTag("Terrain"))
            {
                Instantiate(InstantiateGo, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }
}