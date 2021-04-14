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

    void Update()
    {
        if (action)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject gameobject = Instantiate(original, position, Quaternion.identity);
            if(gameobject.TryGetComponent<Body>(out Body body))
            {
                Vector2 force = Random.insideUnitSphere.normalized * speed;

                body.AddForce(force); //, Body.eForceMode.Velocity);
                body.damping = damping; ;
                body.shape.size = size;
                body.shape.density = density;
                World.Instance.bodies.Add(body);
            }
        }
    }

    bool action { get; set; } = false;

    public override void StartAction()
    {
        action = true;
    }

    public override void StopAction()
    {
        action = false;
    }
}
