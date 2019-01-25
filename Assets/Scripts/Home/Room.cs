using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Home elements/Room")]
public class Room : ScriptableObject
{
    public Cell[] row1 = new Cell[3];
    public Cell[] row2 = new Cell[3];
    public Cell[] row3 = new Cell[3];

    private Cell[][] shape;

    private void OnEnable()
    {
        shape = new Cell[][] { row1, row2, row3 };
    }
}
