using UnityEngine;

public class SwitchOn : MonoBehaviour, IAction
{
    public Room room;
    public ActionEvent actionEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!room.Light)
            actionEvent.Raise(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!room.Light)
            actionEvent.Raise(null);
    }

    public void DoAction()
    {
        room.Light = true;
        actionEvent.Raise(null);
    }
}
