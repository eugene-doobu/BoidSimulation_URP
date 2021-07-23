using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    Slider slider;
    Text text;

    private void Awake()
    {
        slider = GetComponentInParent<Slider>();
        text = GetComponent<Text>();
    }

    private void Start()
    {
        text.text = slider.value.ToString();
        slider.onValueChanged.AddListener(v =>
        {
            text.text = v.ToString();
        });
    }
}
