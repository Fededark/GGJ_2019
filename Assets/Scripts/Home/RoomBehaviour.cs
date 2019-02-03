﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public Room room;

    public GameObject sprite;
    public GameObject darkness;
    public GameObject hidden;
    public HomeInfo info;
    public Transform[] objectPositions;

    private bool isDark, isHidden;
    private GameObject pickup = null;

    private void Awake()
    {
        room.roomChange.OnRaise += RoomChange_OnRaise;
        room.lightChange.OnRaise += LightChange_OnRaise;
    }

    private void OnDestroy()
    {
        room.roomChange.OnRaise -= RoomChange_OnRaise;
        room.lightChange.OnRaise -= LightChange_OnRaise;
    }

    private void LightChange_OnRaise(bool dark)
    {
        darkness.SetActive(dark);
        //TODO pickup
    }

    private void RoomChange_OnRaise(bool entered)
    {
        hidden.SetActive(!entered);
        if (entered && pickup != null)
        {
            pickup.transform.up = info.playerTransform.up;
        }
    }

    public void ApplyRotation()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, -90f * room.rotation);
    }

    public void OnDrawGizmos()
    {
        Vector3 r = new Vector3(room.X, room.Y, 0);
        foreach(var c in room.connections)
        {
            Gizmos.DrawLine(r, new Vector3(c.X, c.Y, 0));
        }

        for(int x=-1; x<2; x++)
        {
            for (int y=-1; y<2; y++)
            {
                if (!room.HasCell(x, y)) continue;
                Debug.Assert(room.GetCell(x, y).room == room, room.name);
                for (int s = 0; s < 4; s++) {
                    Vector3 elem = new Vector3(room.X + x, room.Y + y, 0);
                    float shift = 0.3f;
                    switch (s)
                    {
                        case Cell.UP:
                            elem += new Vector3(0, shift, 0);
                            break;
                        case Cell.RIGHT:
                            elem += new Vector3(shift, 0, 0);
                            break;
                        case Cell.DOWN:
                            elem += new Vector3(0, -shift, 0);
                            break;
                        case Cell.LEFT:
                            elem += new Vector3(-shift, 0, 0);
                            break;
                    }
                    switch (room.GetWallType(x, y, s))
                    {
                        case WallType.Wall:
                            Gizmos.DrawCube(elem, Vector3.one * 0.1f);
                            break;
                        case WallType.Door:
                            Gizmos.DrawSphere(elem, 0.1f);
                            break;
                    }
                    

                }

            }
        }
    }

    public void PlaceObject(GameObject prefab)
    {
        pickup = Instantiate(prefab, transform);
        pickup.transform.position = objectPositions[Random.Range(0, objectPositions.Length)].position;
    }

    //private void OnMouseUpAsButton()
    //{
    //    if (Home.Instance.info.playerMode) return;
    //    RoomMoveManager.Instance.OnRoomClick(this);
    //}

}
