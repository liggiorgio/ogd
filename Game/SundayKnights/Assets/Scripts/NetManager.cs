using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            GameObject.Find("TilesManager").GetComponent<TilesManager>().StartCoroutine("StartGame");
            GameObject.Find("GameManager").GetComponent<GameManager>().StartCoroutine("StartCountdown");
        }
        
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        if (NetworkClient.allClients.Count > 1)
        {
            GameObject.Find("TilesManager").GetComponent<TilesManager>().StartCoroutine("StartGame");
            GameObject.Find("GameManager").GetComponent<GameManager>().StartCoroutine("StartCountdown");
        }
    }
}
