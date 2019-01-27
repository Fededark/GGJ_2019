using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    public ActionEvent actionEvent;
    public Button button;

    private IAction action = null;

    private void Awake()
    {
        actionEvent.OnRaise += ActionEvent_OnRaise;
    }

    private void Start()
    {
        UpdateButton();
    }

    private void Update()
    {
        if (action != null && Input.GetButtonDown("PlayerAction"))
        {
            action.DoAction();
        }
    }

    public void DoAction()
    {
        if (action != null)
            action.DoAction();
    }

    private void ActionEvent_OnRaise(IAction obj)
    {
        action = obj;
        button.gameObject.SetActive(obj != null);
    }

    private void UpdateButton()
    {
        //TODO
        button.gameObject.SetActive(action != null);
    }
}
