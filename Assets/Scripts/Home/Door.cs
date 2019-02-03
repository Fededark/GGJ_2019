using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Home.Instance.TryToMove(collision.gameObject.transform.position);        
    }
}
