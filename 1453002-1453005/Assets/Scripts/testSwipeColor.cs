using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testSwipeColor : MonoBehaviour {

    public GameObject UI;
    public GameObject spawnerBalloons;

    Text maxQ, curQ, scoreQ;

    public static testSwipeColor instance;
    public int currentColor = 0;
    public int currentQuestion = 1;
    public Dictionary<string, GameObject> balloonIns;
    public int score = 0;
    public int maxQuestion = 0;
    public FBClassData classQuestion = null;
    public GameObject countDown;
    int countdown = 50;
    private void Awake()
    {
        instance = this;
        balloonIns = new Dictionary<string, GameObject>();
    }
    // Use this for initialization
    void Start () {
        spawnerBalloons.SetActive(true);
        //maxQ = UI.transform.findChildRecursively("MaxQuestion").gameObject.GetComponent<Text>();
        //maxQ.text = "Total Question : " + maxQuestion;
        //curQ = UI.transform.findChildRecursively("CurQuestion").gameObject.GetComponent<Text>();
        //scoreQ = UI.transform.findChildRecursively("Score").gameObject.GetComponent<Text>();

        loadQuestion(currentQuestion);
        maxQuestion = FBGameData.instance.getClassData("Question").objects.Count;
        classQuestion = FBGameData.instance.getClassData("Question");
        StartCoroutine(Minus());
    }	
    public void loadQuestion(int curQues)
    {
        FBClassData Questions = FBGameData.instance.getClassData("Question");
        gameObject.transform.findChildRecursively("Content").gameObject.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + curQues)).getFieldValue("Content").stringValue;
        if(balloonIns.ContainsKey("A"))
        {
            balloonIns["A"].transform.findChildRecursively("Text").GetComponent<Text>().text = classQuestion.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + curQues)).getFieldValue("Option1").stringValue;
        }
        if (balloonIns.ContainsKey("B"))
        {
            balloonIns["B"].transform.findChildRecursively("Text").GetComponent<Text>().text = classQuestion.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + curQues)).getFieldValue("Option2").stringValue;
        }
        if (balloonIns.ContainsKey("C"))
        {
            balloonIns["C"].transform.findChildRecursively("Text").GetComponent<Text>().text = classQuestion.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + curQues)).getFieldValue("Option3").stringValue;
        }
        if (balloonIns.ContainsKey("D"))
        {
            balloonIns["D"].transform.findChildRecursively("Text").GetComponent<Text>().text = classQuestion.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + curQues)).getFieldValue("Option4").stringValue;
        }
        //updateUI();
    }

    void Update()
    {
        //if (Input.GetKeyDown("space"))
        //{
        //    foreach (Transform child in UI.transform)
        //        Debug.Log(child.name);
        //}

    }
    IEnumerator Minus()
    {
        yield return new WaitForSeconds(1);
        countdown--;
        countDown.GetComponent<Text>().text = countdown.ToString();
        if (countdown >= 0) {
            StartCoroutine(Minus());
        }
      
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
            answer = "C";
        if (raw.Contains("4"))
            answer = "D";
        return answer;

    }
    void updateUI()
    {
        //curQ.text = "Current Question : " + currentQuestion;
        //scoreQ.text = "Score : " + score;

    }


    
}
