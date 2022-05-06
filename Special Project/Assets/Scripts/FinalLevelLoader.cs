using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private SceneChanger sceneChanger;
    private GameObject[] DontDestroyOnLoadObjects;


    private void Start()
    {
        DontDestroyOnLoadObjects = GetDontDestroyOnLoadObjects();
        sceneChanger = returnObjectFromArray(DontDestroyOnLoadObjects, "SceneManager").GetComponent<SceneChanger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = new Vector3(957.34f, 379.97f, 103.4f);
            sceneChanger.LoadScene(sceneName);
        }
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

    public static GameObject returnObjectFromArray(GameObject[] array, string tagName)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].gameObject.tag == tagName)
            {
                return array[i];
            }
        }

        return null;
    }
}
