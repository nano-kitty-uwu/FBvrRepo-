using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public List<Image> hearts; 
    private int currentHealth;

    void Start()
    {
        currentHealth = hearts.Count; 
    }

    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            hearts[currentHealth].enabled = false; 

            if (currentHealth <= 0)
            {
                Debug.Log("Game Over! Reloading scene...");
                ReloadScene();
            }
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}