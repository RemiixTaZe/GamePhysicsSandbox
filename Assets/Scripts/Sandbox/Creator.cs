using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : Action
{
    public GameObject original;
    public FloatData speed;
    public FloatData damping;
    public FloatData size;
    public FloatData density;


    bool action { get; set; } = false;
    bool onetime { get; set; } = false;

    void Update()
    {
        if (action && (onetime || Input.GetKey(KeyCode.LeftControl)))
        {
            onetime = false;
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject gameobject = Instantiate(original, position, Quaternion.identity);
            if(gameobject.TryGetComponent<Body>(out Body body))
            {
                Vector2 force = Random.insideUnitSphere.normalized * speed;

                body.AddForce(force, Body.eForceMode.Velocity);
                body.damping = damping; ;
                body.shape.size = size;
                body.shape.density = density;
                World.Instance.bodies.Add(body);
            }
        }
    }

    public override void StartAction()
    {
        action = true;
        onetime = true;
    }

    public override void StopAction()
    {
        action = false;
    }
}
