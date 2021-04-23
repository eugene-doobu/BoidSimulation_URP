using System;

namespace SaveData
{
    [Serializable]
    public class SimulationSetting
    {
        public int maxObjectNum;
        public float cohesionRadius;
        public float alignmentRadius;
        public float separateRadius;
        public float maxSpeed;
        public float maxSteer;
        public float cohesionWeight;
        public float alignmentWeight;
        public float separateWeight;
        public float avoidWallWeight;
        public float avoidObstacleWeight;
        public float avoidObstacleDistance;
    }

    [Serializable]
    public class PlayerSetting
    {
        public float scrollSpeed;
        public float rotateXSpeed;
        public float rotateYspeed;
        public float moveSpeed;
        public float keyMoveSpeed;
    }
}