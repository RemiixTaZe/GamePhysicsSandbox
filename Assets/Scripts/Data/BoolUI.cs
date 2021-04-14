using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class BoolUI : MonoBehaviour
{
    public Toggle toggle = null;
    public Text label = null;
    public BoolData data = null;

    private void OnValidate()
    {
        if (data != null)
        {
            name = data.name;
            label.text = name;
        }
    }

    private void Start()
    {
        toggle.onValueChanged.AddListener(UpdateValue);
    }

    void Update()
    {
        toggle.isOn = data.value;
    }

    void UpdateValue(bool value)
    {
        data.value = value;
    }
}
