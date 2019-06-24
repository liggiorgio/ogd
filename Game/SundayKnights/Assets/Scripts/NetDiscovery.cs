using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetDiscovery : NetworkDiscovery
{
    private bool discovered = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        if (!discovered)
        {
            GetComponent<ClientConnect>().RunClient(fromAddress);
            StopBroadcast();
            discovered = true;
        }
    }

}
