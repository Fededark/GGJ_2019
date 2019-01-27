using ChanibaL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BoolEvent winEvent;

    public Timer totalTime;
    public Timer moveTime;
    public Timer lightTime;

    public BoolEvent[] objects;

    public int taken = 0;

    private void Awake()
    {
        winEvent.OnRaise += WinEvent_OnRaise;        
        totalTime.Reset();
        moveTime.Reset();
        lightTime.Reset();
        foreach (var ev in objects)
        {
            ev.OnRaise += Ev_OnRaise;
        }
    }

    private void WinEvent_OnRaise(bool obj)
    {
        if (taken >= objects.Length)
        {
            //win
            SceneManager.LoadScene("win");
        }
    }

    private void Ev_OnRaise(bool obj)
    {
        taken++;
    }

    private void Update()
    {
        if (totalTime.PassTime())
        {
            //game over
            SceneManager.LoadScene("lose");
        }

        if (moveTime.PassTime())
            MoveRoom();

        if (lightTime.PassTime())
            SwitchOff();
    }

    public void MoveRoom()
    {
        RoomMoveManager.Instance.RandomMove();
    }

    public void SwitchOff()
    {
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

[System.Serializable]
public class Timer
{
    public float min;
    public float max;
    public float current;

    public void Reset()
    {
        if (max < min)
            current = min;
        else
            current = RandomGenerator.global.GetFloatRange(min, max);
    }


    public bool PassTime()
    {
        current -= Time.deltaTime;
        if (current <= 0)
        {
            Reset();
            return true;
        }
        else
            return false;
    }
}
