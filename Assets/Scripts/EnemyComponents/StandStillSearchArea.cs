using System;
using UnityEngine;

namespace EnemyComponents
{
    public class StandStillSearchArea : MonoBehaviour
    {
        private float _z;

        private void Start()
        {
            _z = transform.position.z;
        }

        private void Update()
        {
            // var trans = transform;
            // var transPosition = trans.position;
            // transPosition = new Vector3(transPosition.x, transPosition.y, _z);
            // trans.position = transPosition;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<BasicControl>())
            {
                GetComponentInParent<StandStillEnemyController>().FindPlayer(other.transform);
                transform.position = other.transform.position;
            }
        }
    }
}