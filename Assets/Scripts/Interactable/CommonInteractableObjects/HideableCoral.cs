using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableCoral : InteractableObject
{
    bool isContainingSon = false;
    bool isContainingFather = false;
    public GameObject interactingCharacter_Son;
    public GameObject interactingCharacter_Father;
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.15f);
        Gizmos.DrawSphere(transform.position, 1.5f);
    }
    protected override void Update()
    {
        base.Update();

        if (isContainingSon && interactingCharacter_Son != null)
        {
            if (Input.GetAxisRaw("Interact B") == 0 && actable)
            {
                actable = false;
                isContainingSon = false;
                interactingCharacter_Son.SetActive(true);
                interactingCharacter_Son = null;
            }
        }

        if (isContainingFather && interactingCharacter_Father != null)
        {
            if (Input.GetAxisRaw("Interact") == 0 && actable)
            {
                actable = false;
                isContainingFather = false;
                interactingCharacter_Father.SetActive(true);
                interactingCharacter_Father = null;
            }
        }

        if (activatedTimer / 2 >= 1 && activatedByMP)
        {
            transform.parent.GetChild(0).transform.localScale = new Vector3(activatedTimer, activatedTimer, activatedTimer) / 2;
        }
        else
        {
            transform.parent.GetChild(0).transform.localScale = Vector3.one;
        }
    }
    public override void InteractTrigger(int interactType, GameObject interactingCharacter)
    {
        if (actable && activatedByMP && interactingCharacter.GetComponent<BasicControl>().isInteracting)
        {
            if (interactType == 1)
            {
                actable = false;
                interactingCharacter.SetActive(false);
                isContainingFather = true;
                interactingCharacter_Father = interactingCharacter;
            }
            else if (interactType == 2)
            {
                actable = false;
                interactingCharacter.SetActive(false);
                isContainingSon = true;
                interactingCharacter_Son = interactingCharacter;
            }
        }
    }
}
