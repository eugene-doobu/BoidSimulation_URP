//#define RaycastOn
using UnityEngine;
using System.Runtime.InteropServices;

namespace BoidsSimulationOnGPU
{
    public class GPUBoids : MonoBehaviour
    {
        // Boid 데이터의 구조체
        [System.Serializable]
        struct BoidData
        {
            public Vector3 Velocity; // 속도
            public Vector3 Position; // 위치
        }
        // 스레드 그룹의 크기
        const int SIMULATION_BLOCK_SIZE = 256;

        #region Boids Parameters
        // 최대 개체 수
        [Range(256, 65536)]
        public int MaxObjectNum = 16384;

        // 결합을 적용하는 다른 개체와의 반경
        public float CohesionNeighborhoodRadius = 2.0f;
        // 정렬을 적용하는 다른 개체와의 반경
        public float AlignmentNeighborhoodRadius = 2.0f;
        // 분리를 적용하는 다른 개체와의 반경
        public float SeparateNeighborhoodRadius = 1.0f;

        // 속도의 최대치
        public float MaxSpeed = 5.0f;
        // 조향력의 최대치
        public float MaxSteerForce = 0.5f;

        // 결합하는 힘의 시뮬레이션 가중치
        public float CohesionWeight = 1.0f;
        // 정렬하는 힘의 시뮬레이션 가중치
        public float AlignmentWeight = 1.0f;
        // 분리하는 힘의 시뮬레이션 가중치
        public float SeparateWeight = 3.0f;

        // 벽을 피하는 힘의 시뮬레이션 가중치
        public float AvoidWallWeight = 10.0f;
        // 장애물을 피하는 힘의 시뮬레이션 가중치
        public float AvoidObstacleWeight = 100.0f;
        // 장애물을 감지하는 거리(시야)
        public float AvoidObstacleDistance = 0.9f;

        // 벽의 중심 좌표
        public Vector3 WallCenter = Vector3.zero;
        // 벽의 크기
        public Vector3 WallSize = new Vector3(32.0f, 32.0f, 32.0f);
        #endregion

        #region Built-in Resources
        // Boids 시뮬레이션을 실행하는 ComputeShader의 참조
        public ComputeShader BoidsCS;
        #endregion

        #region Private Resources
        // Boid 조향력(Force)을 포함하는 버퍼
        ComputeBuffer _boidForceBuffer;
        // Boid의 기본 데이터(속도, 위치)를 포함하는 버퍼
        ComputeBuffer _boidDataBuffer;
        // Obstacle의 위치를 포함하는 버퍼
        ComputeBuffer _obstacleDataBuffer;
        // 현재 Boid 데이터를 저장
        BoidData[] currBoidDataArray;
        #endregion

        #region Accessors
        // Boid의 기본 데이터를 저장하는 버퍼를 반환
        public ComputeBuffer GetBoidDataBuffer()
        {
            return this._boidDataBuffer != null ? this._boidDataBuffer : null;
        }

        // 개체 수를 반환합니다
        public int GetMaxObjectNum()
        {
            return this.MaxObjectNum;
        }

        // 시뮬레이션 영역의 중심 좌표를 반환
        public Vector3 GetSimulationAreaCenter()
        {
            return this.WallCenter;
        }

        // 시뮬레이션 영역의 박스의 크기를 반환
        public Vector3 GetSimulationAreaSize()
        {
            return this.WallSize;
        }
        #endregion

        #region MonoBehaviour Functions
        void Start()
        {
            // 버퍼 초기화
            InitBuffer();
        }

        void Update()
        {
            // 시뮬레이션
            Simulation();
        }

        void OnDestroy()
        {
            // 버퍼를 해제
            ReleaseBuffer();
        }

        void OnDrawGizmos()
        {
            // 디버그로서 시뮬레이션 영역을 와이어 프레임으로 렌더링
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(WallCenter, WallSize);
        }
        #endregion

        #region Private Functions
        // 버퍼 초기화
        void InitBuffer()
        {
            // 보이드 데이터 어레이 배열생성
            currBoidDataArray = new BoidData[MaxObjectNum];

            // 버퍼 초기화
            _boidDataBuffer = new ComputeBuffer(MaxObjectNum,
                Marshal.SizeOf(typeof(BoidData)));
            _boidForceBuffer = new ComputeBuffer(MaxObjectNum,
                Marshal.SizeOf(typeof(Vector3)));
            _obstacleDataBuffer = new ComputeBuffer(MaxObjectNum,
                Marshal.SizeOf(typeof(Vector3)));
            
            // Boid 데이터, fORCE 버퍼를 초기화
            var forceArr = new Vector3[MaxObjectNum];
            var boidDataArr = new BoidData[MaxObjectNum];
            var obstacleArr = new Vector3[MaxObjectNum];
            for (var i = 0; i < MaxObjectNum; i++)
            {
                forceArr[i] = Vector3.zero;
                boidDataArr[i].Position = Random.insideUnitSphere * 1.0f;
                boidDataArr[i].Velocity = Random.insideUnitSphere * 0.1f;
                obstacleArr[i] = Vector3.zero;
            }
            _boidForceBuffer.SetData(forceArr);
            _boidDataBuffer.SetData(boidDataArr);
            _obstacleDataBuffer.SetData(obstacleArr);
            forceArr = null;
            boidDataArr = null;
            obstacleArr = null;
        }

        // 시뮬레이션
        void Simulation()
        {
            ComputeShader cs = BoidsCS;
            int id = -1;

            // 스레드그룹 수를 구하기
            int threadGroupSize = Mathf.CeilToInt(MaxObjectNum / SIMULATION_BLOCK_SIZE);
                // 조향력을 계산
            id = cs.FindKernel("ForceCS"); // 커널 ID를 가져옴
            cs.SetInt("_MaxBoidObjectNum", MaxObjectNum);
            cs.SetFloat("_CohesionNeighborhoodRadius", CohesionNeighborhoodRadius);
            cs.SetFloat("_AlignmentNeighborhoodRadius", AlignmentNeighborhoodRadius);
            cs.SetFloat("_SeparateNeighborhoodRadius", SeparateNeighborhoodRadius);
            cs.SetFloat("_MaxSpeed", MaxSpeed);
            cs.SetFloat("_MaxSteerForce", MaxSteerForce);
            cs.SetFloat("_SeparateWeight", SeparateWeight);
            cs.SetFloat("_CohesionWeight", CohesionWeight);
            cs.SetFloat("_AlignmentWeight", AlignmentWeight);
            cs.SetVector("_WallCenter", WallCenter);
            cs.SetVector("_WallSize", WallSize);
            cs.SetFloat("_AvoidWallWeight", AvoidWallWeight);
            cs.SetFloat("_AvoidObstacleWeight", AvoidObstacleWeight);
            cs.SetBuffer(id, "_BoidDataBufferRead", _boidDataBuffer);
            cs.SetBuffer(id, "_BoidForceBufferWrite", _boidForceBuffer);
            cs.Dispatch(id, threadGroupSize, 1, 1); // ComputeShader를 실행


            // 계산된 조항력으로부터 속도와 위치를 업데이트
            id = cs.FindKernel("IntegrateCS"); // 커널 ID를 가져옴

            // 계산된 데이터 저장
            this._boidDataBuffer.GetData(currBoidDataArray);

            // 장애물 회피 계산
            var obstacleArr = ComputeObstacleCollision();

            // 컴퓨트 셰이더에 데이터 입력
            _obstacleDataBuffer.SetData(obstacleArr);
            cs.SetFloat("_DeltaTime", Time.deltaTime);
            cs.SetBuffer(id, "_ObstaclePosBufferRead", _obstacleDataBuffer);
            cs.SetBuffer(id, "_BoidForceBufferRead", _boidForceBuffer);
            cs.SetBuffer(id, "_BoidDataBufferWrite", _boidDataBuffer);
            cs.Dispatch(id, threadGroupSize, 1, 1); // ComputeShader를 실행
            obstacleArr = null;
        }

        Vector3[] ComputeObstacleCollision()
        {
            var obstacleArr = new Vector3[MaxObjectNum];
            for (int i = 0; i < MaxObjectNum; i++)
            {
                RaycastHit hit;
                if (Physics.Raycast(currBoidDataArray[i].Position,
                    currBoidDataArray[i].Velocity,
                    out hit,
                    AvoidObstacleDistance))
                {
#if RaycastOn
                Debug.DrawRay(currBoidDataArray[i].Position,
                    currBoidDataArray[i].Velocity.normalized * AvoidObstacleDistance,
                    Color.red);
#endif
                    obstacleArr[i] = Vector3.Normalize(currBoidDataArray[i].Position - hit.point);
                }
                else
                {
#if RaycastOn
                Debug.DrawRay(currBoidDataArray[i].Position,
                    currBoidDataArray[i].Velocity.normalized * AvoidObstacleDistance,
                    Color.green);
#endif
                    obstacleArr[i] = Vector3.zero;
                }
            }
            return obstacleArr;
        }

        // 버퍼를 해제
        void ReleaseBuffer()
        {
            if (_boidDataBuffer != null)
            {
                _boidDataBuffer.Release();
                _boidDataBuffer = null;
            }

            if (_boidForceBuffer != null)
            {
                _boidForceBuffer.Release();
                _boidForceBuffer = null;
            }
            if (_obstacleDataBuffer != null)
            {
                _obstacleDataBuffer.Release();
                _obstacleDataBuffer = null;
            }
        }
#endregion
    } // class
} // namespace