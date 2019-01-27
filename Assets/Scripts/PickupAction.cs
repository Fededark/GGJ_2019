using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAction : MonoBehaviour, IAction
{
    public BoolEvent onPickup;
    public ActionEvent actionEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        actionEvent.Raise(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        actionEvent.Raise(null);
    }

    public void DoAction()
    {
        onPickup.Raise(true);
        actionEvent.Raise(null);
        Destroy(gameObject);
    }
}
