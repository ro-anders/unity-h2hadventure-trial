using System.Collections;
using System.Collections.Generic;
using GameEngine;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSync : NetworkBehaviour
{
    public int slot = -1;

    public UnityTransport xport;

    // Use this for initialization
    void Start()
    {
        Debug.Log("PlayerSync started");
        GameObject quadGameObject = GameObject.Find("Quad");
        xport = quadGameObject.GetComponent<UnityTransport>();
        CmdAssignSlot();
    }

    public int getSlot() {
        return slot;
    }

    [Command]
    public void CmdAssignSlot() {
        if (slot < 0) {
            slot = xport.assignPlayerSlot();
            // This code is repeated from client RPC
            xport.registerSync(this);
            Debug.Log("Player #" + slot + " setup." +
                      (isLocalPlayer ? " This is the local player." : "") +
                      (isServer ? " This is on the server." : ""));
        }
        RpcAssignSlot(slot);
    }

    [ClientRpc]
    public void RpcAssignSlot(int inSlot) {
        if (slot < 0)
        {
            slot = inSlot;
            // Repeat this code in server CMD
            xport.registerSync(this);
            Debug.Log("Player #" + slot + " setup." +
                      (isLocalPlayer ? " This is the local player." : "") +
                      (isServer ? " This is on the server." : ""));
        }
    }

    [Command]
    public void CmdBroadcast(RemoteAction remoteAction) {
        RpcReceiveBroadcast(remoteAction);
    }

    [ClientRpc]
    public void RpcReceiveBroadcast(RemoteAction remoteAction) {
        Debug.Log("Player #" + slot + " Received " + remoteAction.typeCode + " message from player #" + (remoteAction.sender + 1));
        xport.receiveBroadcast(slot, remoteAction);
    }

}
