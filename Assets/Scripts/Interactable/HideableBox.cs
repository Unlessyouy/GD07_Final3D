using System.Collections;
using System.Collections.Generic;
using CharacterControl;
using UnityEngine;

public class HideableBox : InteractableObject
{
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.15f);
        Gizmos.DrawSphere(transform.position, 1.875f);
    }
    protected override void Update()
    {
        base.Update();
        if (actable && canBeActed && interactedObject.GetComponent<CompanionControl>() != null)
        {
            CompanionControl companion = interactedObject.GetComponent<CompanionControl>();
            if ((Input.GetKeyDown(KeyCode.RightControl) || interactInput == 1) && interactType == 2)
            {
                canBeActed = false;
                if (!companion.hiding)
                {
                    companion.hiding = true;
                    interactedObject.SetActive(false);
                }
                else
                {
                    companion.hiding = false;
                    interactedObject.SetActive(true);
                }
            }
        }
    }
}