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
    protected override void Update()
    {
        base.Update();
        if (actable && canBeActed)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || interactInput == 1) && interactType == 1)
            {
                transform.parent.parent.GetChild(1).gameObject.SetActive(true);
                Destroy(gameObject);
                if (interactedObject.GetComponent<Animator>() != null)
                {
                    interactedObject.GetComponent<Animator>().SetBool("isInteracting", true);
                    Invoke("ExitInteracting", interactTime);
                }
            }
            //else if ((Input.GetKeyDown(KeyCode.RightControl) || interactInput == 1) && interactType == 2)
            //{
            //    transform.parent.parent.GetChild(1).gameObject.SetActive(true);
            //    Destroy(gameObject);
            //}
        }
    }
}
