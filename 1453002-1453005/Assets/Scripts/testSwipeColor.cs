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


    private void Awake()
    {
        instance = this;
        balloonIns = new Dictionary<string, GameObject>();
    }
    // Use this for initialization
    void Start () {
        spawnerBalloons.SetActive(true);
        maxQ = UI.transform.findChildRecursively("MaxQuestion").gameObject.GetComponent<Text>();
        maxQ.text = "Total Question : " + maxQuestion;
        curQ = UI.transform.findChildRecursively("CurQuestion").gameObject.GetComponent<Text>();
        scoreQ = UI.transform.findChildRecursively("Score").gameObject.GetComponent<Text>();

        loadQuestion(currentQuestion);
        maxQuestion = FBGameData.instance.getClassData("Question").objects.Count;
        classQuestion = FBGameData.instance.getClassData("Question");

        
    }	
    public void loadQuestion(int curQues)
    {
        FBClassData Questions = FBGameData.instance.getClassData("Question");
        gameObject.transform.findChildRecursively("Content").gameObject.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + curQues)).getFieldValue("Content").stringValue;
        if(balloonIns.ContainsKey("A"))
        {
            balloonIns["A"].transform.findChildRecursively("Content").GetComponent<TextMesh>().text = classQuestion.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + curQues)).getFieldValue("Option1").stringValue;
        }
        if (balloonIns.ContainsKey("B"))
        {
            balloonIns["B"].transform.findChildRecursively("Content").GetComponent<TextMesh>().text = classQuestion.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + curQues)).getFieldValue("Option2").stringValue;
        }
        if (balloonIns.ContainsKey("C"))
        {
            balloonIns["C"].transform.findChildRecursively("Content").GetComponent<TextMesh>().text = classQuestion.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + curQues)).getFieldValue("Option3").stringValue;
        }
        if (balloonIns.ContainsKey("D"))
        {
            balloonIns["D"].transform.findChildRecursively("Content").GetComponent<TextMesh>().text = classQuestion.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + curQues)).getFieldValue("Option4").stringValue;
        }
        updateUI();
    }

    private void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            foreach (Transform child in UI.transform)
                Debug.Log(child.name);
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
        curQ.text = "Current Question : " + currentQuestion;
        scoreQ.text = "Score : " + score;

    }

    public void playTestSwipe()
    {
        GameObject player = GameObject.Find("Player").gameObject ;
        GameObject swipePlayer =  Instantiate(MainSceneScript.instance.dictionaryPlayers["SwipePlayer"], player.transform);

        player.name = "tmp";
        player.SetActive(false);
        swipePlayer.name = "Player";
        swipePlayer.SetActive(true);

    }
    
}
