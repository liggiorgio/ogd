using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ClientConnect : MonoBehaviour
{
    private NetworkClient client;
    private IEnumerator TimeoutRoutine;
    private NetworkManager manager;
    private bool clientStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<NetworkManager>();
        if ( GetComponent<NetDiscovery>().Initialize() )
            GetComponent<NetDiscovery>().StartAsClient();
        TimeoutRoutine = BroadcastTimeout();
        StartCoroutine(TimeoutRoutine);
        //if (!clientStarted)
        //    RunClient();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RunClient(string addr)
    {
        StopCoroutine(TimeoutRoutine);
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
            GetComponent<NetDiscovery>().StopBroadcast();
            clientStarted = false;
        }
    }

    public void DisconnectClient()
    {
        client.Disconnect();
        GetComponent<NetDiscovery>().StopBroadcast();
        SceneManager.LoadScene("MenuScene");
    }

    // Become host if no other host is available
    IEnumerator BroadcastTimeout()
    {
        Debug.Log("Searching for games...");
        yield return new WaitForSeconds(5f);
        Debug.Log("Becoming host!");
        //manager.StopClient();
        //NetworkTransport.Shutdown();
        //NetworkTransport.Init();
        GameObject.Find("LoadingText").GetComponent<Text>().text = "Waiting for\nopponents...";
        GetComponent<NetDiscovery>().StopBroadcast();
        this.enabled = false;
        GetComponent<HostConnect>().enabled = true;
        GetComponent<HostConnect>().RunHost();
    }
}
