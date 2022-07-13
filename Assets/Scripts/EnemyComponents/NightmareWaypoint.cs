using System.Collections.Generic;
using UnityEngine;

namespace EnemyComponents
{
    public class NightmareWaypoint : MonoBehaviour
    {
        [SerializeField] private List<float> WayPointSpeed = new List<float>();
        private float _radius = 0.5f;

        private void OnDrawGizmos()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(GetWaypointPosition(i), _radius);
                Gizmos.DrawLine(GetWaypointPosition(i), GetWaypointPosition(GetNextWaypointIndex(i)));
            }
        }

        public int GetNextWaypointIndex(int i)
        {
            // var nextIndex = i == transform.childCount - 1 ? 0 : i + 1;
            var nextIndex = i == transform.childCount - 1 ? i : i + 1;
            return nextIndex;
        }

        public int GetWaypointCount()
        {
            return transform.childCount;
        }

        public Vector3 GetWaypointPosition(int i)
        {
            return transform.GetChild(i).position;
        }

        public float GetWayPointSpeed(int i)
        {
            return WayPointSpeed[i];
        }
    }
}