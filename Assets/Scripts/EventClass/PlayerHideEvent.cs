namespace EventClass
{
    public class PlayerHideEvent
    {
        public BasicControl PlayerCharacter { get; set; }
        public bool IsHiding { get; set; }

        public PlayerHideEvent(BasicControl playerCharacter, bool isHiding)
        {
            PlayerCharacter = playerCharacter;
            IsHiding = isHiding;
        }
    }
}