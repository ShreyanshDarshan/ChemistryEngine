using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMaskCreator : MonoBehaviour
{
    [SerializeField] private MeshFilter fluid_surface;
    [SerializeField] private MeshFilter mesh_filter;
    [SerializeField] private bool flipped = false;
    // Start is called before the first frame update
    void Start()
    {
        mesh_filter = GetComponent<MeshFilter>();
        // create a cube mesh
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[8];
        vertices[0] = new Vector3(-1, -1, -1);
        vertices[1] = new Vector3(-1, -1, 1);
        vertices[2] = new Vector3(-1, 1, -1);
        vertices[3] = new Vector3(-1, 1, 1);
        vertices[4] = new Vector3(1, -1, -1);
        vertices[5] = new Vector3(1, -1, 1);
        vertices[6] = new Vector3(1, 1, -1);
        vertices[7] = new Vector3(1, 1, 1);
        mesh.vertices = vertices;
        int[] triangles = new int[36] { 0, 1, 2, 1, 3, 2, 4, 6, 5, 5, 6, 7, 0, 2, 4, 4, 2, 6, 1, 5, 3, 5, 7, 3, 0, 4, 1, 4, 5, 1, 2, 3, 6, 6, 3, 7 };
        if (flipped)
        {
            for (int i = 0; i < triangles.Length; i += 3)
            {
                int temp = triangles[i + 0];
                triangles[i + 0] = triangles[i + 1];
                triangles[i + 1] = temp;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh_filter.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        // set first 4 vertices of cube to same location as vertices of quad
        Vector3[] vertices = mesh_filter.mesh.vertices;
        vertices[0] = fluid_surface.mesh.vertices[0];
        vertices[1] = fluid_surface.mesh.vertices[1];
        vertices[2] = fluid_surface.mesh.vertices[2];
        vertices[3] = fluid_surface.mesh.vertices[3];

        // set the rest 4 vertices to an offset of the first 4 vertices
        Vector3 offset = new Vector3(0, 0, -1f);
        vertices[4] = vertices[0] + offset;
        vertices[5] = vertices[1] + offset;
        vertices[6] = vertices[2] + offset;
        vertices[7] = vertices[3] + offset;
        mesh_filter.mesh.vertices = vertices;

    }
}
