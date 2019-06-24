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
        //manager = GetComponent<NetManager>();
        //if (!hostStarted)
        //    RunHost();
    }

    void Update()
    {
        //if (GameObject.Find("GameManager").GetComponent<GameManager>().finished)
        //    EndGame();
    }

    public void RunHost()
    {
        if (!hostStarted)
        {
            manager = GetComponent<NetManager>();
            manager.maxConnections = 2;
            manager.networkPort = Const.port;
            manager.networkAddress = Const.ipAddress;
            manager.StartHost();

            hostStarted = true;

            if(NetworkServer.active && NetworkClient.active)
                Debug.Log("Server partito");

            if ( GetComponent<NetDiscovery>().Initialize() )
                GetComponent<NetDiscovery>().StartAsServer();
        }
        else
        {
            manager.StopHost();
            GetComponent<NetDiscovery>().StopBroadcast();
            hostStarted = false;
        }
    }

    public void EndGame()
    {
        GetComponent<NetDiscovery>().StopBroadcast();
        manager.StopHost();
    }
}
