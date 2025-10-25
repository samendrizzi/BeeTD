using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class StructureUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private bool isTower = false;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject UIInfo;
    [SerializeField] private TMP_Text UIText;
    [SerializeField] private Button button0;
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button button3;
    [SerializeField] private Button button4;
    [SerializeField] private Button button5;
    [SerializeField] private Button button6;
    [SerializeField] private Button button7;
    [SerializeField] private Button button8;
    [SerializeField] private GameObject rangeIndicator;
    private float upgrade1Cost;
    private float upgrade2Cost;
    private float upgrade3Cost;
    private float upgrade4Cost;
    private float upgrade5Cost;
    private float sellPrice;
    private float cost;
    private int index;
    private int[] upgradeIndex = new int[] { };
    private bool isHive = false;
    private bool isFlower = false;
    private bool isBuildable = false;
    private float UIscale;
    private bool hasTargetSettings = false;
    public string targetSetting = "Near";
    private string[] targetingOptions;
    public int targetingIndex = 0;
    public bool influence = false;
    private GameObject plot;

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    private void Start()
    {
        CheckInfluence();
        index = -1;
        //Build UI
        if (isTower == true) 
        {
            //Scale different between plot and tower prefabs
            UIscale = UI.transform.localScale.x / 2f;
            index = GetComponent<Identify>().ID;
            upgradeIndex = GlobalValues.main.TOWERupgradeIndex[index];
            hasTargetSettings = GlobalValues.main.TOWERhasTargetSettings[index];
            targetingOptions = GlobalValues.main.targetingOptions;
            if (hasTargetSettings == true)
            {
                button5.gameObject.SetActive(true);
                button5.gameObject.GetComponentInChildren<TMP_Text>().text = "Targeting: " + targetSetting;
            }
            cost = GlobalValues.main.TOWERcost[index];
            if (index == 0)
            {
                sellPrice = 0f;
            }
            else
            {
                sellPrice = Mathf.Round((cost) * GlobalValues.main.sellRatio);
            }
            //sell button
            button3.gameObject.SetActive(true);
            button3.gameObject.GetComponentInChildren<TMP_Text>().text = "Sell: " + sellPrice.ToString() + "Nectar";
            //info button
            button2.gameObject.SetActive(true);
            button2.gameObject.GetComponentInChildren<TMP_Text>().text = "Info";
            plot = Physics2D.CircleCastAll(transform.position, 0.1f, (Vector2)transform.position, 0f, GlobalValues.main.plotMask)[0].transform.gameObject;
            isHive = plot.GetComponent<Plot>().isHive;
        }
        else 
        {
            //Scale different between plot and tower prefabs
            UIscale = UI.transform.localScale.x / 2.28f;
            isHive = gameObject.GetComponent<Plot>().isHive;
            isBuildable = gameObject.GetComponent<Plot>().isBuildable;
            cost = 0f;
            float range = GlobalValues.main.flowerRange; ;
            LayerMask flowerMask = GlobalValues.main.flowerMask;
            RaycastHit2D[] flowers = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, flowerMask);
            if (flowers.Length > 0) 
            {
                isFlower = true;
            }
            if (isBuildable == true) 
            {
                if (isHive == true) 
                {
                    upgradeIndex = GlobalValues.main.buildableHive;
                }
                else if (isFlower == true)
                {
                    upgradeIndex = GlobalValues.main.buildableFlower;
                }
                else 
                {
                    upgradeIndex = GlobalValues.main.buildable;
                }
            }
        }
        //exit button
        button4.gameObject.SetActive(true);
        button4.gameObject.GetComponentInChildren<TMP_Text>().text = "Exit";
        //upgrade options
        if (upgradeIndex.Length >= 1)
        {
            button7.gameObject.SetActive(true);
            upgrade1Cost = GlobalValues.main.TOWERcost[upgradeIndex[0]] - cost;
            button7.gameObject.GetComponentInChildren<TMP_Text>().text = GlobalValues.main.TOWERname[upgradeIndex[0]] + ": " + upgrade1Cost.ToString() + " Nectar";
            if (upgradeIndex.Length >= 2)
            {
                button0.gameObject.SetActive(true);
                upgrade2Cost = GlobalValues.main.TOWERcost[upgradeIndex[1]] - cost;
                button0.gameObject.GetComponentInChildren<TMP_Text>().text = GlobalValues.main.TOWERname[upgradeIndex[1]] + ": " + upgrade2Cost.ToString() + " Nectar";
                if (upgradeIndex.Length >= 3)
                {
                    button1.gameObject.SetActive(true);
                    upgrade3Cost = GlobalValues.main.TOWERcost[upgradeIndex[2]] - cost;
                    button1.gameObject.GetComponentInChildren<TMP_Text>().text = GlobalValues.main.TOWERname[upgradeIndex[2]] + ": " + upgrade3Cost.ToString() + " Nectar";
                    if (upgradeIndex.Length >= 4)
                    {
                        button6.gameObject.SetActive(true);
                        upgrade4Cost = GlobalValues.main.TOWERcost[upgradeIndex[3]] - cost;
                        button6.gameObject.GetComponentInChildren<TMP_Text>().text = GlobalValues.main.TOWERname[upgradeIndex[3]] + ": " + upgrade4Cost.ToString() + " Nectar";
                        if (upgradeIndex.Length >= 5)
                        {
                            button5.gameObject.SetActive(true);
                            upgrade5Cost = GlobalValues.main.TOWERcost[upgradeIndex[4]] - cost;
                            button5.gameObject.GetComponentInChildren<TMP_Text>().text = GlobalValues.main.TOWERname[upgradeIndex[4]] + ": " + upgrade5Cost.ToString() + " Nectar";
                        }
                    }
                }
            }
        }
        if (isTower == true)
        {
            StartCoroutine(RevealFog());
        } 
    }

    public void Update()
    {
        float cameraSize = Camera.main.orthographicSize * UIscale;
        if (UI.activeSelf == true)
        {
            var scaleFactor = new Vector3(cameraSize, cameraSize, cameraSize);
            UI.transform.localScale = scaleFactor;
            if ((GlobalValues.main.investmentMask & (1 << gameObject.layer)) != 0 || (GlobalValues.main.incomeMask & (1 << gameObject.layer)) != 0)
            {
                //Depreciated as level progresses
                sellPrice = Mathf.Round((cost) * GlobalValues.main.sellRatio * (WaveSpawner.main.numberOfWaves - WaveSpawner.main.currentWave) / WaveSpawner.main.numberOfWaves);
                button3.gameObject.GetComponentInChildren<TMP_Text>().text = "Sell: " + sellPrice.ToString() + " Nectar";
            }
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CloseUI();
        CloseRangeUI();
    }

    public void OpenUI()
    {
        //Check influence
        influence = CheckInfluence();
        if (upgradeIndex.Length >= 1)
        {
            if (influence == true)
            {
                button7.gameObject.SetActive(true);
                if (upgradeIndex.Length >= 2)
                {
                    button0.gameObject.SetActive(true);
                    if (upgradeIndex.Length >= 3)
                    {
                        button1.gameObject.SetActive(true);
                        if (upgradeIndex.Length >= 4)
                        {
                            button6.gameObject.SetActive(true);
                            if (upgradeIndex.Length >= 5)
                            {
                                button5.gameObject.SetActive(true);
                            }
                        }
                    }
                }
            }
            else
            {
                button7.gameObject.SetActive(false);
                if (upgradeIndex.Length >= 2)
                {
                    button0.gameObject.SetActive(false);
                    if (upgradeIndex.Length >= 3)
                    {
                        button1.gameObject.SetActive(false);
                        if (upgradeIndex.Length >= 4)
                        {
                            button6.gameObject.SetActive(false);
                            if (upgradeIndex.Length >= 5)
                            {
                                button5.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
        UI.SetActive(true);
        OpenRangeUI();
    }

    public void CloseUI()
    {     
        button8.gameObject.SetActive(false);
        UIInfo.SetActive(false);
        UI.SetActive(false);
    }

    public void Upgrade1()
    {
        if (LevelManager.main.nectar >= upgrade1Cost)
        {
            RaycastHit2D[] plots = Physics2D.CircleCastAll(transform.position, 0.1f, (Vector2)transform.position, 0f, GlobalValues.main.plotMask);
            Plot plotScript = plots[0].transform.GetComponent<Plot>();
            LevelManager.main.nectar -= upgrade1Cost;
            plotScript.Build(upgradeIndex[0]);
        }
    }

    public void Upgrade2()
    {
        if (LevelManager.main.nectar >= upgrade2Cost)
        {
            RaycastHit2D[] plots = Physics2D.CircleCastAll(transform.position, 0.1f, (Vector2)transform.position, 0f, GlobalValues.main.plotMask);
            Plot plotScript = plots[0].transform.GetComponent<Plot>();
            LevelManager.main.nectar -= upgrade2Cost;
            plotScript.Build(upgradeIndex[1]);
        }
    }

    public void Upgrade3()
    {
        if (LevelManager.main.nectar >= upgrade3Cost)
        {
            RaycastHit2D[] plots = Physics2D.CircleCastAll(transform.position, 0.1f, (Vector2)transform.position, 0f, GlobalValues.main.plotMask);
            Plot plotScript = plots[0].transform.GetComponent<Plot>();
            LevelManager.main.nectar -= upgrade3Cost;
            plotScript.Build(upgradeIndex[2]);
        }
    }

    public void Upgrade4()
    {
        if (LevelManager.main.nectar >= upgrade4Cost)
        {
            RaycastHit2D[] plots = Physics2D.CircleCastAll(transform.position, 0.1f, (Vector2)transform.position, 0f, GlobalValues.main.plotMask);
            Plot plotScript = plots[0].transform.GetComponent<Plot>();
            LevelManager.main.nectar -= upgrade4Cost;
            plotScript.Build(upgradeIndex[3]);
        }
    }

    public void Upgrade5()
    {
        if (LevelManager.main.nectar >= upgrade5Cost)
        {
            RaycastHit2D[] plots = Physics2D.CircleCastAll(transform.position, 0.1f, (Vector2)transform.position, 0f, GlobalValues.main.plotMask);
            Plot plotScript = plots[0].transform.GetComponent<Plot>();
            LevelManager.main.nectar -= upgrade5Cost;
            plotScript.Build(upgradeIndex[4]);
        }
    }

    public void Info()
    {
        string name = GlobalValues.main.TOWERname[index];
        string damage = "N/A";
        string attSpeed = "N/A";
        string targRange = "N/A";
        string effRatio = "N/A";
        string eff = "N/A";
        string effDur = "N/A";
        string efficiency = "N/A";
        string igTerrain = "N/A";
        string hGen = "N/A";
        string hInvest = "N/A";
        string hBonus = "N/A";
        string flower = "N/A";
        int flowerIndex;
        if (gameObject.GetComponent<Turret>() != null)
        {
            damage = gameObject.GetComponent<Turret>().damage.ToString();
            attSpeed = gameObject.GetComponent<Turret>().aps.ToString();
            targRange = gameObject.GetComponent<Turret>().targetingRange.ToString();
            eff = gameObject.GetComponent<Turret>().effect;
            effRatio = gameObject.GetComponent<Turret>().effectRatio.ToString();
            effDur = gameObject.GetComponent<Turret>().effectDuration.ToString();
            efficiency = gameObject.GetComponent<Turret>().efficiency.ToString();
            igTerrain = gameObject.GetComponent<Turret>().ignoreTerrain.ToString();
            hGen = gameObject.GetComponent<Turret>().generationRate.ToString();
            hInvest = gameObject.GetComponent<Turret>().investmentRate.ToString();
            hBonus = gameObject.GetComponent<Turret>().honeyRate.ToString();
            flowerIndex = gameObject.GetComponent<Turret>().flowerIndex;
            if (flowerIndex >= 0)
            {
                flower = GlobalValues.main.FLOWERText[flowerIndex];
            }
        }
        string info = GlobalValues.main.TOWERname[index] + " Information\n_______________\n\nDamage: " + damage + "\nAttack Speed: " + attSpeed + "\nTargeting Range: " + targRange + "\nEffect Ratio: " + effRatio + "\nEffect Duration: " + effDur + "\nEfficiency: " + efficiency + "\nWill Ignore Terrain: " + igTerrain + "\nnectar Generation: " + hGen  + "\nnectar Investment: " + hInvest + "\nBonus Investment: " + hBonus + "\n\nFlower: " + flower + "\n\nSell Value: " + sellPrice.ToString();
        UIText.text = info;
        UIInfo.SetActive(!UIInfo.activeSelf);
    }

    public void Sell()
    {
        button8.gameObject.SetActive(true);
    }

    public void ConfirmSell()
    {
        CloseUI();
        LevelManager.main.nectar += sellPrice;
        gameObject.GetComponent<Turret>().generationRate = 0f;
        gameObject.GetComponent<Turret>().honeyRate = 0f;
        gameObject.GetComponent<Turret>().investmentRate = 0f;
        LevelManager.main.CalculateInvestment();
        LevelManager.main.CalculateIncome();
        Destroy(gameObject);
    }

    private void ChangeTargetSettings()
    {
        targetingIndex++;
        if (targetingIndex >= targetingOptions.Length)
        {
            targetingIndex = 0;
        }
        targetSetting = targetingOptions[targetingIndex];
        button5.gameObject.GetComponentInChildren<TMP_Text>().text = "Targeting: " + targetSetting;
        if (gameObject.GetComponent<Turret>() != null)
        {
            gameObject.GetComponent<Turret>().targetSetting = targetSetting;
            gameObject.GetComponent<Turret>().target = null;
        }
    }

    public void Button0()
    {
        Upgrade2();
    }

    public void Button1()
    {
        Upgrade3();
    }

    public void Button2()
    {
        Info();
    }

    public void Button3()
    {
        Sell();
    }

    public void Button4()
    {
        CloseUI();
    }

    public void Button5()
    {
        if (hasTargetSettings == true)
        {
            ChangeTargetSettings();
        }
        else if (upgradeIndex.Length >= 5)
        {
            Upgrade5();
        }
    }

    public void Button6()
    {
        Upgrade4();
    }

    public void Button7()
    {
        Upgrade1();
    }

    public void Button8()
    {
        ConfirmSell();
    }

    private IEnumerator RevealFog()
    {
        yield return new WaitForSeconds(0.1f);
        float targetingRange = GlobalValues.main.TOWERtargetingRange[index];

        if (targetingRange != 0f)
        {
            //find resource nodes
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, GlobalValues.main.flowerMask);
            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform.GetComponent<Plot>().fog == true)
                    {
                        if (!Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))
                        {
                            hits[i].transform.GetComponent<Plot>().Found();
                        }
                    }
                }
            }
            //find plots
            hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, GlobalValues.main.plotMask);
            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform.GetComponent<Plot>().fog == true)
                    {
                        if (!Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))
                        {
                            hits[i].transform.GetComponent<Plot>().Found();
                        }
                    }
                }
            }
            //find obstructions
            hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, GlobalValues.main.obstructionMask);
            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform.GetComponent<Plot>().fog == true)
                    {
                        //if (!Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))
                        //{
                            hits[i].transform.GetComponent<Plot>().Found();
                        //}
                    }
                }
            }
        }
        else
        {
            Debug.Log(gameObject.name + " has no targeting range.");
        }
    }

    public void OpenRangeUI()
    {
        if (isTower == true)
        {
            float scale = gameObject.GetComponent<Turret>().targetingRange * 2f; //Circle scale uses diameter
            rangeIndicator.transform.localScale = new Vector3(scale, scale, 1f);
            rangeIndicator.SetActive(true);
        }      
    }

    public void CloseRangeUI()
    {
        if (isTower == true)
        {
            rangeIndicator.SetActive(false);
        }           
    }

    public bool CheckInfluence()
    {
        if (isHive || Vector2.Distance(transform.position, LevelManager.main.influenceCenter.position) <= Influence.main.currentRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
