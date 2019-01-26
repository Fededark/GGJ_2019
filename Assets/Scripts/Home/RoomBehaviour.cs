using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public Room room;

    public GameObject sprite;
    public GameObject darkness;

    private void Awake()
    {
        room.roomChange.OnRaise += sprite.SetActive;
        room.lightChange.OnRaise += darkness.SetActive;
    }

    public void ApplyRotation()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, -90f * room.rotation);
    }
    
}
