using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class WaveDataLoader : MonoBehaviour
{
    public static WaveDataLoader main;

    [System.Serializable]
    public class WaveData
    {
        public WaveImport[][] mobImports;
        public string[][] waveTags;
        public string[] waveDescriptions;
    }

    [System.Serializable]
    public class WaveImport
    {
        public string mob;
        public VariantType variant;
        public Prest[] prestiges;
        public int path;
    }

    public static WaveData waveData;
    public static WaveImport waveImport;
    private static MobStruct[][] waveMobStruct = new MobStruct[][] { };
    
    private void Awake()
    {
        main = this;
    }

    public void Load(TextAsset waveFile)
    {
        if (waveFile == null)
        {
            Debug.Log("Wave Data Missing.");
        }
        waveData = JsonConvert.DeserializeObject<WaveData>(waveFile.text);
        ParseWaveData();
        WaveSpawner.main.enemySpawns = waveMobStruct;
        WaveSpawner.main.waveTags = waveData.waveTags;
        WaveSpawner.main.waveDescriptions = waveData.waveDescriptions;
    }

    private void ParseWaveData()
    {
        Array.Resize(ref waveMobStruct, waveData.mobImports.Length);
        for (int i1 = 0; i1 < waveData.mobImports.Length; i1++)
        {
            Array.Resize(ref waveMobStruct[i1], waveData.mobImports[i1].Length);
            for (int i2 = 0; i2 < waveData.mobImports[i1].Length; i2++)
            {
                waveMobStruct[i1][i2] = new MobStruct(FindPrefab(waveData.mobImports[i1][i2].mob), waveData.mobImports[i1][i2].variant, waveData.mobImports[i1][i2].prestiges, waveData.mobImports[i1][i2].path);
            }
        }
    }

    private GameObject FindPrefab(string prefabName)
    {
        int i = Array.IndexOf(GlobalValues.main.spawnPrefabReferences, prefabName);
        if (i == -1)
        {
            Debug.Log("No prefab found for " + prefabName);
        }
        GameObject prefab = GlobalValues.main.spawnPrefabs[i];
        return prefab;
    }
}


