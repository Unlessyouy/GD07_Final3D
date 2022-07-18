using System;
using System.Collections.Generic;
using EnemyComponents;
using EventClass;
using HackMan.Scripts;

namespace Systems
{
    public class PlayerHideSystem : Singleton<PlayerHideSystem>
    {
        private List<AnglerFishController> _anglerFishes = new List<AnglerFishController>();

        private void OnEnable()
        {
            NewEventSystem.Instance.Subscribe<PlayerHideEvent>(PlayerHide);
        }

        private void OnDisable()
        {
            NewEventSystem.Instance.Unsubscribe<PlayerHideEvent>(PlayerHide);
        }

        private void PlayerHide(PlayerHideEvent eventArgs)
        {
            foreach (var anglerFish in _anglerFishes)
            {
                anglerFish.PlayerHideInBush(eventArgs.PlayerCharacter, eventArgs.IsHiding);
            }
        }
    }
}