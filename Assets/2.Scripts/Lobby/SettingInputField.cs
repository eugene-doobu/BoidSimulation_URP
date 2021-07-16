using UnityEngine;
using SaveData;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

namespace BoidsSimulationOnGPU
{
    [RequireComponent(typeof(InputField))]
    public class SettingInputField:MonoBehaviour
    {
        [SerializeField] SaveTypeEnum saveType = SaveTypeEnum.None;
        [SerializeField] float maxValue = 0;
        [SerializeField] float minValue = 0;
        InputField field;

        private void Start()
        {
            field = GetComponent<InputField>();

            // InputField에 Event연결
            field.onValueChanged.AddListener((_) => OnValueChanged());
            field.onEndEdit     .AddListener((_) => OnEndEdit());
        }

        /// <summary>
        /// InputField의 OnValueChagned에 연결
        /// 입력되는 문자가 숫자와 점이 아닌경우 입력을 취소
        /// </summary>
        public void OnValueChanged()
        {
            if (!CheckValueIsNumber()) 
                field.text = field.text.Substring(0, field.text.Length - 1);
        }

        /// <summary>
        /// InputField의 OnEndEdit에 연결
        /// 입력된 값이 최대값~최소값 범위에 만족하는 값인지 확인하고
        /// 아닌경우 경고 메시지를 출력
        /// </summary>
        public void OnEndEdit()
        {
            if (!CheckValueIsNumber())
            {
                // 경고 메시지 출력
                // ,,
            } 
            else if (!CheckValueRangeCheck(float.Parse(field.text)))
            {
                // 경고 메시지 출력
                // ,,
            }
            field.text = "";
        }

        /// <summary>
        /// 입력된 문자열이 숫자로만 이루어져있는지 확인
        /// </summary>
        /// <returns>모든 캐릭터가 숫자인경우 true</returns>
        private bool CheckValueIsNumber() 
            => field.text.All(char.IsDigit);

        /// <summary>
        /// 입력된 문자열이 최소~최대값 범위에 해당하는지 확인
        /// </summary>
        /// <param name="value">확인할 값</param>
        /// <returns>해당하면 true 리턴</returns>
        private bool CheckValueRangeCheck(float value)
            => value <= maxValue && value >= minValue;
    }
}
