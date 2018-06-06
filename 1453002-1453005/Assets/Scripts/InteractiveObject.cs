using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DG.Tweening;
public class InteractiveObject : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler,
                           IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
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
        Debug.Log("EXIT");
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
                upCheck = !upCheck;
                Transform getPoint;
                Vector3  nextPos;
                if (upCheck == true)
                {
                    getPoint = GameObject.Find("Up-point").transform;
                    upCheck = false;
                    Debug.Log("Go up");
                }
                else
                {
                    getPoint = GameObject.Find("Down-point").transform;
                    upCheck = true;
                    Debug.Log("Go down");
                }
                nextPos = new Vector3(getPoint.position.x, getPoint.position.y, getPoint.position.z);
                MainSceneScript.instance.player.transform.DOMove(nextPos, 1.5f);
                MainSceneScript.instance.player.transform.findChildRecursively("Player").localPosition = new Vector3(0, 0, 0);
                MainSceneScript.instance.player.transform.findChildRecursively("Player").rotation = getPoint.transform.rotation;

                //MainSceneScript.instance.player.transform.findChildRecursively("Player").transform.DOMove(nextPos, 1.5f);
                Player.instance.SetState(Player.PlayerState.None);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
