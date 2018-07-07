using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ObjectHandleEvent : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler,
                           IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
    GameObject tmp;
	// Use this for initialization

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(gameObject.name == "sumerian_sphinx_right")
        {           
            Debug.Log(gameObject.name +"- hover");
            Player.instance.SetState(Player.PlayerState.Selecting);
        }
        if(gameObject.name == "sumerian_sphinx_left")
        {            
            Player.instance.SetState(Player.PlayerState.Selecting);
        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.name == "sumerian_sphinx_right")
        {        
            Player.instance.SetState(Player.PlayerState.None);
        }
        if (gameObject.name == "sumerian_sphinx_left")
        {          
            Player.instance.SetState(Player.PlayerState.None);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.name == "sumerian_sphinx_right")
        {
            tmp = GameObject.Find("sumerian_sphinx_left");
            if(Vector3.Distance(gameObject.transform.position,tmp.transform.position) <35)
            {
                Exam1GamePlay.instance.board.SetActive(true);
                gameObject.transform.DOMoveX(gameObject.transform.position.x - 20, 0.5f);
                tmp.transform.DOMoveX(tmp.transform.position.x + 20, 0.5f);
            }
            if (Vector3.Distance(gameObject.transform.position, tmp.transform.position) > 35)
            {
                Exam1GamePlay.instance.board.SetActive(false);
                gameObject.transform.DOMoveX(gameObject.transform.position.x + 20, 0.5f);
                tmp.transform.DOMoveX(tmp.transform.position.x - 20, 0.5f);
            }

        }   
        if (gameObject.name == "sumerian_sphinx_left")
        {
            tmp = GameObject.Find("sumerian_sphinx_right");
            if (Vector3.Distance(gameObject.transform.position, tmp.transform.position) < 35)
            {
                Exam1GamePlay.instance.board.SetActive(true);
                gameObject.transform.DOMoveX(gameObject.transform.position.x + 20, 0.5f);
                tmp.transform.DOMoveX(tmp.transform.position.x - 20, 0.5f);
            }
            if (Vector3.Distance(gameObject.transform.position, tmp.transform.position) > 35)
            {
                Exam1GamePlay.instance.board.SetActive(false);
                gameObject.transform.DOMoveX(gameObject.transform.position.x - 20, 0.5f);
                tmp.transform.DOMoveX(tmp.transform.position.x+ 20, 0.5f);
            }
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
