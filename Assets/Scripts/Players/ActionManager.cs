using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    public ActionEvent actionEvent;
    public Button button;
    public Image light, pick;

    public AudioClip click, pickSound;

    private AudioClip actionSound;

    private IAction action = null;
    private AudioSource audio;

    private void Awake()
    {
        actionEvent.OnRaise += ActionEvent_OnRaise;
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        UpdateButton();
    }

    private void Update()
    {
        if (action != null && Input.GetButtonDown("PlayerAction"))
        {
            audio.PlayOneShot(actionSound);
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
        bool isSwitch = obj is SwitchOn;
        if (isSwitch && action != null)
            return;

        action = obj;
        light.enabled = isSwitch;
        pick.enabled = !light.enabled;
        actionSound = isSwitch ? click : pickSound;
        button.gameObject.SetActive(obj != null);
    }

    private void UpdateButton()
    {
        //TODO
        button.gameObject.SetActive(action != null);
    }
}
