using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    [HideInInspector]
    public bool actable = false;
    bool isOn = false;
    private void Update()
    {
        if (actable && Input.GetKeyDown(KeyCode.E))
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
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerControl>() != null)
        {
            actable = false;
        }
    }
}