using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour {

#region CONFIG
    public float playerHeight = 1.8f;
    #endregion

#region Player Util
    public void MoveTo(Vector3 pos,Quaternion rot)
    {
        Vector3 correctPos = new Vector3(pos.x, pos.y + playerHeight, pos.z);
        this.transform.DOMove(correctPos, 1f);
        this.transform.rotation = rot;
        if (visualPlayer)
        {
            visualPlayer.transform.DOMove(correctPos, 1f);
            visualPlayer.transform.rotation = rot;
        }
    

    }
    #endregion

    public GameObject defaultLaser;
    public GameObject visualPlayer;
    public DaydreamElements.Teleport.TeleportController teleportController;
    public static Player instance;

    public enum PlayerState
    {
        None,
        Teleporting,
        Selecting,
        PlayingGame
    };

    private void Awake()
    {
        instance = this;
    }
  
    public PlayerState currentState;

	// Use this for initialization
	void Start () {
       if(this.gameObject.scene.name == "Showroom2_01")
        {
            //if(this.gameObject.transform.position.y !=0  && gameObject.transform.position.z !=0)
            //{
            //    gameObject.transform.position = new Vector3(0.01f, 0.78f, -11.78f);
            //}
            //Debug.Log("Set pos");
            //this.gameObject.transform.position = new Vector3(0.01f, 100f, -11.78f);
        }
	}

    public void SetState(PlayerState state)
    {
        currentState = state;
        if (state == PlayerState.None)
        {
            Invoke("enableTeleport", 1f);
            defaultLaser.SetActive(true);
        }
        else if (state == PlayerState.Teleporting)
        {
            defaultLaser.SetActive(false);
        }
        else if (state == PlayerState.Selecting)
        {
            teleportController.gameObject.SetActive(false);
        }
        else if (state == PlayerState.PlayingGame)
        {
            teleportController.gameObject.SetActive(false);
            defaultLaser.SetActive(true);
        }

     
        currentState = state;

    }

    public void enableTeleport()
    {
        teleportController.gameObject.SetActive(true);
    }
    public void enableTeleport(bool value)
    {
        if(value)
        {
            teleportController.gameObject.SetActive(true);
            defaultLaser.SetActive(false);
        }
        else
        {
            teleportController.gameObject.SetActive(false);
            defaultLaser.SetActive(true);
        }
    }

    void UpdateState()
    {
        if (currentState == PlayerState.None)
        {
                
        }
         if (currentState == PlayerState.Selecting)
        {
            defaultLaser.SetActive(true);
            teleportController.gameObject.SetActive(false);
        }
         if (currentState == PlayerState.PlayingGame)
        {
            defaultLaser.SetActive(true);
            teleportController.gameObject.SetActive(false);
        }
         if(currentState ==PlayerState.Teleporting)
        {
            defaultLaser.SetActive(false);
            teleportController.gameObject.SetActive(true);
        }

    }

    


    // Update is called once per frame
    void Update () {


        //test voice

        UpdateState();

        if (visualPlayer)
        {
            visualPlayer.transform.position = this.transform.position;
            visualPlayer.transform.rotation = this.transform.rotation;
        }
    }
}
