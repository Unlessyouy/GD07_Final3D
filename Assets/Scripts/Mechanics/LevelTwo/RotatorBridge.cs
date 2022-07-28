using System;
using System.Collections.Generic;
using EventClass;
using Interactable.MindPowerComponent;
using Systems;
using UnityEngine;

namespace Mechanics.LevelTwo
{
    public class RotatorBridge : MonoBehaviour
    {
        [SerializeField] private List<JellyfishLightup_MPC> JellyFishes;

        [SerializeField] private Vector3 RotateVector;
        [SerializeField] private float RotateSpeed;
        private bool _canRotate;
        
        

        private void OnEnable()
        {
            NewEventSystem.Instance.Subscribe<JellyfishLightEvent>(CheckJellyFishes);
        }

        private void OnDisable()
        {
            NewEventSystem.Instance.Unsubscribe<JellyfishLightEvent>(CheckJellyFishes);
        }

        private void Update()
        {
            if (_canRotate)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(RotateVector),
                    RotateSpeed * Time.deltaTime);
            }
        }


        private void CheckJellyFishes(JellyfishLightEvent args)
        {
            foreach (var jellyFish in JellyFishes)
            {
                if (!jellyFish._isLitUp)
                {
                    return;
                }
            }

            _canRotate = true;
        }
    }
}