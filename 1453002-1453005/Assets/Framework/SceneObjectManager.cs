using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectManager : MonoBehaviour {

    public static SceneObjectManager instance;
    public GameObject TextCanvas, VideoCanvas;
    private void Awake()
    {
        instance = this;
    }   

    #region common  
    FBClassData sceneObject;
    List< FBClassObject> interactiveObjects;
    float timeLeft = 10;
    int startImage1 = 0;
    int startImage2 = 3;
    private void Update()
    {
        if (this.gameObject.name == "Showroom2_01")
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                GameObject disease1, disease2;
                disease1 = GameObject.Find("Disease1");
                disease2 = GameObject.Find("Disease2");
                int numImg1 = 1;
                int numImg2 = 2;
                if (startImage1 >= 3)
                    startImage1 = 0;
                if (startImage2 >= 6)
                    startImage2 = 3;
                numImg1 = startImage1 + 1;
                numImg2 = startImage2 + 1;
                disease1.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture = MediaManager.instance.dicImages["Hiv-info" + numImg1];
                disease1.transform.GetChild(1).GetComponent<MeshRenderer>().material.mainTexture = MediaManager.instance.dicImages["Hiv-info" + numImg2];
                disease1.transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = MediaManager.instance.dicImages["Hiv-cause" + numImg1];
                disease1.transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = MediaManager.instance.dicImages["Hiv-symptom" + numImg1];
                disease1.transform.GetChild(4).GetComponent<MeshRenderer>().material.mainTexture = MediaManager.instance.dicImages["Hiv-prevention" + numImg1];
                disease1.transform.GetChild(5).GetComponent<MeshRenderer>().material.mainTexture = MediaManager.instance.dicImages["Hiv-example" + numImg1];

                disease2.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture = MediaManager.instance.dicImages["Lung-info" + numImg1];
                disease2.transform.GetChild(1).GetComponent<MeshRenderer>().material.mainTexture = MediaManager.instance.dicImages["Lung-info" + numImg2];
                disease2.transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = MediaManager.instance.dicImages["Lung-cause" + numImg1];
                disease2.transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = MediaManager.instance.dicImages["Lung-symptom" + numImg1];
                disease2.transform.GetChild(4).GetComponent<MeshRenderer>().material.mainTexture = MediaManager.instance.dicImages["Lung-prevention" + numImg1];
                disease2.transform.GetChild(5).GetComponent<MeshRenderer>().material.mainTexture = MediaManager.instance.dicImages["Lung-example" + numImg1];




                startImage1 += 1;
                startImage2 += 1;
                timeLeft = 10;

            }
        }
    }
    public void initSceneInteractiveObjects(UnityEngine.SceneManagement.Scene scene, GameObject[] rootObjects = null)
    {
        if (rootObjects == null)
            rootObjects = scene.GetRootGameObjects();
        initInteractiveObjects(scene.name, rootObjects);     
        if(scene.name == "Showroom2_01")
        {
            for(int i = 0; i<rootObjects.Length; i++)
            {
                if(rootObjects[i].name.Contains("Disease1") || rootObjects[i].name.Contains("Disease2"))
                {
                    foreach(Transform child in rootObjects[i].transform)
                    {
                        if (!child.name.Contains("Video"))
                        {
                            MediaManager.instance.initImage(child.gameObject);

                        }
                    
                    }
                }
            }
        }
    }

    public void initInteractiveObjects(string sceneName, GameObject[] rootObjects)
    {
        sceneObject = FBGameData.instance.getClassData("UIObject");
        List<FBClassObject> interactiveObjects = sceneObject.getObjects("SceneName", new FBValue(sceneName));

        if (interactiveObjects.Count <= 0)
            return;

        Dictionary<string, SceneObject> objectDict = new Dictionary<string, SceneObject>();

        for(int i = 0; i < interactiveObjects.Count; i++)
        {
            string objectName = interactiveObjects[i].getFieldValue("ObjectName").stringValue;

            SceneObject scnObj = null;
            objectDict.TryGetValue(objectName, out scnObj);
            if(scnObj == null)
            {
                GameObject interactiveObject = null;
                for (int j = 0; j < rootObjects.Length; j++)
                {
                    Transform transform;
                    if (rootObjects[j].name == objectName)
                        transform = rootObjects[j].transform;
                    else
                        transform = rootObjects[j].transform.findChildRecursively(objectName);
                    if (transform)
                    {
                        interactiveObject = transform.gameObject;
                        break;
                    }
                }
                if (interactiveObject)
                {                   
                    scnObj = interactiveObject.addMissingComponent<SceneObject>();
                    //scnObj.gameObject.addMissingComponent<BoxCollider>();                   
                    Transform TextPos = scnObj.transform.findChildRecursively("TextPos");
                    Transform ClipPos = scnObj.transform.findChildRecursively("ClipPos");
                    if (TextPos)
                    {
                        GameObject temp = TextCanvas;
                        GameObject createdObject = Instantiate(temp,TextPos);
                        createdObject.transform.position = TextPos.position;
                      
                    }
                    if(ClipPos)
                    {
                        GameObject videoMarker = VideoCanvas;
                        GameObject clipCanvas = Instantiate(videoMarker, ClipPos);
                        clipCanvas.transform.position = ClipPos.position;
                    }                  
                    objectDict[objectName] = scnObj;
                }
            }
            if (scnObj)
            {
                // set trigger and action
                SceneObject.instance.addEvent(interactiveObjects[i].getFieldValue("Trigger1").intValue, interactiveObjects[i].getFieldValue("Trigger2").intValue, interactiveObjects[i].getFieldValue("Trigger3").intValue,
                                              interactiveObjects[i].getFieldValue("Action1").intValue, interactiveObjects[i].getFieldValue("Action2").intValue, interactiveObjects[i].getFieldValue("Action3").intValue,
                                              interactiveObjects[i].getFieldValue("Param1").stringValue, interactiveObjects[i].getFieldValue("Param2").stringValue, interactiveObjects[i].getFieldValue("Param3").stringValue);
              
                
            }
        }
    }
    
    #endregion
}
