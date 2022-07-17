using EventClass;
using Systems;
using UnityEngine;

namespace EnemyComponents
{
    public class OceanNightmareController : MonoBehaviour
    {
        [SerializeField] private NightmareWaypoint Waypoint;
        [SerializeField] private float LightUpDistance = 4f;
        [SerializeField] private float BackForce = 4f;
        private int _index = 0;
        private Vector3 _direction;

        private void Update()
        {
            var position = Waypoint.GetWaypointPosition(_index);
            var speed = Waypoint.GetWayPointSpeed(_index);
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            _direction = (position - transform.position).normalized;
            
            if (_index != Waypoint.GetWaypointCount() - 1 && Vector3.Distance(transform.position, position) <= 1f)
            {
                _index++;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<BasicControl>())
            {
                NewEventSystem.Instance.Publish(new GameEndEvent(true));
            }
        }

        public void RetreatByLit(Transform lightTransform)
        {
            var distance = Vector3.Distance(lightTransform.position, transform.position);
            if (distance <= LightUpDistance)
            {
                transform.position -= BackForce * _direction;
            }
        }
    }
}