using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BVHNode
{
    public AABB aabb;
    public List<Body> bodies = null; 
    public BVHNode left = null; 
    public BVHNode right = null; 

    public BVHNode(List<Body> bodies) 
    { 
        this.bodies = bodies; 
        ComputeBoundary(); 
        Split(); 
    }

    public void Query(AABB aabb, List<Body> bodies)
    {
        if (!this.aabb.Contains(aabb)) return;
        if (this.bodies.Count > 0)
        {
            bodies.AddRange(this.bodies);
        }
        left?.Query(aabb, bodies);
        right?.Query(aabb, bodies);
    }

    public void ComputeBoundary()
    {
        if (bodies.Count > 0)
        {
            aabb.center = bodies[0].position; 
            aabb.size = Vector3.zero; 
            foreach (Body body in bodies)
            {
                aabb.Expand(body.shape.aabb);
            }
        }
    }

    public void Split() 
    { 
        int length = bodies.Count;
        int half = length/2; 
        if (half >= 1) 
        { 
            left = new BVHNode(bodies.GetRange(0, half)); 
            right = new BVHNode(bodies.GetRange(half, half + (length % 2))); 
            bodies.Clear();
        }
    }

    public void Draw() 
    { 
        aabb.Draw(Color.white);
        left?.Draw();
        right?.Draw();
    }
}
