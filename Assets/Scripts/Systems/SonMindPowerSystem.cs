using System;
using System.Collections.Generic;
using System.Linq;
using EventClass;
using HackMan.Scripts;
using Interactable.MindPowerComponent;

namespace Systems
{
    public class SonMindPowerSystem : Singleton<SonMindPowerSystem>
    {
        private List<EventMPCBase> _eventMPCObjects = new List<EventMPCBase>();

        private void OnEnable()
        {
            NewEventSystem.Instance.Subscribe<MindPowerEvent>(OnSonMindPower);
            _eventMPCObjects = FindObjectsOfType<EventMPCBase>().ToList();
        }

        private void OnDisable()
        {
            NewEventSystem.Instance.Unsubscribe<MindPowerEvent>(OnSonMindPower);
        }

        private void OnSonMindPower(MindPowerEvent eventArgs)
        {
            foreach (var eventMpcObject in _eventMPCObjects)
            {
                eventMpcObject.OnMentalPowerActivate(eventArgs.SonTransform);
            }
        }
    }
}