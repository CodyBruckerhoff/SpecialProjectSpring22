using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //Gameplay Values
    public float playerHealthTotal;
    public float playerArmor;
    public float playerHealthCurrent;
    public float playerHealthRegen;

    // Health Regen System
    private float regenTimer;
    [SerializeField] private float regenTimerStart;

    //UI Elements
    [SerializeField] private TextMeshProUGUI healthDisplay;
    public Slider slider;

    //Scene Elements
    public SceneChanger sceneChanger;
    private GameObject[] DontDestroyOnLoadObjects;



    private void Awake()
    {
        playerHealthCurrent = playerHealthTotal;
        DontDestroyOnLoadObjects = GetDontDestroyOnLoadObjects();
        //sceneChanger = DontDestroyOnLoadObjects[0].GetComponent<SceneChanger>();
    }

    private void Update()
    {
        if(healthDisplay != null)
        {
            healthDisplay.SetText(playerHealthCurrent + " / " + playerHealthTotal);
            slider.value = (playerHealthCurrent / playerHealthTotal);
        }
        regenTimer -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (regenTimer <= 0 && playerHealthCurrent < playerHealthTotal)
            RegendHealth();
    }

    public void TakeDamage(float damage)
    {
        regenTimer = regenTimerStart;
        float resistance = 1 - (damage / 100);

        playerHealthCurrent -= damage * resistance;

        if (playerHealthCurrent <= 0)
            Invoke(nameof(GameOver), .5f);
    }

    public void RegendHealth()
    {
        playerHealthCurrent += playerHealthRegen;
        if (playerHealthCurrent > playerHealthTotal)
            playerHealthCurrent = playerHealthTotal;
    }

    private void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //SceneManager.LoadScene("GameOverScene");
        sceneChanger.LoadScene("GameOverScene");
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
