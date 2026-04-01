using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class WaveSpawner : MonoBehaviour
{

    public static WaveSpawner main;

    [Header("Trackers")]
    public MobStruct[][] enemySpawns;
    private int numberOfWaves;
    public int currentWave = 0;
    private MobStruct[][] enemiesToSpawnThisWave;
    public string[][] waveTags;
    public string[] waveDescriptions;
    private float[] timeSinceLastSpawn;
    private int[] enemiesLeftToSpawn;
    private int[] spawnIndex;
    private float[] enemiesPerSecond;
    public int enemiesAlive;
    private bool isSpawning = false;
    private bool[] pathIsSpawning;
    private float waveSpawnRatio;
    private float timeBetweenWaves;
    public float waveCountdown = 10f;
    private bool finalWave = false;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        numberOfWaves = LevelManager.main.numberOfWaves;
        waveCountdown = GlobalValues.main.startingWaveCountdown;
        waveSpawnRatio = GlobalValues.main.waveSpawnRatio;
        timeBetweenWaves = GlobalValues.main.waveLength;
        //enemySpawns = new MobStruct[][] {LevelManager.main.wave1, LevelManager.main.wave2, LevelManager.main.wave3, LevelManager.main.wave4, LevelManager.main.wave5, LevelManager.main.wave6, LevelManager.main.wave7, LevelManager.main.wave8, LevelManager.main.wave9, LevelManager.main.wave10, LevelManager.main.wave11, LevelManager.main.wave12, LevelManager.main.wave13, LevelManager.main.wave14, LevelManager.main.wave15, LevelManager.main.wave16, LevelManager.main.wave17, LevelManager.main.wave18, LevelManager.main.wave19, LevelManager.main.wave20, LevelManager.main.wave21, LevelManager.main.wave22, LevelManager.main.wave23, LevelManager.main.wave24, LevelManager.main.wave25, LevelManager.main.wave26, LevelManager.main.wave27, LevelManager.main.wave28, LevelManager.main.wave29, LevelManager.main.wave30, LevelManager.main.wave31, LevelManager.main.wave32, LevelManager.main.wave33, LevelManager.main.wave34, LevelManager.main.wave35, LevelManager.main.wave36, LevelManager.main.wave37, LevelManager.main.wave38, LevelManager.main.wave39, LevelManager.main.wave40, LevelManager.main.wave41, LevelManager.main.wave42, LevelManager.main.wave43, LevelManager.main.wave44, LevelManager.main.wave45, LevelManager.main.wave46, LevelManager.main.wave47, LevelManager.main.wave48, LevelManager.main.wave49, LevelManager.main.wave50};
        WaveDataLoader.main.Load(LevelManager.main.waveFile);
        Array.Resize(ref timeSinceLastSpawn, LevelManager.main.numberOfPaths);
        Array.Resize(ref enemiesLeftToSpawn, LevelManager.main.numberOfPaths);
        Array.Resize(ref spawnIndex, LevelManager.main.numberOfPaths);
        Array.Resize(ref enemiesPerSecond, LevelManager.main.numberOfPaths);
        Array.Resize(ref pathIsSpawning, LevelManager.main.numberOfPaths);
        Array.Resize(ref enemiesToSpawnThisWave, LevelManager.main.numberOfPaths);
    }

    private void Update()
    {
        if (LevelManager.main.state == Level.VICTORY || LevelManager.main.state == Level.DEFEAT)
        {
            return;
        }
        if (finalWave == false)
         {
             waveCountdown -= Time.deltaTime;
             if (waveCountdown <= 0)
             {
                 EndWave();
             }
         }
         else if (finalWave == true && enemiesAlive == 0 && isSpawning == false)
         {
             LevelManager.main.EndLevel();
         }
         if (!isSpawning)
         {
             return;
         }
         for (int i = 0; i < LevelManager.main.numberOfPaths; i++)
         {
            timeSinceLastSpawn[i] += Time.deltaTime;
            if (timeSinceLastSpawn[i] >= (1f / enemiesPerSecond[i]) && enemiesLeftToSpawn[i] > 0)
            {
                enemiesLeftToSpawn[i]--;
                timeSinceLastSpawn[i] = 0f;
                SpawnEnemy(enemiesToSpawnThisWave[i][spawnIndex[i]]);
            }
         }
    }

    private void StartWave()
    {
        LevelManager.main.Interest();
        SoundManager.main.PlaySound(LevelManager.main.waveStartSound);
        LevelManager.main.OrganizeBees();
        if (currentWave >= numberOfWaves)
        {
            return;
        }
        else 
        {
            currentWave++;
            waveCountdown = timeBetweenWaves;
            LevelManager.main.state = Level.STARTED;
            UIManager.main.WaveUpdate();
            isSpawning = true;
            for (int i = 0; i < LevelManager.main.numberOfPaths; i++)
            {
                enemiesToSpawnThisWave[i] = new MobStruct[] { };
                spawnIndex[i] = 0;
                for (int x = 0; x < enemySpawns[currentWave - 1].Length; x++)
                {
                    if (enemySpawns[currentWave - 1][x].path == i + 1)
                    {
                        Array.Resize(ref enemiesToSpawnThisWave[i], enemiesToSpawnThisWave[i].Length + 1);
                        enemiesToSpawnThisWave[i][enemiesToSpawnThisWave[i].Length - 1] = enemySpawns[currentWave - 1][x];
                    }
                }
                enemiesLeftToSpawn[i] = enemiesToSpawnThisWave[i].Length;
                enemiesPerSecond[i] = (enemiesLeftToSpawn[i]) / (timeBetweenWaves * waveSpawnRatio);
                pathIsSpawning[i] = true;
            }
        }
        if (currentWave == numberOfWaves && finalWave == false)
        {
            LevelManager.main.state = Level.FINALWAVE;
            finalWave = true;
            waveCountdown = 0f;
        }
    }

    private void EndWave()
    {
        if (currentWave <= numberOfWaves)
        {
            StartWave();
        }
    }

    private void SpawnEnemy(MobStruct spawn)
    {
        GameObject prefabToSpawn = spawn.prefab;;
        Prest[] prestiges = spawn.prestiges;
        VariantType variant = spawn.variant;
        int pathIndex = spawn.path - 1;
        Transform start = LevelManager.main.pathsStart[pathIndex];
        Transform nextPoint = LevelManager.main.pathsNextPoint[pathIndex];;
        spawnIndex[pathIndex]++;
        if (prefabToSpawn == null)
        {
            return;
        }
        Quaternion enemyRotation;
        if (prefabToSpawn.GetComponent<Attributes>().rotationSpeed > 0)
        {
            float angle = Mathf.Atan2(nextPoint.position.y - start.position.y, nextPoint.position.x - start.position.x) * Mathf.Rad2Deg - 90f;
            enemyRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
        else
        {
            enemyRotation = Quaternion.identity;
        }
        prefabToSpawn.GetComponent<Attributes>().variant = variant;
        prefabToSpawn.GetComponent<Attributes>().prestiges = prestiges;
        prefabToSpawn.GetComponent<Attributes>().onPath = pathIndex;
        GameObject enemy = Instantiate(prefabToSpawn, start.position, enemyRotation);
        if (enemiesLeftToSpawn[pathIndex] <= 0)
        {
            pathIsSpawning[pathIndex] = false;
            if (Array.IndexOf(pathIsSpawning, true) == -1)
            {
                isSpawning = false;
            }
        }
        
    }

    public void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    public void EnemySpawned()
    {
        enemiesAlive++;
    }
}
