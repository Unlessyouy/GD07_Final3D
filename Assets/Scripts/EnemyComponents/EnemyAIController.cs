using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyComponents
{
    public enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Stunned,
    }

    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField] private EnemyState _enemyState;
        [SerializeField] private PatrolPathComponent PatrolPath;
        [SerializeField] private float WayPointTolerance = 1f;
        [SerializeField] private float ChaseDistance = 5f;
        [SerializeField] private float SuspiciousTime = 5f;
        [SerializeField] private float DwellingTime = 2f;

        [Space(20)] [SerializeField] private float MaxSpeed = 5f;
        [Range(0, 1)] public float PatrolFraction = 0.4f;

        private Vector3 _guardPosition;
        private int _wayPointIndex = 0;
        private float _lastSawPlayerTime = Mathf.Infinity;
        private float _timeArriveWaypoint = Mathf.Infinity;

        private NavMeshAgent _navMeshAgent;
        private GameObject _player;

        private void Start()
        {
            _guardPosition = transform.position;
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            switch (_enemyState)
            {
                case EnemyState.Idle:
                    SearchBehaviour();
                    break;
                case EnemyState.Patrol:
                    SearchBehaviour();
                    PatrolBehaviour();
                    break;
                case EnemyState.Chase:
                    ChaseBehaviour();
                    break;
                case EnemyState.Stunned:
                    StunnedBehaviour();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #region Stunned Behaviour

        private void StunnedBehaviour()
        {
            Debug.Log("Enemy is stunned...");
        }

        #endregion

        #region Chase Behaviour

        private void ChaseBehaviour()
        {
            var lastSawPlayerPosition = _player.transform.position;
            
            if (DistanceToPlayer() <= ChaseDistance)
            {
                MoveTo(_player.transform.position, 1f);
                _lastSawPlayerTime = .0f;
            }

            else if (_lastSawPlayerTime <= SuspiciousTime)
            {
                MoveTo(lastSawPlayerPosition, 1f);
            }

            else
            {
                _enemyState = EnemyState.Patrol;
            }

            _lastSawPlayerTime += Time.deltaTime;
        }

        #endregion

        #region Patrol Behaviour

        private void PatrolBehaviour()
        {
            var nextPosition = _guardPosition;

            if (PatrolPath)
            {
                var wayPointPosition = PatrolPath.GetWaypointPosition(_wayPointIndex);
                var distance = Vector3.Distance(transform.position, wayPointPosition);

                if (distance <= WayPointTolerance)
                {
                    _timeArriveWaypoint += Time.deltaTime;

                    if (_timeArriveWaypoint >= DwellingTime)
                    {
                        _wayPointIndex = PatrolPath.GetNextWaypointIndex(_wayPointIndex);
                    }
                    else
                    {
                        CancelMove();
                    }
                }
                else
                {
                    _timeArriveWaypoint = .0f;
                }

                nextPosition = PatrolPath.GetWaypointPosition(_wayPointIndex);
                MoveTo(nextPosition, PatrolFraction);
            }
        }

        #endregion

        #region Search Behaviour

        private void SearchBehaviour()
        {
        }

        #endregion
        
        #region Movement

        private void CancelMove()
        {
            _navMeshAgent.isStopped = true;
        }

        private void MoveTo(Vector3 destination, float speedFraction)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.speed = MaxSpeed * Mathf.Clamp01(speedFraction);
            _navMeshAgent.SetDestination(destination);
        }

        #endregion

        private double DistanceToPlayer()
        {
            var distance = Vector3.Distance(_player.transform.position, transform.position);
            return distance;
        }

        #region Public Methods

        public void FindPlayer(GameObject player)
        {
            if (_enemyState != EnemyState.Chase)
            {
                CancelMove();
                _enemyState = EnemyState.Chase;
            }

            _player = player;
        }

        #endregion
    }
}