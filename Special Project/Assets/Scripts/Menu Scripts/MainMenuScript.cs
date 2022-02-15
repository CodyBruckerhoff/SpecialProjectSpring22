using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    private bool settingToggle = false;
    public GameObject settingMenu;

    private void Awake()
    {
        settingMenu.SetActive(settingToggle);
    }


    public void toggleSettings()
    {
        if (settingToggle)
        {
            settingToggle = false;
        }
        else
            settingToggle = true;

        settingMenu.SetActive(settingToggle);

    }
}
