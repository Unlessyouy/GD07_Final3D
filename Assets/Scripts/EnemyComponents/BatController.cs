using System;
using UnityEngine;

namespace EnemyComponents
{
    public class BatController : MonoBehaviour
    {
        [SerializeField] private float MoveSpeed = 3f;
        [SerializeField] private GameObject BombVFX;

        private Vector3 _targetPosition;
        private bool _isAimTarget;

        private void Update()
        {
            if (_isAimTarget)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, _targetPosition, MoveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, _targetPosition) <= 0.1f)
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
                _targetPosition = other.transform.position;

                _isAimTarget = true;
            }
        }
    }
}