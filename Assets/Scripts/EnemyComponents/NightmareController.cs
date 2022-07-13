using System;
using EventClass;
using Systems;
using UnityEngine;

namespace EnemyComponents
{
    public class NightmareController : MonoBehaviour
    {
        [SerializeField] private NightmareWaypoint Waypoint;
        private int _index = 0;
        
        private void Update()
        {
            var position = Waypoint.GetWaypointPosition(_index);
            var speed = Waypoint.GetWayPointSpeed(_index);
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            
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
    }
}