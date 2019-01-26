using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Home elements/Room")]
public class Room : ScriptableObject
{
    public GameObject prefab;
    public Cell[] row1 = new Cell[3];
    public Cell[] row2 = new Cell[3];
    public Cell[] row3 = new Cell[3];

    public Cell[,] shape;
    private int rotation = 0;

    private void OnEnable()
    {
        shape = new Cell[3,3];
        for (int c=0; c<3; c++)
        {
            shape[c, 0] = row1[c]; 
            shape[c, 1] = row2[c]; 
            shape[c, 2] = row3[c]; 
        }
    }

    private void SetRoom(Cell c)
    {
        if (c != null)
            c.room = this;
    }

    public void Rotate(bool clockwise)
    {
        Cell[,] tmp = new Cell[3,3];
        for(int c=0;c<3; c++)
        {
            for(int r=0; r<3; r++)
            {
                if (clockwise)
                    tmp[r, 2 - c] = shape[c, r];
                else
                    tmp[2 - r, c] = shape[c, r];
            }
        }

        rotation = (rotation + (clockwise ? 1 : 4)) % 4;
    }

    public bool HasCell(int x, int y)
    {
        return shape[x, y] != null;
    }

    public WallType GetWallType(int x, int y, int side)
    {
        Cell cell = shape[x, y];
        if (cell == null) return WallType.Empty;
        return cell.walls[(side + rotation) % 4];
    }
}
