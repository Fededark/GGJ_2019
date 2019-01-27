using UnityEngine;

public class SwitchOn : MonoBehaviour, IAction
{
    public Room room;
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
        room.Light = true;
    }
}
