using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSync : NetworkBehaviour
{

    public UnityAdventureView syncController;

    private int slot = -1;
    private UnityAdventureView view;

    // Use this for initialization
    void Start()
    {
        Debug.Log("PlayerSync started");
        GameObject quadGameObject = GameObject.Find("Quad");
        view = quadGameObject.GetComponent<UnityAdventureView>();
        CmdAskServerForSlot();
    }

    public int getSlot() {
        return slot;
    }

    [Command]
    public void CmdAskServerForSlot() {
        if (slot < 0)
        {
            slot = view.assignPlayerSlot();
        }
        RpcAssignSlotToClients(slot);
    }

    [ClientRpc]
    public void RpcAssignSlotToClients(int inSlot) {
        slot = inSlot;
        // Start() may not have been called.  Discern view.
        GameObject quadGameObject = GameObject.Find("Quad");
        view = quadGameObject.GetComponent<UnityAdventureView>();
        view.registerSync(this, isLocalPlayer);
    }


}
