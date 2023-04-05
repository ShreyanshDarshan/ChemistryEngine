using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalsList : MonoBehaviour
{
    public List<string> chemicals;
    // Start is called before the first frame update
    void Awake()
    {
        chemicals = new List<string>();
        chemicals.Add("H2O");
        chemicals.Add("HCl");
        chemicals.Add("NaOH");
        chemicals.Add("NaCl");
        chemicals.Add("Phenolphtalein");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
