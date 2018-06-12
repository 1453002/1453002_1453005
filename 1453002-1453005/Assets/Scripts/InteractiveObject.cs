using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DG.Tweening;
public class InteractiveObject : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler,
                           IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
    GameObject player;
    public GameObject video1;
    public GameObject video2;
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
            Debug.Log(this.gameObject.name);
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
                Transform getPoint;
                Vector3 nextPos;
                if (upCheck == false)
                {
                    getPoint = GameObject.Find("Up-Point").transform;
                    Debug.Log("Go up");
                }
                else
                {
                    getPoint = GameObject.Find("Down-Point").transform;
                    Debug.Log("Go down");
                }

                nextPos = new Vector3(getPoint.position.x, getPoint.position.y, getPoint.position.z);
                Debug.Log("UPUPUPUPU");

                player.transform.findChildRecursively("Player").transform.DOMove(nextPos, 1.5f);

                upCheck = !upCheck;

            }
            if (this.gameObject.name == "PlayCube1")
            {
                    BaseUI.instance.PlayVideo(video1);
            }
            if (this.gameObject.name == "PlayCube2")
            {
                BaseUI.instance.PlayVideo(video2);
            }
            if (this.gameObject.name.Contains("Upstair"))
            {
                player.transform.findChildRecursively("Player").transform.DOMove(this.transform.position, 1.5f);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
