using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// 버튼 종류
//   - 설정 적용
//   - 초기화
//   - 취소

namespace BoidsSimulationOnGPU
{
    public class SettingButtons : MonoBehaviour
    {
        Transform tr;
        GatheringSettingData gather;
        Button applyBtn;
        Button initBtn;
        Button exitBtn;

        GameObject settingInfo;
        GameObject initInfo;

        void Awake()
        {
            tr = GetComponent<Transform>();
            gather = GetComponent<GatheringSettingData>();
            applyBtn = tr.Find("Group_Footer/Btn_ApplySetting").GetComponent<Button>();
            initBtn  = tr.Find("Group_Footer/Btn_InitSetting").GetComponent<Button>();
            exitBtn  = tr.Find("Group_Footer/Btn_ExitModal").GetComponent<Button>();

            settingInfo = tr.Find("Group_Setting").gameObject;
            initInfo    = tr.Find("Group_Init").gameObject;

            // 이벤트 등록
            applyBtn.onClick.AddListener(() => OnClickSettingButton());
            initBtn .onClick.AddListener(() => OnClickInitButton());
            exitBtn .onClick.AddListener(() => OnClickExitButton());
        }

        /// <summary>
        /// 셋팅 버튼을 누른 경우
        /// 셋팅 완료 안내 창이 활성화되면서
        /// 셋팅값이 저장된 json파일을 InputField값으로 변경한다.
        /// </summary>
        public void OnClickSettingButton()
        {
            var filemgr = LobbyManager.instance.FileManager;

            var simulData = gather.GatheringSimulationSetting();
            var playerData = gather.GatheringPlayerSetting();

            filemgr.SetFileData(simulData, playerData);

            // 파일 저장 안내 UI 활성화
            settingInfo.SetActive(true);
        }

        /// <summary>
        /// 초기화 버튼을 누른 경우
        /// 모든 설정값을 초기화 후,
        /// 셋팅값이 저장된 json파일을 초기화 값으로 변경한다.
        /// </summary>
        public void OnClickInitButton()
        {
            // UI와 Setting값 초기화
            gather.InitSettingFields();

            // 초기화 안내 UI 활성화
            initInfo.SetActive(true);
        }

        /// <summary>
        /// 나가기 버튼을 누른 경우
        /// UI 패널을 닫는다.
        /// </summary>
        public void OnClickExitButton()
        {
            // 부모의 모달창을 비활성화
            transform.parent.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
