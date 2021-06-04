using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoidsSimulationOnGPU
{
    public class SharkMovement : MonoBehaviour
    {
        Vector3 wallSize;
        Vector3 wallCenter;
        void Start()
        {
            wallSize = SimulationManager.instance.GPUBoids.WallSize;
            wallCenter = SimulationManager.instance.GPUBoids.WallCenter;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
