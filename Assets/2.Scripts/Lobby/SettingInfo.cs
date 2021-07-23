using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingInfo : MonoBehaviour
{
    CanvasGroup canvasGroup;

    WaitForSeconds fadeWait;
    WaitForSeconds fadeTick;

    float fadewWaitTime = 0.5f;
    float fadeTickTime = 0.012f;
    int fadeStep = 50;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        fadeWait = new WaitForSeconds(fadewWaitTime);
        fadeTick = new WaitForSeconds(fadeTickTime);
    }

    private void OnEnable()
    {
        // √ ±‚»≠
        StopAllCoroutines();
        canvasGroup.alpha = 1;

        // Fade
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return fadeWait;
        for (int i=0; i< fadeStep; ++i)
        {
            yield return fadeTick;
            canvasGroup.alpha = 1 - i * (1.0f / fadeStep);
        }
        canvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }
}
