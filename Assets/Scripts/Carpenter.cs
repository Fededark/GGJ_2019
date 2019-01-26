using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpenter : MonoBehaviour
{
    public HomeBuilder home;
    public PlayerDoorMovement player;

    // Start is called before the first frame update
    void Start()
    {
        home.Build(transform);
        player.AssignTo(home.info.hall);
        player.transform.position = home.info.SpawnPoint;
    }

}
