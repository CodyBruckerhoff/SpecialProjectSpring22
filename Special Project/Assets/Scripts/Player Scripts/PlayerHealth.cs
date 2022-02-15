using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //Gameplay Values
    [SerializeField] private float playerHealthTotal;
    [SerializeField] private float playerArmor;
    [SerializeField] private float playerHealthCurrent;

    //UI Elements
    [SerializeField] private TextMeshProUGUI healthDisplay;
    public Slider slider;

    private void Awake()
    {
        playerHealthCurrent = playerHealthTotal;
    }

    private void Update()
    {
        if(healthDisplay != null)
        {
            healthDisplay.SetText(playerHealthCurrent + " / " + playerHealthTotal);
            slider.value = (playerHealthCurrent / playerHealthTotal);
        }
    }

    public void TakeDamage(float damage)
    {
        float resistance = 1 - (damage / 100);

        playerHealthCurrent -= damage * resistance;

        if (playerHealthCurrent <= 0)
            Invoke(nameof(GameOver), .5f);
    }

    public void RegendHealth(float health)
    {

    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
