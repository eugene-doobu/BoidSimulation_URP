using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicTiling : MonoBehaviour
{
    Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        StartCoroutine(TilingUp());
    }

    IEnumerator TilingUp()
    {
        for(int i=1; i<=10; i++)
        {
            yield return new WaitForSeconds(1f);
            mat.SetTextureScale("_BaseMap", new Vector2(i, i));
        }
    }
}
