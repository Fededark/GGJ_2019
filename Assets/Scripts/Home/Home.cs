using UnityEngine;

[System.Serializable]
public class Home
{
    public static Home Instance = null;

    public Cell[,] cells;
    public Vector2 doorDistance;

    public Vector3 GetAdjacentPosition(Vector3 v)
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
            int newY;
            float adjY;
            if (yDiff < 0)
            {
                newY = gridY - 1;
                adjY = newY + doorDistance.y;
            }
            else
            {
                newY = gridY + 1;
                adjY = newY - doorDistance.y;
            }

            if (newY < 0 || newY > cells.GetLength(1) || !CellsExist(gridX, newY))
                return new Vector3(-1, -1, -1);

            return new Vector3(gridX, adjY);
        }
        else
        {
            int newX;
            float adjX;
            if (xDiff < 0)
            {
                newX = gridX - 1;
                adjX = newX + doorDistance.x;
            }
            else
            {
                newX = gridX + 1;
                adjX = newX - doorDistance.x;
            }

            if (newX < 0 || newX > cells.GetLength(0) || !CellsExist(newX, gridY))
                return new Vector3(-1, -1, -1);

            return new Vector3(adjX, gridY);
        }

    }

    public Vector2Int GetAdjacent(Vector3 v)
    {
        var x = v.x;
        var y = v.y;

        int gridX = Mathf.RoundToInt(x);
        int gridY = Mathf.RoundToInt(y);


        float xDiff = gridX - x;
        float yDiff = gridY - y;


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
