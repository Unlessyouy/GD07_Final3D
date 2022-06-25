using System;
using UnityEngine;

namespace Mechanics
{
    public class WarmableComponent : MonoBehaviour
    {
        [Range(0, 1)] [SerializeField] private float WarmAmount = 1f;
        private bool IsWarmed { get; set; }

        private Material _material;

        private void Start()
        {
            _material = GetComponent<MeshRenderer>().material;
        }

        public void WarmSelf(float warmSpeed)
        {
            // if (IsWarmed) return;
            WarmAmount -= warmSpeed;
            
            Debug.Log("start");
            _material.SetFloat("_IceSlider", WarmAmount);

            if (WarmAmount <= 0f)
            {
                Debug.Log("Warmed");
                IsWarmed = true;
            }
        }
    }
}