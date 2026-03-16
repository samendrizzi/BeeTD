using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveFile : MonoBehaviour
{

    public static SaveFile main;

    public TextAsset saveFile;
    public float[] easyScores;
    public float[] easyUnlocks;
    public float[] mediumScores;
    public float[] mediumUnlocks;
    public float[] hardScores;
    public float[] hardUnlocks;

    [System.Serializable]
    public class GameData
    {
        public float[] easyScores;
        public float[] easyUnlocks;
        public float[] mediumScores;
        public float[] mediumUnlocks;
        public float[] hardScores;
        public float[] hardUnlocks;
    }

    public static GameData gameData;
    
    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        Load();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            Save();
        }
    }

    public void Save()
    {
        gameData.easyScores = easyScores;
        gameData.easyUnlocks = easyUnlocks;
        gameData.mediumScores = mediumScores;
        gameData.mediumUnlocks = mediumUnlocks;
        gameData.hardScores = hardScores;
        gameData.hardUnlocks = hardUnlocks;

        string strOutput = JsonUtility.ToJson(gameData);
        File.WriteAllText(Application.dataPath + "/Save.txt", strOutput);
    }

    public void Load()
    {
        if (saveFile == null)
        {
            Save();
        }
        gameData = JsonUtility.FromJson<GameData>(saveFile.text);
        easyScores = gameData.easyScores;
        easyUnlocks = gameData.easyUnlocks;
        mediumScores = gameData.mediumScores;
        mediumUnlocks = gameData.mediumUnlocks;
        hardScores = gameData.hardScores;
        hardUnlocks = gameData.hardUnlocks;
    }
}


