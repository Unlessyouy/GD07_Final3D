using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    [HideInInspector]
    public bool actable = false;
    int interactType;//1 = player; 2 = companion;
    bool isOn = false;
    private void Update()
    {
        if (  (actable && Input.GetKeyDown(KeyCode.Space) && interactType == 1)  ||  (actable && Input.GetKeyDown(KeyCode.RightControl) && interactType == 2)  )
        {
            if (!isOn)
            {
                isOn = true;
                GetComponent<Animator>().Play("LightRadiusAppear");
                transform.parent.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
            else
            {
                isOn = false;
                GetComponent<Animator>().Play("LightRadiusDisappear");
                transform.parent.GetComponent<MeshRenderer>().material.color = Color.black;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControl>() != null)
        {
            actable = true;
            interactType = 1;
        }
        else if (other.GetComponent<CompanionControl>() != null)
        {
            actable = true;
            interactType = 2;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerControl>() != null)
        {
            actable = true;
            interactType = 1;
        }
        else if (other.GetComponent<CompanionControl>() != null)
        {
            actable = true;
            interactType = 2;
        }
    }
}