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
        /// 버튼에 등록된 objTr로 lerp하게 카메라 이동
        /// </summary>
        /// <param name="objTr">이동할 Transfrom</param>
        public void OnTrnasferButtonClick(Transform objTr)
        {
            float duration = 1f;

            tr.DORotate(objTr.rotation.eulerAngles, duration, RotateMode.Fast);
            tr.DOMove(objTr.position, duration);
        }
    }
}