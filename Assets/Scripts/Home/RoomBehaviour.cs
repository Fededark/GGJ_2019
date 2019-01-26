using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public Room room;

    public GameObject sprite;
    public GameObject darkness;
    public GameObject hidden;
    public HomeInfo info;

    private bool isDark, isHidden;

    private void Awake()
    {
        room.roomChange.OnRaise += RoomChange_OnRaise; ;
        room.lightChange.OnRaise += darkness.SetActive;
    }

    private void RoomChange_OnRaise(bool obj)
    {
        hidden.SetActive(!obj);
    }

    public void ApplyRotation()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, -90f * room.rotation);
    }

    private void OnMouseUpAsButton()
    {
        if (Home.Instance.info.playerMode) return;
        if (RoomMoveManager.Instance.placeholder != null) return;
        RoomBehaviour ph = Instantiate(this);
        RoomMoveManager.Instance.placeholder = ph;
        ph.GetComponent<TakUtility.MouseFollow>().enabled = true;
    }

}
