using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ContactSolver
{
    public static void Resolve(List<Contact> contacts)
    {
        foreach(Contact contact in contacts)
        {
            float totalInverseMass = contact.bodyA.inverseMass + contact.bodyB.inverseMass;
            Vector2 seperation = (contact.normal * contact.depth)/ totalInverseMass;
            contact.bodyA.position += seperation * contact.bodyA.inverseMass;
            contact.bodyB.position -= seperation * contact.bodyB.inverseMass;
        }
    }
}
