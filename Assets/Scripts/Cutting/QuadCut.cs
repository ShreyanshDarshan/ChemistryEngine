using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadCut : MonoBehaviour
{
    public GameObject cut_base;
    public GameObject cut_piece;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        Debug.Log("Cutting");
        Cutter.Cut(cut_base, cut_piece, transform.position, transform.forward);
        // }
    }
}
