using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Home elements/Cell")]
public class Cell : ScriptableObject
{
    public const int UP = 0;
    public const int RIGHT = 1;
    public const int DOWN = 2;
    public const int LEFT = 3;

    public WallType[] walls = new WallType[4];
    public Room room;
    public BoolEvent[] doorsState = new BoolEvent[4];


    public WallType GetWallType(int side)
    {
        return walls[(side + room.rotation) % 4];
    }

    public void checkDoorState(int direction, bool locked)
    {
        doorsState[(direction + room.rotation) % 4].Raise(locked);
    }


    public void UpdateDoorsState(Home father, int x, int y)
    {
        WallType top = father.GetCell(x, y + 1).GetWallType(Cell.DOWN);
        WallType down = father.GetCell(x, y - 1).GetWallType(Cell.UP);
        WallType left = father.GetCell(x - 1, y).GetWallType(Cell.RIGHT);
        WallType right = father.GetCell(x + 1, y).GetWallType(Cell.LEFT);

        checkDoorState(Cell.UP, top.Equals(GetWallType(Cell.UP)));
        checkDoorState(Cell.DOWN, down.Equals(GetWallType(Cell.DOWN)));
        checkDoorState(Cell.LEFT, left.Equals(GetWallType(Cell.LEFT)));
        checkDoorState(Cell.RIGHT, right.Equals(GetWallType(Cell.RIGHT)));


    }

    
    
}
