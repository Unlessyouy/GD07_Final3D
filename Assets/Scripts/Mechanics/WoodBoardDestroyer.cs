using System;
using UnityEngine;

namespace Mechanics
{
    public class WoodBoardDestroyer : MonoBehaviour
    {
        public float GetWoodBoardVelocity()
        {
            return GetComponent<Rigidbody>().velocity.magnitude;
        }
    }
}