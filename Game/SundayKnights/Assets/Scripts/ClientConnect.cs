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
    }

    // Update is called once per frame
    void Update()
    {
        if (!clientStarted)
            RunClient();
    }

    public void RunClient()
    {
        if (!clientStarted)
        {
            client = new NetworkClient();
            manager.networkAddress = "192.168.1.147";
            manager.networkPort = 1337;
            client = manager.StartClient();

            clientStarted = true;

            if(NetworkClient.active)
                Debug.Log("Cliente Startato");
        }
        else
        {
            manager.StopClient();
            clientStarted = false;
        }
    }
}
