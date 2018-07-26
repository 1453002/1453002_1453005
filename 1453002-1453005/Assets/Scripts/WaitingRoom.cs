using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DoozyUI;

public class WaitingRoom : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler,
                           IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.name == "Museum" || gameObject.name == "Medical")
        {
         
            gameObject.transform.localScale *= 1.15f;
        }
           
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.name == "Museum" || gameObject.name == "Medical")
        {
            gameObject.transform.localScale /= 1.15f;   
            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(gameObject.name == "Museum")
        {
            FBFade.instance.loadSceneAsyncAndFadeOut("Baked_MuseumVR_vol1", 1);


        }

       if(gameObject.name == "Medical")
        {
            FBFade.instance.loadSceneAsyncAndFadeOut("Showroom2_01", 0);
           // SceneManager.LoadScene("Showroom2_01");
        }
    }  

    public void OnPointerUp(PointerEventData eventData)
    {

    }
 
}
