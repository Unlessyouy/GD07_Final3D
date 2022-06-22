using System;
using UnityEngine;

namespace Mechanics
{
    public class BalanceMechanism : MonoBehaviour
    {
        [SerializeField] private Transform LeftJoint;
        [SerializeField] private Transform RightJoint;
        [SerializeField] private Transform LeftPlane;
        [SerializeField] private Transform RightPlane;
        
        private void FixedUpdate()
        {
            LeftPlane.position = LeftJoint.position;
            RightPlane.position = RightJoint.position;
        }
    }
}
