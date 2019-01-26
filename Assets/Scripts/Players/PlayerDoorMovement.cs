using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerDoorMovement : MonoBehaviour
{
    private static Color transparent = new Color(1f, 1f, 1f, 0f);

    public HomeInfo homeInfo;
    public Image fade;
    public float fadeSpeed;
    private Room actualRoom = null;

    private PlayerMovement movement;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        homeInfo.doorPassEvent.OnDoorPassed += OnDoorPassed;
    }

    public void AssignTo(Room room)
    {
        actualRoom = room;
        transform.SetParent(room.GO.transform);
    }

    private void OnDoorPassed(Vector2Int from, Vector2Int to)
    {
        Vector3 newPosition;
        if (from.x == to.x)
        {
            if (from.y < to.y)
                newPosition = new Vector3(to.x, to.y - homeInfo.doorDistance.y);
            else
                newPosition = new Vector3(to.x, to.y + homeInfo.doorDistance.y);
        }
        else
        {
            if (from.x < to.x)
                newPosition = new Vector3(to.x - homeInfo.doorDistance.x, to.y);
            else
                newPosition = new Vector3(to.x + homeInfo.doorDistance.x, to.y);
        }
        var lastRoom = actualRoom;
        AssignTo(Home.Instance.RoomAt(to));
        StartCoroutine(FadeCoroutine(lastRoom, newPosition));
    }


    private IEnumerator FadeCoroutine(Room lastRoom, Vector3 destination)
    {
        movement.canMove = false;
        float div = 1f / fadeSpeed;

        float remaining = fadeSpeed;
        while (remaining > 0f)
        {
            fade.color = new Color(0f, 0f, 0f, 1f - remaining * div);
            yield return null;
            remaining -= Time.deltaTime;
        }

        fade.color = Color.black;
        lastRoom.SetVisible(false);
        transform.position = destination;
        actualRoom.SetVisible(true);

        remaining = fadeSpeed;
        while (remaining > 0f)
        {
            fade.color = new Color(0f, 0f, 0f, remaining * div);
            yield return null;
            remaining -= Time.deltaTime;
        }

        fade.color = transparent;
        movement.canMove = true;
    }
}
