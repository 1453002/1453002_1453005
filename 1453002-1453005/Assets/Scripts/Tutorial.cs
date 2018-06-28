using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour {

    // Use this for initialization
    public List<GameObject> tutorial=new List<GameObject>();
    string tmp1, tmp2, tmp3, tmp4, tmp5, tmp6,tmp7;
    string findNameObject;
    string showString;
    int count = 2;
    void Start () {
        tmp1 = "1./ Hover ground and press center button to move for both scenes   ";
        tmp2 = "2./ Double click to hold moveable objects ";
        tmp3 = "3./ Rotate controller to rotate object when holding ";
        tmp4 = "4./ Game 1 on ground : double click to hold bones move to suitable place at picture";
        tmp5 = "5./ Game 2 on floor  : press center to shoot the arrow and get score ";
        tmp6 = "6./ Press app button to out game and get down to first floor";
        for (int i = 0; i < tutorial.Count; i++)
        {
            StartCoroutine(ShowTutorial(tutorial[i],count));
            count++;
        }
        StartCoroutine(Turnoff());
    }

    IEnumerator ShowTutorial(GameObject temp, int count) {
        yield return new WaitForSeconds(count);
        char lastLetter = temp.gameObject.name[temp.gameObject.name.Length - 1];
        
        if (int.Parse(lastLetter.ToString()) == 1)
        {
            temp.GetComponent<Text>().text = tmp1;
        }
        if (int.Parse(lastLetter.ToString()) == 2)
        {
            temp.GetComponent<Text>().text = tmp2;
        }
        if (int.Parse(lastLetter.ToString()) == 3)
        {
            temp.GetComponent<Text>().text = tmp3;
        }
        if (int.Parse(lastLetter.ToString()) == 4)
        {
            temp.GetComponent<Text>().text = tmp4;
        }
        if (int.Parse(lastLetter.ToString()) == 5)
        {
            temp.GetComponent<Text>().text = tmp5;
        }
        if (int.Parse(lastLetter.ToString()) == 6)
        {
            temp.GetComponent<Text>().text = tmp6;
        }
    }
    IEnumerator Turnoff() {
        yield return new WaitForSeconds(15);
        this.gameObject.SetActive(false);
    }
}
