using System;
using EventClass;
using Systems;
using UnityEngine;

namespace EnemyComponents
{
    public class AnglerFishController : MonoBehaviour
    {
        [SerializeField] private AnglerFishWaypoint Waypoint;
        [SerializeField] private float PatrolSpeed = 2f;
        [SerializeField] private float ChaseSpeed = 5f;
        [SerializeField] private float RotateSpeed = 180f;

        [Header("Newton Inertia")] [SerializeField]
        private float Distance = 2f;

        private bool _isChasingPlayer;
        private bool _isRotating;
        private Vector3 _target;
        private Transform _targetTransform;
        private BasicControl _targetPlayer;

        private int _index = 0;

        public bool IsPlayerHideInBush = false;

        private void Update()
        {
            if (Vector3.Dot(_target - transform.position, transform.forward) <= 0)
            {
                transform.Rotate(0, -RotateSpeed * Time.deltaTime, 0);
            }
            else
            {
                transform.Rotate(0, RotateSpeed * Time.deltaTime, 0);
            }
            
            if (_isChasingPlayer && !_isRotating)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target, ChaseSpeed * Time.deltaTime);

                if (Mathf.Abs(Vector3.Distance(transform.position, _target)) <= 0.5f)
                {
                    _target = _targetTransform.position;
                    _target += (_target - transform.position).normalized * Distance;
                }
            }
            
            if(!_isChasingPlayer && !_isRotating)
            {
                _target = Waypoint.GetWaypointPosition(_index);
                transform.position = Vector3.MoveTowards(transform.position, _target, PatrolSpeed * Time.deltaTime);

                if (Mathf.Abs(Vector3.Distance(transform.position, _target)) <= 0.5f)
                {
                    _index = Waypoint.GetNextWaypointIndex(_index);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<BasicControl>())
            {
                NewEventSystem.Instance.Publish(new GameEndEvent(true));
            }
        }

        public void FindPlayer(BasicControl player)
        {
            _targetPlayer = player;
            _targetTransform = player.transform;
            _target = _targetTransform.position;
            _target += (_target - transform.position).normalized * Distance;
            _isChasingPlayer = true;
        }

        public void PlayerHideInBush(BasicControl player, bool isHiding)
        {
            if (player == _targetPlayer)
            {
                IsPlayerHideInBush = isHiding;
                if (IsPlayerHideInBush)
                {
                    _isChasingPlayer = false;
                }
            }
        }
    }
}