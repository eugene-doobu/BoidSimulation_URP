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
            field.text = ""; // PlaceHolder �ʱ�ȭ
        }

        private void Start()
        {
            // InputField�� Event����
            field.onValueChanged.AddListener((_) => OnValueChanged());
            field.onEndEdit     .AddListener((_) => OnEndEdit());
        }

        /// <summary>
        /// InputField�� OnValueChagned�� ����
        /// �ԷµǴ� ���ڰ� ���ڿ� ���� �ƴѰ�� �Է��� ���
        /// </summary>
        public void OnValueChanged()
        {
            if (!CheckValueIsNumber()) 
                field.text = field.text.Substring(0, field.text.Length - 1);
        }

        /// <summary>
        /// InputField�� OnEndEdit�� ����
        /// �Էµ� ���� �ִ밪~�ּҰ� ������ �����ϴ� ������ Ȯ���ϰ�
        /// �ƴѰ�� ��� �޽����� ���
        /// </summary>
        public void OnEndEdit()
        {
            // .�� 2�� �̻��� ��� �ؽ�Ʈ �ʱ�ȭ
            MatchCollection matches = Regex.Matches(field.text, "[.]");
            if(matches.Count > 1)
            {
                field.text = "";
                return;
            }
        }

        /// <summary>
        /// �Էµ� ���ڿ��� ���ڿ� ��(.)���θ� �̷�����ִ��� Ȯ��
        /// </summary>
        /// <returns>��� ĳ���Ͱ� �����ΰ�� true</returns>
        private bool CheckValueIsNumber() 
            => field.text.All(c => { return char.IsDigit(c) || c == '.'; });
    }
}
