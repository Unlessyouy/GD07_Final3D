using System;
using UnityEngine;

namespace EnemyComponents
{
    public class BatController : MonoBehaviour
    {
        [SerializeField] private float MoveSpeed = 3f;
        [SerializeField] private GameObject BombVFX;

        private Transform _targetTransform;
        private bool _isAimTarget;

        private void Update()
        {
            if (_isAimTarget)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, _targetTransform.position, MoveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, _targetTransform.position) <= 0.1f)
                {
                    Instantiate(BombVFX, transform.position, transform.rotation);
                    gameObject.SetActive(false);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isAimTarget && other.GetComponent<BasicControl>())
            {
                _targetTransform = other.transform;

                _isAimTarget = true;
            }
        }
    }
}