using UnityEngine;

[System.Serializable]
public class Home
{
    public static Home Instance = null;

    public Cell[,] cells;
    public HomeInfo info;

    public void Init()
    {
        info.doorPassEvent.OnDoorPassed += OnDoorPassed;
    }

    public Room RoomAt(Vector2Int coord)
    {
        return cells[coord.x, coord.y].room;
    }

    private void OnDoorPassed(Vector2Int from, Vector2Int to)
    {
        cells[from.x, from.y].room.GO.SetActive(false);
        cells[to.x, to.y].room.GO.SetActive(true);
    }

    public void TryToMove(Vector3 v)
    {
        var x = v.x;
        var y = v.y;

        int gridX = Mathf.RoundToInt(x);
        int gridY = Mathf.RoundToInt(y);


        float xDiff = x - gridX;
        float yDiff = y - gridY;


        if (Mathf.Abs(xDiff) < Mathf.Abs(yDiff))
        {
            //Y movement
            int offset = yDiff < 0 ? -1 : 1;
            int newY = gridY + offset;

            if (newY >= 0 && newY < cells.GetLength(1) && CellsExist(gridX, newY))
                info.doorPassEvent.Raise(new Vector2Int(gridX, gridY), new Vector2Int(gridX, gridY + offset));
        }
        else
        {
            int offset = xDiff < 0 ? -1 : 1;
            int newX = gridX + offset;

            if (newX >= 0 && newX < cells.GetLength(0) && CellsExist(newX, gridY))
                info.doorPassEvent.Raise(new Vector2Int(gridX, gridY), new Vector2Int(gridX + offset, gridY));
        }
    }

    public Vector2Int GetAdjacent(Vector3 v)
    {
        var x = v.x;
        var y = v.y;

        int gridX = Mathf.RoundToInt(x);
        int gridY = Mathf.RoundToInt(y);


        float xDiff = x - gridX;
        float yDiff = y - gridY;


        if (Mathf.Abs(xDiff) < Mathf.Abs(yDiff))
        {
            //Y movement
            int offset = yDiff < 0 ? -1 : 1;
            int newY = gridY + offset;

            if (newY < 0 || newY > cells.GetLength(1) || !CellsExist(gridX, newY))
                return new Vector2Int(-1, -1);
        
            return new Vector2Int(gridX, gridY + offset);
        }
        else
        {
            int offset = xDiff < 0 ? -1 : 1;
            int newX = gridX + offset;

            if (newX < 0 || newX > cells.GetLength(0) || !CellsExist(newX, gridY))
                return new Vector2Int(-1, -1);

            return new Vector2Int(gridX + offset, gridY);
        }

    }


    private bool CellsExist(int x, int y)
    {
        return cells[x, y] != null;
    }

}
