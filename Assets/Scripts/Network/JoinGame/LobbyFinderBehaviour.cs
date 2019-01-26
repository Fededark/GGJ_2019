using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyFinderBehaviour : MonoBehaviour
{
    public LobbyScrollList lobbyScrollList;

    public void AddLobby(Lobby lobby)
    {
        lobbyScrollList.AddLobby(lobby);
    }
}
