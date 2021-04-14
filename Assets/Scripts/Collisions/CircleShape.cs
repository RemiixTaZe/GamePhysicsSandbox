using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShape : Shape
{
    public float radius { get => size * 0.5f; }

    public override float size { get => transform.localScale.x; set => transform.localScale = Vector2.one * value; }
    public override eType type => eType.Circle;

    public override float mass => (Mathf.PI * radius) * density;

}
