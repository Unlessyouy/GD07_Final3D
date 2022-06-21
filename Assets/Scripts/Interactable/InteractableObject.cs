using System.Collections;
using System.Collections.Generic;
using CharacterControl;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [HideInInspector]
    public bool actable;
    protected bool canBeActed;
    protected float actableCoolDown;
    protected int interactType;//1 = player; 2 = companion;
    protected GameObject interactedObject;
    protected float interactInput;
    protected float interactTime = 0.5f;
    protected virtual void Update()
    {
        if (!canBeActed)
        {
            actableCoolDown += Time.deltaTime;
            if (actableCoolDown >= 0.2f)
            {
                canBeActed = true;
                actableCoolDown = 0f;
            }
        }

        if (interactedObject != null)
        {
            if (interactedObject.GetComponent<PlayerControl>() != null)
            {
                interactInput = Input.GetAxis("Interact");
            }
            else if (interactedObject.GetComponent<CompanionControl>() != null)
            {
                interactInput = Input.GetAxis("Interact B");
            }
            else
            {
                interactInput = 0;
            }
        }
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControl>() != null)
        {
            other.GetComponent<PlayerControl>().canInteract = true;
            actable = true;
            interactType = 1;
            interactedObject = other.gameObject;
        }
        else if (other.GetComponent<CompanionControl>() != null)
        {
            actable = true;
            interactType = 2;
            interactedObject = other.gameObject;
        }
    }
    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerControl>() != null)
        {
            other.GetComponent<PlayerControl>().canInteract = true;
            actable = true;
            interactType = 1;
            interactedObject = other.gameObject;
        }
        else if (other.GetComponent<CompanionControl>() != null)
        {
            actable = true;
            interactType = 2;
            interactedObject = other.gameObject;
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerControl>() != null)
        {
            other.GetComponent<PlayerControl>().canInteract = false;
            actable = false;
            interactType = 0;
            interactedObject = null;
        }
        else if (other.GetComponent<CompanionControl>() != null)
        {
            actable = false;
            interactType = 0;
            interactedObject = null;
        }
    }
    protected void ExitInteracting()
    {
        if (interactedObject.GetComponent<Animator>() != null)
        {
            interactedObject.GetComponent<Animator>().SetBool("isInteracting", false);
        }
    }
}