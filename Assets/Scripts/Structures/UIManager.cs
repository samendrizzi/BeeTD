using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager main;

    [Header("References")]
    [SerializeField] public TextMeshProUGUI nectarCounterUI;
    [SerializeField] public TextMeshProUGUI honeyCounterRequiredUI;
    [SerializeField] public TextMeshProUGUI waveSpawnCounterUI;
    [SerializeField] public TextMeshProUGUI WaveTrackerUI;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button normalSpeedButton;
    [SerializeField] private Button fastSpeedButton;
    [SerializeField] private Button veryFastSpeedButton;
    [SerializeField] public GameObject victoryUI;
    [SerializeField] public GameObject defeatUI;
    [SerializeField] public Slider queenHealthBar;
    [SerializeField] public Slider honeyRequiredBar;
    [SerializeField] public TextMeshProUGUI queenHealthNumbered;
    [SerializeField] public GameObject[] unitsToBuy;
    public string speed = "Normal";
    public bool pause = false;
    private Color normalColor = Color.white;
    private Color pressedColor = Color.grey;


    private void Awake()
    {
        main = this;
        if (!LevelManager.main)
        {
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        queenHealthBar.maxValue = LevelManager.main.queenBeeMaxHP;
        honeyRequiredBar.maxValue = LevelManager.main.honeyRequired;
        //Set Speed
        NormalSpeed();
    }

    private void Update()
    {
        nectarCounterUI.text = Mathf.Round(LevelManager.main.nectar).ToString();
        honeyRequiredBar.value = Mathf.Round(LevelManager.main.honey);
        honeyCounterRequiredUI.text = Mathf.Round(LevelManager.main.honey).ToString() + " / " + LevelManager.main.honeyRequired;
        waveSpawnCounterUI.text = Mathf.Round(GameObject.Find("LevelManager").GetComponent<WaveSpawner>().waveCountdown).ToString();
        queenHealthBar.value = LevelManager.main.queenBeeHP;
        queenHealthNumbered.text = Mathf.Round(LevelManager.main.queenBeeHP).ToString() + " / " + Mathf.Round(LevelManager.main.queenBeeMaxHP).ToString();
    }


    public void WaveUpdate()
    {
        WaveTrackerUI.text = "Wave " + WaveSpawner.main.currentWave + " of " + WaveSpawner.main.numberOfWaves;
    }

    public void VictoryUI()
    {
        victoryUI.SetActive(true);
    }

    public void DefeatUI()
    {
        defeatUI.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Additive);
        GlobalValues.main.SetUI(false);
        SceneManager.UnloadSceneAsync(LevelManager.main.gameObject.scene);
    }

    public void TogglePause()
    {
        pause = !pause;
        LevelManager.main.ChangeTiming(speed, pause);
        if (pause == true)
        {
            pauseButton.GetComponent<Image>().color = pressedColor;
        }
        else
        {
            pauseButton.GetComponent<Image>().color = normalColor;
        }
    }

    public void NormalSpeed()
    {
        speed = "Normal";
        if (pause == true)
        {
            TogglePause();
        }
        LevelManager.main.ChangeTiming(speed, pause);
        normalSpeedButton.GetComponent<Image>().color = pressedColor;
        fastSpeedButton.GetComponent<Image>().color = normalColor;
        veryFastSpeedButton.GetComponent<Image>().color = normalColor;
    }

    public void FastSpeed()
    {
        speed = "Fast";
        if (pause == true)
        {
            TogglePause();
        }
        LevelManager.main.ChangeTiming(speed, pause);
        normalSpeedButton.GetComponent<Image>().color = normalColor;
        fastSpeedButton.GetComponent<Image>().color = pressedColor;
        veryFastSpeedButton.GetComponent<Image>().color = normalColor;
    }

    public void VeryFastSpeed()
    {
        speed = "Very Fast";
        if (pause == true)
        {
            TogglePause();
        }
        LevelManager.main.ChangeTiming(speed, pause);
        normalSpeedButton.GetComponent<Image>().color = normalColor;
        fastSpeedButton.GetComponent<Image>().color = normalColor;
        veryFastSpeedButton.GetComponent<Image>().color = pressedColor;
    }

    public void Reset()
    {
        victoryUI.SetActive(false);
        defeatUI.SetActive(false);
        queenHealthBar.maxValue = LevelManager.main.queenBeeMaxHP;
        honeyRequiredBar.maxValue = LevelManager.main.honeyRequired;
        //Set button colors
        normalSpeedButton.GetComponent<Image>().color = pressedColor;
        pauseButton.GetComponent<Image>().color = normalColor;
        fastSpeedButton.GetComponent<Image>().color = normalColor;
        veryFastSpeedButton.GetComponent<Image>().color = pressedColor;
        WaveUpdate();
    }

    public void BuyUnit(int i)
    {
        LevelManager.main.BuyUnit(i);
    }
}

