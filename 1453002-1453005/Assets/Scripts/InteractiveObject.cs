using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections.Generic;
public class InteractiveObject : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler,
                           IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
    GameObject player;
    public GameObject video1;
    public GameObject video2;
    public GameObject checkPlay;
    public GameObject gamePlay;
    public GameObject GVR1;
    public GameObject GVR2;
    public GameObject boneReal;
    public GameObject boneMarker;
    public GameObject fakeWall;
    public List<GameObject> upStairs = new List<GameObject>();
    void Start() {
           player=GameObject.Find("GVR-1");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (this.gameObject.scene.name == "Showroom2_01" && Player.instance.currentState != Player.PlayerState.PlayingGame)
        {
            Player.instance.SetState(Player.PlayerState.Selecting);
            Debug.Log(this.gameObject.name);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
   
        if (this.gameObject.scene.name == "Showroom2_01" && Player.instance.currentState!=Player.PlayerState.PlayingGame)
        {
            Player.instance.SetState(Player.PlayerState.None);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.gameObject.scene.name == "Showroom2_01")
        {
            if (this.gameObject.name == "stiege")
            {
                Transform getPoint;
                Vector3 nextPos;

                getPoint = GameObject.Find("Up-Point").transform;
                nextPos = new Vector3(getPoint.position.x, getPoint.position.y, getPoint.position.z);

                player.transform.findChildRecursively("Player").transform.DOMove(nextPos, 1.5f);
                for(int i=0;i<upStairs.Count;i++)
                {
                    upStairs[i].gameObject.SetActive(true);
                }

            }
            if (this.gameObject.name.Contains("Video"))
            {
                    BaseUI.instance.PlayVideo(this.gameObject);
            }
            if (this.gameObject.name.Contains("Upstair") && Player.instance.currentState != Player.PlayerState.PlayingGame)
            {
                if (this.gameObject.name == "Upstair6" && Player.instance.currentState != Player.PlayerState.PlayingGame)
                {
                    checkPlay.SetActive(true);
                }
                Vector3 temp = new Vector3(this.transform.position.x,this.transform.position.y+1.5f,this.transform.position.z);
                player.transform.findChildRecursively("Player").transform.DOMove(temp, 1.5f);
            }
            if (this.gameObject.name == "Bone-Build")
            {
                Vector3 temp1 = new Vector3(this.transform.position.x, this.transform.position.y+1f, this.transform.position.z+0.5f);
                player.transform.findChildRecursively("Player").transform.DOMove(temp1, 1.5f);
                foreach (Transform t in boneReal.transform)
                {
                    t.GetComponent<BoxCollider>().enabled=true;
                    t.GetComponent<Rigidbody>().useGravity=true;
                    t.GetComponent<Rigidbody>().isKinematic = false;
                }
                fakeWall.SetActive(true);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
