using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class HoneyPanelEntry : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI honeyBuffText;
    [SerializeField] private TextMeshProUGUI honeyBuffAmount;
    [SerializeField] private TextMeshProUGUI honeyBuffAmountText;
    [SerializeField] private Slider honeySlider;
    [SerializeField] private Image honeySliderImage;

    //Trackers
    private int indexGlobal;
    private int indexLevel;
    private HoneyBuff honeyBuff;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Setup(int index)
    {
        indexLevel = index;
        honeyBuff = LevelManager.main.honeyBuffs[index];
        indexGlobal = (int)honeyBuff;
        honeyBuffText.text = GlobalValues.main.honeyBuffText[indexGlobal];
        if (GlobalValues.main.honeyBuffUnit[indexGlobal] == "%") 
        {
            honeyBuffAmount.text = (LevelManager.main.honeyBuffAmount[indexLevel] * 100).ToString() + GlobalValues.main.honeyBuffUnit[indexGlobal];
        }
        else
        {
            honeyBuffAmount.text = LevelManager.main.honeyBuffAmount[indexLevel].ToString() + GlobalValues.main.honeyBuffUnit[indexGlobal];
        }
        honeyBuffAmountText.text = LevelManager.main.honey + " / " + Mathf.Round(HoneyBuffManager.main.honeyThresholds[indexLevel]);
        honeySlider.maxValue = HoneyBuffManager.main.honeyThresholds[indexLevel];
        gameObject.SetActive(true);
    }

    public void RefreshUI()
    {
        if (LevelManager.main.honey >= HoneyBuffManager.main.honeyThresholds[indexLevel]) 
        {
            honeyBuffAmountText.text = "Buff Unlocked";
            honeySlider.value = honeySlider.maxValue;
        }
        else 
        {
            honeyBuffAmountText.text = Mathf.Round(LevelManager.main.honey) + " / " + HoneyBuffManager.main.honeyThresholds[indexLevel];
            honeySlider.value = LevelManager.main.honey;
        }
    }
}
