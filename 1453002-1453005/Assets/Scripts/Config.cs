using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {

    static public Config instance;
    //scene init
    public float fadeDuration = 0.5f;
   
  
    //scene Museum
    public int numHoveredObjectsToTest = 5;
    public int scoreEachQuestionMuseum = 10;
  

    //scene Medical
    public int scoreEachQuestionMedical = 10;

    private void Awake()
    {
        instance = this;        
    }
    private void Start()
    {
        FBFade.instance.fadeIn(fadeDuration);
    }
}
