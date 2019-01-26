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

    public void OnRoomClick(RoomBehaviour rb)
    {
        if (placeholder != null)
        {
            if (placeholder != rb) return;
            //place
            var pos = placeholder.transform.position;
            Vector2Int destPos = new Vector2Int((int)pos.x, (int)pos.y);
            if (Home.Instance.CanBePlaced(placeholder.room, destPos.x, destPos.y))
            {
                Home.Instance.MoveRoom(src, placeholder.room, destPos);
                GameObject.Destroy(placeholder.gameObject);
                placeholder = null;
                src = null;
            }
            return;
        }
        if (!Home.Instance.CanBeRemoved(rb.room))
        {
            Debug.Log("Cannot be removed", rb.gameObject);
            return;
        }

        src = rb;
        RoomBehaviour ph = GameObject.Instantiate(rb);
        placeholder = ph;
        ph.GetComponent<TakUtility.MouseFollow>().enabled = true;
    }
}
