using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrop : MonoBehaviour {

    static public TestDrop instance;
    public GameObject playerDrop;
    
    public Dictionary<string, GameObject> markers;
    public List<GameObject> listMarker;
    // Use this for initialization

    private void Awake()
    {
        instance = this;
    }
    void Start () {
        if (listMarker.Count == 0 || playerDrop == null)
            return;
        markers = new Dictionary<string, GameObject>();
        for(int i = 0; i< listMarker.Count; i++)
        {
            markers.Add(listMarker[i].name, listMarker[i]);
        }


        //playerDrop.transform.position = new Vector3(-19.94f, -12f, 49.2f);
        //playerDrop.transform.rotation.SetLookRotation(new Vector3(0, -19.916f, 0));
        //Player.instance.teleportController.gameObject.SetActive(false);
        Player.instance.SetState(Player.PlayerState.PlayingGame);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onChooseDrop()
    {
        playerDrop.transform.position = new Vector3(-19.94f, -8.39f, 49.2f);
        playerDrop.transform.rotation.SetLookRotation(new Vector3(0, -19.916f, 0));
        Player.instance.teleportController.gameObject.SetActive(false);
    }
}
