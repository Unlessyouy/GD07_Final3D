using Mechanics.Core;

namespace Progression
{
    public class SaveLevelInfo
    {
        public string LevelName { get; }
        public int BeatNumber { get; }

        public SaveLevelInfo(string levelName, int beatNumber)
        {
            LevelName = levelName;
            BeatNumber = beatNumber;
        }
    }
}