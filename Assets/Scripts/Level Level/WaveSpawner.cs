using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class WaveSpawner : MonoBehaviour
{

    public static WaveSpawner main;

    [Header("Attributes")]
    [SerializeField] public int numberOfWaves = 15;
    [SerializeField] private MobStruct[] wave1;
    [SerializeField] private MobStruct[] wave2;
    [SerializeField] private MobStruct[] wave3;
    [SerializeField] private MobStruct[] wave4;
    [SerializeField] private MobStruct[] wave5;
    [SerializeField] private MobStruct[] wave6;
    [SerializeField] private MobStruct[] wave7;
    [SerializeField] private MobStruct[] wave8;
    [SerializeField] private MobStruct[] wave9;
    [SerializeField] private MobStruct[] wave10;
    [SerializeField] private MobStruct[] wave11;
    [SerializeField] private MobStruct[] wave12;
    [SerializeField] private MobStruct[] wave13;
    [SerializeField] private MobStruct[] wave14;
    [SerializeField] private MobStruct[] wave15;
    [SerializeField] private MobStruct[] wave16;
    [SerializeField] private MobStruct[] wave17;
    [SerializeField] private MobStruct[] wave18;
    [SerializeField] private MobStruct[] wave19;
    [SerializeField] private MobStruct[] wave20;
    [SerializeField] private MobStruct[] wave21;
    [SerializeField] private MobStruct[] wave22;
    [SerializeField] private MobStruct[] wave23;
    [SerializeField] private MobStruct[] wave24;
    [SerializeField] private MobStruct[] wave25;
    [SerializeField] private MobStruct[] wave26;
    [SerializeField] private MobStruct[] wave27;
    [SerializeField] private MobStruct[] wave28;
    [SerializeField] private MobStruct[] wave29;
    [SerializeField] private MobStruct[] wave30;
    [SerializeField] private MobStruct[] wave31;
    [SerializeField] private MobStruct[] wave32;
    [SerializeField] private MobStruct[] wave33;
    [SerializeField] private MobStruct[] wave34;
    [SerializeField] private MobStruct[] wave35;
    [SerializeField] private MobStruct[] wave36;
    [SerializeField] private MobStruct[] wave37;
    [SerializeField] private MobStruct[] wave38;
    [SerializeField] private MobStruct[] wave39;
    [SerializeField] private MobStruct[] wave40;
    [SerializeField] private MobStruct[] wave41;
    [SerializeField] private MobStruct[] wave42;
    [SerializeField] private MobStruct[] wave43;
    [SerializeField] private MobStruct[] wave44;
    [SerializeField] private MobStruct[] wave45;
    [SerializeField] private MobStruct[] wave46;
    [SerializeField] private MobStruct[] wave47;
    [SerializeField] private MobStruct[] wave48;
    [SerializeField] private MobStruct[] wave49;
    [SerializeField] private MobStruct[] wave50;

    [Header("Trackers")]
    private MobStruct[][] enemySpawns;
    public int currentWave = 0;
    private MobStruct[][] enemiesToSpawnThisWave;
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
        waveCountdown = GlobalValues.main.startingWaveCountdown;
        waveSpawnRatio = GlobalValues.main.waveSpawnRatio;
        timeBetweenWaves = GlobalValues.main.waveLength;
        enemySpawns = new MobStruct[][] {wave1, wave2, wave3, wave4, wave5, wave6, wave7, wave8, wave9, wave10, wave11, wave12, wave13, wave14, wave15, wave16, wave17, wave18, wave19, wave20, wave21, wave22, wave23, wave24, wave25, wave26, wave27, wave28, wave29, wave30, wave31, wave32, wave33, wave34, wave35, wave36, wave37, wave38, wave39, wave40, wave41, wave42, wave43, wave44, wave45, wave46, wave47, wave48, wave49, wave50};
        Array.Resize(ref timeSinceLastSpawn, LevelManager.main.numberOfPaths);
        Array.Resize(ref enemiesLeftToSpawn, LevelManager.main.numberOfPaths);
        Array.Resize(ref spawnIndex, LevelManager.main.numberOfPaths);
        Array.Resize(ref enemiesPerSecond, LevelManager.main.numberOfPaths);
        Array.Resize(ref pathIsSpawning, LevelManager.main.numberOfPaths);
        Array.Resize(ref enemiesToSpawnThisWave, LevelManager.main.numberOfPaths);
    }

    private void Update()
    {
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
        LevelManager.main.OrganizeBees();
        if (currentWave >= numberOfWaves)
        {
            LevelManager.main.finalWave = true;
            finalWave = true;
            waveCountdown = 0f;
        }
        else
        {
            currentWave++;
            waveCountdown = timeBetweenWaves;
            LevelManager.main.levelStarted = true;
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
        string prestige = spawn.prestige;
        int pathIndex = spawn.path - 1;
        Transform start = LevelManager.main.pathsStart[pathIndex];
        Transform nextPoint = LevelManager.main.pathsNextPoint[pathIndex];;
        spawnIndex[pathIndex]++;
        if (prefabToSpawn == null)
        {
            return;
        }
        float angle = Mathf.Atan2(nextPoint.position.y - start.position.y, nextPoint.position.x - start.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion enemyRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        GameObject enemy = Instantiate(prefabToSpawn, start.position, enemyRotation);
        enemy.GetComponent<Attributes>().prestige = prestige;
        enemy.GetComponent<Attributes>().onPath = pathIndex;
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
