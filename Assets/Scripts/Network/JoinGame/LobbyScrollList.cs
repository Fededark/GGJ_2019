using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Lobby
{
    public string address;
    public string name;
}

public class LobbyScrollList : MonoBehaviour
{
    public List<Lobby> lobbyList;
    public Transform contentPanel;
    public SimpleObjectPool buttonObjectPool;

    public void AddLobby(string address, string lobbyName)
    {
        if(lobbyList.Find(lobby => lobby.address == address) == null)
        {
            lobbyList.Add(new Lobby { address = address, name = name });
        }
    }

    public void AddLobbyButtons()
    {
        lobbyList.ForEach(lobby =>
        {
            GameObject newLobbyButton = buttonObjectPool.GetObject();
            newLobbyButton.transform.SetParent(contentPanel);

            
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
