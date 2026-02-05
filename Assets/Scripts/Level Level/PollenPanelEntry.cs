using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PollenPanelEntry : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI pollenColor;
    [SerializeField] private TextMeshProUGUI pollenTier;
    [SerializeField] private TextMeshProUGUI pollenAmountText;
    [SerializeField] private Slider pollenSlider;

    //Trackers
    private string color;
    private FlowerType flowerType;
    private int tier;
    private float pollenAmount;
    private float[] tierThresholds;
    private int indexGlobal;
    private int indexLevel;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Setup(FlowerType type)
    {
        flowerType = type;
        color = GlobalValues.main.flowerNames[(int)type];
        indexLevel = FindLevelIndex(type);
        indexGlobal = FindGlobalIndex(type);
        tierThresholds = GlobalValues.main.FLOWERPollenThreshold[indexGlobal];
        RefreshPollen();
        pollenColor.text = color;
        gameObject.SetActive(true);
    }

    public void RefreshPollen()
    {
        tier = PollenManager.main.flowerLevel[indexLevel];
        pollenAmount = PollenManager.main.flowerPollen[indexLevel];
        RefreshUI();
    }

    private void RefreshUI()
    {
        pollenTier.text = "Tier " + tier;
        pollenAmountText.text = pollenAmount + " / " + tierThresholds[tier - 1];
    }

    private int FindGlobalIndex(FlowerType type)
    {
        return (int)type;
    }

    private int FindLevelIndex(FlowerType type)
    {
        return Array.IndexOf(PollenManager.main.flowersAvailable, type);
    }
}
