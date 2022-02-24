using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindSceneChanger : MonoBehaviour
{


    public SceneChanger sceneChanger;
    private GameObject[] DontDestroyOnLoadObjects;
    public Button button;
    
    // Start is called before the first frame update
    void Awake()
    {

        //DontDestroyOnLoadObjects = GetDontDestroyOnLoadObjects();
        //sceneChanger = DontDestroyOnLoadObjects[0].GetComponent<SceneChanger>();
        //button.onClick.AddListener(sceneChanger.PreviousScene);
    }

    private void Update()
    {
        DontDestroyOnLoadObjects = GetDontDestroyOnLoadObjects();
        sceneChanger = DontDestroyOnLoadObjects[0].GetComponent<SceneChanger>();
        button.onClick.AddListener(sceneChanger.PreviousScene);
    }

    public static GameObject[] GetDontDestroyOnLoadObjects()
    {
        GameObject temp = null;
        try
        {
            temp = new GameObject();
            Object.DontDestroyOnLoad(temp);
            UnityEngine.SceneManagement.Scene dontDestroyOnLoad = temp.scene;
            Object.DestroyImmediate(temp);
            temp = null;

            return dontDestroyOnLoad.GetRootGameObjects();
        }
        finally
        {
            if (temp != null)
                Object.DestroyImmediate(temp);
        }
    }
}
