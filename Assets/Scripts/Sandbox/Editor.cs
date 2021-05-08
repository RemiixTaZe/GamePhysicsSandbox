using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor : MonoBehaviour
{
    public Action[] actions;

    delegate void ActionDelegate();

    ActionDelegate startActionHandler;
    ActionDelegate stopActionHandler;

    private void Start()
    {
        SetAction(Action.eActionType.Creator);   
    }

    public void StartAction()
    {
        if(startActionHandler != null) startActionHandler();
    }

    public void StopAction()
    {
        if(stopActionHandler != null) stopActionHandler();
    }

    public void SetAction(Action.eActionType actionType)
    {
        startActionHandler = actions[(int)actionType].StartAction;
        stopActionHandler = actions[(int)actionType].StopAction;
    }
}
