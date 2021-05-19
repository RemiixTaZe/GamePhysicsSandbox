using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuadtreeNode
{
    AABB aabb;
    int capacity;
    List<Body> bodies;
    bool subdivided = false;

    QuadtreeNode northeast;
    QuadtreeNode northwest;
    QuadtreeNode southeast;
    QuadtreeNode southwest;

    public QuadtreeNode(AABB aabb, int capacity) 
    {
        this.aabb = aabb;
        this.capacity = capacity;

        bodies = new List<Body>();
    }

    public void Insert(Body body)
    {
        if (!aabb.Contains(body.shape.aabb)) return;

        if(bodies.Count < capacity)
        {
            bodies.Add(body);
        }
        else
        {
            if(!subdivided)
            {
                Subdivide();
            }

            northeast.Insert(body);
            northwest.Insert(body);
            southeast.Insert(body);
            southwest.Insert(body);
        }

    }

    private void Subdivide()
    {
        float xo = aabb.extents.x * 0.5f;
        float yo = aabb.extents.y * 0.5f;
        northeast = new QuadtreeNode(new AABB(new Vector2(aabb.center.x - xo, aabb.center.y + yo), aabb.extents), capacity);
        northwest = new QuadtreeNode(new AABB(new Vector2(aabb.center.x + xo, aabb.center.y + yo), aabb.extents), capacity);
        southeast = new QuadtreeNode(new AABB(new Vector2(aabb.center.x - xo, aabb.center.y - yo), aabb.extents), capacity);
        southwest = new QuadtreeNode(new AABB(new Vector2(aabb.center.x + xo, aabb.center.y - yo), aabb.extents), capacity);
        subdivided = true;
    }

    public void Query(AABB aabb, List<Body> bodies)
    {
        if (!this.aabb.Contains(aabb)) return;

        bodies.AddRange(this.bodies.Where(body => body.shape.aabb.Contains(aabb)));

        if(subdivided)
        {
            northeast.Query(aabb, bodies);
            northwest.Query(aabb, bodies);
            southeast.Query(aabb, bodies);
            southwest.Query(aabb, bodies);
        }
    }

    public void Draw()
    {
        aabb.Draw(Color.white);

        northeast?.Draw();
        northwest?.Draw();
        southeast?.Draw();
        southwest?.Draw();
    }
}
