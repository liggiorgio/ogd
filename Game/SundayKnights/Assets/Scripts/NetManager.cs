using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetManager : NetworkManager
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);
        //GetComponent<NetDiscovery>().StopBroadcast();
        if (NetworkServer.connections.Count == 2)   // there are two connections on the host's side, they and the other player
        {
            Debug.Log("Henlo 1");
            GameObject.Find("LoadingText").GetComponent<Text>().text = "";
            GameObject.Find("Black").transform.position = new Vector3(1500f, 0f, 0f);
            GameObject.Find("TilesManager").GetComponent<TilesManager>().StartCoroutine("StartGame");
            GameObject.Find("GameManager").GetComponent<GameManager>().StartCoroutine("StartCountdown");
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log(NetworkClient.allClients.Count.ToString());
        if (GetComponent<ClientConnect>().enabled)    // there's only one client on the guest's side
        {
            Debug.Log("Henlo 2");
            GetComponent<NetDiscovery>().StopBroadcast();
            GameObject.Find("LoadingText").GetComponent<Text>().text = "";
            GameObject.Find("Black").transform.position = new Vector3(1500f, 0f, 0f);
            GameObject.Find("TilesManager").GetComponent<TilesManager>().StartCoroutine("StartGame");
            GameObject.Find("GameManager").GetComponent<GameManager>().StartCoroutine("StartCountdown");
        }
    }
}
