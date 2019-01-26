using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyFinder : NetworkDiscovery
{
    private LobbyFindderBehaviour behaviour;
    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        Debug.Log("Received broadcast from: " + fromAddress + " with the data: " + data);
    }

    private void Awake()
    {
        behaviour = GetComponent<LobbyFindderBehaviour>();
    }
}
