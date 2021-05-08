using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum eState
    {
        Up,
        Down
    }

    [System.Serializable]
    public struct EventInfo
    {
        public PointerEventData.InputButton button;
        public eState state;
        public UnityEvent uevent;
    }

    public EventInfo[] eventInfos;

    public void OnPointerDown(PointerEventData eventData)
    {
        foreach(var eventInfo in eventInfos)
        {
            if (eventData.button == eventInfo.button && eventInfo.state == eState.Down)
            {
                eventInfo.uevent.Invoke();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        foreach (var eventInfo in eventInfos)
        {
            if (eventData.button == eventInfo.button && eventInfo.state == eState.Up)
            {
                eventInfo.uevent.Invoke();
            }
        }
    }
}
