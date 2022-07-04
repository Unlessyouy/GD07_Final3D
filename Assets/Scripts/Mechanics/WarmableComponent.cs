using System;
using UnityEngine;

namespace Mechanics
{
    public class WarmableComponent : MonoBehaviour
    {
        [Range(0, 1)] [SerializeField] private float WarmAmount = 1f;
        public bool IsWarmed { get; set; }

        private Material _material;

        private void Start()
        {
            _material = GetComponent<MeshRenderer>().material;
        }

        public void WarmSelf(float warmSpeed)
        {
            WarmAmount -= warmSpeed;
            
            _material.SetFloat("_IceSlider", WarmAmount);

            if (WarmAmount <= 0f)
            {
                IsWarmed = true;
                WarmAmount = 0f;
                _material.SetFloat("_IceSlider", WarmAmount);
            }
        }
    }
}