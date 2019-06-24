using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientConnect : MonoBehaviour
{
    private NetworkClient client;

    private NetworkManager manager;
    private bool clientStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<NetworkManager>();
        if (!clientStarted)
            RunClient();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RunClient()
    {
        if (!clientStarted)
        {
            client = new NetworkClient();
            manager.networkAddress = Const.ipAddress;
            manager.networkPort = Const.port;
            client = manager.StartClient();

            clientStarted = true;

            if(NetworkClient.active && client.isConnected)
                Debug.Log("Cliente Startato");
        }
        else
        {
            manager.StopClient();
            clientStarted = false;
        }
    }

    public void DisconnectClient()
    {
        client.Disconnect();
    }
}
