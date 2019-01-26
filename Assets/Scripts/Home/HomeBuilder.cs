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
            room.id = roomIdx;
            roomIdx++;
            info.roomDict.Add(room.id, room);
            for (int c = 0; c < 3; c++)
            {
                for (int r = 0; r < 3; r++)
                {
                    Cell cell = room.shape[c, r];
                    if (cell != null)
                    {
                        cell.id = cellIdx;
                        cellIdx++;
                        info.cellDict.Add(cell.id, cell);
                        home.cells[c + room.x - 1, r + room.y - 1] = cell;
                    }
                }
            }
            var go = room.InstantiateGO(parent);
            go.transform.position = new Vector3(room.x, room.y, 0f);
            room.SetVisible(room.hall);
            if (room.hall)
            {
                info.hall = room;
                info.SpawnPoint = go.transform.position;
            }
        }

        var shuff = RandomGenerator.global.GetShuffled(new List<Room>(rooms));
        int split = (shuff.Count + 1) / 2;
        for (int i = 0; i < split; i++)
            shuff[i].Light = false;
        for (int i = split; i < shuff.Count; i++)
            shuff[i].Light = true;
        info.hall.Light = true;

        home.info = info;
        home.Init();
        Home.Instance = home;
        return home;
    }
}
