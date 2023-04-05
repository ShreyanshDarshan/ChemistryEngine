using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositionManager : MonoBehaviour
{
    public Dictionary<string, float> composition;
    public float volumeCapacity = 0.0f;
    public float volumeOccupied = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
        Bounds bounds = renderer.bounds;
        volumeCapacity = bounds.size.x * bounds.size.y * bounds.size.z;
        renderer.enabled = false;

        composition = new Dictionary<string, float>();
        ChemicalsList chemicalsList = GameObject.FindGameObjectWithTag("ChemicalsList").GetComponent<ChemicalsList>();
        foreach (string chemical in chemicalsList.chemicals)
        {
            composition.Add(chemical, 0.0f);
        }
        AddChemical[] chemicals = GetComponents<AddChemical>();
        foreach (AddChemical chemical in chemicals)
        {
            AddChemical(chemical.chemical, chemical.volumePercentage * volumeCapacity / 100.0f);
        }
    }

    public void AddChemical(string chemical, float volume)
    {
        // Debug.Log(chemical.chemical + " " + chemical.volumePercentage);
        composition[chemical] += volume;
        volumeOccupied += volume;
    }

    public void RemoveChemical(string chemical, float volume)
    {
        composition[chemical] -= volume;
        volumeOccupied -= volume;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
