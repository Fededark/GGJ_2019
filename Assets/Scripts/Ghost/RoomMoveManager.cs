using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMoveManager : MonoBehaviour
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

}
