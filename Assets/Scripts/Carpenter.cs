using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpenter : MonoBehaviour
{
    public HomeBuilder home;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        home.Build(transform);
        player.transform.position = Home.Instance.info.SpawnPoint;
    }

}
