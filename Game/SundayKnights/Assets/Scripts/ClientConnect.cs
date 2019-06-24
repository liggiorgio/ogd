using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ClientConnect : MonoBehaviour
{
    private NetworkClient client;

    private NetworkManager manager;
    private bool clientStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<NetworkManager>();
        if ( GetComponent<NetDiscovery>().Initialize() )
            GetComponent<NetDiscovery>().StartAsClient();
        //if (!clientStarted)
        //    RunClient();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RunClient(string addr)
    {
        if (!clientStarted)
        {
            client = new NetworkClient();
            manager.networkAddress = addr;
            manager.networkPort = Const.port;
            client = manager.StartClient();

            clientStarted = true;

            if(NetworkClient.active && client.isConnected)
                Debug.Log("Cliente Startato");
        }
        else
        {
            manager.StopClient();
            NetworkTransport.Shutdown();
            NetworkTransport.Init();
            clientStarted = false;
        }
    }

    public void DisconnectClient()
    {
        client.Disconnect();
        NetworkTransport.Shutdown();
        NetworkTransport.Init();
        SceneManager.LoadScene("MenuScene");
    }
}
