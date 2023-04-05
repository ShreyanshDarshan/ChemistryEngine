using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private float volumeCapacity = 0.0f;
    public float volumeOccupied = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Bounds bounds = GetComponentInChildren<Renderer>().bounds;
        volumeCapacity = bounds.size.x * bounds.size.y * bounds.size.z;
    }

    public float GetCapacity()
    {
        return volumeCapacity;
    }

    public float GetOccupied()
    {
        return volumeOccupied;
    }

    public void SetOccupied(float volume)
    {
        volumeOccupied = volume;
    }

    public void AddOccupied(float volume)
    {
        volumeOccupied += volume;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
