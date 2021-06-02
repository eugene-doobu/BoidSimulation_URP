using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoidsSimulationOnGPU;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager instance;

    [SerializeField] GPUBoids gPUBoids;
    [SerializeField] CameraOperate cameraOperate;

    #region Properties
    public GPUBoids GPUBoids
    {
        get { return gPUBoids; }
    }
    public CameraOperate CameraOperate
    {
        get { return CameraOperate; }
    }
    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}