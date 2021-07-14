using UnityEngine;
using SaveData;

namespace BoidsSimulationOnGPU
{
    [RequireComponent(typeof(UnityEngine.UI.InputField))]
    public class SettingInputField:MonoBehaviour
    {
        [SerializeField] SaveTypeEnum saveType = SaveTypeEnum.None;
        [SerializeField] float maxValue = 0;
        [SerializeField] float minValue = 0;

        private void Start()
        {
            // InputField에 Event연결
        }

        /// <summary>
        /// InputField의 OnValueChagned에 연결
        /// 입력되는 문자가 숫자와 점이 아닌경우 입력을 취소
        /// </summary>
        public void OnValueChanged()
        {
        }

        /// <summary>
        /// InputField의 OnEndEdit에 연결
        /// 입력된 값이 최대값~최소값 범위에 만족하는 값인지 확인하고
        /// 아닌경우 경고 메시지를 출력
        /// </summary>
        public void OnEndEdit()
        {
        }

        private int CheckValueIsNumber()
        {
            return 0;
        }

        private int CheckValueRangeCheck(float value)
        {
            if (value > maxValue || value < minValue) return 2;
            return 0;
        }
    }
}
