using System.Collections;
using System.Collections.Generic;
using CharacterControl;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    protected bool actable;
    protected float actableCoolDownTimer = 0;
    public float actableCoolDown;

    public bool needMP;

    public bool activatedByMP;
    protected float activatedTimer;
    public float activatedTime;

    protected virtual void Start()
    {
        activatedTimer = activatedTime;
    }
    protected virtual void Update()
    {
        if (!actable)
        {
            actableCoolDownTimer += Time.deltaTime;
            if (actableCoolDownTimer >= actableCoolDown)
            {
                actable = true;
                actableCoolDownTimer = 0;
            }
        }

        if (needMP && activatedByMP)
        {
            activatedTimer -= Time.deltaTime;
            if (activatedTimer <= 0)
            {
                activatedByMP = false;
                activatedTimer = activatedTime;
            }
        }
    }
    public virtual void InteractTrigger(int interactType, GameObject interactingCharacter)
    {

    }
}