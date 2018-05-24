using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneScript : MonoBehaviour {

    public static MainSceneScript instance;
    public List<GameObject> listPlayers;
    public Dictionary<string, GameObject> dictionaryPlayers;
    
    private void Awake()
    {
        instance = this;
        FBGameData.instance.loadGameDatabase("Data");
        Debug.Log(FBGameData.instance.loadGameDatabase("Data"));
        dictionaryPlayers = new Dictionary<string, GameObject>();
    }
    // Use this for initialization
    void Start () {
        SceneObjectManager.instance.initSceneInteractiveObjects(this.gameObject.scene);    
        
        for(int  i = 0; i< listPlayers.Count; i++)
        {
            dictionaryPlayers.Add(listPlayers[i].name, listPlayers[i]);
        }
    }
	
	// Update is called once per frame

}
