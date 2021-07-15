using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoidsSimulationOnGPU
{
    public class SimulationManager : MonoBehaviour
    {
        public static SimulationManager instance;

        [SerializeField] GPUBoids gPUBoids;
        [SerializeField] CameraOperate cameraOperate;
        [SerializeField] FileManager fileManager;
        [SerializeField] SharkManager sharkManager;
        [SerializeField] SettingDataSpreader settingDataSpreader;

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

        public SharkManager SharkManager
        {
            get { return sharkManager; }
        }

        public SettingDataSpreader SettingDataSpreader
        {
            get { return settingDataSpreader; }
        }
        #endregion

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            // 경로 초기화
            fileManager.InitPath();

            // 데이터 읽어오기
            settingDataSpreader.InitSettingDataSpreader(instance);
        }
    }
}