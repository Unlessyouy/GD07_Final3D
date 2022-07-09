using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapSetActive : MonoBehaviour
{
    [SerializeField] private GameObject GoToActive;
    [SerializeField] private bool IsActive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BasicControl>())
        {
            GoToActive.SetActive(IsActive);
        }
    }
}