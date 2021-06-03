using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BoidsSimulationOnGPU
{
    public class CameraMoveToObject : MonoBehaviour
    {
        Transform tr;

        void Start()
        {
            tr = GetComponent<Transform>();
        }

        /// <summary>
        /// ��ư�� ��ϵ� objTr�� lerp�ϰ� ī�޶� �̵�
        /// </summary>
        /// <param name="objTr">�̵��� Transfrom</param>
        public void OnTrnasferButtonClick(Transform objTr)
        {
            float duration = 1f;

            tr.DORotate(objTr.rotation.eulerAngles, duration, RotateMode.Fast);
            tr.DOMove(objTr.position, duration);
        }
    }
}