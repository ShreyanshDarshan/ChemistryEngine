using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FluidRenderer : MonoBehaviour
{
    [SerializeField] private MeshFilter fluid_mesh;
    [SerializeField] private Material fluid_material;
    public RenderTexture fluidTexture;
    public RenderTexture fluidTextureDepth;
    [SerializeField] private Material depth_material;
    [SerializeField] private LayerMask fluidLayer;
    private int layer_value;
    private Camera fluidCamera;
    // Start is called before the first frame update
    void Start()
    {
        layer_value = (int)Mathf.Log(fluidLayer.value, 2f);
        fluidCamera = GetComponent<Camera>();
        // int height = XRSettings.eyeTextureHeight;
        // int width = XRSettings.eyeTextureWidth;
        fluidTexture = new RenderTexture(Screen.width, Screen.height, 32, RenderTextureFormat.ARGBFloat);
        fluidTextureDepth = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGBFloat);
    }

    void InitializeTextures()
    {
        int height = XRSettings.eyeTextureHeight;
        int width = XRSettings.eyeTextureWidth;
        fluidTexture = new RenderTexture(width, height, 32, RenderTextureFormat.ARGBFloat);
        fluidTextureDepth = new RenderTexture(width, height, 0, RenderTextureFormat.ARGBFloat);
        fluidTexture.filterMode = FilterMode.Point;
        fluidTextureDepth.filterMode = FilterMode.Point;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Fluid Texture Width: " + fluidTexture.width);
        Debug.Log("Fluid Texture Height: " + fluidTexture.height);
        if (XRSettings.eyeTextureWidth != fluidTexture.width || XRSettings.eyeTextureHeight != fluidTexture.height)
        {
            if (XRSettings.eyeTextureWidth > 0 && XRSettings.eyeTextureHeight > 0)
            {
                fluidTexture.Release();
                fluidTextureDepth.Release();
                InitializeTextures();
            }
        }
        fluidCamera.targetTexture = fluidTexture;
        Graphics.DrawMesh(fluid_mesh.mesh, fluid_mesh.transform.localToWorldMatrix, fluid_material, layer_value, fluidCamera);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(fluidTexture, fluidTextureDepth, depth_material);
        Graphics.Blit(source, destination);
    }
}
