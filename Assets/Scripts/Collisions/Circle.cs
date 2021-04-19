using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Circle 
{
    public Vector2 center { get; set; }
    public float radius { get; set; }

    public Circle(Vector2 center, float radius)
    {
        this.center = center;
        this.radius = radius;
    }

    public bool Contains(Circle circle)
    {
        Vector2 direction = circle.center - center;
        float sqrDistance = (direction.sqrMagnitude);
        float sqrRadius = (radius + circle.radius) * (radius + circle.radius);

        return (sqrDistance <= sqrRadius);
    }
}
