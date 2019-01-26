using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Home.Instance.TryToMove(collision.gameObject.transform.position);        
    }
}
