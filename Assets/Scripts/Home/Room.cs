using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Home elements/Room")]
public class Room : ScriptableObject
{
    public bool hall = false;
    public BoolEvent roomChange;
    public BoolEvent lightChange;

    private bool light = true;

    [SerializeField]
    private GameObject prefab;

    public GameObject GO { get; private set; }
    public GameObject InstantiateGO(Transform parent)
    {
        var go = Instantiate(prefab, parent);
        GO = go;
        return go;
    }

    public Cell[] row1 = new Cell[3];
    public Cell[] row2 = new Cell[3];
    public Cell[] row3 = new Cell[3];

    public Cell[,] shape;
    public int rotation = 0;

    public void CopyFrom(Room other)
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                shape[x, y] = other.shape[x, y];
            }
        }
        rotation = other.rotation;

    }

    private void OnEnable()
    {
        shape = new Cell[3,3];
        for (int c=0; c<3; c++)
        {
            shape[c, 0] = row1[c]; 
            shape[c, 1] = row2[c]; 
            shape[c, 2] = row3[c];
            SetRoom(row1[c]);
            SetRoom(row2[c]);
            SetRoom(row3[c]);
        }
        if (lightChange == null)
            lightChange = CreateInstance<BoolEvent>();
        if (roomChange == null)
            roomChange = CreateInstance<BoolEvent>();
    }

    public void SetVisible(bool visible)
    {
        roomChange.Raise(visible);
    }

    public bool Light
    {
        get { return light; }
        set
        {
            light = value;
            lightChange.Raise(!light);
            if (Home.Instance != null)
                Home.Instance.info.globalLightChange.Raise(!light);
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

    /// <summary>
    /// Finds if Room has a cell in position (x, y) where x and y are 0 in the center of the room
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Cell GetCell(int x, int y)
    {
        return shape[x + 1, y + 1];
    }

    /// <summary>
    /// Finds if Room has a cell in position (x, y) where x and y are 0 in the center of the room
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool HasCell(int x, int y)
    {
        return shape[x+1, y+1] != null;
    }

    /// <summary>
    /// Returns wall type. X and Y are 0 on the center of the room and can range from -1 to +1
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="side"></param>
    /// <returns></returns>
    public WallType GetWallType(int x, int y, int side)
    {
        Cell cell = shape[x+1, y+1];
        if (cell == null) return WallType.Empty;
        return cell.walls[(side + rotation) % 4];
    }


    public bool Overlap(int x, int y, Cell c)
    {
        if (c == null)
            return false;
        else
        {
            if (c.room.Equals(shape[x + 1, y + 1].room))
                return false;
            
        }
        return true;
    }
}
