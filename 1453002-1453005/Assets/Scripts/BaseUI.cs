using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DoozyUI;
using UnityEngine.Video;
public class BaseUI : MonoBehaviour {
    public static BaseUI instance;
    string textPosName = "TextPos";
    string clipPosName = "ClipPos";
    string picPosName = "PicPos";
    bool isPlaying1 = false;
    bool isPlaying2 = false;
    [HideInInspector]
    public Dictionary<string, VideoClip> VideoClips;

    public List<VideoClip> listClips;

    private void Awake()
    {
        instance = this;

        VideoClips = new Dictionary<string, VideoClip>();
        for (int i = 0; i < listClips.Count; i++)
            VideoClips.Add(listClips[i].name, listClips[i]);
    }

    public void ShowTextObject(string textID, GameObject obj)
    {        
        FBClassData TextObject = FBGameData.instance.getClassData("TextUIObject");
        FBClassObject objTmp = TextObject.getObject("TextID", new FBValue(FBDataType.String, textID));
        string name = TextObject.getObject("TextID", new FBValue(FBDataType.String, textID)).getFieldValue("Name").stringValue;
        string culture = TextObject.getObject("TextID", new FBValue(FBDataType.String, textID)).getFieldValue("Culture").stringValue;
        string author = TextObject.getObject("TextID", new FBValue(FBDataType.String, textID)).getFieldValue("Author").stringValue;
        string content = TextObject.getObject("TextID", new FBValue(FBDataType.String, textID)).getFieldValue("Content").stringValue;

        obj.transform.findChildRecursively("Name").GetComponentInChildren<Text>().text = name;
        obj.transform.findChildRecursively("Culture").GetComponentInChildren<Text>().text = culture;
        obj.transform.findChildRecursively("Author").GetComponentInChildren<Text>().text = author;
        obj.transform.findChildRecursively("Content").GetComponentInChildren<Text>().text = content;
                
        obj.transform.findChildRecursively("UIManager").GetComponent<UIElement>().Show(false);
        
    }
    public void HideTextObject(GameObject obj) {
        obj.transform.Find(textPosName).GetComponentInChildren<UIElement>().Hide(false);
    }
    public void PlayVideo(GameObject obj)
    {
        if (obj.name == "Desease-Video1")
        {
            if (isPlaying1 == false)
            {
                obj.GetComponent<VideoPlayer>().Play();
                obj.GetComponent<AudioSource>().Play();
                isPlaying1 = true;
            }
            else
            {
                obj.GetComponent<VideoPlayer>().Stop();
                obj.GetComponent<AudioSource>().Stop();
                isPlaying1 = false;
            }
        }
        else
        {
            if (isPlaying2 == false)
            {
                obj.GetComponent<VideoPlayer>().Play();
                obj.GetComponent<AudioSource>().Play();
                isPlaying2 = true;
            }
            else
            {
                obj.GetComponent<VideoPlayer>().Stop();
                obj.GetComponent<AudioSource>().Stop();
                isPlaying2 = false;
            }
        }
        
    }
    public void PauseVideo(GameObject obj) {
        obj.GetComponent<VideoPlayer>().Stop();
        obj.GetComponent<AudioSource>().Stop();
    }
    public void ShowTextOnClick(string content, Vector3 pos, float delaytime = 0)
    {
        GameObject temp = Resources.Load<GameObject>("Prefabs/RemoteText");
        temp.GetComponentInChildren<UIElement>().GetInAnimations.moveIn.delay = delaytime;
        temp.GetComponentInChildren<Text>().text = content;
        GameObject textShow = Instantiate(temp);
        textShow.transform.position = pos;
        GameObject camera = GameObject.Find("Player");
        textShow.transform.LookAt(camera.transform);
        StartCoroutine(DeleteText(textShow, 2));
    }
    public IEnumerator DeleteText(GameObject obj, float waitTime) {
        yield return new WaitForSeconds(waitTime);
        DestroyObject(obj.gameObject);
    }
    IEnumerator PlayProcess(VideoClip clip, GameObject obj)
    {
        RawImage image = obj.transform.Find(clipPosName).GetComponentInChildren<RawImage>();
        VideoPlayer videoPlayer = obj.transform.Find(clipPosName).GetComponentInChildren<VideoPlayer>();
        videoPlayer.clip = clip;
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(1);
            break;
        }
        image.texture = videoPlayer.texture;
        videoPlayer.Play();

    }

    public void ShowImageObject(GameObject obj , Sprite spr)
    {
        obj.transform.Find(picPosName).findChildRecursively("RawImage").GetComponent<Image>().sprite = spr;
        obj.transform.Find(picPosName).findChildRecursively("UIManager").GetComponent<UIElement>().Show(false);

    }
    public void HideImageObject(GameObject obj)
    {
        obj.transform.Find(picPosName).findChildRecursively("UIManager").GetComponent<UIElement>().Hide(false);
    }
}
