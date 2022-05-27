using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasicControl : MonoBehaviour
{
    protected Rigidbody rb;
    protected Animator anim;
    public float movingSpeed;
    protected float horizontalInput;
    protected float verticalInput;

    protected float lightValue = 100;

    public bool alive;
    public bool connected;
    public bool lightNear;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        alive = true;
        connected = true;
        lightNear = false;
    }
    protected virtual void Update()
    {
        if (alive)
        {
            if (lightNear)
            {
                lightValue += 1f * Time.deltaTime;
            }
            else if (connected)
            {
                lightValue -= 1f * Time.deltaTime;
            }
            else
            {
                lightValue -= 5f * Time.deltaTime;
            }

            if (lightValue >= 100)
            {
                lightValue = 100;
            }
            if (lightValue <= 0)
            {
                alive = false;
                connected = false;
                GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
        //Debug.Log(lightValue);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<LightRadius>() != null)
        {
            lightNear = true;
        }
    }
    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<LightRadius>() != null)
        {
            lightNear = true;
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<LightRadius>() != null)
        {
            lightNear = false;
        }
    }
    protected float CheckAngle(float Value)
    {
        float Angle = Value - 180;
        if (Angle > 0)
        {
            return Angle - 180;
        }
        return Angle + 180;
    }
}