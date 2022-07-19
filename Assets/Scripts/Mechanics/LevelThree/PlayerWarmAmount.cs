using System;
using System.Collections;
using EventClass;
using Interactable.MindPowerComponent;
using Systems;
using UnityEngine;

namespace Mechanics.LevelThree
{
    public class PlayerWarmAmount : MonoBehaviour
    {
        [Range(0, 1)] [SerializeField] protected float WarmSpeed = 0.1f;

        private float _warmAmount = 0f;

        public bool IsWarmed { get; set; }
        
        private bool IsFrozen;

        private float _timer = .0f;

        [SerializeField] private SkinnedMeshRenderer SkinnedMeshRenderer;
        private Material _material;
        private Animator _anim;

        private void Start()
        {
            _material = SkinnedMeshRenderer.material;
            _anim = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (IsFrozen) return;
            if (IsWarmed)
            {
                WarmSelf();
            }
            else
            {
                FreezeSelf();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            var fireBrazier = other.GetComponent<FireBrazier>();
            if (!fireBrazier) return;
            IsWarmed = fireBrazier.IsFire;
        }

        private void OnTriggerExit(Collider other)
        {
            var fireBrazier = other.GetComponent<FireBrazier>();
            if (!fireBrazier) return;
            IsWarmed = false;
        }

        private void FreezeSelf()
        {
            if (_warmAmount < 1f)
            {
                _warmAmount += WarmSpeed * Time.deltaTime;
                _material.SetFloat("_IceSlider", _warmAmount);
            }
            else if (!IsFrozen)
            {
                CompletelyFrozen();
                _warmAmount = 1f;
                _material.SetFloat("_IceSlider", _warmAmount);
            }
        }

        private void WarmSelf()
        {
            if (_warmAmount > 0f)
            {
                _warmAmount -= 5f * WarmSpeed * Time.deltaTime;
                _material.SetFloat("_IceSlider", _warmAmount);
            }

            if (!(_warmAmount <= 0f)) return;
            IsWarmed = true;
            _warmAmount = 0f;
            _material.SetFloat("_IceSlider", _warmAmount);
            CompletelyWarm();
        }

        private void CompletelyWarm()
        {
        }

        private void CompletelyFrozen()
        {
            StartCoroutine(StartFrozen());
        }

        private IEnumerator StartFrozen()
        {
            IsFrozen = true;
            SynchronousControlSingleton.Instance.Freeze();

            var temps = _anim.GetCurrentAnimatorClipInfo(0);
            var clipInfo = new AnimatorClipInfo();
            if (temps.Length > 0)
            {
                clipInfo = temps[0];
            }

            _anim.Play(clipInfo.clip.name, 0, 0);
            _anim.speed = 0;
            
            yield return new WaitForSeconds(2f);

            NewEventSystem.Instance.Publish(new GameEndEvent(true));
        }
    }
}