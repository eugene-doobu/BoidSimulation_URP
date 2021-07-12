using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using BoidsSimulationOnGPU;
using SaveData;
using System.Text;

namespace BoidsSimulationOnGPU
{
    public class FileManager : MonoBehaviour
    {
        #region Variables
        private readonly string simulationSettingFile = "SimulationSetting.json";
        private readonly string playerSettingFile = "PlayerSetting.json";
        private string simulationSettingPath = null;
        private string playerSettingPath = null;
        #endregion

        #region Properties
        public string SimulationSettingPath
        {
            get { return simulationSettingPath; }
        }

        public string PlayerSettingPath
        {
            get { return playerSettingPath; }
        }
        #endregion

        #region UnityEventFuncs
        private void Awake()
        {
            simulationSettingPath   = Path.Combine(Application.persistentDataPath, simulationSettingFile);
            playerSettingPath       = Path.Combine(Application.persistentDataPath, playerSettingFile);
        }
        #endregion

        #region PublicFuncs
        /// <summary>
        /// Setting 데이터를 모두 초기화하는 함수
        /// 초기값은 'SettingData.md' 파일 참조
        /// </summary>
        public void InitFileData()
        {
            var simulationValue = new SimulationSetting();
            simulationValue.numOfFish = 65536;
            simulationValue.numOfShark = 3;
            simulationValue.cohesionRadius = 2;
            simulationValue.alignmentRadius = 2;
            simulationValue.separateRadius = 1;
            simulationValue.avoidObstacleDistance = 0.9f;
            simulationValue.cohesionWeight = 1;
            simulationValue.alignmentWeight = 1;
            simulationValue.separateWeight = 3;
            simulationValue.avoidObstacleWeight = 100;
            simulationValue.maxSpeed = 5;
            simulationValue.maxSteer = 0.5f;
            File.WriteAllText(simulationSettingPath, JsonUtility.ToJson(simulationValue), Encoding.Default);

            var playerValue = new PlayerSetting();
            playerValue.scrollSpeed = 1;
            playerValue.rotateXSpeed = 1;
            playerValue.rotateYspeed = 1;
            playerValue.moveSpeed = 1;
            playerValue.keyMoveSpeed = 10;
            File.WriteAllText(playerSettingPath, JsonUtility.ToJson(playerValue), Encoding.Default);
        }

        /// <summary>
        /// Setting UI에서 설정한 값을 파일에 반영하는 함수
        /// </summary>
        /// <param name="simulationData">Setting UI에 설정된 시뮬레이션 설정 값</param>
        /// <param name="playerData">PlayerSetting에 설정된 플레이어 설정 값</param>
        public void SetFileData(SimulationSetting simulationData, PlayerSetting playerData)
        {
            File.WriteAllText(simulationSettingFile,
                JsonUtility.ToJson(simulationData),
                Encoding.Default);
            File.WriteAllText(playerSettingFile,
                JsonUtility.ToJson(playerData),
                Encoding.Default);
        }

        /// <summary>
        /// Simuation Scene에서 설정용 Json파일을 읽기 위한 함수
        /// </summary>
        public void GetFileData(ref SimulationSetting simulationData, ref PlayerSetting playerData)
        {
            var simulFI     = new FileInfo(simulationSettingPath);
            var playerFI    = new FileInfo(playerSettingPath);
            if(!simulFI.Exists || !playerFI.Exists)
            {
                Debug.LogError("no exists file");
                InitFileData();
            }

            simulationData  = JsonUtility.FromJson<SimulationSetting>(File.ReadAllText(simulationSettingPath));
            playerData      = JsonUtility.FromJson<PlayerSetting>(File.ReadAllText(playerSettingPath));
        }
        #endregion

        #region PrivateFuncs
        #endregion

        #region tmp
        [System.Obsolete]
        void Tmp__InitFileDataTest() => InitFileData();
        #endregion
    }
}
