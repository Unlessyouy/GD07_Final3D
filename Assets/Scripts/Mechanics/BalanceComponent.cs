using System;
using System.Collections.Generic;
using Mechanics.LevelTwo;
using UnityEngine;

namespace Mechanics
{
    public class BalanceComponent : MonoBehaviour
    {
        [SerializeField] private int Number;
        [SerializeField] private List<StarFish> StarFishes;

        public bool IsBalanced;
        private List<TurtleController> _turtles = new List<TurtleController>();

        private void OnTriggerEnter(Collider other)
        {
            var turtle = other.GetComponent<TurtleController>();
            if (turtle && !_turtles.Contains(turtle))
            {
                _turtles.Add(turtle);

                if (_turtles.Count <= Number)
                {
                    for (int i = 0; i < _turtles.Count; i++)
                    {
                        StarFishes[i].LitUP();
                    }
                }

                if (_turtles.Count == Number)
                {
                    IsBalanced = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var turtle = other.gameObject.GetComponent<TurtleController>();
            if (_turtles.Contains(turtle))
            {
                _turtles.Remove(turtle);
                
                StarFishes[_turtles.Count].Distinguish();
                
                IsBalanced = false;
            }
        }

        // private void OnCollisionEnter(Collision collision)
        // {
        //     var turtle = collision.gameObject.GetComponent<TurtleController>();
        //     if (turtle && !_turtles.Contains(turtle))
        //     {
        //         _turtles.Add(turtle);
        //
        //         if (_turtles.Count <= Number)
        //         {
        //             for (int i = 0; i < _turtles.Count; i++)
        //             {
        //                 StarFishes[i].LitUP();
        //             }
        //         }
        //
        //         if (_turtles.Count == Number)
        //         {
        //             IsBalanced = true;
        //         }
        //     }
        // }
        //
        // private void OnCollisionExit(Collision other)
        // {
        //     var turtle = other.gameObject.GetComponent<TurtleController>();
        //     if (_turtles.Contains(turtle))
        //     {
        //         _turtles.Remove(turtle);
        //         
        //         Debug.Log(_turtles.Count);
        //         StarFishes[_turtles.Count].Distinguish();
        //         
        //         IsBalanced = false;
        //     }
        // }
    }
}