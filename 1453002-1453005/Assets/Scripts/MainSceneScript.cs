using System.Collections;
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
    public GameObject menuPlay;
    [HideInInspector]
    public GoogleVR.Demos.DemoInputManager demoInputManager;
    public GameObject chooseMenu;
    public GameObject microbe1;
    public GameObject microbe2;
    public GameObject microbe3;
    public GameObject dna;
    public GameObject cell;
    [HideInInspector]
    public GameObject musicObject = null;
    private void Awake()
    {
        instance = this;
        FBGameData.instance.loadGameDatabase("Data");
        dicPlayers = new Dictionary<string, GameObject>();      

    }
    // Use this for initialization
    void Start () {
        FBFade.instance.fadeIn(Config.instance.fadeDuration);
        SceneObjectManager.instance.initSceneInteractiveObjects(this.gameObject.scene);
        listPlayer[1].name = "GVR-2";
        for (int i = 0; i < listPlayer.Count; i++)
        {
            dicPlayers.Add(listPlayer[i].name, listPlayer[i]);
        }

        player = dicPlayers["GVR-1"];
       
        
        if (this.gameObject.scene.name == "Showroom2_01")
        {
            chooseMenu.SetActive(false);
            MainSceneScript.instance.player.transform.position = new Vector3(0.01f, 1.61f, -12.6f);
            MainSceneScript.instance.player.transform.findChildRecursively("Player").localPosition = new Vector3(0, 0, 0);
            musicObject = GameObject.Find("Microbe");
            if (musicObject)
                SoundResonanceManager.instance.playBgm(musicObject, "bgm2", true);
        }
    }

    // Update is called once per frame
    private void Update()
   {
        if(gameObject.scene.name == "Showroom2_01")
        {
            microbe1.transform.Rotate(new Vector3(0, 0, 2), Space.Self);
            microbe2.transform.Rotate(new Vector3(0, 0, 2), Space.Self);
            microbe3.transform.Rotate(new Vector3(0, 0, 2), Space.Self);
            dna.transform.Rotate(new Vector3(0, 2, 0), Space.Self);
            cell.transform.Rotate(new Vector3(0, 2, 0), Space.Self);
            if (this.gameObject.scene.name == "Showroom2_01" && GvrController.AppButtonUp)
            {
                if (player.transform.findChildRecursively("Player").transform.position.y >= 3)
                {
                    //Game upstair off
                    if (!listPlayer[0].activeSelf)
                    {
                        listPlayer[0].SetActive(true);
                        listPlayer[1].SetActive(false);
                        menuPlay.SetActive(false);
                        Player.instance.SetState(Player.PlayerState.None);
                        testSwipeColor.instance.UnActiveGame();

                    }
                    else
                    {
                        //Go down stair
                        Transform getPoint = GameObject.Find("Down-Point").transform;
                        Vector3 temp = getPoint.transform.position;
                        player.transform.findChildRecursively("Player").transform.DOMove(temp, 1.5f);
                    }
                }
                else
                {
                    //Game skeleton out
                    Player.instance.SetState(Player.PlayerState.None);
                    TestDrop.instance.onOutGame();
                }
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
        chooseMenu.SetActive(true);
    }
    public void changeGVRStyle()
    {
        if (Player.instance.currentState != Player.PlayerState.PlayingGame)
        {
            menuPlay.SetActive(true);
            chooseMenu.SetActive(false);
            Player.instance.SetState(Player.PlayerState.PlayingGame);
            Vector3 temp = listPlayer[0].transform.findChildRecursively("Player").transform.position;
            Quaternion quaTemp = listPlayer[0].transform.findChildRecursively("Player").transform.rotation;
            listPlayer[0].gameObject.SetActive(false);
            listPlayer[1].gameObject.SetActive(true);
            listPlayer[1].transform.findChildRecursively("PlayerPlay").transform.position = temp;
            listPlayer[1].transform.findChildRecursively("PlayerPlay").transform.rotation = quaTemp;

        }
    }
    public void TurnOffChooseMenu() {
        chooseMenu.SetActive(false);
    }

}
