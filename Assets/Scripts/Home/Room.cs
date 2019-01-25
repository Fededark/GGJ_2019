using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Home elements/Room")]
public class Room : ScriptableObject
{
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
            shape[0, c] = row1[c]; 
            shape[1, c] = row2[c]; 
            shape[2, c] = row3[c]; 
        }
    }

    public void Rotate(bool clockwise)
    {
        Cell[,] tmp = new Cell[3,3];
        for(int r=0;r<3; r++)
        {
            for(int c=0; c<3; c++)
            {
                if (clockwise)
                    tmp[c, 2 - r] = shape[r, c];
                else
                    tmp[2 - c, r] = shape[r, c];
            }
        }

        rotation = (rotation + (clockwise ? 1 : 4)) % 4;
    }

    public bool HasCell(int r, int c)
    {
        return shape[r, c] != null;
    }

    public WallType GetWallType(int r, int c, int side)
    {
        Cell cell = shape[r, c];
        if (cell == null) return WallType.Empty;
        return cell.walls[(side + rotation) % 4];
    }
}
