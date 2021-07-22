using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoidsSimulationOnGPU;

namespace BoidsSimulationOnGPU
{
    public class LobbyManager : MonoBehaviour
    {
        public static LobbyManager instance;

        [SerializeField] FileManager fileManager;

        public FileManager FileManager
        {
            get { return fileManager; }
        }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            fileManager.InitPath();
        }
    }
}
