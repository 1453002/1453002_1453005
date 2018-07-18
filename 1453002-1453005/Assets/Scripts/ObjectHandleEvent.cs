using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class ObjectHandleEvent : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler,
                           IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
    GameObject tmp;
	// Use this for initialization

    public void OnPointerEnter(PointerEventData eventData)
    {
        Player.instance.SetState(Player.PlayerState.Selecting);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Player.instance.SetState(Player.PlayerState.None);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Exam1GamePlay.instance.countHovers >= Exam1GamePlay.instance.numObjHoverRequire)
        {
            if (gameObject.name == "TestChoseObj")
            {                
                GameObject obj1 = GameObject.Find("sumerian_sphinx_left");
                GameObject obj2 = GameObject.Find("sumerian_sphinx_right");
                Exam1GamePlay.instance.board.SetActive(true);
                Exam1GamePlay.instance.roman_augustus.SetActive(false);
                Exam1GamePlay.instance.initBoard();

                doStatuesMove1(obj1, obj2);
                GamePlay.instance.VRplayer.transform.DOMove(gameObject.transform.position, 1);
                Player.instance.SetState(Player.PlayerState.PlayingGame);
                disableActivationAfterSeconds(1.2f);
            }
        }
        if(gameObject.name == "btnOk")
        {            
               
                Exam1GamePlay.instance.finishMultipleTest();
                GameObject obj1 = GameObject.Find("sumerian_sphinx_left");
                GameObject obj2 = GameObject.Find("sumerian_sphinx_right");
                Exam1GamePlay.instance.board.SetActive(false);
                Exam1GamePlay.instance.roman_augustus.SetActive(true);

                doStatuesMove1(obj1, obj2);
                
                
         
            Player.instance.SetState(Player.PlayerState.None);
        }
        if(gameObject.name == "btnCancel")
        {
            Exam1GamePlay.instance.Notification.SetActive(false);
            Player.instance.SetState(Player.PlayerState.None);
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
    void disableNotifiAfterSeconds(float time)
    {
        Invoke("inactiveObject", time);
    }
    void disableActivationAfterSeconds(float time)
    {
        Invoke("inactiveActivationObj", time);
    }

    void inactiveNotifi()
    {
        Exam1GamePlay.instance.Notification.SetActive(false);
    }
    void inactiveActivationObj()
    {
        Exam1GamePlay.instance.ActivationTestObject.SetActive(false);
    }
}
