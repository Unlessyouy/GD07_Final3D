using UnityEngine;

namespace EventClass
{
    public class MindPowerEvent
    {
        public Transform SonTransform { get; set; }

        public MindPowerEvent(Transform sonTransform)
        {
            SonTransform = sonTransform;
        }
    }
}