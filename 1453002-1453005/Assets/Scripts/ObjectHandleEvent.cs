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
        if (Exam1GamePlay.instance.countHovers >=0/* Exam1GamePlay.instance.numObjHoverRequire*/)
        {
            if (gameObject.name == "sumerian_sphinx_right")
            {
                tmp = GameObject.Find("sumerian_sphinx_left");
                Exam1GamePlay.instance.board.SetActive(true);
                Exam1GamePlay.instance.roman_augustus.SetActive(false);
                Exam1GamePlay.instance.initBoard();

                doStatuesMove1(tmp, gameObject);
            }
            if (gameObject.name == "sumerian_sphinx_left")
            {
                tmp = GameObject.Find("sumerian_sphinx_right");
                Exam1GamePlay.instance.board.SetActive(true);
                Exam1GamePlay.instance.roman_augustus.SetActive(false);
                Exam1GamePlay.instance.initBoard();

                doStatuesMove1(gameObject, tmp);

            }

            //if(gameObject.name == "roman_augustus")
            //{
            //    GameObject obj1 = GameObject.Find("sumerian_sphinx_left");
            //    GameObject obj2 = GameObject.Find("sumerian_sphinx_right");
            //    Exam1GamePlay.instance.board.SetActive(true);
            //    Exam1GamePlay.instance.roman_augustus.SetActive(false);
            //    Exam1GamePlay.instance.initBoard();

            //    doStatuesMove1(obj1, obj2);
            //}
        }
    }
    void doStatuesMove1(GameObject obj1, GameObject obj2)
    {
        obj1.transform.DOMoveX(obj1.transform.position.x - 20, 0.5f);
        obj2.transform.DOMoveX(obj2.transform.position.x + 20, 0.5f);
        StartCoroutine(doStatuesMove2(obj1, obj2));
    }

    IEnumerator doStatuesMove2(GameObject obj1, GameObject obj2)
    {
        yield return new WaitForSeconds(0.5f);
        obj1.transform.DOMoveX(obj1.transform.position.x + 20, 0.5f);
        obj2.transform.DOMoveX(obj2.transform.position.x - 20, 0.5f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
