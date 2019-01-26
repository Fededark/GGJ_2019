using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/BoolEvent")]
public class BoolEvent : ScriptableObject
{
    public event Action<bool> OnRaise;

    public void Raise(bool value)
    {
        if (OnRaise != null)
            OnRaise(value);
    }
}
