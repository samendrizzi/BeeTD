using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [Header("References")]
    [SerializeField] public Transform queenBee;

    [Header("Attributes")]
    [SerializeField] public float queenBeeHP = 100f;
    [SerializeField] public float queenBeeMaxHP = 100f;
    [SerializeField] public float honey = 100f;
    [SerializeField] public float honeyRequired = 100f;
    [SerializeField] public float percentageOfFlowersUsed = 1f;
    [SerializeField] public Transform influenceCenter;
    [SerializeField] public Transform[] path1;
    [SerializeField] public Transform[] flyingPath1;
    [SerializeField] public Transform[] path2;
    [SerializeField] public Transform[] flyingPath2;
    public LayerMask incomeMask;
    public LayerMask investmentMask;
    public float incomeRate;
    public float investmentRate;
    public float bonusInvestmentRate;
    private float numberOfOpenedFlowers = 0;
    private float numberOfClosedFlowers = 0;
    private GameObject[] flowers = new GameObject[] { };
    private GameObject[] flowersToBloom = new GameObject[] { };
    private LayerMask flowerMask;
    public bool levelStarted = false;
    public bool finalWave = false;
    public string speed = "Normal";
    public bool pause = false;
    public float timing = 1f;
    public float honeyGeneratedRatio = 0f;

    public float nectar;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        SceneManager.SetActiveScene(gameObject.scene);
        incomeMask = GlobalValues.main.incomeMask;
        investmentMask = GlobalValues.main.investmentMask;
        GlobalValues.main.SetUI(true);
        speed = UIManager.main.speed;
        pause = UIManager.main.pause;
        honeyRequired = honeyRequired * GlobalValues.main.difficultyMultiplier;
        flowerMask = GlobalValues.main.flowerMask;
        FindAllFlowers();
        BloomFlowers();
        investmentMask = GlobalValues.main.investmentMask;
        incomeMask = GlobalValues.main.incomeMask;
        CalculateIncome();
        CalculateInvestment();
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

    public void CalculateInvestment()
    {
        investmentRate = 0f;
        GameObject[] root = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in root)
        {
            if ((investmentMask | (1 << obj.layer)) == investmentMask && obj.GetComponent<Turret>().isDestroyed == false)
            {
                investmentRate += obj.GetComponent<Turret>().investmentRate;
            }
        }
    }

    public void CalculateIncome()
    {
        incomeRate = GlobalValues.main.basenectarGeneration;
        bonusInvestmentRate = 0f;
        GameObject[] root = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in root)
        {
            if ((incomeMask | (1 << obj.layer)) == incomeMask)
            {
                incomeRate += obj.GetComponent<Turret>().generationRate;
                bonusInvestmentRate += obj.GetComponent<Turret>().honeyRate;
            }
        }
    }

    private void Update()
    {
        if (finalWave == false && levelStarted == true)
        {
            honey += (bonusInvestmentRate + investmentRate) * Time.deltaTime * LevelManager.main.timing;
            nectar += (incomeRate) * Time.deltaTime * LevelManager.main.timing;
        }
        else if (levelStarted == true)
        {
            nectar += (incomeRate) * Time.deltaTime * LevelManager.main.timing;
        }
        honeyGeneratedRatio = honey / honeyRequired;
    }

    public void HitQueen(float damage)
    {
        if (queenBeeHP <= 0f)
        {
            QueenDies();
        }
        else
        {
            queenBeeHP -= damage;
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
        GameObject[] root = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in root)
        {
            if ((flowerMask | (1 << obj.layer)) == flowerMask)
            {
                Array.Resize(ref flowers, flowers.Length + 1);
                flowers[flowers.Length - 1] = obj;
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
        Victory();
    }

    private void Victory()
    {
        if (GlobalValues.main.difficultySetting == "Easy")
        {
            if (SaveFile.gameData.easyScores[GlobalValues.main.levelIndex] < honey)
            {
                SaveFile.gameData.easyScores[GlobalValues.main.levelIndex] = (int)honey;
            }
        }
        else if (GlobalValues.main.difficultySetting == "Medium")
        {
            if (SaveFile.gameData.mediumScores[GlobalValues.main.levelIndex] < honey)
            {
                SaveFile.gameData.mediumScores[GlobalValues.main.levelIndex] = (int)honey;
            }
        }
        else if (GlobalValues.main.difficultySetting == "Hard")
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
    }
}
