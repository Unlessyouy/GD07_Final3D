using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDownEnd : InteractableObject
{
    BasicControl climber;
    protected override void Start()
    {
        base.Start();
        actable = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.15f);
        Gizmos.DrawSphere(transform.position, 2f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BasicControl>() != null)
        {
            climber = other.GetComponent<BasicControl>();
            if (climber != null && climber.isClimbing == true)
            {
                climber.isClimbing = false;
                other.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
    public override void InteractTrigger(int interactType, GameObject interactingCharacter)
    {
        if (actable)
        {
            climber = interactingCharacter.GetComponent<BasicControl>();

            if (!climber.isClimbing)
            {
                climber.isClimbing = true;
                climber.GetComponent<Rigidbody>().useGravity = false;
                climber.transform.position = transform.position;
            }
        }
    }
}