using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasicControl : MonoBehaviour
{
    protected Rigidbody rb;
    public float movingSpeed;
    protected float horizontalInput;
    protected float verticalInput;

    protected float lightValue = 100;

    public bool alive;
    public bool connected;
    public bool lightNear;

    protected virtual void Start()
    {
        alive = true;
        connected = false;
        lightNear = false;
    }
    protected virtual void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");//×óÓÒ£¬×ó-1£¬ÓÒ1
        verticalInput = Input.GetAxisRaw("Vertical");//Ç°ºó

        if (alive)
        {
            if (lightNear)
            {
                lightValue += 1f * Time.deltaTime;
            }
            else if (connected)
            {
                lightValue -= 0.25f * Time.deltaTime;
            }
            else
            {
                lightValue -= 1f * Time.deltaTime;
            }

            if (lightValue >= 100)
            {
                lightValue = 100;
            }
            if (lightValue <= 0)
            {
                alive = false;
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
    public float CheckAngle(float Value)
    {
        float Angle = Value - 180;
        if (Angle > 0)
        {
            return Angle - 180;
        }
        return Angle + 180;
    }
}