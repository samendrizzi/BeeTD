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
    [SerializeField] private Image pollenSliderImage;

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
        indexLevel = FindLevelIndex();
        indexGlobal = FindGlobalIndex();
        tierThresholds = GlobalValues.main.FLOWERPollenThreshold[indexGlobal];
        RefreshPollen(type);
        SetSliderColor();
        pollenColor.text = color;
        gameObject.SetActive(true);
    }

    public void RefreshPollen(FlowerType type)
    {
        if (type != flowerType)
        {
            return;
        }
        tier = PollenManager.main.flowerLevel[indexLevel];
        pollenAmount = PollenManager.main.flowerPollen[indexLevel];
        RefreshUI();
    }

    private void RefreshUI()
    {
        pollenTier.text = "Tier " + tier;
        if (tier >= tierThresholds.Length)
        {
            pollenAmountText.text = "MAX Tier" ;
            pollenSlider.maxValue = 1f;
            pollenSlider.value = 1f;
        }
        else if (tier > 0)
        {
            pollenAmountText.text = pollenAmount + " / " + tierThresholds[tier];
            pollenSlider.maxValue = tierThresholds[tier] - tierThresholds[tier - 1];
            pollenSlider.value = pollenAmount - tierThresholds[tier - 1];
        }
        else
        {
            pollenAmountText.text = pollenAmount + " / " + tierThresholds[tier];
            pollenSlider.maxValue = tierThresholds[tier];
            pollenSlider.value = pollenAmount;
        }
    }

    private int FindGlobalIndex()
    {
        return (int)flowerType;
    }

    private int FindLevelIndex()
    {
        return Array.IndexOf(PollenManager.main.flowersAvailable, flowerType);
    }

    private void SetSliderColor()
    {
        pollenAmountText.color = Color.black;
        pollenTier.color = Color.black;
        pollenColor.color = Color.black;
        if (flowerType == FlowerType.ORANGE)
        {
            pollenSliderImage.color = Color.orange;
        }
        else if (flowerType == FlowerType.BROWN)
        {
            pollenSliderImage.color = Color.brown;
        }
        else if (flowerType == FlowerType.GREEN)
        {
            pollenSliderImage.color = Color.green;
        }
        else if (flowerType == FlowerType.BLUE)
        {
            pollenSliderImage.color = Color.blue;
        }
        else if (flowerType == FlowerType.YELLOW)
        {
            pollenSliderImage.color = Color.yellow;
        }
        else if (flowerType == FlowerType.RED)
        {
            pollenSliderImage.color = Color.red;
        }
        else if (flowerType == FlowerType.BLACK)
        {
            pollenSliderImage.color = Color.black;
            pollenAmountText.color = Color.grey;
            pollenTier.color = Color.grey;
            pollenColor.color = Color.grey;
        }
        else if (flowerType == FlowerType.WHITE)
        {
            pollenSliderImage.color = Color.white;
        }
        else if (flowerType == FlowerType.PURPLE)
        {
            pollenSliderImage.color = Color.purple;
        }
        else if (flowerType == FlowerType.PINK)
        {
            pollenSliderImage.color = Color.pink;
        }
        else if (flowerType == FlowerType.GOLD)
        {
            pollenSliderImage.color = Color.gold;
        }
    }
}
