using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSync : NetworkBehaviour {

    public UnityAdventureView syncController;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            syncController.registerSync(this);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
