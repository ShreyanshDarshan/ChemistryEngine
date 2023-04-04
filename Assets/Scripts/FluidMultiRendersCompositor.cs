using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FluidMultiRendersCompositor : MonoBehaviour
{
    [SerializeField] private Material composite_material;
    [SerializeField] private Material overlay_material;

    [SerializeField] private bool LeftEyeRendering = true;
    // [SerializeField] private RenderTexture composite_texture;
    // Start is called before the first frame update
    void Start()
    {
        LeftEyeRendering = true;
        // composite_texture = new RenderTexture(Screen.width, Screen.height, 32, RenderTextureFormat.ARGBFloat);
    }

    void OnPreRender()
    {
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        LeftEyeRendering = !LeftEyeRendering;
        Debug.Log("OnPreRender: " + LeftEyeRendering);
        overlay_material.SetFloat("_LeftOrRight", LeftEyeRendering ? 0 : 1);
        string CameraChildName = LeftEyeRendering ? "LeftCameras" : "RightCameras";

        RenderTexture final_composite = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.ARGBFloat);
        final_composite.filterMode = FilterMode.Point;

        GameObject[] fluid_objects = GameObject.FindGameObjectsWithTag("Fluid");
        Graphics.Blit(source, final_composite, overlay_material);
        Graphics.Blit(final_composite, source);
        for (int i = fluid_objects.Length - 1; i >= 0; i--)
        {
            FluidRenderer Body, BodyBack, Cube, CubeBack, Surface;
            Body = fluid_objects[i].transform.Find("LeftCameras").Find("BodyCamera").GetComponent<FluidRenderer>();
            BodyBack = fluid_objects[i].transform.Find("LeftCameras").Find("BodyBackCamera").GetComponent<FluidRenderer>();
            Cube = fluid_objects[i].transform.Find("LeftCameras").Find("CubeCamera").GetComponent<FluidRenderer>();
            CubeBack = fluid_objects[i].transform.Find("LeftCameras").Find("CubeBackCamera").GetComponent<FluidRenderer>();
            Surface = fluid_objects[i].transform.Find("LeftCameras").Find("SurfaceCamera").GetComponent<FluidRenderer>();
            composite_material.SetTexture("_DepthTex", Body.fluidTextureDepth);
            composite_material.SetTexture("_SurfaceTex", Surface.fluidTexture);
            composite_material.SetTexture("_SurfaceDepthTex", Surface.fluidTextureDepth);
            composite_material.SetTexture("_BackDepthTex", BodyBack.fluidTextureDepth);
            composite_material.SetTexture("_CubeDepthTex", Cube.fluidTextureDepth);
            composite_material.SetTexture("_CubeBackDepthTex", CubeBack.fluidTextureDepth);
            RenderTexture left_composite_texture = RenderTexture.GetTemporary(Screen.width, Screen.height, 32, RenderTextureFormat.ARGBFloat);
            left_composite_texture.filterMode = FilterMode.Point;
            Graphics.Blit(Body.fluidTexture, left_composite_texture, composite_material);
            overlay_material.SetTexture("_LeftFluidTex", left_composite_texture);

            Body = fluid_objects[i].transform.Find("RightCameras").Find("BodyCamera").GetComponent<FluidRenderer>();
            BodyBack = fluid_objects[i].transform.Find("RightCameras").Find("BodyBackCamera").GetComponent<FluidRenderer>();
            Cube = fluid_objects[i].transform.Find("RightCameras").Find("CubeCamera").GetComponent<FluidRenderer>();
            CubeBack = fluid_objects[i].transform.Find("RightCameras").Find("CubeBackCamera").GetComponent<FluidRenderer>();
            Surface = fluid_objects[i].transform.Find("RightCameras").Find("SurfaceCamera").GetComponent<FluidRenderer>();
            composite_material.SetTexture("_DepthTex", Body.fluidTextureDepth);
            composite_material.SetTexture("_SurfaceTex", Surface.fluidTexture);
            composite_material.SetTexture("_SurfaceDepthTex", Surface.fluidTextureDepth);
            composite_material.SetTexture("_BackDepthTex", BodyBack.fluidTextureDepth);
            composite_material.SetTexture("_CubeDepthTex", Cube.fluidTextureDepth);
            composite_material.SetTexture("_CubeBackDepthTex", CubeBack.fluidTextureDepth);
            RenderTexture right_composite_texture = RenderTexture.GetTemporary(Screen.width, Screen.height, 32, RenderTextureFormat.ARGBFloat);
            right_composite_texture.filterMode = FilterMode.Point;
            Graphics.Blit(Body.fluidTexture, right_composite_texture, composite_material);
            overlay_material.SetTexture("_RightFluidTex", right_composite_texture);
            overlay_material.SetTexture("_RightFluidTex", right_composite_texture);

            Graphics.Blit(source, final_composite, overlay_material);
            Graphics.Blit(final_composite, source);

            // Release temporary render texture
            RenderTexture.ReleaseTemporary(left_composite_texture);
            RenderTexture.ReleaseTemporary(right_composite_texture);
        }
        Graphics.Blit(final_composite, destination);
        RenderTexture.ReleaseTemporary(final_composite);

        // Graphics.Blit(source, destination, composite_material);
        // RenderTexture.ReleaseTemporary(final_composite);
        // XRDisplaySubsystem.
    }
}
