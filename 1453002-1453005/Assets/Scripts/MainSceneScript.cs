﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MainSceneScript : MonoBehaviour {

    public static MainSceneScript instance;

    [HideInInspector]
    public GameObject player = null;

    Vector3 lastPosOfPlayer;
    public List<GameObject> listPlayer;
    public Dictionary<string, GameObject> dicPlayers;


    [HideInInspector]
    public GoogleVR.Demos.DemoInputManager demoInputManager;

    public GameObject testRequire;

    private void Awake()
    {
        instance = this;
        FBGameData.instance.loadGameDatabase("Data");
        dicPlayers = new Dictionary<string, GameObject>();      

    }
    // Use this for initialization
    void Start () {
        SceneObjectManager.instance.initSceneInteractiveObjects(this.gameObject.scene);
        listPlayer[1].name = "GVR-2";
        for (int i = 0; i < listPlayer.Count; i++)
        {
            dicPlayers.Add(listPlayer[i].name, listPlayer[i]);
        }

        player = dicPlayers["GVR-1"];
      //  GamePlay.instance.VRplayer = player.transform.findChildRecursively("Player").gameObject;
      
        testRequire.SetActive(false);
        if (this.gameObject.scene.name == "Showroom2_01")
        {
            MainSceneScript.instance.player.transform.position = new Vector3(0.01f, 1.61f, -12.6f);
            MainSceneScript.instance.player.transform.findChildRecursively("Player").localPosition = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    private void Update()
   {
    //    if (GvrController.AppButtonUp && Player.instance.currentState != Player.PlayerState.PlayingGame)
    //    {
    //        testRequire.SetActive(true);
    //        Player.instance.SetState(Player.PlayerState.Selecting);
    //    }
    //    if (GvrController.AppButtonUp && Player.instance.currentState == Player.PlayerState.PlayingGame)
    //    {
    //        RequireTest.instance.stopTest();

    //    }        
        if(this.gameObject.scene.name == "Showroom2_01" && GvrController.AppButtonUp)
        {
            if (player.transform.findChildRecursively("Player").transform.position.y >= 3)
            {
                Transform getPoint = GameObject.Find("Down-Point").transform;
                Vector3 temp = getPoint.transform.position;
                player.transform.findChildRecursively("Player").transform.DOMove(temp, 1.5f);
            }
            else {
                Player.instance.SetState(Player.PlayerState.None);
                TestDrop.instance.onOutGame();
            }
        }
    
    }


    public void createPlayer()
    {
        player.transform.position = lastPosOfPlayer;
    
    }
    public void destroyPlayer()
    {
        lastPosOfPlayer = player.transform.position;
        testRequire.SetActive(true);
    }
     
}
