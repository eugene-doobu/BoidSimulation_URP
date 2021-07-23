using UnityEngine;
using SaveData;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;

namespace BoidsSimulationOnGPU
{
    [RequireComponent(typeof(InputField))]
    public class SettingInputField:MonoBehaviour
    {
        InputField field;

        private void Awake()
        {
            field = GetComponent<InputField>();
            field.text = ""; // PlaceHolder 초기화
        }

        private void Start()
        {
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
            // .이 2개 이상인 경우 텍스트 초기화
            MatchCollection matches = Regex.Matches(field.text, "[.]");
            if(matches.Count > 1)
            {
                field.text = "";
                return;
            }
        }

        /// <summary>
        /// 입력된 문자열이 숫자와 점(.)으로만 이루어져있는지 확인
        /// </summary>
        /// <returns>모든 캐릭터가 숫자인경우 true</returns>
        private bool CheckValueIsNumber() 
            => field.text.All(c => { return char.IsDigit(c) || c == '.'; });
    }
}
