using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveData;

namespace BoidsSimulationOnGPU
{
    public class SettingDataSpreader : MonoBehaviour
    {
        private SimulationManager manager;

        public void InitSettingDataSpreader(SimulationManager _manager)
        {
            manager = _manager;
            SpreaderData();
        }

        private void SpreaderData()
        {
            // 데이터 읽어오기
            var simulationData = new SimulationSetting();
            var playerData = new PlayerSetting();
            manager.FileManager.GetFileData(ref simulationData, ref playerData);

            // 데이터 뿌려주기
            manager.GPUBoids?.GetSettingData(simulationData);
            manager.SharkManager.GetSettingData(simulationData);
            manager.CameraOperate.GetSettingData(playerData);
        }
    }
}

