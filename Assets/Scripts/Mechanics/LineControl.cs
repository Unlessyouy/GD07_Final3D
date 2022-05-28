using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControl : MonoBehaviour
{
    EdgeCollider2D edgeCollider;
    LineRenderer lineRenderer;
    private void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {

    }
}
