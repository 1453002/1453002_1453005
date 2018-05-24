using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exam1GamePlay : MonoBehaviour
{
    public static Exam1GamePlay instance;
    // Use this for initialization
    public GameObject board;
    public Dictionary<int, string> answers;
    public int currentQuestion = 1;
    public int score = 0;
    GameObject a, b, c, d, content, cubeImage;
    public int maxQuestion = 0;
    public GameObject UI;
    Text maxQ, curQ, scoreUI;

    private void Awake()
    {
        instance = this;
        answers = new Dictionary<int, string>();
    }

    void Start () {

        maxQ = UI.transform.findChildRecursively("MaxQuestion").gameObject.GetComponent<Text>();
        curQ = UI.transform.findChildRecursively("CurQuestion").gameObject.GetComponent<Text>();
        scoreUI = UI.transform.findChildRecursively("Score").gameObject.GetComponent<Text>();
        
        
        a = board.transform.findChildRecursively("A").gameObject;
        c = board.transform.findChildRecursively("C").gameObject;
        d = board.transform.findChildRecursively("D").gameObject;
        b = board.transform.findChildRecursively("B").gameObject;
        content = board.transform.findChildRecursively("Content").gameObject;
        loadQuestion(currentQuestion);
        maxQuestion = FBGameData.instance.getClassData("Question").objects.Count;
        maxQ.text = "Total question : " + maxQuestion;
        cubeImage = board.transform.findChildRecursively("cube-image").gameObject;
        cubeImage.SetActive(false);
    }	
	

    public void loadQuestion(int ques)
    {
        FBClassData Questions = FBGameData.instance.getClassData("Question");
        string cnt = null; 
        if (Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques)) !=null)
        {
            cnt = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques)).getFieldValue("Content").stringValue;
        }else if (Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques+"-img")) !=null)
        {
            cnt = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques + "-img")).getFieldValue("Content").stringValue;
        }
        content.GetComponent<TextMesh>().text = cnt;
        string questionID = null;
        if (Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques)) != null)
        {
            questionID = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques)).getFieldValue("QuestionID").stringValue;
            a.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques)).getFieldValue("Option1").stringValue;
            b.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques)).getFieldValue("Option2").stringValue;
            c.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques)).getFieldValue("Option3").stringValue;
            d.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques)).getFieldValue("Option4").stringValue;
        }
        else if (Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques + "-img")) !=null)
        {
            questionID = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques + "-img")).getFieldValue("QuestionID").stringValue;
            a.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques + "-img")).getFieldValue("Option1").stringValue;
            b.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques + "-img")).getFieldValue("Option2").stringValue;
            c.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques + "-img")).getFieldValue("Option3").stringValue;
            d.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques + "-img")).getFieldValue("Option4").stringValue;
        }
        if(questionID.Contains("-img"))
        {
            cubeImage.SetActive(true);
            cubeImage.GetComponent<MeshRenderer>().material.mainTexture = MediaManager.instance.dicImages[questionID];
        }
       
        updateUI();
    }
  
    public string getAnswer(int ques)
    {
        string answer = FBGameData.instance.getClassData("Question").getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + ques)).getFieldValue("Answer").stringValue;
        return convert2Answer(answer);
    }
    
    string convert2Answer(string raw)
    {
        string answer = null;
        if (raw.Contains("1"))
            answer = "A";
        if (raw.Contains("2"))
            answer = "B";
        if (raw.Contains("3"))
            answer =  "C";
        if (raw.Contains("4"))
            answer = "D";
        return answer;
        
    }
    void updateUI()
    {
        curQ.text = "current question : " + currentQuestion;
        scoreUI.text = "Score : " + score;
    }
   
}
