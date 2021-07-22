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

        public SimulationSetting()
        {
            numOfFish = 65536;
            numOfShark = 3;
            cohesionRadius = 2;
            alignmentRadius = 2;
            separateRadius = 1;
            avoidObstacleDistance = 0.9f;
            cohesionWeight = 1;
            alignmentWeight = 1;
            separateWeight = 3;
            avoidObstacleWeight = 100;
            maxSpeed = 5;
            maxSteer = 0.5f;
        }
    }

    [Serializable]
    public class PlayerSetting
    {
        public float scrollSpeed;
        public float rotateXSpeed;
        public float rotateYspeed;
        public float moveSpeed;
        public float keyMoveSpeed;

        public PlayerSetting()
        {
            scrollSpeed = 1;
            rotateXSpeed = 1;
            rotateYspeed = 1;
            moveSpeed = 1;
            keyMoveSpeed = 10;
        }
    }
}