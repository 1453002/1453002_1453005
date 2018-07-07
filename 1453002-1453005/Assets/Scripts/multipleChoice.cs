using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class multipleChoice : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler,
                           IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject.scene.name == "MultipleChoice" || this.gameObject.scene.name == "Baked_MuseumVR_vol1")
        {
            Vector3 scale = gameObject.transform.localScale;
            this.gameObject.transform.localScale = new Vector3(scale.x*7f, scale.y*7f, scale.z*1f);            
        }
        if(this.gameObject.scene.name == "swipeMenuShot")
        {

        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.gameObject.scene.name == "MultipleChoice" || this.gameObject.scene.name == "Baked_MuseumVR_vol1")
        {
            Vector3 scale = gameObject.transform.localScale;
            this.gameObject.transform.localScale = new Vector3(scale.x/ 7f, scale.y / 7f, scale.z / 1f);            
        }
        if(this.gameObject.scene.name == "swipeMenuShot")
        {

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.gameObject.scene.name == "MultipleChoice" || this.gameObject.scene.name == "Baked_MuseumVR_vol1")
        {
            if (Exam1GamePlay.instance.getAnswer(Exam1GamePlay.instance.currentQuestion).Equals(this.gameObject.transform.GetChild(0).name))
                Exam1GamePlay.instance.score += 10;
            if (Exam1GamePlay.instance.currentQuestion + 1 <= Exam1GamePlay.instance.maxQuestion)
            {
                Exam1GamePlay.instance.currentQuestion += 1;
                Exam1GamePlay.instance.loadQuestion(Exam1GamePlay.instance.currentQuestion);
            }
        }
        if(this.gameObject.scene.name == "swipeMenuShot")
        {
           
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
