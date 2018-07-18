using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class testSwipeColor : MonoBehaviour
    {
        public static testSwipeColor instance;
        public GameObject UI;
        public GameObject spawnerBalloons;
        public int currentColor = 0;
        public int currentQuestion = 1;
        public Dictionary<string, GameObject> balloonIns;
        public int score = 0;
        public int maxQuestion = 0;
        public FBClassData classQuestion = null;
        public GameObject countDown;
        public GameObject scoreOBJ;
        public GameObject questionNum;
        int countdown = 50;
        private void Awake()
        {
            instance = this;
            balloonIns = new Dictionary<string, GameObject>();
        }
        public void loadQuestion(int curQues)
        {
            FBClassData Questions = FBGameData.instance.getClassData("Question");
            gameObject.transform.findChildRecursively("Content").gameObject.GetComponent<TextMesh>().text = Questions.getObject("QuestionID", new FBValue(FBDataType.String, "yte-question" + curQues)).getFieldValue("Content").stringValue;
            if (balloonIns.ContainsKey("A"))
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
            updateUI();
        }
        IEnumerator Minus()
        {
            yield return new WaitForSeconds(1);
            countdown--;
            countDown.GetComponent<Text>().text = countdown.ToString();
            if (countdown >= 0)
            {
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
        public void ActiveGame()
        {
        SoundResonanceManager.instance.pauseAllSoundOnObj(MainSceneScript.instance.musicObject);
        currentQuestion = 1;
            score = 0;
            countdown = 50;
            spawnerBalloons.SetActive(true);
            currentColor = 0;
            loadQuestion(currentQuestion);
            maxQuestion = FBGameData.instance.getClassData("Question").objects.Count;
            classQuestion = FBGameData.instance.getClassData("Question");
            StartCoroutine(Minus());
        }
        public void UnActiveGame()
        {
            gameObject.transform.findChildRecursively("Content").gameObject.GetComponent<TextMesh>().text = "CONGRATULATIONS !!! YOU HAVE ACHIEVED :"+"  "+ score.ToString() + " SCORE ." + "\n" + " PRESS HOME BUTTON TO OUT GAME ";
            foreach (var obj in balloonIns)
            {
                Destroy(obj.Value);
            }

            SoundResonanceManager.instance.resumeSound(MainSceneScript.instance.musicObject);

        }
        void updateUI()
        {
            scoreOBJ.GetComponent<Text>().text = score.ToString();
            questionNum.GetComponent<Text>().text = currentQuestion.ToString();

        }
}
