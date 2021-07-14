using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveData;

namespace BoidsSimulationOnGPU
{
    public class SharkManager : MonoBehaviour
    {
        [SerializeField] GameObject sharkPrefab = null;
        List<GameObject> sharks = new List<GameObject>();
        int numOfShark = 3;

        private void Start()
        {
            for (int i = 0; i <numOfShark; i++)
            {
                sharks.Add(Instantiate(sharkPrefab));
                sharks[i].transform.SetParent(transform);
            }
        }

        public void GetSettingData(SimulationSetting data)
        {
            numOfShark = data.numOfShark;
        }
    }
}
