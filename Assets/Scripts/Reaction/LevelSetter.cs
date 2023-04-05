using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetter : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    // [SerializeField] private GameObject top;
    // [SerializeField] private GameObject bottom;
    [SerializeField] private Transform surface;

    [SerializeField] private CompositionManager compositionManager;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        compositionManager = GetComponent<CompositionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] vertices = meshFilter.mesh.vertices;
        Vector3 topmostVertex = transform.TransformPoint(vertices[0]);
        Vector3 bottommostVertex = transform.TransformPoint(vertices[0]);
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = transform.TransformPoint(vertices[i]);
            if (vertices[i].y > topmostVertex.y)
            {
                topmostVertex = vertices[i];
            }
            if (vertices[i].y < bottommostVertex.y)
            {
                bottommostVertex = vertices[i];
            }
        }

        surface.position = Vector3.Lerp(bottommostVertex, topmostVertex, compositionManager.volumeOccupied / compositionManager.volumeCapacity);
    }

}
