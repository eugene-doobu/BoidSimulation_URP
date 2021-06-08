using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoidsSimulationOnGPU
{
    public class SharkManager : MonoBehaviour
    {
        [SerializeField] GameObject sharkPrefab = null;
        List<GameObject> sharks = new List<GameObject>();

        private void Start()
        {
            for (int i = 0; i <SimulationManager.instance.numOfShark; i++)
            {
                sharks.Add(Instantiate(sharkPrefab));
                sharks[i].transform.SetParent(transform);
            }
        }
    }
}
