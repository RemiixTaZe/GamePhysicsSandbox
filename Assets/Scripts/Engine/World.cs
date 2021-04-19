using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public FloatData gravity;
    public FloatData gravitation;
    public FloatData fixedFps;
    public StringData fpsText;

    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }
    public List<Body> bodies { get; set; } = new List<Body>();

    float fixedDeltaTime { get { return 1.0f / fixedFps.value; } }
    float fps = 0;
    float fpsAverage = 0;
    float smoothing = 0.975f;
    float timeAccumulator = 0;

    static World instance;
    static public World Instance { get { return instance; } }


    private void Awake()
    {
        instance = this;
    }

    void Update()
    {

        float dt = Time.deltaTime;
        fps = (1.0f / dt);
        fpsAverage = (fpsAverage * smoothing) + (fps * (1.0f - smoothing));

        fpsText.value = "FPS: " + fpsAverage.ToString("F1");

        if (!simulate.value) return;

        GravitationalForce.ApplyForce(bodies, gravitation.value);


        timeAccumulator += dt;
        while (timeAccumulator >= fixedDeltaTime) 
        { 
            bodies.ForEach(body => body.Step(fixedDeltaTime));
            bodies.ForEach(body => Integrator.SemiImplicitEuler(body, fixedDeltaTime));

            bodies.ForEach(body => body.shape.color = Color.white);
            Collision.CreateContacts(bodies, out List<Contact> contacts);
            contacts.ForEach(contact => { contact.bodyA.shape.color = Color.red; contact.bodyB.shape.color = Color.red; });

            timeAccumulator = timeAccumulator - fixedDeltaTime; 
        }

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);

    }
}
