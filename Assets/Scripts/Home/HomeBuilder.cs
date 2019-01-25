using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Home elements/Home")]
public class HomeBuilder : ScriptableObject
{
    public int rows, cols;
    public RoomPosition[] rooms;

    [System.Serializable]
    public struct RoomPosition
    {
        public Vector2Int coord;
        public Room room;
    }

    public Home Build()
    {
        Home home = new Home();
        home.cells = new Cell[rows, cols];
        foreach (var room in rooms)
        {
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    home.cells[r + room.coord.x, c + room.coord.y] = room.room.shape[r, c];
                }
            }
        }
        return home;
    }
}
