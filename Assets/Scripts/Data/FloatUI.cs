using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatUI : MonoBehaviour
{
    public Slider slider = null;
    public TMP_Text label = null;
    public TMP_Text valueText = null;
    public float min = 0;
    public float max = 1;

    public FloatData data = null;

    private void OnValidate()
    {
        if (data != null)
        {
            name = data.name;
            label.text = name;
        }
        slider.minValue = min;
        slider.maxValue = max;
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(UpdateValue);
    }

    void Update()
    {
        slider.value = data.value;
        valueText.text = data.value.ToString("F2");
    }

    void UpdateValue(float value)
    {
        data.value = value;
    }
}
