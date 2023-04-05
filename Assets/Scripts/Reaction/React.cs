using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class React : MonoBehaviour
{
    [SerializeField] CompositionManager compositionManager;
    [SerializeField] Material material;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] ChemicalsList chemicalsList;
    // Start is called before the first frame update
    void Start()
    {
        compositionManager = GetComponent<CompositionManager>();
        chemicalsList = GameObject.FindGameObjectWithTag("ChemicalsList").GetComponent<ChemicalsList>();
        meshRenderer = GetComponent<MeshRenderer>();
        material = new Material(meshRenderer.material);
    }

    // Update is called once per frame
    void Update()
    {
        if (compositionManager.composition["HCl"] > 0 && compositionManager.composition["NaOH"] > 0)
        {
            if (compositionManager.composition["HCl"] > compositionManager.composition["NaOH"])
            {
                compositionManager.composition["NaOH"] = 0;
                compositionManager.composition["HCl"] -= compositionManager.composition["NaOH"];
                compositionManager.composition["NaCl"] += compositionManager.composition["NaOH"];
                compositionManager.composition["H2O"] += compositionManager.composition["NaOH"];
            }
            else
            {
                compositionManager.composition["HCl"] = 0;
                compositionManager.composition["NaOH"] -= compositionManager.composition["HCl"];
                compositionManager.composition["NaCl"] += compositionManager.composition["HCl"];
                compositionManager.composition["H2O"] += compositionManager.composition["HCl"];
            }
        }

        if (compositionManager.composition["NaOH"] > 0 && compositionManager.composition["Phenolphthalein"] > 0)
        {
            // material.CopyPropertiesFromMaterial(meshRenderer.material);
            material.color = chemicalsList.NaOHPhenolphthalineColor;//Color.Lerp(chemicalsList.H2OColor, chemicalsList.NaOHPhenolphthalineColor, compositionManager.composition["NaOH"]) / compositionManager.volumeCapacity;
            meshRenderer.material = material;
        }


    }
}
