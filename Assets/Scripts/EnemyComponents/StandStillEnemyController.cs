using System;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyComponents
{
    public class StandStillEnemyController : MonoBehaviour
    {
        [SerializeField] private Transform StartPoint;
        [SerializeField] private Transform EndPoint;
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
            
            var searchAreaPosition = SearchArea.transform.position;

            if (Mathf.Abs(Vector3.Distance(searchAreaPosition, EndPoint.position)) <= 0.5f)
            {
                _targetPosition = StartPoint.position;
            }

            if (Mathf.Abs(Vector3.Distance(searchAreaPosition, StartPoint.position)) <= 0.5f)
            {
                _targetPosition = EndPoint.position;
            }

            SearchArea.transform.position =
                Vector3.MoveTowards(searchAreaPosition, _targetPosition, SearchSpeed * Time.deltaTime);

            var targetDirection = searchAreaPosition - transform.position;
            var newDirection =
                Vector3.RotateTowards(transform.forward, targetDirection, RotateSpeed * Time.deltaTime, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        public void FindPlayer(Transform playerTrans)
        {
            _navMeshAgent.SetDestination(playerTrans.position);
            _isChasing = true;
        }
    }
}