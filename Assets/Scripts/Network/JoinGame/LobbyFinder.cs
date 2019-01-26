using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyFinder : NetworkDiscovery
{
    private LobbyFinderBehaviour behaviour;

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        Debug.Log("Received broadcast from: " + fromAddress + " with the data: " + data);

        if (behaviour)
        {
            behaviour.AddLobby(new Lobby { address = fromAddress, name = data });
        }
    }

    private void Awake()
    {
        behaviour = GetComponent<LobbyFinderBehaviour>();
    }
}
