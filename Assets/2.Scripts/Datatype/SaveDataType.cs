using System;

namespace SaveData
{
    [Serializable]
    public class SimulationSetting
    {
        public int numOfFish;
        public int numOfShark;
        public float cohesionRadius;
        public float alignmentRadius;
        public float separateRadius;
        public float avoidObstacleDistance;
        public float cohesionWeight;
        public float alignmentWeight;
        public float separateWeight;
        public float avoidObstacleWeight;
        public float maxSpeed;
        public float maxSteer;
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