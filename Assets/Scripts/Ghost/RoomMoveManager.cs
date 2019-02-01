using ChanibaL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMoveManager
{
    private static RoomMoveManager inst = null;

    public static RoomMoveManager Instance
    {
        get
        {
            if (inst == null)
                inst = new RoomMoveManager();
            return inst;
        }
    }

    public RoomBehaviour placeholder = null;
    public RoomBehaviour src = null;

    private Room tmpRoom;

    //public void OnRoomClick(RoomBehaviour rb)
    //{
    //    if (placeholder != null)
    //    {
    //        if (placeholder != rb) return;
    //        //place
    //        var pos = placeholder.transform.position;
    //        Vector2Int destPos = new Vector2Int((int)pos.x, (int)pos.y);
    //        if (Home.Instance.CanBePlaced(placeholder.room, destPos.x, destPos.y))
    //        {
    //            Home.Instance.MoveRoom(src, placeholder.room, destPos);
    //            GameObject.Destroy(placeholder.gameObject);
    //            placeholder = null;
    //            src = null;
    //        }
    //        return;
    //    }
    //    if (!Home.Instance.CanBeRemoved(rb.room))
    //    {
    //        Debug.Log("Cannot be removed", rb.gameObject);
    //        return;
    //    }

    //    src = rb;
    //    RoomBehaviour ph = GameObject.Instantiate(rb);
    //    placeholder = ph;
    //    ph.GetComponent<TakUtility.MouseFollow>().enabled = true;
    //}

    public bool RandomMove()
    {
        if (tmpRoom == null)
            tmpRoom = ScriptableObject.CreateInstance<Room>();
        var h = Home.Instance;
        var move = new List<Room>();
        foreach (var r in h.rooms)
        {
            if (h.CanBeRemoved(r))
                move.Add(r);
        }
        if (move.Count == 0) return false;
        move = move.GetShuffled();
        foreach (var r in move)
        {
            tmpRoom.CopyFrom(r);
            foreach (var rotation in RandomRotations())
            {
                switch (rotation)
                {
                    case -1:
                        tmpRoom.Rotate(false);
                        break;
                    case 1:
                        tmpRoom.Rotate(true);
                        break;
                    case 2:
                        tmpRoom.Rotate(true);
                        tmpRoom.Rotate(true);
                        break;
                }
                var pos = h.WhereCanBePlaced(tmpRoom);
                if (pos.Count > 0)
                {
                    var p = pos[Random.Range(0, pos.Count)];
                    h.MoveRoom(r.GO.GetComponent<RoomBehaviour>(), tmpRoom, p);
                    return true;
                }
            }
        }
        return false;
    }

    private int[] RandomRotations()
    {
        int[] dir = new int[] { Cell.UP, Cell.RIGHT, Cell.DOWN, Cell.LEFT};
        for (int i = dir.Length-1; i >= 1; i--)
        {
            int p = Random.Range(0, i+1);
            if (p != i)
            {
                int tmp = dir[p];
                dir[p] = dir[i];
                dir[i] = tmp;
            }
        }
        int lastSide = Cell.UP;
        int[] rel = new int[4];
        for (int i = 0; i < 4; i++)
        {
            int diff = (dir[i] - lastSide + 4) % 4;
            if (diff == 3) diff = -1;
            lastSide = dir[i];
            rel[i] = diff;
        }
        return rel;
    }
}
