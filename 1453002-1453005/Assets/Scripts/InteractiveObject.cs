using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DG.Tweening;
public class InteractiveObject : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler,
                           IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
    GameObject player;
    void Start() {
           player=GameObject.Find("GVR-1");
    }
    bool upCheck = false;
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (this.gameObject.scene.name == "Showroom2_01")
        {
            Player.instance.SetState(Player.PlayerState.Selecting);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
   
        if (this.gameObject.scene.name == "Showroom2_01")
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
                //upCheck = !upCheck;
                Transform getPoint;
                Vector3  nextPos;
                //if (upCheck == true)
                //{
                //    getPoint = GameObject.Find("Up-point").transform;
                //    upCheck = false;
                //    Debug.Log("Go up");
                //}
                //else
                //{
                //    getPoint = GameObject.Find("Down-point").transform;
                //    upCheck = true;
                //    Debug.Log("Go down");
                //}
                getPoint = GameObject.Find("Up-Point").transform;
                nextPos = new Vector3(getPoint.position.x, getPoint.position.y, getPoint.position.z);
                Debug.Log("UPUPUPUPU");
                //player.transform.DOMove(nextPos, 1.5f,true);
                //MainSceneScript.instance.player.transform.findChildRecursively("Player").localPosition = new Vector3(0, 0, 0);
                //MainSceneScript.instance.player.transform.findChildRecursively("Player").rotation = getPoint.transform.rotation;

                //player.transform.transform.DOMove(nextPos,1.5f,false);
                player.transform.findChildRecursively("Player").transform.DOComplete();
                player.transform.findChildRecursively("Player").transform.DOMove(nextPos,1.5f);
                //Player tmp = player.GetComponentInChildren<Player>();
                //if(tmp)
                //{
                //    tmp.MoveTo(nextPos, Quaternion.identity);
                //}
                Player.instance.SetState(Player.PlayerState.None);
            }
            GameObject temp;
            if (this.gameObject.name == "PlayCube1" )
            {
                temp = GameObject.Find("Disease1-Video");
                BaseUI.instance.PlayVideo(temp);
            }
            if (this.gameObject.name == "PlayCube2")
            {
                temp = GameObject.Find("Disease2-Video");
                BaseUI.instance.PlayVideo(temp);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
