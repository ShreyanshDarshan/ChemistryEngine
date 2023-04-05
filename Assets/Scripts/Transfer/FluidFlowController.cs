using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidFlowController : MonoBehaviour
{
    [SerializeField] private int numEmitters = 10;
    [SerializeField] private float radius = 0.01f;
    [SerializeField] private Transform surface;
    [SerializeField] private GameObject thinCapEmitter;
    [SerializeField] private GameObject wideCapEmitter;
    public float totalFluidOutflowRate = 0.0f;
    public float totalFluidInflowRate = 0.0f;
    Transform[] particleEmitters;
    public float flowRateMultiplier = 300.0f;
    public float startSpeedMultiplier = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        particleEmitters = GetComponentsInChildren<Transform>();
        radius = GetComponent<SphereCollider>().radius;
        GameObject wideCapInstantiated = Instantiate(wideCapEmitter, transform.position, transform.rotation);
        wideCapInstantiated.transform.parent = transform;
        wideCapInstantiated.GetComponent<OverflowDetector>().surface = surface;
        var ps = wideCapInstantiated.GetComponent<ParticleSystem>();
        var shape = ps.shape;
        shape.radius = radius;
        for (int i = 0; i < numEmitters; i++)
        {
            float angle = i * Mathf.PI * 2 / numEmitters;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            GameObject thinCapInstantiated = Instantiate(thinCapEmitter, transform.position + transform.TransformDirection(pos), transform.rotation);
            thinCapInstantiated.transform.parent = transform;
            thinCapInstantiated.GetComponent<OverflowDetector>().surface = surface;
        }

    }

    // Update is called once per frame
    void Update()
    {
        totalFluidOutflowRate = 0.0f;
        foreach (Transform emitter in transform)
        {
            GameObject emitterObject = emitter.gameObject;
            OverflowDetector overflowDetector = emitterObject.GetComponent<OverflowDetector>();
            // if (overflowDetector.isOverflowing)
            // {

            ParticleSystem ps = emitterObject.GetComponent<ParticleSystem>();
            var emission = ps.emission;
            emission.rateOverTimeMultiplier = Mathf.Clamp(overflowDetector.distanceFromSurface * flowRateMultiplier, 0.0f, 30.0f);
            var main = ps.main;
            main.startSpeedMultiplier = Mathf.Clamp(overflowDetector.distanceFromSurface * startSpeedMultiplier, 0.0f, 30.0f);
            Debug.Log("Emission rate: " + emission.rateOverTimeMultiplier);
            Debug.Log("Start speed: " + main.startSpeedMultiplier);

            totalFluidOutflowRate += emission.rateOverTimeMultiplier;

            // ps.emission = emission;
            // ps.Play();

            // }
            // else
            // {
            //     ParticleSystem ps = emitterObject.GetComponent<ParticleSystem>();
            //     var emission = ps.emission;
            //     emission.rateOverTime = 0.0f;
            //     // ps.Pause();
            // }
        }
    }
}
