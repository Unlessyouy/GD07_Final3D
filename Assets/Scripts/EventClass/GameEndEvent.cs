namespace EventClass
{
    public class GameEndEvent
    {
        public bool IsGameOver { get;}

        public GameEndEvent(bool isGameOver)
        {
            this.IsGameOver = isGameOver;
        }
    }
}