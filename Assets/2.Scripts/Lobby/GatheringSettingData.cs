using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveData;
using BoidsSimulationOnGPU;


namespace BoidsSimulationOnGPU
{   
    [RequireComponent(typeof(ButtonFuncs))]
    public class GatheringSettingData : MonoBehaviour
    {
        #region variables
        [SerializeField] Transform simulationSettingUIsTr;
        Slider numOfFish;
        Slider numOfShark;
        InputField cohesionRadius;
        InputField alignmentRadius;
        InputField separateRadius;
        InputField avoidObstacleDistance;
        InputField cohesionWeight;
        InputField alignmentWeight;
        InputField separateWeight;
        InputField avoidObstacleWeight;
        InputField maxSpeed;
        InputField maxSteer;

        [SerializeField] Transform playerSettingUIsTr;
        Slider scrollSpeed;
        Slider rotateXSpeed;
        Slider rotateYspeed;
        Slider moveSpeed;
        Slider keyMoveSpeed;
        #endregion

        #region UnityEventFuncs
        private void Awake()
        {
            // Setting 컴포넌트 캐싱
        }
        #endregion

        /// <summary>
        /// json 설정파일을 수정하기 위해 SettingUI의 Simulation 데이터를 읽어들이는 함수
        /// </summary>
        public SimulationSetting GatheringSimulationSetting()
        {
            var simulationData = new SimulationSetting();
            simulationData.numOfFish            = (int)numOfFish.value;
            simulationData.numOfShark           = (int)numOfShark.value;
            simulationData.cohesionRadius       = float.Parse(cohesionRadius.text);
            simulationData.alignmentRadius      = float.Parse(alignmentRadius.text);
            simulationData.separateRadius       = float.Parse(separateRadius.text);
            simulationData.avoidObstacleDistance = float.Parse(avoidObstacleDistance.text);
            simulationData.cohesionWeight       = float.Parse(cohesionWeight.text);
            simulationData.alignmentWeight      = float.Parse(alignmentWeight.text);
            simulationData.separateWeight       = float.Parse(separateWeight.text);
            simulationData.avoidObstacleWeight  = float.Parse(avoidObstacleWeight.text);
            simulationData.maxSpeed             = float.Parse(maxSpeed.text);
            simulationData.maxSteer             = float.Parse(maxSteer.text);

            return simulationData;
        }

        /// <summary>
        /// json 설정파일을 수정하기 위해 SettingUI의 Player 데이터를 읽어들이는 함수
        /// </summary>
        public PlayerSetting GatheringPlayerSetting()
        {
            var playerData = new PlayerSetting();
            playerData.scrollSpeed = scrollSpeed.value;
            playerData.rotateXSpeed = rotateXSpeed.value;
            playerData.rotateYspeed = rotateYspeed.value;
            playerData.moveSpeed = moveSpeed.value;
            playerData.keyMoveSpeed = keyMoveSpeed.value;

            return playerData;
        }
    }
}