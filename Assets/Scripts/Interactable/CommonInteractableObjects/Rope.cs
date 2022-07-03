using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : InteractableObject
{
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.15f);
        Gizmos.DrawSphere(transform.position, 1.25f);
    }
    public override void InteractTrigger(int interactType, GameObject interactingCharacter)
    {
        if (actable)
        {
            transform.parent.parent.GetChild(1).gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
