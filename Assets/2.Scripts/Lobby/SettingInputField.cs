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
            if (!CheckValueIsNumber())
            {
                // ��� �޽��� ���
                // ,,
            } 
            else if (!CheckValueRangeCheck(float.Parse(field.text)))
            {
                // ��� �޽��� ���
                // ,,
            }
            field.text = "";
        }

        /// <summary>
        /// �Էµ� ���ڿ��� ���ڷθ� �̷�����ִ��� Ȯ��
        /// </summary>
        /// <returns>��� ĳ���Ͱ� �����ΰ�� true</returns>
        private bool CheckValueIsNumber() 
            => field.text.All(char.IsDigit);

        /// <summary>
        /// �Էµ� ���ڿ��� �ּ�~�ִ밪 ������ �ش��ϴ��� Ȯ��
        /// </summary>
        /// <param name="value">Ȯ���� ��</param>
        /// <returns>�ش��ϸ� true ����</returns>
        private bool CheckValueRangeCheck(float value)
            => value <= maxValue && value >= minValue;
    }
}
