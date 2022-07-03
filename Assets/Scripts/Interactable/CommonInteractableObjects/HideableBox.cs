using System.Collections;
using System.Collections.Generic;
using CharacterControl;
using UnityEngine;

public class HideableBox : InteractableObject
{
    bool isContainingSon = false;
    public GameObject interactingCharacter_Son;
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.15f);
        Gizmos.DrawSphere(transform.position, 1.875f);
    }
    protected override void Update()
    {
        base.Update();
        if (isContainingSon && interactingCharacter_Son != null)
        {
            if (Input.GetAxisRaw("Interact B") == 1)
            {
                actable = false;
                isContainingSon = false;
                interactingCharacter_Son.SetActive(true);
                interactingCharacter_Son = null;
            }
        }
    }
    public override void InteractTrigger(int interactType, GameObject interactingCharacter)
    {
        if (interactType == 2 && actable)
        {
            actable = false;
            interactingCharacter.SetActive(false);
            isContainingSon = true;
            interactingCharacter_Son = interactingCharacter;
        }
    }
}