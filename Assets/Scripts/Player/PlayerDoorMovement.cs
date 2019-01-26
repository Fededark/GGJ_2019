using UnityEngine;
using System.Collections;

public class PlayerDoorMovement : MonoBehaviour
{
    public HomeInfo homeInfo;

    void Awake()
    {
        homeInfo.doorPassEvent.OnDoorPassed += OnDoorPassed;
    }

    public void AssignTo(Room room)
    {
        transform.SetParent(room.GO.transform);
    }

    private void OnDoorPassed(Vector2Int from, Vector2Int to)
    {
        Vector3 newPosition;
        if (from.x == to.x)
        {
            if (from.y < to.y)
                newPosition = new Vector3(to.x, to.y - homeInfo.doorDistance.y);
            else
                newPosition = new Vector3(to.x, to.y + homeInfo.doorDistance.y);
        }
        else
        {
            if (from.x < to.x)
                newPosition = new Vector3(to.x - homeInfo.doorDistance.x, to.y);
            else
                newPosition = new Vector3(to.x + homeInfo.doorDistance.x, to.y);
        }
        AssignTo(Home.Instance.RoomAt(to));
        transform.position = newPosition;
        //TODO
    }
}
