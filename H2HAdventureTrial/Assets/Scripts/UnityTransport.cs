using System.Collections;
using System.Collections.Generic;
using GameEngine;
using UnityEngine;

public class UnityTransport : MonoBehaviour, Transport
{

    private PlayerSync thisPlayer;

    private List<PlayerSync> allPlayers = new List<PlayerSync>();

    private Queue<RemoteAction> receviedActions = new Queue<RemoteAction>();

    // This is only used by the view on the server
    private int lastUsedSlot = -1;

    public void registerSync(PlayerSync inPlayerSync)
    {
        Debug.Log("Registering " + (inPlayerSync.isLocalPlayer ? "local " : "remote ") + "player # " + inPlayerSync.getSlot());
        allPlayers.Add(inPlayerSync);
        if (inPlayerSync.isLocalPlayer)
        {
            thisPlayer = inPlayerSync;
            GameObject quadGameObject = GameObject.Find("Quad");
            UnityAdventureView view = quadGameObject.GetComponent<UnityAdventureView>();
            view.AdventureSetup(thisPlayer.getSlot());
            Debug.Log("Adventure game has been setup for player " + thisPlayer.getSlot());
        }
    }

    // This should only ever be called on the server
    public int assignPlayerSlot()
    {
        int newPlayerSlot = ++lastUsedSlot;
        return newPlayerSlot;
    }

    void Transport.send(RemoteAction action)
    {
        action.setSender(thisPlayer.getSlot());
        Debug.Log("Sending " + action.typeCode.ToString("g") + " message for player " + action.sender);
        thisPlayer.CmdBroadcast(action.serialize());
    }

    public void receiveBroadcast(int slot, int[] dataPacket)
    {
        ActionType type = (ActionType)dataPacket[0];
        int sender = dataPacket[1];
        if (sender != thisPlayer.slot)
        {
            RemoteAction action=null;
            switch (type)
            {
                case ActionType.PLAYER_MOVE:
                    action = new PlayerMoveAction();
                    break;
            }
            action.deserialize(dataPacket);
            receviedActions.Enqueue(action);
            Debug.Log("Now " + receviedActions.Count + " actions in queue");
        }
        else {
            Debug.Log("Ignoring message from this player (player #" + sender + ").");
        }
    }

    RemoteAction Transport.get()
    {
        if (receviedActions.Count == 0)
        {
            return null;
        }
        else
        {
            RemoteAction nextAction = receviedActions.Dequeue();
            Debug.Log("Dequeuing " + nextAction + " action.");
            return nextAction;
        }
    }
}
