using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public List<string> sceneHistory = new List<string>();  //running history of scenes
                                                            //The last string in the list is always the current scene running



    private void Awake()
    {
        SaveScene();
        //LoadScene(SceneManager.GetActiveScene().name);
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);  //Allow this object to persist between scene changes
        //LoadScene(SceneManager.GetActiveScene().name);
    }

    //Call this whenever you want to load a new scene
    //It will add the new scene to the sceneHistory list
    public void LoadScene(string newScene)
    {
        sceneHistory.Add(newScene);
        SceneManager.LoadScene(newScene);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }

    //Call this whenever you want to load the previous scene
    //It will remove the current scene from the history and then load the new last scene in the history
    //It will return false if we have not moved between scenes enough to have stored a previous scene in the history
    public void PreviousScene()
    {
        if (sceneHistory.Count >= 2)  //Checking that we have actually switched scenes enough to go back to a previous scene
        {
            sceneHistory.RemoveAt(sceneHistory.Count - 1);
            SceneManager.LoadScene(sceneHistory[sceneHistory.Count - 1]);
        }
    }
    public void SaveScene()
    {
        Debug.Log("Save main menu");
        PlayerPrefs.SetInt("SavedMenu", SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedMenu"));
        DestroyImmediate(this.gameObject, true);
    }

}
