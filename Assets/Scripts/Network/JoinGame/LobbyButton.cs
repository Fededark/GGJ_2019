using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyButton : MonoBehaviour
{
    public Button buttonComponent;
    public Text nameLabel;

    private Lobby lobby;
    private LobbyScrollList scrollList;

    // Start is called before the first frame update
    void Start()
    {
        buttonComponent.onClick.AddListener(HandleClick);
    }

    public void Setup(Lobby currentLobby, LobbyScrollList currentScrollList)
    {
        lobby = currentLobby;
        scrollList = currentScrollList;
    }

    public void HandleClick()
    {
        
    }
}
