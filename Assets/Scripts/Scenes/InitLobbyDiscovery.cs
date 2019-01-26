using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InitLobbyDiscovery : MonoBehaviour
{
    public LobbyFinder lobbyFinder;

    void Start()
    {
        if (lobbyFinder)
        {
            lobbyFinder.Initialize();
            lobbyFinder.StartAsClient();
        }
    }
}
