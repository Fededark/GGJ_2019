using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InitLobbyDiscovery : MonoBehaviour
{
    public LobbyFinder lobbyFinder;

    void Start()
    {
        // StartCoroutine(lateStart());
        if (lobbyFinder)
        {
            lobbyFinder.Initialize();
            lobbyFinder.StartAsClient();
        }
    }

    private IEnumerator lateStart()
    {
        yield return new WaitForSeconds(2);
    }
}
