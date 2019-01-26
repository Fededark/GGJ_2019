using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var go = collision.gameObject;
        Vector3 newCoord = Home.Instance.GetAdjacentPosition(go.transform.position);

        if (newCoord.x < 0f) return;

        go.GetComponent<PlayerMovement>().TeleportTo(newCoord);

    }
}
