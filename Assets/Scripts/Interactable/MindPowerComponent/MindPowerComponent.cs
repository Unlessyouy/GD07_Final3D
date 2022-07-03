using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindPowerComponent : MonoBehaviour
{
    public virtual void MindPowerTrigger()
    {
        transform.parent.GetComponentInChildren<InteractableObject>().activatedByMP = true;
    }
}
