using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BroadphaseType", menuName = "Data/Enum/BroadphaseType")]
public class BroadphaseTypeData : EnumData
{
    public enum eType
    {
        None,
        QuadTree,
        BVH
    }

    public eType value;

    public override int index { get => (int)value; set => this.value = (eType)value; }
    public override string[] names => Enum.GetNames(typeof(eType));
}
