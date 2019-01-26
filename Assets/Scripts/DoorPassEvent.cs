using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Door Pass Event")]
public class DoorPassEvent : ScriptableObject
{
    public delegate void PassDoorEvent(Vector2Int from, Vector2Int to);
    public event PassDoorEvent OnDoorPassed;

    public void Raise(Vector2Int from, Vector2Int to)
    {
        if (OnDoorPassed != null)
            OnDoorPassed(from, to);
    }
}

