using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Contact 
{
    public Body bodyA;
    public Body bodyB;
    public float depth;
    public Vector2 normal;
}
