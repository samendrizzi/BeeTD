using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GlobalMap : MonoBehaviour
{

    public static GlobalMap main;

    [SerializeField] private GameObject levelDescriptionUI;
    [SerializeField] public TextMeshProUGUI levelDescription;
    [SerializeField] private TextMeshProUGUI difficultySetting;

    public string levelSelect = "Level 1";

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        VerifySaveFile();
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Global Map");
    }

    public void StartLevel()
    {
        GlobalValues.main.SetDifficulty(difficultySetting.text);
        SceneManager.LoadScene(levelSelect, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Global Map");
    }

    public void ChangeLevel(int level)
    {
        levelSelect = "Level " + level.ToString();
        levelDescription.text = levelSelect + "\n\n High Score (Easy) = " + SaveFile.main.easyScores[level-1] + "\n High Score (Medium) = " + SaveFile.main.mediumScores[level - 1] + "\n High Score (Hard) = " + SaveFile.main.hardScores[level - 1];
        GlobalValues.main.levelIndex = level - 1;
        if (level == 0)
        {
            levelDescriptionUI.SetActive(false);
        }
        else
        {
            levelDescriptionUI.SetActive(true);
        }
    }

    public void VerifySaveFile()
    {
        int levelCount = 0;
        GameObject[] root = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in root)
        {
            if (obj.name == "Level")
            {
                levelCount++;
            }
        }

        if (SaveFile.main.easyScores.Length != levelCount)
        {
            Array.Resize(ref SaveFile.main.easyScores, levelCount);
            Debug.Log(SaveFile.main.easyScores.Length);
        }
        if (SaveFile.main.mediumScores.Length != levelCount)
        {
            Array.Resize(ref SaveFile.main.mediumScores, levelCount);
        }
        if (SaveFile.main.hardScores.Length != levelCount)
        {
            Array.Resize(ref SaveFile.main.hardScores, levelCount);
        }

        SaveFile.main.Save();
    }

}
