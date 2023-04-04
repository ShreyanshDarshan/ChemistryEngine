using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeXRCam : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] Camera currentCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        currentCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        currentCam.fieldOfView = mainCam.fieldOfView;
        // Debug.Log("Focal Length: " + currentCam.focalLength);
        // Debug.Log("Focal Length Main: " + mainCam.focalLength);
    }
}
