using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MediaManager : MonoBehaviour {

    //List of images
    public List<Texture> listImages;
    public Dictionary<string,Texture> dicImages;
    static public MediaManager instance;

    private void Awake()
    {
        instance = this;
        dicImages = new Dictionary<string, Texture>();
    }
    private void Start()
    {
        addTexture2Dic();
    }

    void addTexture2Dic()
    {
        for (int i = 0; i < listImages.Count; i++)
        {
            dicImages.Add(listImages[i].name, listImages[i]);
            Debug.Log(dicImages[listImages[i].name].name);
        }
    }
    // Tool : Object - NameImage
    public void initImage(GameObject obj)
    {
        if(dicImages.Count == 0)
        {
            addTexture2Dic();
        }
        FBClassData imageObject = FBGameData.instance.getClassData("ImageUIObject");
        FBClassData textObject = FBGameData.instance.getClassData("TextUIObject");
        string imgName = imageObject.getObject("ObjectName", new FBValue(FBDataType.String, obj.name)).getFieldValue("ImageID").stringValue;
        
        
        
        //string content = textObject.getObject("TextID", new FBValue(FBDataType.String, imgName)).getFieldValue("Content").stringValue;
        if(dicImages.ContainsKey(imgName))
        {
           // obj.GetComponent<Renderer>().material.SetTexture(dicImages[imgName].name, dicImages[imgName]);
            obj.GetComponent<MeshRenderer>().material.mainTexture = dicImages[imgName];
        }
    //    obj.transform.findChildRecursively("TextPos").gameObject.GetComponent<Text>().text = content;
    }

}
