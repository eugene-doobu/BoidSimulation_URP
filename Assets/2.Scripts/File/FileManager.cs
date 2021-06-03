using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using BoidsSimulationOnGPU;
using SaveData;
using System.Text;

namespace Common
{
    public class FileManager : MonoBehaviour
    {
        GPUBoids gPUBoids;
        CameraOperate cameraOperate;

        private const string simulationSettingFile = "SimulationSetting.json";
        private const string playerSettingFile = "PlayerSetting.json";
        private string simulationSettingPath = null;
        private string playerSettingPath = null;

        private void Awake()
        {
            simulationSettingPath = Path.Combine(Application.persistentDataPath, simulationSettingFile);
            playerSettingPath = Path.Combine(Application.persistentDataPath, playerSettingFile);
        }

        void Start()
        {
            gPUBoids = SimulationManager.instance.GPUBoids;
            cameraOperate = SimulationManager.instance.CameraOperate;
            StartCoroutine(ReadFile());
        }

        IEnumerator ReadFile()
        {
            // ≈∏¿Ãπ÷: Start+1
            yield return null;
            GetScript();
            //FileInfo fileInfo;
        }

        private void GetScript()
        {
            if (SimulationManager.instance != null)
            {
                gPUBoids = SimulationManager.instance.GPUBoids;
                cameraOperate = SimulationManager.instance.CameraOperate;
            }
        }
    }
}
