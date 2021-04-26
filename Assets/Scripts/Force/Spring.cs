using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public Body bodyA { get; set; } = null;
    public Body bodyB { get; set; } = null;

    public float restLength { get; set; } = 0.0f;
    public float k { get; set; } = 20.0f;

    public void ApplyForce()
    {
        Vector2 force = Utilities.SpringForce(bodyA.position, bodyB.position, restLength, k);

        bodyA.AddForce(-force);
        bodyB.AddForce( force);
    }
}
