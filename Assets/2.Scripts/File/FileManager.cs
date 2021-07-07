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
        /// Setting 데이터를 모두 초기화하는 함수
        /// 초기값은 'SettingData.md' 파일 참조
        /// </summary>
        public void InitFileData()
        {
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
        /// GPU 시뮬레이션은 Start에서 설정된 값으로 동작됨
        /// LobbyScene에서 데이터를 쓰고, Simulation Scene에서는 그 데이터를 읽기
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
