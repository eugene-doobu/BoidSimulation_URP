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
            // InputField�� Event����
        }

        /// <summary>
        /// InputField�� OnValueChagned�� ����
        /// �ԷµǴ� ���ڰ� ���ڿ� ���� �ƴѰ�� �Է��� ���
        /// </summary>
        public void OnValueChanged()
        {
        }

        /// <summary>
        /// InputField�� OnEndEdit�� ����
        /// �Էµ� ���� �ִ밪~�ּҰ� ������ �����ϴ� ������ Ȯ���ϰ�
        /// �ƴѰ�� ��� �޽����� ���
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
