using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public BoolData collision;
    public BoolData wrap;
    public BoolData collisionDebug;
    public FloatData gravity;
    public FloatData gravitation;
    public FloatData fixedFps;
    public StringData fpsText;
    public StringData collisionText;
    public VectorField vectorField;
    public BroadphaseTypeData broadphaseType;

    public Vector2 WorldSize { get => size * 2; }
    public AABB AABB { get => aabb; }

    AABB aabb;
    BroadPhase broadPhase = new NullBroadPhase();
    BroadPhase[] broadPhases = { new NullBroadPhase(),new Quadtree(), new BVH() };

    private Vector2 size;
    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }
    public List<Body> bodies { get; set; } = new List<Body>();
    public List<Spring> springs { get; set; } = new List<Spring>();
    public List<Force> forces { get; set; } = new List<Force>();

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
        size = Camera.main.ViewportToWorldPoint(Vector2.one);
        aabb = new AABB(Vector2.zero, size * 2);
    }

    void Update()
    {

        float dt = Time.deltaTime;
        fps = (1.0f / dt);
        fpsAverage = (fpsAverage * smoothing) + (fps * (1.0f - smoothing));

        broadPhase = broadPhases[broadphaseType.index];

        fpsText.value = "FPS: " + fpsAverage.ToString("F1");

        springs.ForEach(spring => spring.Draw());
        if (!simulate.value) return;

        GravitationalForce.ApplyForce(bodies, gravitation.value);
        forces.ForEach(force => bodies.ForEach(body => force.ApplyForce(body)));
        springs.ForEach(spring => spring.ApplyForce());
        bodies.ForEach(body => vectorField.ApplyForce(body));

        timeAccumulator += dt;
        while (timeAccumulator >= fixedDeltaTime) 
        { 
            bodies.ForEach(body => body.Step(fixedDeltaTime));
            bodies.ForEach(body => Integrator.SemiImplicitEuler(body, fixedDeltaTime));

            bodies.ForEach(body => body.shape.color = Color.white);
            if(collision)
            {
                bodies.ForEach(body => body.shape.color = Color.white);
                broadPhase.Build(aabb, bodies);

                Collision.CreateBroadPhaseContacts(broadPhase, bodies, out List<Contact> contacts);
                Collision.CreateNarrowPhaseContacts(ref contacts);
                contacts.ForEach(contact => Collision.UpdateContactInfo(ref contact));

                ContactSolver.Resolve(contacts);
                if (collisionDebug) contacts.ForEach(contact => { contact.bodyA.shape.color = Color.magenta; contact.bodyB.shape.color = Color.magenta; });
            }
            timeAccumulator -= fixedDeltaTime; 
        }
        collisionText.value = "Broad Phase: " + BroadPhase.potentialCollisionCount.ToString();
        if (collisionDebug)
        {
            broadPhase.Draw();
        }

        if (wrap) 
        { 
            bodies.ForEach(body => body.position = Utilities.Wrap(body.position, -size, size));
        }

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);

    }
}
