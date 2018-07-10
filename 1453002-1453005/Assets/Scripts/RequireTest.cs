using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireTest : MonoBehaviour {


    public static RequireTest instance;
    // Use this for initialization
    Vector3 gvr2Pos, gvr2Rot;
    GameObject GVR1, GVR2;

    [HideInInspector]
    public GameObject TeleportTarget, LineOriginVisual,Avatar_02; 
  
    private void Awake()
    {
        instance = this;
    }
    void Start () {
        this.gameObject.SetActive(false);
        GVR1 = MainSceneScript.instance.player;

     
    }
	
	public void confirmTest()
    {
        playTest();
    }
    public void cancelRequireTest()
    {
        this.gameObject.SetActive(false);
        Player.instance.SetState(Player.PlayerState.None);
    }

    void playTest()
    {
        MainSceneScript.instance.destroyPlayer();
        
        createGVR2();      
        Player.instance.SetState(Player.PlayerState.PlayingGame);
        this.gameObject.SetActive(false);
    }
   
    public void stopTest()
    {       
        
        MainSceneScript.instance.createPlayer();
    
        Player.instance.SetState(Player.PlayerState.None);
    }
    void createGVR2()
    {
      //  GVR2 = Instantiate(MainSceneScript.instance.dicPlayers["GVR-2"]);    
        if (this.gameObject.scene.name == "Baked_MuseumVR_vol1")        {
           
            MainSceneScript.instance.player.transform.position = new Vector3(3, 7.7f, -33f);
            MainSceneScript.instance.player.transform.eulerAngles = new Vector3(0, 177.526f, 0);
        }
        

    }
}
