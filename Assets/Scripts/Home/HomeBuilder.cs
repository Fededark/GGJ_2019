using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Home elements/Home")]
public class HomeBuilder : ScriptableObject
{
    public HomeInfo info;
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
        foreach (var rp in rooms)
        {
            for (int c = 0; c < 3; c++)
            {
                for (int r = 0; r < 3; r++)
                {
                    if (rp.room.shape[c, r] != null)
                        home.cells[c + rp.coord.x - 1, r + rp.coord.y - 1] = rp.room.shape[c, r];
                }
            }
            var go = Instantiate(rp.room.prefab, parent);
            go.transform.position = new Vector3(rp.coord.x, rp.coord.y, 0f);
            if (rp.room.hall)
                info.SpawnPoint = go.transform.position;
        }
        home.info = info;
        Home.Instance = home;
        return home;
    }
}
