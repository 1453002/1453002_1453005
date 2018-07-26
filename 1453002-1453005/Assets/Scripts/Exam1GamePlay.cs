using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Exam1GamePlay : MonoBehaviour
{
    public static Exam1GamePlay instance;
    public GameObject roman_augustus;
    public GameObject statue_left, statue_right;

    public GameObject Notification;
    public GameObject ActivationTestObject;

    
    //[HideInInspector]
    public int countHovers = 0;
    [HideInInspector]
    public List<string> objectsHovered;

    // Use this for initialization
    public GameObject board;
    public GameObject hintForTest;
    public Dictionary<int, string> answers;
    [HideInInspector]
    public int currentQuestion = 1;
    public int score = 0;
    GameObject a, b, c, d, content, cubeImage;
    public int maxQuestion = 0;
    int maxQuestionOfThisTopic = 0;
    public GameObject UI;
    GameObject maxQ, curQ, scoreUI;

    [HideInInspector]
    public bool isPlayingGame = false;

    GameObject teleport;
    private void Awake()
    {
        instance = this;
        answers = new Dictionary<int, string>();
    }

    void Start () {
        isVisibleObj(board,false);
        hintForTest.SetActive(false);
        Notification.SetActive(false);
        ActivationTestObject.SetActive(false);
        teleport = Player.instance.teleportController.gameObject;

    }

    private void Update()
    {
        if(isPlayingGame)
        {
            teleport.SetActive(false);
        }
        else
        {
            teleport.SetActive(true); 
        }
    }
    public void initBoard()
    {
        isVisibleObj(board,true);
        a = board.transform.findChildRecursively("A").gameObject;
        c = board.transform.findChildRecursively("C").gameObject;
        d = board.transform.findChildRecursively("D").gameObject;
        b = board.transform.findChildRecursively("B").gameObject;
        content = board.transform.findChildRecursively("Content").gameObject;

        maxQ = gameObject.transform.findChildRecursively("TotalQuestion").gameObject;
        scoreUI = gameObject.transform.findChildRecursively("Score").gameObject;
        curQ = gameObject.transform.findChildRecursively("CurrentQuestion").gameObject;
        maxQuestion = FBGameData.instance.getClassData("Question").objects.Count;

        maxQ.GetComponent<TextMesh>().text = "Total question : " + maxQuestion/2;       
        cubeImage = board.transform.findChildRecursively("cube-image").gameObject;
        cubeImage.SetActive(false);
        loadQuestion(currentQuestion);
    }

    public void loadQuestion(int ques)
    {
        FBClassData Questions = FBGameData.instance.getClassData("Question");
        string cnt = null; 
        if (Questions.getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques)) !=null)
        {
            cnt = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques)).getFieldValue("Content").stringValue;
        }else if (Questions.getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques+"-img")) !=null)
        {
            cnt = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques + "-img")).getFieldValue("Content").stringValue;
        }
        content.GetComponent<TextMesh>().text = cnt;
        string questionID = null;
        if (Questions.getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques)) != null)
        {
            questionID = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques)).getFieldValue("QuestionID").stringValue;
            a.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques)).getFieldValue("Option1").stringValue;
            b.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques)).getFieldValue("Option2").stringValue;
            c.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques)).getFieldValue("Option3").stringValue;
            d.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques)).getFieldValue("Option4").stringValue;
        }
        else if (Questions.getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques + "-img")) !=null)
        {
            questionID = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques + "-img")).getFieldValue("QuestionID").stringValue;
            a.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques + "-img")).getFieldValue("Option1").stringValue;
            b.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques + "-img")).getFieldValue("Option2").stringValue;
            c.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques + "-img")).getFieldValue("Option3").stringValue;
            d.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques + "-img")).getFieldValue("Option4").stringValue;
        }
        if(questionID.Contains("-img"))
        {
            if (MediaManager.instance.dicImages.Count == 0)
            {
                MediaManager.instance.addTexture2Dic();
            }
            cubeImage.SetActive(true);
            cubeImage.GetComponent<MeshRenderer>().material.mainTexture = MediaManager.instance.dicImages[questionID];
        }
       
        updateUI();
    }
  
    public string getAnswer(int ques)
    {
        string answer = FBGameData.instance.getClassData("Question").getObject("QuestionID", new FBValue(FBDataType.String, "bt-question" + ques)).getFieldValue("Answer").stringValue;
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
        curQ.GetComponent<TextMesh>().text = "Current question : " + currentQuestion;
        scoreUI.GetComponent<TextMesh>().text = "Score : " + score;
    }

    public void isVisibleObj(GameObject obj ,bool value)
    {
        foreach(Transform child in obj.transform)
        {
            child.gameObject.SetActive(value);
        }
    }

    public void finishMultipleTest()
    {
        currentQuestion = 1;
        score = 0;
    }

    public void displayHintTest()
    {
        hintForTest.SetActive(true);
        Invoke("disableHintTest", 5);
    }

    void disableHintTest()
    {
        hintForTest.SetActive(false);
    }

    public void doLeftRightStatueMove()
    {
        statue_left.transform.DOMoveX(statue_left.transform.position.x - 20, 0.5f);
        statue_right.transform.DOMoveX(statue_right.transform.position.x + 20, 0.5f);
        StartCoroutine(doStatuesMove(statue_left, statue_right));
    }

    IEnumerator doStatuesMove(GameObject obj1, GameObject obj2)
    {
        yield return new WaitForSeconds(0.5f);
        obj1.transform.DOMoveX(obj1.transform.position.x + 20, 0.5f);
        obj2.transform.DOMoveX(obj2.transform.position.x - 20, 0.5f);
    }
}
