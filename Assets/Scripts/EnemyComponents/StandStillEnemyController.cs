using System;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyComponents
{
    public class StandStillEnemyController : MonoBehaviour
    {
        [SerializeField] private Transform StartPoint;
        [SerializeField] private Transform EndPoint;
        [SerializeField] private GameObject MovingArea;
        [SerializeField] private GameObject SearchArea;
        [SerializeField] private float SearchSpeed = 1f;
        [SerializeField] private float RotateSpeed = 0.5f;
        private Vector3 _targetPosition;

        private NavMeshAgent _navMeshAgent;

        private bool _isChasing;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _targetPosition = StartPoint.position;
        }

        private void Update()
        {
            if (_isChasing) return;

            var searchAreaPosition = MovingArea.transform.position;

            if (Mathf.Abs(Vector3.Distance(searchAreaPosition, EndPoint.position)) <= 0.5f)
            {
                _targetPosition = StartPoint.position;
            }

            if (Mathf.Abs(Vector3.Distance(searchAreaPosition, StartPoint.position)) <= 0.5f)
            {
                _targetPosition = EndPoint.position;
            }

            MovingArea.transform.position =
                Vector3.MoveTowards(searchAreaPosition, _targetPosition, SearchSpeed * Time.deltaTime);

            var targetDirection = searchAreaPosition - transform.position;
            var newDirection =
                Vector3.RotateTowards(transform.forward, targetDirection, RotateSpeed * Time.deltaTime, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDirection);

            Debug.DrawRay(transform.position + Vector3.up, transform.forward * 20f, Color.red);
            if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out var hitInfo) &&
                hitInfo.collider.isTrigger != true &&
                hitInfo.point.z >= MovingArea.transform.position.z)
            {
                SearchArea.transform.position = hitInfo.point;
            }
            else
            {
                SearchArea.transform.position = MovingArea.transform.position;
            }
        }

        public void FindPlayer(Transform playerTrans)
        {
            _navMeshAgent.SetDestination(playerTrans.position);
            _isChasing = true;
        }
    }
}