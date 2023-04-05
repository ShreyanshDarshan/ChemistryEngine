using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositionManager : MonoBehaviour
{
    public Dictionary<string, float> composition;
    // Start is called before the first frame update
    void Start()
    {
        composition = new Dictionary<string, float>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
