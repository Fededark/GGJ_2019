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
    private int rotation = 0;

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
