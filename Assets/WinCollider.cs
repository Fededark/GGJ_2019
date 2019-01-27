using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollider : MonoBehaviour
{
    public BoolEvent winEvent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        winEvent.Raise(true);
    }
}
