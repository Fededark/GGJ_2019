using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Home info")]
public class HomeInfo : ScriptableObject
{

    public Vector2 doorDistance;
    public DoorPassEvent doorPassEvent;
    public Room hall;
    public BoolEvent globalLightChange;
    public Vector3 SpawnPoint { get; set; }

}
