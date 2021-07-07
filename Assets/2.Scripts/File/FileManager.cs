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
            simulationSettingPath = Path.Combine(Application.persistentDataPath, simulationSettingFile);
            playerSettingPath = Path.Combine(Application.persistentDataPath, playerSettingFile);
        }
        #endregion

        #region PublicFuncs
        /// <summary>
        /// Setting �����͸� ��� �ʱ�ȭ�ϴ� �Լ�
        /// �ʱⰪ�� 'SettingData.md' ���� ����
        /// </summary>
        public void InitFileData()
        {
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
        /// GPU �ùķ��̼��� Start���� ������ ������ ���۵�
        /// LobbyScene���� �����͸� ����, Simulation Scene������ �� �����͸� �б�
        /// </summary>
        public void GetFileData()
        {
            /*
            File.WriteAllText(userDitectPath,
                JsonUtility.ToJson(new UserDitectSetting(
                    kinectUserSetting.MinUDValue,
                    kinectUserSetting.MaxUDValue,
                    kinectUserSetting.LRDValue,
                    kinectUserSetting.LRCValue)),
                Encoding.Default);
            */
        }
        #endregion

        #region PrivateFuncs
        #endregion
    }
}
