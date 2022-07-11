using HackMan.Scripts;
using Mechanics.Core;
using Progression;

namespace Systems
{
    public class GameStartSystem : Singleton<GameStartSystem>
    {
        private CheckpointComponent _respawnPoint;
        protected override void Awake()
        {
            base.Awake();

            var characters = FindObjectOfType<SynchronousControlSingleton>();

            var levelInfo = AppDataSystem.Load<SaveLevelInfo>("SavedLevelInfo");

            if (levelInfo != null)
            {
                var checkpoints = FindObjectsOfType<CheckpointComponent>();
                foreach (var checkpoint in checkpoints)
                {
                    if (checkpoint.BeatNumber == levelInfo.BeatNumber)
                    {
                        _respawnPoint = checkpoint;
                        checkpoint.LitUp();
                    }
                }

                if (_respawnPoint != null)
                {
                    characters.transform.position = _respawnPoint.GetRespawnPointPosition();
                }
            }
        }
    }
}