using System;
using UnityEngine;

namespace Mechanics.LevelThree
{
    public class InstantiateRock : MonoBehaviour
    {
        [SerializeField] private GameObject go;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(go, transform);
            }
        }
    }
}