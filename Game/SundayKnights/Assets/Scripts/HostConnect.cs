using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkManager))]
public class HostConnect : MonoBehaviour
{
    private NetworkManager manager;
    private bool hostStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<NetworkManager>();
    }

    void Update()
    {
        if (!hostStarted)
            RunHost();
    }

    public void RunHost()
    {
        if (!hostStarted)
        {
            manager.maxConnections = 2;
            manager.networkPort = 1337;
            manager.networkAddress = "192.168.1.147";
            manager.StartHost();

            hostStarted = true;

            if(NetworkServer.active && NetworkClient.active)
                Debug.Log("Server partito");

            
        }
        else
        {
            manager.StopHost();
            hostStarted = false;
        }
    }

// Update is called once per frame
    /*void LateUpdate()
    {
        if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
            manager.StartHost();

        if (NetworkClient.active && !ClientScene.ready)
        {
            ClientScene.Ready(manager.client.connection);
            if (ClientScene.localPlayers.Count == 0)
                ClientScene.AddPlayer(0);
            else
                ClientScene.AddPlayer((short)ClientScene.localPlayers.Count);
        }
    }*/
}
