using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkManager : MonoBehaviour
{
    [SerializeField] GameObject sharkPrefab = null;
    List<GameObject> sharks = new List<GameObject>();

    private void Awake()
    {
        for(int i=0; i<5; i++)
        {
            sharks.Add(Instantiate(sharkPrefab));
            sharks[i].transform.SetParent(transform);
        }
    }
}
