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
            File.WriteAllText(simulationSettingPath,
                JsonUtility.ToJson(simulationData),
                Encoding.Default);

            var playerData = new PlayerSetting();
            File.WriteAllText(playerSettingPath,
                JsonUtility.ToJson(playerData),
                Encoding.Default);

        }

        /// <summary>
        /// Setting UI���� ������ ���� ���Ͽ� �ݿ��ϴ� �Լ�
        /// </summary>
        /// <param name="simulationData">Setting UI�� ������ �ùķ��̼� ���� ��</param>
        /// <param name="playerData">PlayerSetting�� ������ �÷��̾� ���� ��</param>
        public void SetFileData(SimulationSetting simulationData, PlayerSetting playerData)
        {
            File.WriteAllText(simulationSettingPath,
                JsonUtility.ToJson(simulationData),
                Encoding.Default);
            File.WriteAllText(playerSettingPath,
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
