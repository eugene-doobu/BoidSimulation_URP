using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoidsSimulationOnGPU
{
    // 이 GameObject에 GPUBoids 컴포넌트가 연결되어 있음을 보장
    [RequireComponent(typeof(GPUBoids))]
    public class BoidsRender : MonoBehaviour
    {
        #region Paremeters
        // 그리기 할 Boids 객체의 스케일
        public Vector3 ObjectScale = new Vector3(0.1f, 0.2f, 0.5f);
        #endregion

        #region Script References
        // GPUBoids 스크립트 참조
        public GPUBoids GPUBoidsScript;
        #endregion

        #region Built-in Resources
        // 그리기를 할 메쉬 참조
        public Mesh InstanceMesh;
        // 그리기를 위한 머터리얼의 참조
        public Material InstanceRenderMaterial;
        #endregion

        #region Private Variables
        // GPU 인스턴싱을 위한 인수 (ComputerBuffer 전송용)
        // 인스턴스 당 인덱스 수, 인스턴스 수
        // 시작 인덱스 위치, 베이스 정점 위치, 인스턴스의 시작 위치
        uint[] args = new uint[5] { 0, 0, 0, 0, 0 };
        // GPU 인스턴싱을 위한 인수버퍼
        ComputeBuffer argsBuffer;
        #endregion

        #region MonoBehaviour Functions
        void Start()
        {
            //인수 버퍼 초기화
            argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint),
                ComputeBufferType.IndirectArguments);
        }

        void Update()
        {
            // 메쉬를 인스턴싱
            RenderInstancedMesh();
        }

        void OnDisable()
        {
            // 인수버퍼를 해제
            if (argsBuffer != null)
                argsBuffer.Release();
            argsBuffer = null;
        }
        #endregion

        #region Private Functions
        void RenderInstancedMesh()
        {
            try
            {
                // 렌더링용 메터리얼이 Null 또는 GPUBoids 스크립트가 Null,
                // 또는 GPU 인스턴싱이 지원되지 않으면 처리를 하지 않는다.
                if (InstanceRenderMaterial == null || GPUBoidsScript == null ||
                    !SystemInfo.supportsInstancing)
                    return;

                // 지정한 메쉬의 인덱스 가져오기
                uint numIndices = (InstanceMesh != null) ?
                    (uint)InstanceMesh.GetIndexCount(0) : 0;
                args[0] = numIndices; // 메쉬의 인덱스 수를 설정(초기화)
                args[1] = (uint)GPUBoidsScript.GetMaxObjectNum(); // 인스턴수 수 초기화
                argsBuffer.SetData(args); // 버퍼에 설정(초기화)

                // Boid 데이터를 저장하는 버퍼를 메터리얼에 설정(초기화)
                InstanceRenderMaterial.SetBuffer("_BoidDataBuffer",
                    GPUBoidsScript.GetBoidDataBuffer());
                // Boid 객체 스케일을 설정(초기화)
                InstanceRenderMaterial.SetVector("_ObjectScale", ObjectScale);
                // 境界領域を定義
                var bounds = new Bounds
                (
                    GPUBoidsScript.GetSimulationAreaCenter(), // 중심
                    GPUBoidsScript.GetSimulationAreaSize()    // 크기
                );
                // 메쉬를 GPU 인스턴싱하여 그리기
                Graphics.DrawMeshInstancedIndirect
                (
                    InstanceMesh,           // 인스턴싱하는 메쉬
                    0,                      // submesh 인덱스
                    InstanceRenderMaterial, // 그리기를 할 메터리얼
                    bounds,                 // 경계 영역
                    argsBuffer              // GPU 인스턴싱을 위한 인수의 버퍼
                );

                // Debug.Log((uint)GPUBoidsScript.GetMaxObjectNum());
            }
            catch
            {
                Debug.Log("Fail RenderInstancedMesh()");
            }
        }
        #endregion
    }
}