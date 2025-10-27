using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class WaveSpawner : MonoBehaviour
{

    public static WaveSpawner main;

    [SerializeField] private GameObject[][] enemySpawnsPath1;
    [SerializeField] private GameObject[] wave1Path1;
    [SerializeField] private GameObject[] wave2Path1;
    [SerializeField] private GameObject[] wave3Path1;
    [SerializeField] private GameObject[] wave4Path1;
    [SerializeField] private GameObject[] wave5Path1;
    [SerializeField] private GameObject[] wave6Path1;
    [SerializeField] private GameObject[] wave7Path1;
    [SerializeField] private GameObject[] wave8Path1;
    [SerializeField] private GameObject[] wave9Path1;
    [SerializeField] private GameObject[] wave10Path1;
    [SerializeField] private GameObject[] wave11Path1;
    [SerializeField] private GameObject[] wave12Path1;
    [SerializeField] private GameObject[] wave13Path1;
    [SerializeField] private GameObject[] wave14Path1;
    [SerializeField] private GameObject[] wave15Path1;
    [SerializeField] private GameObject[] wave16Path1;
    [SerializeField] private GameObject[] wave17Path1;
    [SerializeField] private GameObject[] wave18Path1;
    [SerializeField] private GameObject[] wave19Path1;
    [SerializeField] private GameObject[] wave20Path1;
    [SerializeField] private GameObject[] wave21Path1;
    [SerializeField] private GameObject[] wave22Path1;
    [SerializeField] private GameObject[] wave23Path1;
    [SerializeField] private GameObject[] wave24Path1;
    [SerializeField] private GameObject[] wave25Path1;
    [SerializeField] private GameObject[] wave26Path1;
    [SerializeField] private GameObject[] wave27Path1;
    [SerializeField] private GameObject[] wave28Path1;
    [SerializeField] private GameObject[] wave29Path1;
    [SerializeField] private GameObject[] wave30Path1;
    //[SerializeField] private GameObject[] wave31Path1;
    //[SerializeField] private GameObject[] wave32Path1;
    //[SerializeField] private GameObject[] wave33Path1;
    //[SerializeField] private GameObject[] wave34Path1;
    //[SerializeField] private GameObject[] wave35Path1;
    //[SerializeField] private GameObject[] wave36Path1;
    //[SerializeField] private GameObject[] wave37Path1;
    //[SerializeField] private GameObject[] wave38Path1;
    //[SerializeField] private GameObject[] wave39Path1;
    //[SerializeField] private GameObject[] wave40Path1;
    //[SerializeField] private GameObject[] wave41Path1;
    //[SerializeField] private GameObject[] wave42Path1;
    //[SerializeField] private GameObject[] wave43Path1;
    //[SerializeField] private GameObject[] wave44Path1;
    //[SerializeField] private GameObject[] wave45Path1;
    //[SerializeField] private GameObject[] wave46Path1;
    //[SerializeField] private GameObject[] wave47Path1;
    //[SerializeField] private GameObject[] wave48Path1;
    //[SerializeField] private GameObject[] wave49Path1;
    //[SerializeField] private GameObject[] wave50Path1;
    //[SerializeField] private GameObject[] wave51Path1;
    //[SerializeField] private GameObject[] wave52Path1;
    //[SerializeField] private GameObject[] wave53Path1;
    //[SerializeField] private GameObject[] wave54Path1;
    //[SerializeField] private GameObject[] wave55Path1;
    //[SerializeField] private GameObject[] wave56Path1;
    //[SerializeField] private GameObject[] wave57Path1;
    //[SerializeField] private GameObject[] wave58Path1;
    //[SerializeField] private GameObject[] wave59Path1;
    //[SerializeField] private GameObject[] wave60Path1;
    //[SerializeField] private GameObject[] wave61Path1;
    //[SerializeField] private GameObject[] wave62Path1;
    //[SerializeField] private GameObject[] wave63Path1;
    //[SerializeField] private GameObject[] wave64Path1;
    //[SerializeField] private GameObject[] wave65Path1;
    //[SerializeField] private GameObject[] wave66Path1;
    //[SerializeField] private GameObject[] wave67Path1;
    //[SerializeField] private GameObject[] wave68Path1;
    //[SerializeField] private GameObject[] wave69Path1;
    //[SerializeField] private GameObject[] wave70Path1;
    //[SerializeField] private GameObject[] wave71Path1;
    //[SerializeField] private GameObject[] wave72Path1;
    //[SerializeField] private GameObject[] wave73Path1;
    //[SerializeField] private GameObject[] wave74Path1;
    //[SerializeField] private GameObject[] wave75Path1;
    //[SerializeField] private GameObject[] wave76Path1;
    //[SerializeField] private GameObject[] wave77Path1;
    //[SerializeField] private GameObject[] wave78Path1;
    //[SerializeField] private GameObject[] wave79Path1;
    //[SerializeField] private GameObject[] wave80Path1;
    //[SerializeField] private GameObject[] wave81Path1;
    //[SerializeField] private GameObject[] wave82Path1;
    //[SerializeField] private GameObject[] wave83Path1;
    //[SerializeField] private GameObject[] wave84Path1;
    //[SerializeField] private GameObject[] wave85Path1;
    //[SerializeField] private GameObject[] wave86Path1;
    //[SerializeField] private GameObject[] wave87Path1;
    //[SerializeField] private GameObject[] wave88Path1;
    //[SerializeField] private GameObject[] wave89Path1;
    //[SerializeField] private GameObject[] wave90Path1;
    //[SerializeField] private GameObject[] wave91Path1;
    //[SerializeField] private GameObject[] wave92Path1;
    //[SerializeField] private GameObject[] wave93Path1;
    //[SerializeField] private GameObject[] wave94Path1;
    //[SerializeField] private GameObject[] wave95Path1;
    //[SerializeField] private GameObject[] wave96Path1;
    //[SerializeField] private GameObject[] wave97Path1;
    //[SerializeField] private GameObject[] wave98Path1;
    //[SerializeField] private GameObject[] wave99Path1;
    //[SerializeField] private GameObject[] wave100Path1;

    [SerializeField] private GameObject[][] enemySpawnsPath2;
    [SerializeField] private GameObject[] wave1Path2;
    [SerializeField] private GameObject[] wave2Path2;
    [SerializeField] private GameObject[] wave3Path2;
    [SerializeField] private GameObject[] wave4Path2;
    [SerializeField] private GameObject[] wave5Path2;
    [SerializeField] private GameObject[] wave6Path2;
    [SerializeField] private GameObject[] wave7Path2;
    [SerializeField] private GameObject[] wave8Path2;
    [SerializeField] private GameObject[] wave9Path2;
    [SerializeField] private GameObject[] wave10Path2;
    [SerializeField] private GameObject[] wave11Path2;
    [SerializeField] private GameObject[] wave12Path2;
    [SerializeField] private GameObject[] wave13Path2;
    [SerializeField] private GameObject[] wave14Path2;
    [SerializeField] private GameObject[] wave15Path2;
    [SerializeField] private GameObject[] wave16Path2;
    [SerializeField] private GameObject[] wave17Path2;
    [SerializeField] private GameObject[] wave18Path2;
    [SerializeField] private GameObject[] wave19Path2;
    [SerializeField] private GameObject[] wave20Path2;
    [SerializeField] private GameObject[] wave21Path2;
    [SerializeField] private GameObject[] wave22Path2;
    [SerializeField] private GameObject[] wave23Path2;
    [SerializeField] private GameObject[] wave24Path2;
    [SerializeField] private GameObject[] wave25Path2;
    [SerializeField] private GameObject[] wave26Path2;
    [SerializeField] private GameObject[] wave27Path2;
    [SerializeField] private GameObject[] wave28Path2;
    [SerializeField] private GameObject[] wave29Path2;
    [SerializeField] private GameObject[] wave30Path2;
    //[SerializeField] private GameObject[] wave31Path2;
    //[SerializeField] private GameObject[] wave32Path2;
    //[SerializeField] private GameObject[] wave33Path2;
    //[SerializeField] private GameObject[] wave34Path2;
    //[SerializeField] private GameObject[] wave35Path2;
    //[SerializeField] private GameObject[] wave36Path2;
    //[SerializeField] private GameObject[] wave37Path2;
    //[SerializeField] private GameObject[] wave38Path2;
    //[SerializeField] private GameObject[] wave39Path2;
    //[SerializeField] private GameObject[] wave40Path2;
    //[SerializeField] private GameObject[] wave41Path2;
    //[SerializeField] private GameObject[] wave42Path2;
    //[SerializeField] private GameObject[] wave43Path2;
    //[SerializeField] private GameObject[] wave44Path2;
    //[SerializeField] private GameObject[] wave45Path2;
    //[SerializeField] private GameObject[] wave46Path2;
    //[SerializeField] private GameObject[] wave47Path2;
    //[SerializeField] private GameObject[] wave48Path2;
    //[SerializeField] private GameObject[] wave49Path2;
    //[SerializeField] private GameObject[] wave50Path2;
    //[SerializeField] private GameObject[] wave51Path2;
    //[SerializeField] private GameObject[] wave52Path2;
    //[SerializeField] private GameObject[] wave53Path2;
    //[SerializeField] private GameObject[] wave54Path2;
    //[SerializeField] private GameObject[] wave55Path2;
    //[SerializeField] private GameObject[] wave56Path2;
    //[SerializeField] private GameObject[] wave57Path2;
    //[SerializeField] private GameObject[] wave58Path2;
    //[SerializeField] private GameObject[] wave59Path2;
    //[SerializeField] private GameObject[] wave60Path2;
    //[SerializeField] private GameObject[] wave61Path2;
    //[SerializeField] private GameObject[] wave62Path2;
    //[SerializeField] private GameObject[] wave63Path2;
    //[SerializeField] private GameObject[] wave64Path2;
    //[SerializeField] private GameObject[] wave65Path2;
    //[SerializeField] private GameObject[] wave66Path2;
    //[SerializeField] private GameObject[] wave67Path2;
    //[SerializeField] private GameObject[] wave68Path2;
    //[SerializeField] private GameObject[] wave69Path2;
    //[SerializeField] private GameObject[] wave70Path2;
    //[SerializeField] private GameObject[] wave71Path2;
    //[SerializeField] private GameObject[] wave72Path2;
    //[SerializeField] private GameObject[] wave73Path2;
    //[SerializeField] private GameObject[] wave74Path2;
    //[SerializeField] private GameObject[] wave75Path2;
    //[SerializeField] private GameObject[] wave76Path2;
    //[SerializeField] private GameObject[] wave77Path2;
    //[SerializeField] private GameObject[] wave78Path2;
    //[SerializeField] private GameObject[] wave79Path2;
    //[SerializeField] private GameObject[] wave80Path2;
    //[SerializeField] private GameObject[] wave81Path2;
    //[SerializeField] private GameObject[] wave82Path2;
    //[SerializeField] private GameObject[] wave83Path2;
    //[SerializeField] private GameObject[] wave84Path2;
    //[SerializeField] private GameObject[] wave85Path2;
    //[SerializeField] private GameObject[] wave86Path2;
    //[SerializeField] private GameObject[] wave87Path2;
    //[SerializeField] private GameObject[] wave88Path2;
    //[SerializeField] private GameObject[] wave89Path2;
    //[SerializeField] private GameObject[] wave90Path2;
    //[SerializeField] private GameObject[] wave91Path2;
    //[SerializeField] private GameObject[] wave92Path2;
    //[SerializeField] private GameObject[] wave93Path2;
    //[SerializeField] private GameObject[] wave94Path2;
    //[SerializeField] private GameObject[] wave95Path2;
    //[SerializeField] private GameObject[] wave96Path2;
    //[SerializeField] private GameObject[] wave97Path2;
    //[SerializeField] private GameObject[] wave98Path2;
    //[SerializeField] private GameObject[] wave99Path2;
    //[SerializeField] private GameObject[] wave100Path2;

    [Header("Attributes")]
    [SerializeField] public int numberOfWaves = 15;
    public int currentWave = 0;
    private float timeSinceLastSpawnPath1;
    private float timeSinceLastSpawnPath2;
    public int enemiesAlive;
    private int enemiesLeftToSpawnPath1;
    private int enemiesLeftToSpawnPath2;
    private bool isSpawning = false;
    private float waveSpawnRatio;
    private float timeBetweenWaves;
    private int spawnIndexPath1 = 0;
    private int spawnIndexPath2 = 0;
    private float enemiesPerSecondPath1;
    private float enemiesPerSecondPath2;
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
        waveSpawnRatio = GlobalValues.main.waveSpawnRatio;
        timeBetweenWaves = GlobalValues.main.waveLength;
        enemySpawnsPath1 = new GameObject[][] { wave1Path1, wave2Path1, wave3Path1, wave4Path1, wave5Path1, wave6Path1, wave7Path1, wave8Path1, wave9Path1, wave10Path1, wave11Path1, wave12Path1, wave13Path1, wave14Path1, wave15Path1, wave16Path1, wave17Path1, wave18Path1, wave19Path1, wave20Path1, wave21Path1, wave22Path1, wave23Path1, wave24Path1, wave25Path1, wave26Path1, wave27Path1, wave28Path1, wave29Path1, wave30Path1 };//, wave31Path1, wave32Path1, wave33Path1, wave34Path1, wave35Path1, wave36Path1, wave37Path1, wave38Path1, wave39Path1, wave40Path1, wave41Path1, wave42Path1, wave43Path1, wave44Path1, wave45Path1, wave46Path1, wave47Path1, wave48Path1, wave49Path1, wave50Path1, wave51Path1, wave52Path1, wave53Path1, wave54Path1, wave55Path1, wave56Path1, wave57Path1, wave58Path1, wave59Path1, wave60Path1, wave61Path1, wave62Path1, wave63Path1, wave64Path1, wave65Path1, wave66Path1, wave67Path1, wave68Path1, wave69Path1, wave70Path1, wave71Path1, wave72Path1, wave73Path1, wave74Path1, wave75Path1, wave76Path1, wave77Path1, wave78Path1, wave79Path1, wave80Path1, wave81Path1, wave82Path1, wave83Path1, wave84Path1, wave85Path1, wave86Path1, wave87Path1, wave88Path1, wave89Path1, wave90Path1, wave91Path1, wave92Path1, wave93Path1, wave94Path1, wave95Path1, wave96Path1, wave97Path1, wave98Path1, wave99Path1, wave100Path1 };
        enemySpawnsPath2 = new GameObject[][] { wave1Path2, wave2Path2, wave3Path2, wave4Path2, wave5Path2, wave6Path2, wave7Path2, wave8Path2, wave9Path2, wave10Path2, wave11Path2, wave12Path2, wave13Path2, wave14Path2, wave15Path2, wave16Path2, wave17Path2, wave18Path2, wave19Path2, wave20Path2, wave21Path2, wave22Path2, wave23Path2, wave24Path2, wave25Path2, wave26Path2, wave27Path2, wave28Path2, wave29Path2, wave30Path2 };//, wave31Path2, wave32Path2, wave33Path2, wave34Path2, wave35Path2, wave36Path2, wave37Path2, wave38Path2, wave39Path2, wave40Path2, wave41Path2, wave42Path2, wave43Path2, wave44Path2, wave45Path2, wave46Path2, wave47Path2, wave48Path2, wave49Path2, wave50Path2, wave51Path2, wave52Path2, wave53Path2, wave54Path2, wave55Path2, wave56Path2, wave57Path2, wave58Path2, wave59Path2, wave60Path2, wave61Path2, wave62Path2, wave63Path2, wave64Path2, wave65Path2, wave66Path2, wave67Path2, wave68Path2, wave69Path2, wave70Path2, wave71Path2, wave72Path2, wave73Path2, wave74Path2, wave75Path2, wave76Path2, wave77Path2, wave78Path2, wave79Path2, wave80Path2, wave81Path2, wave82Path2, wave83Path2, wave84Path2, wave85Path2, wave86Path2, wave87Path2, wave88Path2, wave89Path2, wave90Path2, wave91Path2, wave92Path2, wave93Path2, wave94Path2, wave95Path2, wave96Path2, wave97Path2, wave98Path2, wave99Path2, wave100Path2 };
        Array.Resize(ref enemySpawnsPath1, numberOfWaves);
        Array.Resize(ref enemySpawnsPath2, numberOfWaves);
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
         timeSinceLastSpawnPath1 += Time.deltaTime;
         timeSinceLastSpawnPath2 += Time.deltaTime;

         if (timeSinceLastSpawnPath1 >= (1f / enemiesPerSecondPath1) && enemiesLeftToSpawnPath1 > 0)
         {
             SpawnEnemy("Path1");
             enemiesLeftToSpawnPath1--;
             timeSinceLastSpawnPath1 = 0f;
         }
         if (timeSinceLastSpawnPath2 >= (1f / enemiesPerSecondPath2) && enemiesLeftToSpawnPath2 > 0)
         {
             SpawnEnemy("Path2");
             enemiesLeftToSpawnPath2--;
             timeSinceLastSpawnPath2 = 0f;
         }
    }

    private void StartWave()
    {
        if (currentWave >= numberOfWaves)
        {
            LevelManager.main.finalWave = true;
            finalWave = true;
            waveCountdown = 0f;
        }
        else
        {
            currentWave++;
            LevelManager.main.levelStarted = true;
            UIManager.main.WaveUpdate();
            isSpawning = true;
            spawnIndexPath1 = 0;
            spawnIndexPath2 = 0;
            enemiesLeftToSpawnPath1 = enemySpawnsPath1[currentWave - 1].Length;
            enemiesPerSecondPath1 = (enemiesLeftToSpawnPath1) / (timeBetweenWaves * waveSpawnRatio);
            enemiesLeftToSpawnPath2 = enemySpawnsPath2[currentWave - 1].Length;
            enemiesPerSecondPath2 = (enemiesLeftToSpawnPath2) / (timeBetweenWaves * waveSpawnRatio);
            waveCountdown = timeBetweenWaves;
        }
    }

    private void EndWave()
    {
        timeSinceLastSpawnPath1 = 0f;
        timeSinceLastSpawnPath2 = 0f;
        if (currentWave <= numberOfWaves)
        {
            StartWave();
        }
    }

    private void SpawnEnemy(string path)
    {
        GameObject prefabToSpawn;
        Transform start;
        Transform nextPoint;
        if (path == "Path1")
        {
            prefabToSpawn = enemySpawnsPath1[currentWave - 1][spawnIndexPath1];
            spawnIndexPath1++;
            start = LevelManager.main.path1[0];
            nextPoint = LevelManager.main.path1[1];
        }
        else
        {
            prefabToSpawn = enemySpawnsPath2[currentWave - 1][spawnIndexPath2];
            spawnIndexPath2++;
            start = LevelManager.main.path2[0];
            nextPoint = LevelManager.main.path2[1];
        }
        if (prefabToSpawn.GetComponent<Identify>().ID == -1)
        {
            return;
        }
        float angle = Mathf.Atan2(nextPoint.position.y - start.position.y, nextPoint.position.x - start.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion enemyRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        GameObject enemy = Instantiate(prefabToSpawn, start.position, enemyRotation);
        if (path == "Path1")
        {
            Library.main.SetPath(enemy, 1);
        }
        else
        {
            Library.main.SetPath(enemy, 2);
        }
        if (spawnIndexPath1 >= enemySpawnsPath1[currentWave - 1].Length && spawnIndexPath2 >= enemySpawnsPath2[currentWave - 1].Length)
        {
            isSpawning = false;
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
