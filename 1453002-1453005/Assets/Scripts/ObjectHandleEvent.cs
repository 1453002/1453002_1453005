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
        if(gameObject.name == "btnOk" || gameObject.name == "btnCancel")
        {
            gameObject.transform.localScale *= 1.15f;
        }
        Player.instance.SetState(Player.PlayerState.Selecting);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.name == "btnOk" || gameObject.name == "btnCancel")
        {
            gameObject.transform.localScale /= 1.15f;
        }
        Player.instance.SetState(Player.PlayerState.None);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Exam1GamePlay.instance.countHovers >= Config.instance.numHoveredObjectsToTest)
        {
            if (gameObject.name == "TestChoseObj")
            {
                Exam1GamePlay.instance.isPlayingGame = true;

                Exam1GamePlay.instance.board.SetActive(true);
                Exam1GamePlay.instance.roman_augustus.SetActive(false);
                Exam1GamePlay.instance.initBoard();
                Exam1GamePlay.instance.doLeftRightStatueMove();
                Vector3 pos = new Vector3(8.12f, -14.127f, -33f);
                GamePlay.instance.VRplayer.transform.DOMove(pos, 1);
                Player.instance.SetState(Player.PlayerState.PlayingGame);
               // GamePlay.instance.VRplayer.transform.findChildRecursively("TeleportController").gameObject.SetActive(false);
                
                Exam1GamePlay.instance.isVisibleObj(Exam1GamePlay.instance.ActivationTestObject, false);
            }
        }
        if (gameObject.name == "btnOk")
        {
          
            Exam1GamePlay.instance.finishMultipleTest();
            Exam1GamePlay.instance.isVisibleObj(Exam1GamePlay.instance.board, true);          
            Exam1GamePlay.instance.doLeftRightStatueMove();
            Player.instance.SetState(Player.PlayerState.None);
            Exam1GamePlay.instance.isVisibleObj(Exam1GamePlay.instance.Notification, false);

            Exam1GamePlay.instance.initBoard();
        }
        if (gameObject.name == "btnCancel")
        {
            Exam1GamePlay.instance.isPlayingGame = false;

            Player.instance.SetState(Player.PlayerState.None);
            Exam1GamePlay.instance.isVisibleObj(Exam1GamePlay.instance.board, false);
            Exam1GamePlay.instance.roman_augustus.SetActive(true);
            Exam1GamePlay.instance.doLeftRightStatueMove();
            Player.instance.SetState(Player.PlayerState.None);
            Exam1GamePlay.instance.isVisibleObj(Exam1GamePlay.instance.Notification, true);
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {

    }


}
