using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Float",menuName = "Data/Float")]
public class FloatData : ScriptableObject
{
    public float value;

    public static implicit operator float(FloatData data) { return data.value; }
}
