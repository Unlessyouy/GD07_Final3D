using System;
using System.Collections.Generic;
using System.Linq;
using EnemyComponents;
using EventClass;
using HackMan.Scripts;
using UnityEngine;

namespace Systems
{
    public class JellyfishLightSystem : Singleton<JellyfishLightSystem>
    {
        private List<OceanNightmareController> _nightmareControllers = new List<OceanNightmareController>();
        private void OnEnable()
        {
            NewEventSystem.Instance.Subscribe<JellyfishLightEvent>(LightUp);

            UpdateNightmareControllers();
        }

        private void OnDisable()
        {
            NewEventSystem.Instance.Unsubscribe<JellyfishLightEvent>(LightUp);
        }

        private void LightUp(JellyfishLightEvent eventArgs)
        {
            foreach (var nightmareController in _nightmareControllers)
            {
               nightmareController.RetreatByLit(eventArgs.JellyfishTransform);
            }
        }

        public void UpdateNightmareControllers()
        {
            _nightmareControllers = FindObjectsOfType<OceanNightmareController>().ToList();
        }
    }
}