using UnityEngine;

namespace EventClass
{
    public class JellyfishLightEvent
    {
        public Transform JellyfishTransform { get; set; }

        public JellyfishLightEvent(Transform jellyfishTransform)
        {
            JellyfishTransform = jellyfishTransform;
        }
    }
}