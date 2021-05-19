using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BVH : BroadPhase
{
    BVHNode rootNode;
    
    public override void Build(AABB aabb, List<Body> bodies)
    {
        potentialCollisionCount = 0;
        List<Body> sorted = new List<Body>(bodies);
        sorted.Sort((body, body2) => body.position.x.CompareTo(body2.position.x));
        rootNode = new BVHNode(sorted);
    }

    public override void Query(AABB aabb, List<Body> bodies)
    {
        rootNode?.Query(aabb, bodies);
        potentialCollisionCount += bodies.Count;
    }
    
    public override void Query(Body body, List<Body> bodies)
    {
        Query(body.shape.aabb, bodies);
    }

    public override void Draw()
    {
        rootNode?.Draw();
    }
}
