using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrop : MonoBehaviour {

    static public TestDrop instance;
    public GameObject playerDrop;
    
    public Dictionary<string, GameObject> markers;
    public List<GameObject> listMarker;
    public GameObject realBone;
    public GameObject boneMarkers;
    public GameObject fakeWall;
    // Use this for initialization
    List<Vector3> starPos=new List<Vector3>();
    List<Quaternion> starRot= new List<Quaternion>();
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
        for (int i = 0; i < realBone.transform.childCount; i++)
        {
            starPos.Add(realBone.transform.GetChild(i).transform.position);
            Quaternion temp = realBone.transform.GetChild(i).transform.rotation;
            starRot.Add(temp);
        }

        //playerDrop.transform.position = new Vector3(-19.94f, -12f, 49.2f);
        //playerDrop.transform.rotation.SetLookRotation(new Vector3(0, -19.916f, 0));
        //Player.instance.teleportController.gameObject.SetActive(false);
        //Player.instance.SetState(Player.PlayerState.PlayingGame);
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
    public void onOutGame() {
        for (int i = 0; i < realBone.transform.childCount; i++)
        {
            realBone.transform.GetChild(i).gameObject.SetActive(true);
            realBone.transform.GetChild(i).transform.position = starPos[i];
            realBone.transform.GetChild(i).transform.rotation = starRot[i];
            realBone.transform.GetChild(i).GetComponent<Rigidbody>().useGravity = false;
            realBone.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = true;
            realBone.transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;
            fakeWall.SetActive(false);
        }
        for (int i = 0; i < boneMarkers.transform.childCount; i++)
        {
            boneMarkers.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
