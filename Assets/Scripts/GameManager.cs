using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float moveTime;
    public float lightTime;

    [Space]
    public float moveRemaining;
    public float lightRemaining;

    private void Awake()
    {
        moveRemaining = moveTime;
        lightRemaining = lightTime;
    }

    private void Update()
    {
        moveRemaining -= Time.deltaTime;
        if (moveRemaining < 0)
            MoveRoom();

        lightRemaining -= Time.deltaTime;
        if (lightRemaining < 0)
            SwitchOff();
    }

    public void MoveRoom()
    {
        RoomMoveManager.Instance.RandomMove();
        moveRemaining = moveTime;
    }

    public void SwitchOff()
    {
        lightRemaining = lightTime;
        var on = new List<Room>();
        foreach(var r in Home.Instance.rooms)
        {
            if (r.Light)
                on.Add(r);
        }
        if (on.Count > 0)
            on[Random.Range(0, on.Count)].Light = false;

    }
}
