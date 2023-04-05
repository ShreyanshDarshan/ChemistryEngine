using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalsList : MonoBehaviour
{
    public List<string> chemicals;
    public Color H2OColor = new Color(0.8f, 0.8f, 0.9f);
    public Color NaOHPhenolphthalineColor = new Color(0.9f, 0.3f, 0.9f);
    // Start is called before the first frame update
    void Awake()
    {
        chemicals = new List<string>();
        chemicals.Add("H2O");
        chemicals.Add("HCl");
        chemicals.Add("NaOH");
        chemicals.Add("NaCl");
        chemicals.Add("Phenolphthalein");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
