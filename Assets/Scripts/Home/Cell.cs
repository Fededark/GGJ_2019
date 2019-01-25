using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Home elements/Cell")]
public class Cell : ScriptableObject
{
    public WallType[] walls = new WallType[4];
    public Room room;
}

public enum WallType { Empty, Wall, Door }