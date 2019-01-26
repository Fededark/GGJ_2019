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
        boolEvent.OnRaise += listener.Invoke;
    }
}

[System.Serializable]
public class UnityBoolEvent : UnityEvent<bool> { }