using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public Room room;

    public GameObject sprite;
    public GameObject darkness;
    public HomeInfo info;

    private void Awake()
    {
        room.roomChange.OnRaise += RoomChange_OnRaise;
        room.lightChange.OnRaise += darkness.SetActive;
    }

    private void RoomChange_OnRaise(bool visible)
    {
        if (info.playerMode)
            sprite.SetActive(visible);
    }

    public void ApplyRotation()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, -90f * room.rotation);
    }

    private void OnMouseUpAsButton()
    {
        if (Home.Instance.info.playerMode) return;
        Debug.Log("click", gameObject);
    }

}
