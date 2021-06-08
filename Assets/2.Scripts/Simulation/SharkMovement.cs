using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BoidsSimulationOnGPU
{
    public class SharkMovement : MonoBehaviour
    {
        public float speed;

        Transform tr;
        Vector3 wallSize;
        Vector3 wallCenter;

        private void Awake()
        {
            tr = GetComponent<Transform>();
        }

        void Start()
        {
            if(SimulationManager.instance.GPUBoids != null)
            {
                wallSize = SimulationManager.instance.GPUBoids.WallSize;
                wallCenter = SimulationManager.instance.GPUBoids.WallCenter;
            }
            StartCoroutine(MoveCycle());
        }

        Vector3 GetRandomPosition()
        {
            return new Vector3(
                    Random.Range(0, wallSize.x) - wallSize.x/2,
                    Random.Range(0, wallSize.y) - wallSize.y/2,
                    Random.Range(0, wallSize.z) - wallSize.z/2
                    );
        }

        IEnumerator MoveCycle()
        {
            // 초기위치 랜덤 설정
            tr.position = GetRandomPosition() * 0.7f + wallCenter;

            // 이동 사이클
            while (true)
            {
                Vector3 movePos = GetRandomPosition() + wallCenter;
                yield return RandomMove(movePos);
            }
        }

        IEnumerator RandomMove(Vector3 movePos)
        {
            bool isMoving = true;
            float duration = (movePos - wallCenter).magnitude / (speed + 0.00001f);

            tr.DOMove(movePos, duration)
                .SetEase(Ease.InSine)
                .OnComplete(() => isMoving = false);
            tr.DOLookAt(movePos, 0.4f);

            while (isMoving)
            {
                yield return null;
            }
        }
    }
}
