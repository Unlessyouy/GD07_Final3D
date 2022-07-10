using System;
using System.Collections;
using Interactable.MindPowerComponent;
using UnityEngine;

namespace Mechanics
{
    public class WarmableComponent : MonoBehaviour
    {
        [Range(0, 1)] [SerializeField] protected float WarmSpeed = 0.25f;
        [Range(0, 1)] [SerializeField] protected float WarmAmount = 1f;
        
        public bool IsWarmed { get; set; }

        private float _timer = .0f;

        private Material _material;

        protected virtual void Start()
        {
            _material = GetComponent<MeshRenderer>().material;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= 1f)
            {
                FreezeSelf();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            var fireBrazier = other.GetComponent<FireBrazier>();
            if (fireBrazier && fireBrazier.IsFire)
            {
                _timer = 0f;
                if (!IsWarmed)
                {
                    WarmSelf(WarmSpeed * Time.deltaTime);
                }
            }
        }
        

        private void FreezeSelf()
        {
            IsWarmed = false;
            
            if (WarmAmount < 1f)
            {
                WarmAmount += WarmSpeed * Time.deltaTime;
                _material.SetFloat("_IceSlider", WarmAmount);
            }
            else
            {
                CompletelyFrozen();
                WarmAmount = 1f;
                _material.SetFloat("_IceSlider", WarmAmount);
            }
        }

        private void WarmSelf(float warmSpeed)
        {
            WarmAmount -= warmSpeed;
            _material.SetFloat("_IceSlider", WarmAmount);

            if (WarmAmount <= 0f)
            {
                IsWarmed = true;
                WarmAmount = 0f;
                _material.SetFloat("_IceSlider", WarmAmount);
                CompletelyWarm();
            }
        }

        protected virtual void CompletelyWarm()
        {
            
        }
        
        protected virtual void CompletelyFrozen()
        {
           
        }
    }
}