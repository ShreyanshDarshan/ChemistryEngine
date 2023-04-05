using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayLevel : MonoBehaviour
{
    Quaternion initialRotation;
    Vector3 initalScale;
    // Quaternion initialParentRotation;
    // Start is called before the first frame update
    void Start()
    {
        // get initial rotation
        initialRotation = transform.rotation;
        initalScale = transform.localScale;
        // get initial parent rotation
        // initialParentRotation = transform.parent.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // get current parent rotation wrt initial parent transform
        // Quaternion parentRotation = Quaternion.Inverse(initialParentRotation) * transform.parent.rotation;
        // freeze rotation in z direction wrt non local initial transform
        transform.rotation = initialRotation;
        transform.localScale = initalScale;
        // Debug.Log(initialParentRotation.eulerAngles);
    }
}
