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

        #region PublicFuncs
        public void InitPath()
        {
            simulationSettingPath = Path.Combine(Application.persistentDataPath, simulationSettingFile);
            playerSettingPath = Path.Combine(Application.persistentDataPath, playerSettingFile);
        }

        /// <summary>
        /// Setting �����͸� ��� �ʱ�ȭ�ϴ� �Լ�
        /// �ʱⰪ�� 'SettingData.md' ���� ����
        /// </summary>
        public void InitFileData()
        {
            var simulationData = new SimulationSetting();
            simulationData.numOfFish = 65536;
            simulationData.numOfShark = 3;
            simulationData.cohesionRadius = 2;
            simulationData.alignmentRadius = 2;
            simulationData.separateRadius = 1;
            simulationData.avoidObstacleDistance = 0.9f;
            simulationData.cohesionWeight = 1;
            simulationData.alignmentWeight = 1;
            simulationData.separateWeight = 3;
            simulationData.avoidObstacleWeight = 100;
            simulationData.maxSpeed = 5;
            simulationData.maxSteer = 0.5f;
            File.WriteAllText(simulationSettingPath, JsonUtility.ToJson(simulationData), Encoding.Default);

            var playerData = new PlayerSetting();
            playerData.scrollSpeed = 1;
            playerData.rotateXSpeed = 1;
            playerData.rotateYspeed = 1;
            playerData.moveSpeed = 1;
            playerData.keyMoveSpeed = 10;
            File.WriteAllText(playerSettingPath, JsonUtility.ToJson(playerData), Encoding.Default);
        }

        /// <summary>
        /// Setting UI���� ������ ���� ���Ͽ� �ݿ��ϴ� �Լ�
        /// </summary>
        /// <param name="simulationData">Setting UI�� ������ �ùķ��̼� ���� ��</param>
        /// <param name="playerData">PlayerSetting�� ������ �÷��̾� ���� ��</param>
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
        /// Simuation Scene���� ������ Json������ �б� ���� �Լ�
        /// </summary>
        public void GetFileData(ref SimulationSetting simulationData, ref PlayerSetting playerData)
        {
            var simulFI     = new FileInfo(simulationSettingPath);
            var playerFI    = new FileInfo(playerSettingPath);
            if(!simulFI.Exists || !playerFI.Exists)
            {
                Debug.Log("no exists file");
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
