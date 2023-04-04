using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEye : MonoBehaviour
{
    [SerializeField] private string eyeName;
    [SerializeField] private Transform eyeTransform;
    // Start is called before the first frame update
    void Start()
    {
        eyeTransform = GameObject.Find(eyeName).transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = eyeTransform.position;
        transform.rotation = eyeTransform.rotation;
        transform.localScale = eyeTransform.localScale;
    }
}
