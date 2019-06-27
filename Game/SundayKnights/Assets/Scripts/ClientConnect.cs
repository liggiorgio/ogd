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
        CreateClient();
        manager = GetComponent<NetworkManager>();
        if ( GetComponent<NetDiscovery>().Initialize() )
            GetComponent<NetDiscovery>().StartAsClient();
        //TimeoutRoutine = BroadcastTimeout();
        //StartCoroutine(TimeoutRoutine);
        //if (!clientStarted)
        //    RunClient();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateClient()
    {
        //client = new NetworkClient();
    }

    public void RunClient(string addr)
    {
        //StopCoroutine(TimeoutRoutine);
        if (!clientStarted)
        {
            manager.networkAddress = addr;
            manager.networkPort = Const.port;
            client = manager.StartClient();

            clientStarted = true;

            if(NetworkClient.active && client.isConnected)
                Debug.Log("Cliente Startato");
            else
                Debug.Log("Cliente non Startato");
        }
        else
        {
            manager.StopClient();
            Debug.Log("Client fermato");
            NetworkTransport.Shutdown();
            NetworkTransport.Init();
            clientStarted = false;
        }
    }

    public void DisconnectClient()
    {
        client.Disconnect();
        //GameObject.Destroy(GameObject.Find("NetworkManager"));
        NetworkTransport.Shutdown();
        NetworkTransport.Init();
        SceneManager.LoadScene("MenuScene");
    }

    // Become host if no other host is available
    IEnumerator BroadcastTimeout()
    {
        Debug.Log("Searching for games...");
        yield return new WaitForSeconds(5f);
        Debug.Log("Becoming host!");
        GameObject.Find("LoadingText").GetComponent<Text>().text = "Waiting for\nopponents...";
        GetComponent<NetDiscovery>().StopBroadcast();
        this.enabled = false;
        GetComponent<HostConnect>().enabled = true;
        GetComponent<HostConnect>().RunHost();
    }
}
