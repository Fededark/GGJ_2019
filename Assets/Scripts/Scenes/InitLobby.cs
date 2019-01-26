using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InitLobby : MonoBehaviour
{
    public NetworkDiscovery networkDiscovery;
    public NetworkLobbyManager lobbyManager;

    private void Start()
    {
        if (networkDiscovery && lobbyManager)
        {
            networkDiscovery.Initialize();
            networkDiscovery.StartAsServer();
            lobbyManager.StartHost();
        }
    }
}
