using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Collision
{
    public static bool TestOverlap(Body bodyA, Body bodyB)
    {
        if (bodyA.type == Body.eType.Static && bodyB.type == Body.eType.Static) return false;

        Circle circleA = new Circle(bodyA.position, ((CircleShape)bodyA.shape).radius);
        Circle circleB = new Circle(bodyB.position, ((CircleShape)bodyB.shape).radius);

        return circleA.Contains(circleB);
    }

    public static void CreateNarrowPhaseContacts(ref List<Contact> contacts)
    {
        contacts.RemoveAll(contact => (TestOverlap(contact.bodyA, contact.bodyB) == false));
    }

    public static void UpdateContactInfo(ref Contact contact)
    {
        if (contact.bodyA.shape.type == Shape.eType.Circle && contact.bodyB.shape.type == Shape.eType.Circle)
        {
            Circle circleA = new Circle(contact.bodyA.position, ((CircleShape)contact.bodyA.shape).radius);
            Circle circleB = new Circle(contact.bodyB.position, ((CircleShape)contact.bodyB.shape).radius);

            Vector2 direction = circleA.center - circleB.center;
            float distance = direction.magnitude;
            contact.depth = (circleA.radius + circleB.radius) - distance;
            contact.normal = direction.normalized;
        }
    }

    public static void CreateBroadPhaseContacts(BroadPhase broadPhase, List<Body> bodies, out List<Contact> contacts)
    {
        contacts = new List<Contact>();
        List<Body> queryBodies = new List<Body>();
        foreach (Body body in bodies)
        {
            queryBodies.Clear();
            broadPhase.Query(body, queryBodies);
            foreach (Body queryBody in queryBodies)
            {
                if (queryBody == body) continue;
                Contact contact = new Contact() { bodyA = body, bodyB = queryBody };
                contacts.Add(contact);
            }
        }
    }

    /*public static void CreateContacts(List<Body> bodies, out List<Contact> contacts)
    {
        contacts = new List<Contact>();

        for (int i = 0; i < bodies.Count - 1; i++)
        {
            for (int j = i + 1; j < bodies.Count; j++)
            {
                Body bodyA = bodies[i];
                Body bodyB = bodies[j];

                if(bodyA.type == Body.eType.Static && bodyB.type == Body.eType.Static) continue;

                Circle circleA = new Circle(bodyA.position, ((CircleShape)bodyA.shape).radius);
                Circle circleB = new Circle(bodyB.position, ((CircleShape)bodyB.shape).radius);

                if(circleA.Contains(circleB))
                {
                    Contact contact = new Contact() { bodyA = bodyA, bodyB = bodyB };
                    Vector2 direction = circleA.center - circleB.center;
                    float distance = direction.magnitude;
                    contact.depth = (circleA.radius + circleB.radius) - distance;
                    contact.normal = direction.normalized;
                    contacts.Add(contact);
                }
            }
        }
    }*/
}
