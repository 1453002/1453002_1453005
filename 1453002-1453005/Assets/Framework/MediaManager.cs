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
        if(this.gameObject.scene.name != "Baked_MuseumVR_vol1")
            addTexture2Dic();
    }

    public void addTexture2Dic()
    {
        for (int i = 0; i < listImages.Count; i++)
        {
            dicImages.Add(listImages[i].name, listImages[i]);
           
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
        if(dicImages.ContainsKey(imgName))
        {
            obj.GetComponent<MeshRenderer>().material.mainTexture = dicImages[imgName];
        }
    }


   
}
