using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject levelPassedPanel, restartPanel;
    public GameObject levelInput;
    public GameObject placeHolder;
    int notEasy=0;

    public void ShowLevelPassed()
    {
        levelPassedPanel.SetActive(true);
    }

    public void ShowRestartPanel()
    {
        restartPanel.SetActive(true);
    }

    public void ContinueButton()
    {
        int currentLevel = PlayerPrefs.GetInt("level");
        currentLevel++;
        PlayerPrefs.SetInt("level", currentLevel);
        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        string text = levelInput.GetComponent<TMP_InputField>().text;
        if(int.TryParse(text, out int level))
        {
            PlayerPrefs.SetInt("level", level);
        }
        SceneManager.LoadScene(0); 
    }

    public void cheatActive()
    {
        notEasy++;
        if (notEasy >= 4)
        {
            levelInput.SetActive(true);
            placeHolder.SetActive(false);
        }
    }
}

