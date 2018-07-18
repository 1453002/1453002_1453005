﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;


public class NetManager : MonoBehaviour {

    public GameObject avatarPrefabs;
    public GameObject interactiveObjectPrefabs;
    public const string setting = "1.0";

	// Use this for initialization
	void Start () {
        PhotonNetwork.ConnectUsingSettings(setting);
        //0707
        var temp = PhotonVoiceNetwork.Client;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public virtual void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        string room_name = string.Empty;
        if (gameObject.scene.name == "Baked_MuseumVR_vol1")
        {
            room_name = "Museum";
        }
        if(gameObject.scene.name == "Showroom2_01")
        {
            room_name = "Medical";
        }
        PhotonNetwork.JoinOrCreateRoom(room_name, new RoomOptions() { MaxPlayers = 4 }, null);
       
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public virtual void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
    }

    // the following methods are implemented to give you some context. re-implement them as needed.

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }
    
    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");

        PhotonNetwork.Instantiate(avatarPrefabs.name, new Vector3(Random.Range(-5,5),0,0), Quaternion.identity, 0);
      //  Debug.Log("Master - room - lobby" + PhotonNetwork.isMasterClient + " " + PhotonNetwork.inRoom + " " + PhotonNetwork.insideLobby);
       
    }
}
