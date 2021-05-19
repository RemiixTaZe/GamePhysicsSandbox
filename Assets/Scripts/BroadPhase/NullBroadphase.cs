using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullBroadPhase : BroadPhase
{
	public List<Body> bodies { get; set; } = new List<Body>();

	public override void Build(AABB aabb, List<Body> bodies)
	{
		potentialCollisionCount = 0;
		this.bodies.Clear();
		this.bodies.AddRange(bodies);
	}

	public override void Query(AABB aabb, List<Body> bodies)
	{
		bodies.AddRange(this.bodies);
		potentialCollisionCount = potentialCollisionCount + bodies.Count;
	}

	public override void Query(Body body, List<Body> bodies)
	{
		Query(body.shape.aabb, bodies);
	}

	public override void Draw()
	{
		//
	}
}

