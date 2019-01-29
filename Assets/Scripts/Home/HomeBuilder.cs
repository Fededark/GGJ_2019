using ChanibaL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Home elements/Home")]
public class HomeBuilder : ScriptableObject
{
    public HomeInfo info;
    public Vector2Int size;
    public Room[] rooms;

    public Home Build(Transform parent)
    {
        Home home = new Home();
        home.cells = new Cell[size.x, size.y];
        int roomIdx = 1;
        int cellIdx = 1;
        foreach (var room in rooms)
        {
            room.Init();
            room.id = roomIdx;
            roomIdx++;
            //info.roomDict.Add(room.id, room);
            for (int c = 0; c < 3; c++)
            {
                for (int r = 0; r < 3; r++)
                {
                    Cell cell = room.shape[c, r];
                    if (cell != null)
                    {
                        cell.id = cellIdx;
                        cellIdx++;
                        //info.cellDict.Add(cell.id, cell);
                        home.cells[c + room.X - 1, r + room.Y - 1] = cell;
                    }
                }
            }
            var go = room.InstantiateGO(parent);
            go.transform.position = new Vector3(room.X, room.Y, 0f);
            room.SetVisible(room.hall);            
            if (room.hall)
            {
                info.hall = room;
                info.SpawnPoint = go.transform.position;
            }
            room.Light = room.hall;
        }

        home.info = info;
        home.Init();
        home.BuildConnections(rooms);
        Home.Instance = home;
        return home;
    }
}
