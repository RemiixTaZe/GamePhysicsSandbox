using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ContactSolver
{
    public static void Resolve(List<Contact> contacts)
    {
        foreach(Contact contact in contacts)
        {
            // separation
            float totalInverseMass = contact.bodyA.inverseMass + contact.bodyB.inverseMass;
            Vector2 seperation = (contact.normal * contact.depth)/ totalInverseMass;
            contact.bodyA.position += seperation * contact.bodyA.inverseMass;
            contact.bodyB.position -= seperation * contact.bodyB.inverseMass;

            // collision impulse
            Vector2 relativeVelocity = contact.bodyA.velocity - contact.bodyB.velocity;
            float normalVelocity = Vector2.Dot(relativeVelocity, contact.normal);

            if (normalVelocity > 0) continue;

            float restitution = (contact.bodyA.restitution + contact.bodyB.restitution)/2;
            float impulseMagnitude = -(1.0f + restitution) * normalVelocity / totalInverseMass;

            Vector2 impulse = contact.normal * impulseMagnitude;
            contact.bodyA.AddForce(contact.bodyA.velocity + (impulse * contact.bodyA.inverseMass), Body.eForceMode.Velocity);
            contact.bodyB.AddForce(contact.bodyB.velocity - (impulse * contact.bodyB.inverseMass), Body.eForceMode.Velocity);
        }
    }
}
