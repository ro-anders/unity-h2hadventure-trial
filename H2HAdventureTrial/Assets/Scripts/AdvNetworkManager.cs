using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AdvNetworkManager : NetworkManager {

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("Client connected!");
    }
}
