using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverflowDetector : MonoBehaviour
{
    public Transform surface;
    public float distanceFromSurface;
    // public bool isOverflowing;
    // Start is called before the first frame update

    void Start()
    {
        // isOverflowing = false;
    }

    // Update is called once per frame
    void Update()
    {
        Plane plane = new Plane(surface.forward, surface.position);
        distanceFromSurface = plane.GetDistanceToPoint(transform.position);
        // distanceFromSurface 
        // if (distance > 0)
        // {
        //     isOverflowing = true;
        // }
        // else
        // {
        //     isOverflowing = false;
        // }
    }
}
