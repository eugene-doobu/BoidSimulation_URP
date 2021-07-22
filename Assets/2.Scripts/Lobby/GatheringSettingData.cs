using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveData;
using BoidsSimulationOnGPU;


namespace BoidsSimulationOnGPU
{   
    [RequireComponent(typeof(SettingButtons))]
    public class GatheringSettingData : MonoBehaviour
    {
        #region variables
        Transform tr;

        // Simulation Setting UI
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

        // Player Setting UI
        Slider scrollSpeed;
        Slider rotateXSpeed;
        Slider rotateYspeed;
        Slider moveSpeed;
        Slider keyMoveSpeed;
        #endregion

        #region UnityEventFuncs
        private void Awake()
        {
            CachingUIComponents();
        }

        /// <summary>
        /// 실행파일이 처음 실행된경우
        /// 셋팅 json파일의 값을 읽고 UI에 뿌려줌
        /// </summary>
        void Start()
        {
            // 데이터 받아오기
            var fileMgr = LobbyManager.instance.FileManager;

            var simulationData  = new SimulationSetting();
            var playerData      = new PlayerSetting();

            fileMgr.GetFileData(ref simulationData, ref playerData);

            // 데이터 뿌려주기
            UIDataChange(simulationData, playerData);
        }
        #endregion

        /// <summary>
        /// json 설정파일을 수정하기 위해 SettingUI의 Simulation 데이터를 읽어들이는 함수
        /// </summary>
        public SimulationSetting GatheringSimulationSetting()
        {
            var simulationData = new SimulationSetting();
            simulationData.numOfFish             = (int)numOfFish.value;
            simulationData.numOfShark            = (int)numOfShark.value;
            simulationData.cohesionRadius        = float.Parse(cohesionRadius.text);
            simulationData.alignmentRadius       = float.Parse(alignmentRadius.text);
            simulationData.separateRadius        = float.Parse(separateRadius.text);
            simulationData.avoidObstacleDistance = float.Parse(avoidObstacleDistance.text);
            simulationData.cohesionWeight        = float.Parse(cohesionWeight.text);
            simulationData.alignmentWeight       = float.Parse(alignmentWeight.text);
            simulationData.separateWeight        = float.Parse(separateWeight.text);
            simulationData.avoidObstacleWeight   = float.Parse(avoidObstacleWeight.text);
            simulationData.maxSpeed              = float.Parse(maxSpeed.text);
            simulationData.maxSteer              = float.Parse(maxSteer.text);

            return simulationData;
        }

        /// <summary>
        /// json 설정파일을 수정하기 위해 SettingUI의 Player 데이터를 읽어들이는 함수
        /// </summary>
        public PlayerSetting GatheringPlayerSetting()
        {
            var playerData = new PlayerSetting();
            playerData.scrollSpeed  = scrollSpeed.value;
            playerData.rotateXSpeed = rotateXSpeed.value;
            playerData.rotateYspeed = rotateYspeed.value;
            playerData.moveSpeed    = moveSpeed.value;
            playerData.keyMoveSpeed = keyMoveSpeed.value;

            return playerData;
        }

        /// <summary>
        /// 초기화 버튼을 눌렀을 경우 모든 설정값을 초기화하는 함수
        /// </summary>
        public void InitSettingFields()
        {
            // json 초기화
            var fileMgr = LobbyManager.instance.FileManager;
            fileMgr.InitFileData();

            // UI값 초기화
            var simulationData = new SimulationSetting();
            var playerData = new PlayerSetting();
            UIDataChange(simulationData, playerData);
        }

        /// <summary>
        /// UI에 클래스 데이터를 뿌려주는 함수
        /// </summary>
        /// <param name="simulationData">simulationData</param>
        /// <param name="playerData">playerData</param>
        private void UIDataChange(SimulationSetting simulationData, PlayerSetting playerData)
        {
            // 시뮬레이션
            numOfFish.value             = simulationData.numOfFish;
            numOfShark.value            = simulationData.numOfShark;
            cohesionRadius.text         = simulationData.cohesionRadius.ToString();
            alignmentRadius.text        = simulationData.alignmentRadius.ToString();
            separateRadius.text         = simulationData.separateRadius.ToString();
            avoidObstacleDistance.text  = simulationData.avoidObstacleDistance.ToString();
            cohesionWeight.text         = simulationData.cohesionWeight.ToString();
            alignmentWeight.text        = simulationData.alignmentWeight.ToString();
            separateWeight.text         = simulationData.separateWeight.ToString();
            avoidObstacleWeight.text    = simulationData.avoidObstacleWeight.ToString();
            maxSpeed.text               = simulationData.maxSpeed.ToString();
            maxSteer.text               = simulationData.maxSteer.ToString();

            // 플레이어
            scrollSpeed.value   = playerData.scrollSpeed;
            rotateXSpeed.value  = playerData.rotateXSpeed;
            rotateYspeed.value  = playerData.rotateYspeed;
            moveSpeed.value     = playerData.moveSpeed;
            keyMoveSpeed.value  = playerData.keyMoveSpeed;
        }

        /// <summary>
        /// 이 스크립트에서 사용할 UI컴포넌트들을 캐싱하는 함수
        /// </summary>
        private void CachingUIComponents()
        {
            // Setting 컴포넌트 캐싱
            tr = GetComponent<Transform>();
            var leftTr = tr.Find("Group_Main/Group_LeftSetting");
            var rightTr = tr.Find("Group_Main/Group_RightSetting");

            // 왼쪽 9개
            numOfFish = leftTr.Find("Group_numOfFish").GetComponentInChildren<Slider>();
            numOfShark = leftTr.Find("Group_numOfShark").GetComponentInChildren<Slider>();

            cohesionRadius = leftTr.Find("Group_cohesionRadius").GetComponentInChildren<InputField>();
            alignmentRadius = leftTr.Find("Group_alignmentRadius").GetComponentInChildren<InputField>();
            separateRadius = leftTr.Find("Group_separateRadius").GetComponentInChildren<InputField>();
            avoidObstacleDistance = leftTr.Find("Group_avoidObstacleDistance").GetComponentInChildren<InputField>();
            cohesionWeight = leftTr.Find("Group_cohesionWeight").GetComponentInChildren<InputField>();
            alignmentWeight = leftTr.Find("Group_alignmentWeight").GetComponentInChildren<InputField>();
            separateWeight = leftTr.Find("Group_separateWeight").GetComponentInChildren<InputField>();

            // 오른쪽 8개
            avoidObstacleWeight = rightTr.Find("Group_avoidObstacleWeight").GetComponentInChildren<InputField>();
            maxSpeed = rightTr.Find("Group_maxSpeed").GetComponentInChildren<InputField>();
            maxSteer = rightTr.Find("Group_maxSteer").GetComponentInChildren<InputField>();

            scrollSpeed = rightTr.Find("Group_scrollSpeed").GetComponentInChildren<Slider>();
            rotateXSpeed = rightTr.Find("Group_rotateXSpeed").GetComponentInChildren<Slider>();
            rotateYspeed = rightTr.Find("Group_rotateYspeed").GetComponentInChildren<Slider>();
            moveSpeed = rightTr.Find("Group_moveSpeed").GetComponentInChildren<Slider>();
            keyMoveSpeed = rightTr.Find("Group_keyMoveSpeed").GetComponentInChildren<Slider>();
        }
    }
}