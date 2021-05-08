using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCreator : Action
{
    public GameObject original;
    public FloatData size;
    public FloatData forceMagnitude;
    public ForceModeData forceMode;

    public override eActionType actionType => eActionType.Force;

    bool action { get; set; } = false;
    bool onetime { get; set; } = false;

    void Update()
    {
        if (action && (onetime || Input.GetKey(KeyCode.LeftControl)))
        {
            onetime = false;
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject gameObject = Instantiate(original, position, Quaternion.identity);
            if (gameObject.TryGetComponent<PointEffector>(out PointEffector effector))
            {
                effector.forceMagnitude = forceMagnitude; 
                effector.shape.size = size;
                effector.forceMode = forceMode.value; 
                World.Instance.forces.Add(effector);
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
