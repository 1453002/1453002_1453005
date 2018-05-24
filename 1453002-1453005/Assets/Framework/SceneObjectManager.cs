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

    public void initSceneInteractiveObjects(UnityEngine.SceneManagement.Scene scene, GameObject[] rootObjects = null)
    {
        if (rootObjects == null)
            rootObjects = scene.GetRootGameObjects();
        initInteractiveObjects(scene.name, rootObjects);     
        if(scene.name == "Showroom2_01")
        {
            for(int i = 0; i<rootObjects.Length; i++)
            {
                if(rootObjects[i].name.Contains("-image"))
                {
                    MediaManager.instance.initImage(rootObjects[i].gameObject);
                }
                if(rootObjects[i].name.Contains("Benh1") || rootObjects[i].name.Contains("Benh2"))
                {
                    foreach(Transform child in rootObjects[i].transform)
                    {
                        if(child.gameObject.name.Contains("-image"))
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
