using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDownEnd : InteractableObject
{
    BasicControl climber;
    private void Start()
    {
        canBeActed = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.15f);
        Gizmos.DrawSphere(transform.position, 2f);
    }
    protected override void Update()
    {
        base.Update();

        if (interactedObject != null && interactedObject.GetComponent<BasicControl>() != null)
        {
            climber = interactedObject.GetComponent<BasicControl>();
        }

        if (actable && canBeActed && !climber.isClimbing && climber != null)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || interactInput == 1) && interactType == 1)
            {
                climber.isClimbing = true;
                climber.GetComponent<Rigidbody>().useGravity = false;
                climber.transform.position = transform.position;
            }
            else if ((Input.GetKeyDown(KeyCode.RightControl) || interactInput == 1) && interactType == 2)
            {
                climber.isClimbing = true;
                climber.GetComponent<Rigidbody>().useGravity = false;
                climber.transform.position = transform.position;
            }
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.GetComponent<BasicControl>() != null)
        {
            climber = other.GetComponent<BasicControl>();
            if (climber != null && climber.isClimbing == true)
            {
                climber.isClimbing = false;
                interactedObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}