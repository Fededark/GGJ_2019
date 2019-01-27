using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoolEventListener : MonoBehaviour
{
    public BoolEvent boolEvent;

    public UnityBoolEvent listener;

    private void Awake()
    {
        if (boolEvent != null)
            boolEvent.OnRaise += listener.Invoke;
        else
            Debug.LogWarning("missing event", gameObject);
    }
}

[System.Serializable]
public class UnityBoolEvent : UnityEvent<bool> { }