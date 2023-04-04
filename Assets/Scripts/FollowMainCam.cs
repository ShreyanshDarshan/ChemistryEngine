using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainCam : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainCam.transform.position;
        transform.rotation = mainCam.transform.rotation;
        transform.localScale = mainCam.transform.localScale;
    }
}
