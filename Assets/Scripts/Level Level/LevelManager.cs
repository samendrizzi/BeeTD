using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [Header("References")]
    [SerializeField] public Transform queenBee;
    [SerializeField] public SoundType victorySound;
    [SerializeField] public SoundType defeatSound;
    [SerializeField] public SoundType waveStartSound;

    [Header("Attributes")]
    [SerializeField] public float honey = 0f;
    [SerializeField] public float bonusNectar = 0f;
    [SerializeField] public float bonusHoney = 0f;
    [SerializeField] public float honeyRequired = 100f;
    [SerializeField] public float percentageOfFlowersUsed = 1f;
    [SerializeField] public float difficultyScaling = 0.05f;
    [SerializeField] public GameObject hiveEntrance;
    [SerializeField] public float startingVision = 5f;
    [SerializeField] public int numberOfPaths = 0;
    [SerializeField] private Transform[] path1;
    [SerializeField] private Transform[] flyingPath1;
    [SerializeField] private Transform[] path2;
    [SerializeField] private Transform[] flyingPath2;
    [SerializeField] private Transform[] path3;
    [SerializeField] private Transform[] flyingPath3;
    [SerializeField] private Transform[] path4;
    [SerializeField] private Transform[] flyingPath4;
    [SerializeField] private Transform[] path5;
    [SerializeField] private Transform[] flyingPath5;
    [SerializeField] private Transform[] path6;
    [SerializeField] private Transform[] flyingPath6;
    [SerializeField] private Transform[] path7;
    [SerializeField] private Transform[] flyingPath7;
    [SerializeField] private Transform[] path8;
    [SerializeField] private Transform[] flyingPath8;
    [SerializeField] private Transform[] path9;
    [SerializeField] private Transform[] flyingPath9;
    [SerializeField] private Transform[] path10;
    [SerializeField] private Transform[] flyingPath10;

    //Trackers
    public Transform[][] paths;
    public Transform[][] flyingPaths;
    public Transform[] pathsStart;
    public Transform[] pathsNextPoint;
    public float queenHP;
    public float queenArmor;
    public float queenMaxHP;
    public float incomeRate;
    public float investmentRate;
    public float bonusInvestmentRate;
    private float numberOfOpenedFlowers = 0;
    private float numberOfClosedFlowers = 0;
    public GameObject[] flowers = new GameObject[] { };
    private GameObject[] flowersToBloom = new GameObject[] { };
    public bool levelStarted = false;
    public bool finalWave = false;
    public string speed = "Normal";
    public bool pause = false;
    public float timing = 1f;
    public float nectarGenerationRate = 3f;
    public float honeyGeneratedRatio = 0f;
    public GameObject[] discoveredFlowers = new GameObject[] { };
    public GameObject[] honeyCombs = new GameObject[] { };
    public GameObject[] workerBees = new GameObject[] { };
    public GameObject[] unassignedBees = new GameObject[] { };
    public GameObject[] nectarBees = new GameObject[] { };
    public GameObject[] honeyBees = new GameObject[] { };
    public GameObject[] soldierBees = new GameObject[] { };
    public Transform[] emptyHoneyCombs = new Transform[] { };
    public Transform[] queuedTargets = new Transform[] { };
    public float workerBeeCost;
    public float nectar;
    public string autoAssignBees = "Nectar";
    public int honeyCombTicks;
    public float honeyPerCombTick;
    private float timeElapsed = 0f;

    private void Awake()
    {
        main = this;
        //FindAllFlowers();
        //BloomFlowers();
    }

    private void Start()
    {
        SceneManager.SetActiveScene(gameObject.scene);
        GlobalValues.main.SetUI(true);
        speed = UIManager.main.speed;
        pause = UIManager.main.pause;
        //honeyRequired = honeyRequired;
        //nectarGenerationRate = 0;
        GameObject[] root = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in root)
        {
            if ((GlobalValues.main.honeyCombMask | (1 << obj.layer)) == GlobalValues.main.honeyCombMask)
            {
                Array.Resize(ref honeyCombs, honeyCombs.Length + 1);
                honeyCombs[honeyCombs.Length - 1] = obj;
            }
        }
        honeyCombs = honeyCombs.OrderBy(point => Vector2.Distance(queenBee.transform.position, point.transform.position)).ToArray();
        UIManager.main.UpdateQueensCommand();
        //Set Speed
        UIManager.main.NormalSpeed(false);
        UIManager.main.TogglePause(false);
        CheckHoneyCombs();
        //Build Path Arrays
        paths = new Transform[][] {path1, path2, path3, path4, path5, path6, path7, path8, path9, path10};
        flyingPaths = new Transform[][] {flyingPath1, flyingPath2, flyingPath3, flyingPath4, flyingPath5, flyingPath6, flyingPath7, flyingPath8, flyingPath9, flyingPath10};
        Array.Resize(ref paths, numberOfPaths);
        Array.Resize(ref flyingPaths, numberOfPaths);
        Array.Resize(ref pathsStart, numberOfPaths);
        Array.Resize(ref pathsNextPoint, numberOfPaths);
        for (int i = 0; i < numberOfPaths; i++)
        {
            pathsStart[i] = paths[i][0];
            pathsNextPoint[i] = paths[i][1];
        }
        nectar = BuffManager.main.startingNectar + bonusNectar;
        honey = BuffManager.main.startingHoney + bonusHoney;
        SetStats();
        queenHP = queenMaxHP;
        StartingReveal();
        SpawnStartingBees();
    }

    public void SetStats()
    {
        //nectarGenerationRate = 0; Not Implemented
        honeyCombTicks = BuffManager.main.honeycombTicks;
        honeyPerCombTick = BuffManager.main.honeycombTicks;
        workerBeeCost = BuffManager.main.beeCost * (1 + (BuffManager.main.beeCostScaling * workerBees.Length));
        queenArmor = BuffManager.main.queenBeeArmor;
        queenHP = BuffManager.main.queenBeeHitPoints;
        CalculateIncome();
    }

    public void IncreaseNectar(float amount)
    {
        nectar += amount;
    }

    public bool Spendnectar(float amount)
    {
        if (amount <= nectar)
        {
            nectar -= amount;
            return true;
        }
        else
        {
            Debug.Log("You broke.");
            return false;
        }
    }

    public void IncreaseHoney(float amount)
    {
        honey += amount;
    }

    public void CalculateIncome()
    {
        incomeRate = 0; //Not Implemented
        bonusInvestmentRate = 0f;
    }

    private void Update()
    {
        //maximum deltaTime
        if (Time.deltaTime > GlobalValues.main.maxDeltaTime)
        {
            float newTimeScale = (GlobalValues.main.maxDeltaTime / Time.deltaTime) * timing;
            Time.timeScale = newTimeScale;
        }
        else
        {
            Time.timeScale = timing;
        }
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= GlobalValues.main.honeycombCheckTime)
        {
            timeElapsed = 0f;
            CheckHoneyCombs();
        }
        if (finalWave == false && levelStarted == true)
        {
            honey += (bonusInvestmentRate + investmentRate) * Time.deltaTime;
            nectar += (incomeRate) * Time.deltaTime;
        }
        else if (levelStarted == true)
        {
            nectar += (incomeRate) * Time.deltaTime;
        }
    }

    public void HitQueen(float dmg, float armorPierce)
    {
        float armorBlock = (queenArmor - armorPierce) / 100f;
        if (armorBlock < 0)
        {
            armorBlock = 0f;
        }
        else if (armorBlock >= 1f)
        {
            //no damage
            return;
        }
        queenHP -= (1f - armorBlock) * dmg;
        if (queenHP <= 0f)
        {
            QueenDies();
        }
    }

    private void QueenDies()
    {
        Defeat();
    }

    private void FindAllFlowers()
    {
        numberOfClosedFlowers = 0f;
        numberOfOpenedFlowers = 0f;
        flowers = new GameObject[] { };
        flowersToBloom = new GameObject[] { };
        discoveredFlowers = new GameObject[] { };
        GameObject[] root = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        if (root.Length == 0)
        {
            return;
        }
        foreach (GameObject obj in root)
        {
            if ((GlobalValues.main.flowerMask | (1 << obj.layer)) == GlobalValues.main.flowerMask)
            {
                Array.Resize(ref flowers, flowers.Length + 1);
                flowers[flowers.Length - 1] = obj;
                //track discovered flowers
                if (obj.GetComponent<Plot>().fog == false)
                {
                    FoundFlower(obj);
                }
                if (obj.GetComponent<Identify>().ID == 0)
                {
                    numberOfClosedFlowers++;
                    Array.Resize(ref flowersToBloom, flowersToBloom.Length + 1);
                    flowersToBloom[flowersToBloom.Length - 1] = obj;
                }
                else
                {
                    numberOfOpenedFlowers++;
                }
            }
        }
    }

    private void BloomFlowers()
    {
        int counter = 0;
        float flowerRatio = 0f;
        while (numberOfClosedFlowers > 0 && flowerRatio < percentageOfFlowersUsed)
        {
            flowerRatio = ((float)numberOfOpenedFlowers / ((float)numberOfClosedFlowers + (float)numberOfOpenedFlowers));
            System.Random RandomGen = new System.Random();
            //Random Unblossomed Flower
            int randompick = RandomGen.Next(flowersToBloom.Length - 1);
            if (flowersToBloom[randompick].GetComponent<Plot>().hasBloomed == false)
            {
                //Random Flower Type
                int randompick2 = RandomGen.Next(GlobalValues.main.FLOWERRarity.Length - 1);
                float rarity = GlobalValues.main.FLOWERRarity[randompick2];
                //Chance to Succeed Roll
                double randompick3 = RandomGen.NextDouble();
                if (rarity > randompick3)
                {
                    flowersToBloom[randompick].GetComponent<Plot>().Bloom(randompick2);
                    FindAllFlowers();
                    flowerRatio = (1f - ((float)flowersToBloom.Length / (float)flowers.Length));
                }
            }
            counter++;
            if (counter >= 5000)
            {
                Debug.Log("Bloom Loop Stuck");
                return;
            }
        }
    }

    public void EndLevel()
    {
        //No other logic atm
        UIManager.main.TogglePause();
        Victory();
    }

    private void Victory()
    {
        SoundManager.main.PlaySound(victorySound, 0f);
        CollectAllHoney();
        if (GlobalValues.main.difficulty == GlobalValues.Difficulty.EASY)
        {
            if (SaveFile.gameData.easyScores[GlobalValues.main.levelIndex] < honey)
            {
                SaveFile.gameData.easyScores[GlobalValues.main.levelIndex] = (int)honey;
            }
        }
        else if (GlobalValues.main.difficulty == GlobalValues.Difficulty.MEDIUM)
        {
            if (SaveFile.gameData.mediumScores[GlobalValues.main.levelIndex] < honey)
            {
                SaveFile.gameData.mediumScores[GlobalValues.main.levelIndex] = (int)honey;
            }
        }
        else if (GlobalValues.main.difficulty == GlobalValues.Difficulty.HARD)
        {
            if (SaveFile.gameData.hardScores[GlobalValues.main.levelIndex] < honey)
            {
                SaveFile.gameData.hardScores[GlobalValues.main.levelIndex] = (int)honey;
            }
        }
        SaveFile.main.Save();
        UIManager.main.VictoryUI();
    }

    private void Defeat()
    {
        SoundManager.main.PlaySound(defeatSound, 0f);
        UIManager.main.DefeatUI();
    }

    public void ChangeTiming(string time, bool paused)
    {
        speed = time;
        if (paused == true)
        {
            timing = 0f;
            pause = true;
        }
        else if (speed == "Normal") {
            timing = GlobalValues.main.normalTiming;
        }
        else if (speed == "Fast")
        {
            timing = GlobalValues.main.fastTiming;
        }
        else if (speed == "Very Fast")
        {
            timing = GlobalValues.main.veryFastTiming;
        }
        Time.timeScale = timing;
    }

    public void OrganizeBees()
    {
        //Update UI Bee Count
        UIManager.main.UpdateQueensCommand();
        //Direct Bees
        OrganizeNectarBees();
        OrganizeHoneyBees();
        OrganizeUnassignedBees();
        OrganizeSoldierBees();
    }

    private void OrganizeNectarBees()
    {
        int numberOfNectarBees = nectarBees.Length;

        if (numberOfNectarBees > 0)
        {
            Attributes Bee = nectarBees[0].GetComponent<Attributes>();
            float beeMoveSpeed = Bee.moveSpeedBase;
            float beeCarryCapacity = Bee.carryCapacity;
            discoveredFlowers = discoveredFlowers.OrderBy(point => Vector2.Distance(queenBee.transform.position, point.transform.position)).ToArray();
            int assignedBees = 0;
            foreach (GameObject obj in discoveredFlowers)
            {
                int maxnumberOfNectarBees = Mathf.FloorToInt((Vector2.Distance(obj.transform.position, queenBee.transform.position) * (2) / beeMoveSpeed) / (beeCarryCapacity / nectarGenerationRate));
                for (int i = 0; i < maxnumberOfNectarBees; i++)
                {
                    if (assignedBees >= numberOfNectarBees)
                    {
                        return;
                    }
                    AssignBeeToFlower(nectarBees[assignedBees], obj);
                    assignedBees++;
                }
                if (assignedBees >= numberOfNectarBees)
                {
                    return;
                }
            }
            int y = 0;
            if (nectarBees.Length == 0 || discoveredFlowers.Length == 0)
            {
                return;
            }
            while (assignedBees < numberOfNectarBees)
            {
                AssignBeeToFlower(nectarBees[assignedBees], discoveredFlowers[y]);
                assignedBees++;
                y++;
                if (y >= discoveredFlowers.Length)
                {
                    y = 0;
                }
            }
        }
    }

    private void OrganizeHoneyBees()
    {
        foreach (GameObject obj in honeyBees)
        {
            Attributes Bee = obj.GetComponent<Attributes>();
            if (Bee.target == null)
            {
                return;
            }
            if (Bee.target != queenBee && (GlobalValues.main.honeyCombMask | (1 << Bee.target.gameObject.layer)) == GlobalValues.main.honeyCombMask)
            {
                Bee.target = null;
            }
        }
    }

    private void OrganizeUnassignedBees()
    {
        int numberOfUnassignedBees = unassignedBees.Length;
        if (numberOfUnassignedBees > 0)
        {
            foreach (GameObject obj in unassignedBees)
            {
                obj.GetComponent<Attributes>().target = null;
                obj.GetComponent<Attributes>().HaltMovement();
            }
        }
    }

    private void OrganizeSoldierBees()
    {
        int numberOfsoldierBees = soldierBees.Length;
        if (numberOfsoldierBees > 0)
        {
            foreach (GameObject obj in soldierBees)
            {
                obj.GetComponent<Attributes>().target = null;
            }
        }
    }

    public void FoundFlower(GameObject flower)
    {
        Array.Resize(ref discoveredFlowers, discoveredFlowers.Length + 1);
        discoveredFlowers[discoveredFlowers.Length - 1] = flower;
        OrganizeNectarBees();
    }

    public void AssignBeeToFlower(GameObject b, GameObject f)
    {
        Attributes Bee = b.GetComponent<Attributes>();
        Bee.flower = f;
        if (Bee.target != queenBee.transform)
        {
            Bee.target = f.transform;
        }
    }

    public void BuyUnit(string work = "")
    {     
        if (nectar >= workerBeeCost)
        {
            //Spawn bee
            nectar -= workerBeeCost;
            SpawnUnit(work);
        }
    }

    private void SpawnUnit(string work)
    {
        GameObject prefabToSpawn = GlobalValues.main.workerBeePrefab;
        Transform start = queenBee.transform;
        Transform nextPoint = gameObject.transform;
        float angle = Mathf.Atan2(nextPoint.position.y - start.position.y, nextPoint.position.x - start.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion unitRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        GameObject unit = Instantiate(prefabToSpawn, start.position, unitRotation);
        //Add bee to array tracking
        Array.Resize(ref workerBees, workerBees.Length + 1);
        workerBees[workerBees.Length - 1] = unit;
        if (work == "")
        {
            work = autoAssignBees;
        }
        if (work == "Unassigned")
        {
            unit.GetComponent<Attributes>().work = "Unassigned";
            Array.Resize(ref unassignedBees, unassignedBees.Length + 1);
            unassignedBees[unassignedBees.Length - 1] = unit;
        }
        else if (work == "Nectar")
        {
            unit.GetComponent<Attributes>().work = "Nectar";
            Array.Resize(ref nectarBees, nectarBees.Length + 1);
            nectarBees[nectarBees.Length - 1] = unit;
        }
        else if (work == "Honey")
        {
            unit.GetComponent<Attributes>().work = "Honey";
            Array.Resize(ref honeyBees, honeyBees.Length + 1);
            honeyBees[honeyBees.Length - 1] = unit;
        }
        else if (work == "Soldier")
        {
            unit.GetComponent<Attributes>().work = "Soldier";
            Array.Resize(ref soldierBees, soldierBees.Length + 1);
            soldierBees[soldierBees.Length - 1] = unit;
        }
        //Update bee cost
        workerBeeCost = BuffManager.main.beeCost * (1 + (BuffManager.main.beeCostScaling * workerBees.Length));
        //Update
        OrganizeBees();
    }

    public void AssignBeeToNectar(bool add) 
    {
        if (add == true) 
        {
            GameObject Bee;
            if (unassignedBees.Length == 0)
            {
                if (autoAssignBees == "Honey" && honeyBees.Length > 0)
                {
                    Bee = honeyBees[honeyBees.Length - 1];
                    Array.Resize(ref honeyBees, honeyBees.Length - 1);
                    Array.Resize(ref nectarBees, nectarBees.Length + 1);
                }
                else if (soldierBees.Length > 0)
                {
                    Bee = soldierBees[soldierBees.Length - 1];
                    Array.Resize(ref soldierBees, soldierBees.Length - 1);
                    Array.Resize(ref nectarBees, nectarBees.Length + 1);
                }
                else if (honeyBees.Length > 0)
                {
                    Bee = honeyBees[honeyBees.Length - 1];
                    Array.Resize(ref honeyBees, honeyBees.Length - 1);
                    Array.Resize(ref nectarBees, nectarBees.Length + 1);
                }
                else
                {
                    return;
                }
            }
            else
            {
                Bee = unassignedBees[unassignedBees.Length - 1];
                Array.Resize(ref unassignedBees, unassignedBees.Length - 1);
                Array.Resize(ref nectarBees, nectarBees.Length + 1);
            }
            //Assigned bee to nectar
            Bee.GetComponent<Attributes>().work = "Nectar";
            nectarBees[nectarBees.Length - 1] = Bee;
            OrganizeBees();
        }
        else if (add == false && nectarBees.Length > 0)
        {
            //Assign nectar bee to unassigned
            GameObject Bee = nectarBees[nectarBees.Length - 1];
            Array.Resize(ref nectarBees, nectarBees.Length - 1);
            Array.Resize(ref unassignedBees, unassignedBees.Length + 1);
            Bee.GetComponent<Attributes>().work = "Unassigned";
            unassignedBees[unassignedBees.Length - 1] = Bee;
            OrganizeBees();
        }
    }

    public void AssignBeeToHoney(bool add) 
    {
        if (add == true) 
        {
            GameObject Bee;
            if (unassignedBees.Length == 0)
            {
                if (autoAssignBees == "Nectar" && nectarBees.Length > 0)
                {
                    Bee = nectarBees[nectarBees.Length - 1];
                    Array.Resize(ref nectarBees, nectarBees.Length - 1);
                    Array.Resize(ref honeyBees, honeyBees.Length + 1);
                }
                else if (soldierBees.Length > 0)
                {
                    Bee = soldierBees[soldierBees.Length - 1];
                    Array.Resize(ref soldierBees, soldierBees.Length - 1);
                    Array.Resize(ref honeyBees, honeyBees.Length + 1);
                }
                else if (nectarBees.Length > 0)
                {
                    Bee = nectarBees[nectarBees.Length - 1];
                    Array.Resize(ref nectarBees, nectarBees.Length - 1);
                    Array.Resize(ref honeyBees, honeyBees.Length + 1);
                }
                else
                {
                    return;
                }
            }
            else
            {
                Bee = unassignedBees[unassignedBees.Length - 1];
                Array.Resize(ref unassignedBees, unassignedBees.Length - 1);
                Array.Resize(ref honeyBees, honeyBees.Length + 1);
            }
            //Assigned unassigned bee to honey
            Bee.GetComponent<Attributes>().work = "Honey";
            honeyBees[honeyBees.Length - 1] = Bee;
            OrganizeBees();
        }
        else if (add == false && honeyBees.Length > 0)
        {
            //Assign honey bee to unassigned
            GameObject Bee = honeyBees[honeyBees.Length - 1];
            Array.Resize(ref honeyBees, honeyBees.Length - 1);
            Array.Resize(ref unassignedBees, unassignedBees.Length + 1);
            Bee.GetComponent<Attributes>().work = "Unassigned";
            unassignedBees[unassignedBees.Length - 1] = Bee;
            OrganizeBees();
        }
    }

    public void AssignBeeToSoldier(bool add)
    {
        if (add == true)
        {
            GameObject Bee;
            if (unassignedBees.Length == 0)
            {
                if (autoAssignBees == "Nectar" && nectarBees.Length > 0)
                {
                    Bee = nectarBees[nectarBees.Length - 1];
                    Array.Resize(ref nectarBees, nectarBees.Length - 1);
                    Array.Resize(ref soldierBees, soldierBees.Length + 1);
                }
                else if (honeyBees.Length > 0)
                {
                    Bee = honeyBees[honeyBees.Length - 1];
                    Array.Resize(ref honeyBees, honeyBees.Length - 1);
                    Array.Resize(ref soldierBees, soldierBees.Length + 1);
                }
                else if (nectarBees.Length > 0)
                {
                    Bee = nectarBees[nectarBees.Length - 1];
                    Array.Resize(ref nectarBees, nectarBees.Length - 1);
                    Array.Resize(ref soldierBees, soldierBees.Length + 1);
                }
                else
                {
                    return;
                }
            }
            else
            {
                Bee = unassignedBees[unassignedBees.Length - 1];
                Array.Resize(ref unassignedBees, unassignedBees.Length - 1);
                Array.Resize(ref soldierBees, soldierBees.Length + 1);
            }
            //Assigned unassigned bee to soldier
            Bee.GetComponent<Attributes>().work = "Soldier";
            soldierBees[soldierBees.Length - 1] = Bee;
            OrganizeBees();
        }
        else if (add == false && soldierBees.Length > 0)
        {
            //Assign soldier bee to unassigned
            GameObject Bee = soldierBees[soldierBees.Length - 1];
            Array.Resize(ref soldierBees, soldierBees.Length - 1);
            Array.Resize(ref unassignedBees, unassignedBees.Length + 1);
            Bee.GetComponent<Attributes>().work = "Unassigned";
            unassignedBees[unassignedBees.Length - 1] = Bee;
            OrganizeBees();
        }
    }

    public void HealQueen(float effectPower)
    {
        float missingHP = queenMaxHP - queenHP;
        if (missingHP > 0)
        {
            float heal = GlobalValues.main.beeHeal * effectPower;
            if (heal < missingHP)
            {
                queenHP += heal;
            }
            else
            {
                queenHP = queenMaxHP;
            }
        }
    }

    public void CheckHoneyCombs()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(queenBee.transform.position, 300f, (Vector2)queenBee.transform.position, 0f, GlobalValues.main.honeyCombMask);
        RaycastHit2D[] hits2 = Physics2D.CircleCastAll(queenBee.transform.position, 300f, (Vector2)queenBee.transform.position, 0f, GlobalValues.main.unitMask);
        emptyHoneyCombs = new Transform[] { };
        queuedTargets = new Transform[] { };
        for (int i = 0; i < hits2.Length; i++)
        {
            Attributes Bee = hits2[i].transform.gameObject.GetComponent<Attributes>();
            if (Bee != null && Bee.target != null)
            {
                Array.Resize(ref queuedTargets, queuedTargets.Length + 1);
                queuedTargets[queuedTargets.Length - 1] = Bee.target;
            }
        }
        for (int i = 0; i < hits.Length; i++)
        {
            Plot plot = hits[i].transform.gameObject.GetComponent<Plot>();
            if (plot.towerObj == null && plot.honeyTicks <= 0)
            {
                bool queued = false;
                for (int i2 = 0; i2 < queuedTargets.Length; i2++)
                {
                    if (hits[i].transform == queuedTargets[i2])
                    {
                        queued = true;
                    }
                }
                if (queued == false)
                {
                    Array.Resize(ref emptyHoneyCombs, emptyHoneyCombs.Length + 1);
                    emptyHoneyCombs[emptyHoneyCombs.Length - 1] = hits[i].transform;
                }
            }
        }
        emptyHoneyCombs = emptyHoneyCombs.OrderBy((comb) => (-1) * Vector2.Distance(comb.position, queenBee.position)).ToArray();
    }

    private void SpawnStartingBees()
    {
        for (int i = 1; i <= BuffManager.main.startingBees; i++) 
        {
            SpawnUnit("Nectar");
        }
    }

    private void StartingReveal()
    {
        StartCoroutine(hiveEntrance.GetComponent<Plot>().RevealFog(startingVision, true));
    }

    public void CollectAllHoney()
    {
        foreach (GameObject comb in honeyCombs)
        {
            comb.GetComponent<Plot>().GetAllHoney();
        }
    }
}
