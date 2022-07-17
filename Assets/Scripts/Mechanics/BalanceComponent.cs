using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class BalanceComponent : MonoBehaviour
    {
        [SerializeField] private int Number;
        private List<TurtleController> _turtles = new List<TurtleController>();

        private void OnCollisionEnter(Collision collision)
        {
            var turtle = collision.gameObject.GetComponent<TurtleController>();
            if (turtle && !_turtles.Contains(turtle))
            {
                _turtles.Add(turtle);

                if (_turtles.Count == Number)
                {
                    Debug.Log("Success");
                }
            }
        }

        private void OnCollisionExit(Collision other)
        {
            var turtle = other.gameObject.GetComponent<TurtleController>();
            if (_turtles.Contains(turtle))
            {
                _turtles.Remove(turtle);
                Debug.Log(_turtles.Count);
            }
        }
    }
}