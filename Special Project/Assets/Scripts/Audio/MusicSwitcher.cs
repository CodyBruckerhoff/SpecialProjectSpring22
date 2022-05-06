using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSwitcher : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu" || SceneManager.GetActiveScene().name == "End Credits")
        {
            FindObjectOfType<AudioManager>().Stop("Music2");
            FindObjectOfType<AudioManager>().Stop("Music3");
            FindObjectOfType<AudioManager>().Play("Music1");
        }
        else if (SceneManager.GetActiveScene().name == "TerrainTester" || SceneManager.GetActiveScene().name == "Final Level")
        {
            FindObjectOfType<AudioManager>().Stop("Music1");
            FindObjectOfType<AudioManager>().Stop("Music3");
            FindObjectOfType<AudioManager>().Play("Music2");
        }
        else
        {
            FindObjectOfType<AudioManager>().Stop("Music2");
            FindObjectOfType<AudioManager>().Stop("Music1");
            FindObjectOfType<AudioManager>().Play("Music3");
        }
    }
}
