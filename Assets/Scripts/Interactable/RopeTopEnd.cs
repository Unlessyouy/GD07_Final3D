using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTopEnd : Interactable
{
    BasicControl climber;
    [SerializeField] private int TopForce = 600;

    private void Start()
    {
        canBeActed = false;
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

        if (actable && canBeActed && !climber.isClimbing)
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
            climber.onRopeTop = true;
            if (climber != null && climber.isClimbing == true)
            {
                climber.isClimbing = false;
                interactedObject.GetComponent<Rigidbody>().useGravity = true;
                interactedObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, TopForce, 0));
            }
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.GetComponent<BasicControl>() != null)
        {
            other.GetComponent<BasicControl>().onRopeTop = false;
        }
    }
}