using System.Collections.Generic;
using Mechanics;

namespace EventClass
{
    public class SwitchTriggerEvent
    {
        public List<TriggerObject> TriggerObjects { get;}

        public SwitchTriggerEvent(List<TriggerObject> triggerObjects)
        {
            this.TriggerObjects = triggerObjects;
        }
    }
}