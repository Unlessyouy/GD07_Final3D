using System;
using System.Collections;
using Interactable.MindPowerComponent;
using Systems;
using UnityEngine;

namespace Mechanics.LevelThree
{
    public class DangerousWind : MonoBehaviour
    {
        // [SerializeField] private Transform StartPoint;
        // [SerializeField] private Transform EndPoint;
        //
        // [SerializeField] private PlayerWarmAmount Son;
        // [SerializeField] private PlayerWarmAmount Father;

        [SerializeField] private float BlowCoolDown = 12f;
        [SerializeField] private float PreTime = 3f;
        [SerializeField] private float LastTime = 8f;
        
        [SerializeField] private GameObject PreSetActive;
        [SerializeField] private GameObject BlowSetActive;

        private float _preTimer = Mathf.Infinity;
        private float _timer = Mathf.Infinity;

        private bool _isBlowing;
        
        // private Transform _sonsTransform;
        // private Transform _fatherTransform;

        // private void Start()
        // {
        //     _sonsTransform = SynchronousControlSingleton.Instance.GetSonTrans();
        //     _fatherTransform = SynchronousControlSingleton.Instance.GetFatherTrans();
        // }

        private void Update()
        {
            _timer += Time.deltaTime;
            _preTimer += Time.deltaTime;

            if (_preTimer >= BlowCoolDown - PreTime)
            {
                StartCoroutine(StartPreBlow());
                _preTimer = 0f;
            }
            
            if (_timer < BlowCoolDown) return;

            StartCoroutine(Blow());
            _timer = 0f;
            _preTimer = 0f;
        }

        private IEnumerator Blow()
        { 
            BlowSetActive.SetActive(true);

            var t = 0f;

            while (t <= 1f)
            {
                t += Time.deltaTime / LastTime;
                
                // if (IsInRange(_sonsTransform))
                // {
                //     Son.IsInWind = IsInWind(_sonsTransform);
                // }
                //
                // if (IsInRange(_fatherTransform))
                // {
                //     Son.IsInWind = IsInWind(_fatherTransform);
                // }
                _isBlowing = true;

                yield return null;
            }
            
            BlowSetActive.SetActive(false);
            _isBlowing = false;
            
            yield return null;
        }

        private IEnumerator StartPreBlow()
        { 
            PreSetActive.SetActive(true);

            yield return new WaitForSeconds(LastTime);
            
            PreSetActive.SetActive(false);
        }

        private void OnTriggerStay(Collider other)
        {
            if(!_isBlowing) return;
            var character = other.GetComponent<PlayerWarmAmount>();
            if (character && !character.IsInShelter)
            {
                character.IsInWind = true;
            }

            var fireBrazier = other.GetComponent<FireBrazier_MPC>();
            if (fireBrazier && !fireBrazier.IsInShelter)
            {
                fireBrazier.ExtinguishFire();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var character = other.GetComponent<PlayerWarmAmount>();
            if (character)
            {
                character.IsInWind = false;
            }
        }


        // private bool IsInRange(Transform targetTrans)
        // {
        //     return targetTrans.position.x >= StartPoint.position.x && targetTrans.position.x <= EndPoint.position.x;
        // }

        // private bool IsInWind(Transform targetTrans)
        // {
        //     var startPoint = new Vector3(targetTrans.position.x, targetTrans.position.y, transform.position.z);
        //     var direction = (targetTrans.position - startPoint).normalized;
        //     var ray = new Ray(startPoint, direction);
        //     Debug.DrawRay(startPoint, direction);
        //
        //     if (Physics.Raycast(ray, out var hitInfo))
        //     {
        //         return hitInfo.collider.CompareTag("Player");
        //     }
        //
        //     return false;
        // }
    }
}