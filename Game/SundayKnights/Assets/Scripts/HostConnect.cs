using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkManager))]
public class HostConnect : MonoBehaviour
{
    private NetManager manager;
    private bool hostStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<NetManager>();
        if (!hostStarted)
            RunHost();
    }

    void Update()
    {
    }

    public void RunHost()
    {
        if (!hostStarted)
        {
            manager.maxConnections = 2;
            manager.networkPort = 7777;
            manager.networkAddress = "192.168.1.42";
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
}
