using System;
using UnityEngine;

public class AbstractEvent<T> : ScriptableObject
{
    public event Action<T> OnRaise;

    public void Raise(T value)
    {
        if (OnRaise != null)
            OnRaise(value);
    }
}
