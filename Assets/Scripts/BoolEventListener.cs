using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoolEventListener : MonoBehaviour
{
    public BoolEvent boolEvent;
    public bool invert = false;

    public UnityBoolEvent listener;

    private void Awake()
    {
        if (boolEvent != null)
        {
            if (invert)
                boolEvent.OnRaise += listener.Invoke;
            else
                boolEvent.OnRaise += invertRaise;
        }
        else
            Debug.LogWarning("missing event", gameObject);
    }

    private void invertRaise(bool obj)
    {
        listener.Invoke(!obj);
    }
}

[System.Serializable]
public class UnityBoolEvent : UnityEvent<bool> { }