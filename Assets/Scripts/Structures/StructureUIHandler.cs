using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

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
    [SerializeField] public GameObject[] upgradeMatrix;
    private float[] upgradeCost;
    private float[] unitCost;
    private float sellPrice;
    private float cost;
    private int index;
    private bool isHive = false;
    private bool isFlower = false;
    private bool isBuildable = false;
    private float UIscale;
    private string[] targetingOptions;
    public bool influence = false;
    private GameObject plot;
    private GameObject[] units;
    private Attributes attributes;

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    private void Start()
    {
        attributes = gameObject.GetComponent<Attributes>();
        //Build UI
        if (isTower == true) 
        {
            //Scale different between plot and tower prefabs
            UIscale = UI.transform.localScale.x / 2f;
            if (attributes.hasTargetSettings == true)
            {
                button5.gameObject.SetActive(true);
                button5.gameObject.GetComponentInChildren<TMP_Text>().text = "Targeting: " + attributes.targetSetting;
            }
            if (attributes.sName == "Scout Tower")
            {
                sellPrice = 0f;
            }
            else
            {
                sellPrice = Mathf.Round((attributes.cost) * GlobalValues.main.sellRatio);
            }
            //sell button
            button3.gameObject.SetActive(true);
            button3.gameObject.GetComponentInChildren<TMP_Text>().text = "Sell: " + sellPrice.ToString() + "Nectar";
            //info button
            button2.gameObject.SetActive(true);
            button2.gameObject.GetComponentInChildren<TMP_Text>().text = "Info";
            plot = Physics2D.CircleCastAll(transform.position, 0.1f, (Vector2)transform.position, 0f, GlobalValues.main.plotMask | GlobalValues.main.flowerMask)[0].transform.gameObject;
            isHive = plot.GetComponent<Plot>().isHive;
        }
        else 
        {
            //Scale different between plot and tower prefabs
            UIscale = UI.transform.localScale.x / 2.28f;
            isHive = gameObject.GetComponent<Plot>().isHive;
            isBuildable = gameObject.GetComponent<Plot>().isBuildable;
            isFlower = gameObject.GetComponent<Plot>().isResourceNode;
            if (isBuildable == true) 
            {
                if (isHive == true) 
                {
                    upgradeMatrix = GlobalValues.main.buildableHive;
                }
                else if (isFlower == true)
                {
                    upgradeMatrix = GlobalValues.main.buildableFlower;
                }
                else 
                {
                    upgradeMatrix = GlobalValues.main.buildable;
                }
            }
        }
        //exit button
        button4.gameObject.SetActive(true);
        button4.gameObject.GetComponentInChildren<TMP_Text>().text = "Exit";
        //upgrade / Purchase options
        Array.Resize(ref upgradeCost, upgradeMatrix.Length);
        Array.Resize(ref unitCost, upgradeMatrix.Length);
        if (upgradeMatrix.Length >= 1)
        {
            if (!isTower)
            {
                button7.gameObject.SetActive(true);
                unitCost[0] = upgradeMatrix[0].GetComponent<Attributes>().cost;
                button7.gameObject.GetComponentInChildren<TMP_Text>().text = upgradeMatrix[0].GetComponent<Attributes>().sName + ": " + unitCost[0].ToString() + " Nectar";
                if (upgradeMatrix.Length >= 2)
                {
                    button0.gameObject.SetActive(true);
                    unitCost[1] = upgradeMatrix[1].GetComponent<Attributes>().cost;
                    button0.gameObject.GetComponentInChildren<TMP_Text>().text = upgradeMatrix[1].GetComponent<Attributes>().sName + ": " + unitCost[1].ToString() + " Nectar";
                    if (upgradeMatrix.Length >= 3)
                    {
                        button1.gameObject.SetActive(true);
                        unitCost[2] = upgradeMatrix[2].GetComponent<Attributes>().cost;
                        button1.gameObject.GetComponentInChildren<TMP_Text>().text = upgradeMatrix[2].GetComponent<Attributes>().sName + ": " + unitCost[2].ToString() + " Nectar";
                        if (upgradeMatrix.Length >= 4)
                        {
                            button6.gameObject.SetActive(true);
                            unitCost[3] = upgradeMatrix[3].GetComponent<Attributes>().cost;
                            button6.gameObject.GetComponentInChildren<TMP_Text>().text = upgradeMatrix[3].GetComponent<Attributes>().sName + ": " + unitCost[3].ToString() + " Nectar";
                            if (upgradeMatrix.Length >= 5)
                            {
                                button5.gameObject.SetActive(true);
                                unitCost[4] = upgradeMatrix[4].GetComponent<Attributes>().cost;
                                button5.gameObject.GetComponentInChildren<TMP_Text>().text = upgradeMatrix[4].GetComponent<Attributes>().sName + ": " + unitCost[4].ToString() + " Nectar";
                            }
                        }
                    }
                }
            }
            else
            {
                button7.gameObject.SetActive(true);
                upgradeCost[0] = upgradeMatrix[0].GetComponent<Attributes>().cost - attributes.cost;
                button7.gameObject.GetComponentInChildren<TMP_Text>().text = upgradeMatrix[0].GetComponent<Attributes>().sName + ": " + upgradeCost[0].ToString() + " Nectar";
                if (upgradeMatrix.Length >= 2)
                {
                    button0.gameObject.SetActive(true);
                    upgradeCost[1] = upgradeMatrix[1].GetComponent<Attributes>().cost - attributes.cost;
                    button0.gameObject.GetComponentInChildren<TMP_Text>().text = upgradeMatrix[1].GetComponent<Attributes>().sName + ": " + upgradeCost[1].ToString() + " Nectar";
                    if (upgradeMatrix.Length >= 3)
                    {
                        button1.gameObject.SetActive(true);
                        upgradeCost[2] = upgradeMatrix[2].GetComponent<Attributes>().cost - attributes.cost;
                        button1.gameObject.GetComponentInChildren<TMP_Text>().text = upgradeMatrix[2].GetComponent<Attributes>().sName + ": " + upgradeCost[2].ToString() + " Nectar";
                        if (upgradeMatrix.Length >= 4)
                        {
                            button6.gameObject.SetActive(true);
                            upgradeCost[3] = upgradeMatrix[3].GetComponent<Attributes>().cost - attributes.cost;
                            button6.gameObject.GetComponentInChildren<TMP_Text>().text = upgradeMatrix[3].GetComponent<Attributes>().sName + ": " + upgradeCost[3].ToString() + " Nectar";
                            if (upgradeMatrix.Length >= 5)
                            {
                                button5.gameObject.SetActive(true);
                                upgradeCost[4] = upgradeMatrix[4].GetComponent<Attributes>().cost - attributes.cost;
                                button5.gameObject.GetComponentInChildren<TMP_Text>().text = upgradeMatrix[4].GetComponent<Attributes>().sName + ": " + upgradeCost[4].ToString() + " Nectar";
                            }
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
                sellPrice = Mathf.Round((attributes.cost) * GlobalValues.main.sellRatio * (WaveSpawner.main.numberOfWaves - WaveSpawner.main.currentWave) / WaveSpawner.main.numberOfWaves);
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
        if (upgradeMatrix.Length >= 1)
        {
            button7.gameObject.SetActive(true);
            if (upgradeMatrix.Length >= 2)
            {
                button0.gameObject.SetActive(true);
                if (upgradeMatrix.Length >= 3)
                {
                    button1.gameObject.SetActive(true);
                    if (upgradeMatrix.Length >= 4)
                    {
                        button6.gameObject.SetActive(true);
                        if (upgradeMatrix.Length >= 5)
                        {
                            button5.gameObject.SetActive(true);
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

    public void Upgrade(int i)
    {
        if (LevelManager.main.nectar >= upgradeCost[i])
        {
            RaycastHit2D[] plots = Physics2D.CircleCastAll(transform.position, 0.1f, (Vector2)transform.position, 0f, GlobalValues.main.plotMask | GlobalValues.main.flowerMask);
            Plot plotScript = plots[0].transform.GetComponent<Plot>();
            LevelManager.main.nectar -= upgradeCost[i];
            plotScript.Build(upgradeMatrix[i]);
        }
    }

    public void BuyUnit(int i)
    {
        if (LevelManager.main.nectar >= unitCost[i])
        {
            LevelManager.main.nectar -= unitCost[i];
            GameObject prefabToSpawn = upgradeMatrix[i]; ;
            Transform start = gameObject.transform;
            Transform nextPoint = LevelManager.main.queenBee.transform;
            float angle = Mathf.Atan2(LevelManager.main.queenBee.transform.position.y - gameObject.transform.position.y, LevelManager.main.queenBee.transform.position.x - gameObject.transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion unitRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            GameObject unit = Instantiate(prefabToSpawn, gameObject.transform.position, unitRotation);
            CloseUI();
            //Array.Resize(ref units, units.Length + 1);
            //units[units.Length - 1] = unit;
        }
    }

    public void Info()
    {
        string name = attributes.sName;
        string damage = "N/A";
        string attSpeed = "N/A";
        string targRange = "N/A";
        string igTerrain = "N/A";
        if (gameObject.GetComponent<Attributes>() != null)
        {
            damage = attributes.actionPower.ToString();
            attSpeed = attributes.actionRate.ToString();
            targRange = attributes.targetingRange.ToString();
            igTerrain = attributes.ignoreTerrain.ToString();
        }
        string info = name + " Information\n_______________\n\nDamage: " + damage + "\nAttack Speed: " + attSpeed + "\nTargeting Range: " + targRange + "\nEffect Ratio: " + "\nWill Ignore Terrain: " + igTerrain + "\nnectar Generation: " + "\n\nSell Value: " + sellPrice.ToString();
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
        Destroy(gameObject);
    }

    private void ChangeTargetSettings()
    {
        attributes.targetingIndex++;
        if (attributes.targetingIndex >= attributes.targetingOptions.Length)
        {
            attributes.targetingIndex = 0;
        }
        attributes.targetSetting = attributes.targetingOptions[attributes.targetingIndex];
        button5.gameObject.GetComponentInChildren<TMP_Text>().text = "Targeting: " + attributes.targetSetting;
        if (attributes != null)
        {
            attributes.target = null;
        }
    }

    public void Button0()
    {
        if (((1 << gameObject.layer) & GlobalValues.main.incomeMask) != 0)
        {
            BuyUnit(1);
        }
        else
        {
            Upgrade(1);
        }
    }

    public void Button1()
    {
        if (((1 << gameObject.layer) & GlobalValues.main.incomeMask) != 0)
        {
            BuyUnit(2);
        }
        else
        {
            Upgrade(2);
        }
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
        if (attributes.hasTargetSettings == true)
        {
            ChangeTargetSettings();
        }
        else if (upgradeMatrix.Length >= 5)
        {
            if (((1 << gameObject.layer) & GlobalValues.main.incomeMask) != 0)
            {
                BuyUnit(4);
            }
            else
            {
                Upgrade(4);
            }
        }
    }

    public void Button6()
    {
        if (((1 << gameObject.layer) & GlobalValues.main.incomeMask) != 0)
        {
            BuyUnit(3);
        }
        else
        {
            Upgrade(3);
        }
    }

    public void Button7()
    {
        if (((1 << gameObject.layer) & GlobalValues.main.incomeMask) != 0)
        {
            BuyUnit(0);
        }
        else
        {
            Upgrade(0);
        }
    }

    public void Button8()
    {
        ConfirmSell();
    }

    private IEnumerator RevealFog()
    {
        float range = attributes.targetingRange;

        if (range != 0f)
        {
            //find resource nodes
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, GlobalValues.main.flowerMask);
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
            hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, GlobalValues.main.plotMask);
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
            hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, GlobalValues.main.obstructionMask);
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
        yield return new WaitForSeconds(0f);
    }

    public void OpenRangeUI()
    {
        if (isTower == true)
        {
            float scale = attributes.targetingRange * 2f; //Circle scale uses diameter
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
}
