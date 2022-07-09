using System;
using EventClass;
using HackMan.Scripts;

namespace Systems
{
    public class SwitchTriggerSystem : Singleton<SwitchTriggerSystem>
    {
        private void OnEnable()
        {
            NewEventSystem.Instance.Subscribe<SwitchTriggerEvent>(OnSwitchTrigger);
        }

        private void OnDisable()
        {
            NewEventSystem.Instance.Unsubscribe<SwitchTriggerEvent>(OnSwitchTrigger);
        }

        private void OnSwitchTrigger(SwitchTriggerEvent switchTriggerArgs)
        {
            foreach (var triggerObject in switchTriggerArgs.TriggerObjects)
            {
                triggerObject.OnActivated();
            }
        }
    }
}