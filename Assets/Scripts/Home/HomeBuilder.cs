using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Home elements/Home")]
public class HomeBuilder : ScriptableObject
{
    public Vector2 doorDistance;

    public Vector2Int size;
    public RoomPosition[] rooms;

    [System.Serializable]
    public struct RoomPosition
    {
        public Vector2Int coord;
        public Room room;
    }

    public Home Build(Transform parent)
    {
        Home home = new Home();
        home.cells = new Cell[size.x, size.y];
        foreach (var room in rooms)
        {
            for (int c = 0; c < 3; c++)
            {
                for (int r = 0; r < 3; r++)
                {
                    if (room.room.shape[c, r] != null)
                        home.cells[c + room.coord.x - 1, r + room.coord.y - 1] = room.room.shape[c, r];
                }
            }
            var go = Instantiate(room.room.prefab, parent);
            go.transform.position = new Vector3(room.coord.x, room.coord.y, 0f);
        }
        home.doorDistance = doorDistance;
        Home.Instance = home;
        return home;
    }
}
