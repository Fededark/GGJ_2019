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
}
