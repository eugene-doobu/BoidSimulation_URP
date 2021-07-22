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
            // ������ �о����
            var simulationData = new SimulationSetting();
            var playerData = new PlayerSetting();
            manager.FileManager.GetFileData(ref simulationData, ref playerData);

            // ������ �ѷ��ֱ�
            manager.GPUBoids?.GetSettingData(simulationData);
            manager.SharkManager.GetSettingData(simulationData);
            manager.CameraOperate.GetSettingData(playerData);
        }
    }
}

