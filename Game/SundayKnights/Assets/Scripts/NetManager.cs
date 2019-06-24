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
        if (NetworkServer.connections.Count > 1)
        {
            GetComponent<NetDiscovery>().StopBroadcast();
            GameObject.Find("LoadingText").GetComponent<Text>().text = "";
            GameObject.Find("Black").transform.position = new Vector3(1500f, 0f, 0f);
            GameObject.Find("TilesManager").GetComponent<TilesManager>().StartCoroutine("StartGame");
            GameObject.Find("GameManager").GetComponent<GameManager>().StartCoroutine("StartCountdown");
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        if (NetworkClient.allClients.Count > 1)
        {
            GetComponent<NetDiscovery>().StopBroadcast();
            GameObject.Find("LoadingText").GetComponent<Text>().text = "";
            GameObject.Find("Black").transform.position = new Vector3(1500f, 0f, 0f);
            GameObject.Find("TilesManager").GetComponent<TilesManager>().StartCoroutine("StartGame");
            GameObject.Find("GameManager").GetComponent<GameManager>().StartCoroutine("StartCountdown");
        }
    }
}
