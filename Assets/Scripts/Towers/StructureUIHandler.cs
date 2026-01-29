using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class StructureUIHandler : MonoBehaviour, IPointerExitHandler
{
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
    public bool isTower = false;
    private float[] upgradeCost;
    private float sellPrice;
    private float cost;
    private float UIscale;
    private string[] targetingOptions;
    private Attributes attributes;
    private Plot plot;
    private Turret tower;

    private void Start()
    {
        //Scale different between plot and tower prefabs
        UIscale = gameObject.transform.localScale.x;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 0.1f, (Vector2)transform.position, 0f, GlobalValues.main.plotMask | GlobalValues.main.honeyCombMask | GlobalValues.main.flowerMask);
        plot = hits[0].transform.gameObject.GetComponent<Plot>();
        rangeIndicator = Instantiate(rangeIndicator, plot.gameObject.transform);
        rangeIndicator.SetActive(false);
        UpdateUI();
    }

    public void Update()
    {
        float cameraSize = Camera.main.orthographicSize * UIscale;
        var scaleFactor = new Vector3(cameraSize, cameraSize, cameraSize);
        gameObject.transform.localScale = scaleFactor;
    }

    public void UpdateUI()
    {
        tower = null;
        attributes = null;
        //Reset State
        button0.gameObject.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);
        button5.gameObject.SetActive(false);
        button6.gameObject.SetActive(false);
        button7.gameObject.SetActive(false);
        button8.gameObject.SetActive(false);
        UIInfo.gameObject.SetActive(false);
        //Build UI
        if (isTower == true)
        {
            tower = plot.towerObj.GetComponent<Turret>();
            attributes = tower.GetComponent<Attributes>();
            upgradeMatrix = tower.upgradeMatrix;
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
                sellPrice = Mathf.Round((attributes.cost - GlobalValues.main.sellNonrefund) * BuffManager.main.towerSell);
            }
            //sell button
            button3.gameObject.SetActive(true);
            button3.gameObject.GetComponentInChildren<TMP_Text>().text = "Sell: " + sellPrice.ToString() + "Nectar";
            //info button
            button2.gameObject.SetActive(true);
            button2.gameObject.GetComponentInChildren<TMP_Text>().text = "Info";
        }
        else
        {
            if (plot.isBuildable == true)
            {
                if (plot.isHive == true)
                {
                    upgradeMatrix = GlobalValues.main.buildableHive;
                }
                else if (plot.isResourceNode == true)
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
        float discount = 0f;
        if (attributes != null)
        {
            discount = attributes.cost;
        }
        if (upgradeMatrix.Length >= 1)
        {
            button7.gameObject.SetActive(true);
            upgradeCost[0] = upgradeMatrix[0].GetComponent<Attributes>().cost - discount;
            button7.gameObject.GetComponentInChildren<TMP_Text>().text = upgradeMatrix[0].GetComponent<Attributes>().sName + ": " + upgradeCost[0].ToString() + " Nectar";
            if (upgradeMatrix.Length >= 2)
            {
                button0.gameObject.SetActive(true);
                upgradeCost[1] = upgradeMatrix[1].GetComponent<Attributes>().cost - discount;
                button0.gameObject.GetComponentInChildren<TMP_Text>().text = upgradeMatrix[1].GetComponent<Attributes>().sName + ": " + upgradeCost[1].ToString() + " Nectar";
                if (upgradeMatrix.Length >= 3)
                {
                    button1.gameObject.SetActive(true);
                    upgradeCost[2] = upgradeMatrix[2].GetComponent<Attributes>().cost - discount;
                    button1.gameObject.GetComponentInChildren<TMP_Text>().text = upgradeMatrix[2].GetComponent<Attributes>().sName + ": " + upgradeCost[2].ToString() + " Nectar";
                    if (upgradeMatrix.Length >= 4)
                    {
                        button6.gameObject.SetActive(true);
                        upgradeCost[3] = upgradeMatrix[3].GetComponent<Attributes>().cost - discount;
                        button6.gameObject.GetComponentInChildren<TMP_Text>().text = upgradeMatrix[3].GetComponent<Attributes>().sName + ": " + upgradeCost[3].ToString() + " Nectar";
                        if (upgradeMatrix.Length >= 5)
                        {
                            button5.gameObject.SetActive(true);
                            upgradeCost[4] = upgradeMatrix[4].GetComponent<Attributes>().cost - discount;
                            button5.gameObject.GetComponentInChildren<TMP_Text>().text = upgradeMatrix[4].GetComponent<Attributes>().sName + ": " + upgradeCost[4].ToString() + " Nectar";
                        }
                    }
                }
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
        gameObject.SetActive(true);
        OpenRangeUI();
    }

    public void CloseUI()
    {     
        button8.gameObject.SetActive(false);
        UIInfo.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Upgrade(int i)
    {
        if (LevelManager.main.nectar >= upgradeCost[i])
        {
            RaycastHit2D[] plots = Physics2D.CircleCastAll(transform.position, 0.1f, (Vector2)transform.position, 0f, GlobalValues.main.plotMask | GlobalValues.main.flowerMask | GlobalValues.main.honeyCombMask);
            Plot plotScript = plots[0].transform.GetComponent<Plot>();
            LevelManager.main.nectar -= upgradeCost[i];
            plotScript.Build(upgradeMatrix[i]);
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
        LevelManager.main.nectar += sellPrice;
        Destroy(tower.gameObject);
        isTower = false;
        UpdateUI();
        CloseUI();
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
        Upgrade(1);
    }

    public void Button1()
    {
        Upgrade(2);
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
        if (upgradeMatrix.Length >= 5)
        {
            Upgrade(4);
        }
        else
        {
            ChangeTargetSettings();
        }
    }

    public void Button6()
    {
        Upgrade(3);
    }

    public void Button7()
    {
        Upgrade(0);
    }

    public void Button8()
    {
        ConfirmSell();
    }

    public void OpenRangeUI()
    {
        if (isTower)
        {
            float scale = attributes.targetingRange * 2f / plot.gameObject.transform.localScale.x; //Circle scale uses diameter
            rangeIndicator.transform.localScale = new Vector3(scale, scale, 1f);
            rangeIndicator.SetActive(true);
        }      
    }

    public void CloseRangeUI()
    {
        if (isTower)
        {
            rangeIndicator.SetActive(false);
        }           
    }
}
