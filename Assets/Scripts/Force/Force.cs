using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Force : MonoBehaviour
{
    public abstract void ApplyForce(Body body);
}
