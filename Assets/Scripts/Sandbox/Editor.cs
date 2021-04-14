using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor : MonoBehaviour
{
    public Action[] actions;

    public void StartAction()
    {
        actions[0].StartAction();
    }

    public void StopAction()
    {
        actions[0].StopAction();
    }
}
