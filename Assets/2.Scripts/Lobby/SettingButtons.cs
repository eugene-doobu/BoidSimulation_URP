using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// ��ư ����
//   - ���� ����
//   - �ʱ�ȭ
//   - ���

namespace BoidsSimulationOnGPU
{
    public class SettingButtons : MonoBehaviour
    {
        Transform tr;
        GatheringSettingData gather;
        Button applyBtn;
        Button initBtn;
        Button exitBtn;

        void Awake()
        {
            tr = GetComponent<Transform>();
            gather = GetComponent<GatheringSettingData>();
            applyBtn = tr.Find("Group_Footer/Btn_ApplySetting").GetComponent<Button>();
            initBtn  = tr.Find("Group_Footer/Btn_InitSetting").GetComponent<Button>();
            exitBtn  = tr.Find("Group_Footer/Btn_ExitModal").GetComponent<Button>();

            // �̺�Ʈ ���
            applyBtn.onClick.AddListener(() => OnClickSettingButton());
            initBtn .onClick.AddListener(() => OnClickInitButton());
            exitBtn .onClick.AddListener(() => OnClickExitButton());
        }

        /// <summary>
        /// ���� ��ư�� ���� ���
        /// ���� �Ϸ� �ȳ� â�� Ȱ��ȭ�Ǹ鼭
        /// ���ð��� ����� json������ InputField������ �����Ѵ�.
        /// </summary>
        public void OnClickSettingButton()
        {
            var filemgr = LobbyManager.instance.FileManager;

            var simulData = gather.GatheringSimulationSetting();
            var playerData = gather.GatheringPlayerSetting();

            filemgr.SetFileData(simulData, playerData);

            // ���� ���� �ȳ� UI Ȱ��ȭ

        }

        /// <summary>
        /// �ʱ�ȭ ��ư�� ���� ���
        /// ��� �������� �ʱ�ȭ ��,
        /// ���ð��� ����� json������ �ʱ�ȭ ������ �����Ѵ�.
        /// </summary>
        public void OnClickInitButton()
        {
            // UI�� Setting�� �ʱ�ȭ
            gather.InitSettingFields();

            // �ʱ�ȭ �ȳ� UI Ȱ��ȭ
        }

        /// <summary>
        /// ������ ��ư�� ���� ���
        /// UI �г��� �ݴ´�.
        /// </summary>
        public void OnClickExitButton()
        {
            gameObject.SetActive(false);
        }
    }
}
