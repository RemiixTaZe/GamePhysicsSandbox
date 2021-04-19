using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bool",menuName = "Data/Bool")]
public class BoolData : ScriptableObject
{
    public bool value;


    public static implicit operator bool(BoolData data) { return data.value; }
}
