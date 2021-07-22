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
        /// ���������� ó�� ����Ȱ��
        /// ���� json������ ���� �а� UI�� �ѷ���
        /// </summary>
        void Start()
        {
            // ������ �޾ƿ���
            var fileMgr = LobbyManager.instance.FileManager;

            var simulationData  = new SimulationSetting();
            var playerData      = new PlayerSetting();

            fileMgr.GetFileData(ref simulationData, ref playerData);

            // ������ �ѷ��ֱ�
            UIDataChange(simulationData, playerData);
        }
        #endregion

        /// <summary>
        /// json ���������� �����ϱ� ���� SettingUI�� Simulation �����͸� �о���̴� �Լ�
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
        /// json ���������� �����ϱ� ���� SettingUI�� Player �����͸� �о���̴� �Լ�
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
        /// �ʱ�ȭ ��ư�� ������ ��� ��� �������� �ʱ�ȭ�ϴ� �Լ�
        /// </summary>
        public void InitSettingFields()
        {
            // json �ʱ�ȭ
            var fileMgr = LobbyManager.instance.FileManager;
            fileMgr.InitFileData();

            // UI�� �ʱ�ȭ
            var simulationData = new SimulationSetting();
            var playerData = new PlayerSetting();
            UIDataChange(simulationData, playerData);
        }

        /// <summary>
        /// UI�� Ŭ���� �����͸� �ѷ��ִ� �Լ�
        /// </summary>
        /// <param name="simulationData">simulationData</param>
        /// <param name="playerData">playerData</param>
        private void UIDataChange(SimulationSetting simulationData, PlayerSetting playerData)
        {
            // �ùķ��̼�
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

            // �÷��̾�
            scrollSpeed.value   = playerData.scrollSpeed;
            rotateXSpeed.value  = playerData.rotateXSpeed;
            rotateYspeed.value  = playerData.rotateYspeed;
            moveSpeed.value     = playerData.moveSpeed;
            keyMoveSpeed.value  = playerData.keyMoveSpeed;
        }

        /// <summary>
        /// �� ��ũ��Ʈ���� ����� UI������Ʈ���� ĳ���ϴ� �Լ�
        /// </summary>
        private void CachingUIComponents()
        {
            // Setting ������Ʈ ĳ��
            tr = GetComponent<Transform>();
            var leftTr = tr.Find("Group_Main/Group_LeftSetting");
            var rightTr = tr.Find("Group_Main/Group_RightSetting");

            // ���� 9��
            numOfFish = leftTr.Find("Group_numOfFish").GetComponentInChildren<Slider>();
            numOfShark = leftTr.Find("Group_numOfShark").GetComponentInChildren<Slider>();

            cohesionRadius = leftTr.Find("Group_cohesionRadius").GetComponentInChildren<InputField>();
            alignmentRadius = leftTr.Find("Group_alignmentRadius").GetComponentInChildren<InputField>();
            separateRadius = leftTr.Find("Group_separateRadius").GetComponentInChildren<InputField>();
            avoidObstacleDistance = leftTr.Find("Group_avoidObstacleDistance").GetComponentInChildren<InputField>();
            cohesionWeight = leftTr.Find("Group_cohesionWeight").GetComponentInChildren<InputField>();
            alignmentWeight = leftTr.Find("Group_alignmentWeight").GetComponentInChildren<InputField>();
            separateWeight = leftTr.Find("Group_separateWeight").GetComponentInChildren<InputField>();

            // ������ 8��
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