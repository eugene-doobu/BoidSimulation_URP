using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoidsSimulationOnGPU;

namespace BoidsSimulationOnGPU
{
    public class SimulationManager : MonoBehaviour
    {
        public static SimulationManager instance;

        [SerializeField] GPUBoids gPUBoids;
        [SerializeField] CameraOperate cameraOperate;
        [SerializeField] FileManager fileManager;

        public float numOfShark = 3;

        #region Properties
        public GPUBoids GPUBoids
        {
            get { return gPUBoids; }
        }
        public CameraOperate CameraOperate
        {
            get { return cameraOperate; }
        }

        public FileManager FileManager
        {
            get { return fileManager; }
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
}