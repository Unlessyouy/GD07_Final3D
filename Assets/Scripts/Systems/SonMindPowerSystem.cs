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
        private List<JellyfishLightup_MPC> _jellyfishes = new List<JellyfishLightup_MPC>();

        private void OnEnable()
        {
            NewEventSystem.Instance.Subscribe<MindPowerEvent>(OnSonMindPower);
            _jellyfishes = FindObjectsOfType<JellyfishLightup_MPC>().ToList();
        }

        private void OnDisable()
        {
            NewEventSystem.Instance.Unsubscribe<MindPowerEvent>(OnSonMindPower);
        }

        private void OnSonMindPower(MindPowerEvent eventArgs)
        {
            foreach (var jellyfish in _jellyfishes)
            {
                jellyfish.OnMentalPowerActivate(eventArgs.SonTransform);
            }
        }
    }
}