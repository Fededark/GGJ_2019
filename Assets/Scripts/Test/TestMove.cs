using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public void Move()
    {
        var h = Home.Instance;
        var pos = new Vector2Int(2, 3);
        var dest = new Vector2Int(3, 3);
        var r = h.RoomAt(pos);
        h.MoveRoom(r.GO.GetComponent<RoomBehaviour>(), r, dest);
    }

    public void Rotate(Room r)
    {
        r.Rotate(false);
        r.GO.GetComponent<RoomBehaviour>().ApplyRotation();
    }
}
