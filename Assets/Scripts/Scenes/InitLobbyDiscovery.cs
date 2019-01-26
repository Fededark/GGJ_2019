using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InitLobbyDiscovery : MonoBehaviour
{
    public NetworkDiscovery lobbyFinder;
    public LobbyJoiner networkManager;

    void Start()
    {
        if (lobbyFinder && networkManager)
        {
            lobbyFinder.Initialize();
            lobbyFinder.StartAsClient();
        }
    }
}
