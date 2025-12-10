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
    [SerializeField] public TextMeshProUGUI buyUnit1;
    [SerializeField] public TextMeshProUGUI unassignedBees;
    [SerializeField] public TextMeshProUGUI nectarBees;
    [SerializeField] public TextMeshProUGUI honeyBees;
    [SerializeField] public TextMeshProUGUI soldierBees;
    [SerializeField] public Toggle autoAssignHoneyBee;
    [SerializeField] public Toggle autoAssignNectarBee;
    [SerializeField] public Toggle autoAssignSoldierBee;

    public string speed = "Normal";
    public bool pause = false;
    private Color normalColor = Color.white;
    private Color pressedColor = Color.grey;
    private int frameCounter = 0;

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
    }

    private void Update()
    {
        if (frameCounter >= GlobalValues.main.UIFrameRatio)
        {
            nectarCounterUI.text = Mathf.FloorToInt(LevelManager.main.nectar).ToString();
            honeyRequiredBar.value = Mathf.FloorToInt(LevelManager.main.honey);
            honeyCounterRequiredUI.text = Mathf.FloorToInt(LevelManager.main.honey).ToString() + " / " + LevelManager.main.honeyRequired;
            waveSpawnCounterUI.text = Mathf.FloorToInt(GameObject.Find("LevelManager").GetComponent<WaveSpawner>().waveCountdown).ToString();
            queenHealthBar.value = LevelManager.main.queenBeeHP;
            queenHealthNumbered.text = Mathf.Round(LevelManager.main.queenBeeHP).ToString() + " / " + Mathf.Round(LevelManager.main.queenBeeMaxHP).ToString();
            frameCounter = 0;
        }
        frameCounter++;
        //Pause
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
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

    public void AssignNectarBee(bool add)
    {
        LevelManager.main.AssignBeeToNectar(add);
    }

    public void AssignHoneyBee(bool add)
    {
        LevelManager.main.AssignBeeToHoney(add);
    }

    public void AssignSoldierBee(bool add)
    {
        LevelManager.main.AssignBeeToSoldier(add);
    }

    public void ToggleAutoNectarBeeAssign() 
    {
        bool value = autoAssignNectarBee.isOn;
        if (value == true)
        {
            LevelManager.main.autoAssignBees = "Nectar";
            autoAssignHoneyBee.SetIsOnWithoutNotify(false);
            autoAssignSoldierBee.SetIsOnWithoutNotify(false);

        }
        else if (value == false)
        {
            LevelManager.main.autoAssignBees = "unassigned";
        }
    }

    public void ToggleAutoHoneyBeeAssign() 
    {
        bool value = autoAssignHoneyBee.isOn;
        if (value == true)
        {
            LevelManager.main.autoAssignBees = "Honey";
            autoAssignNectarBee.SetIsOnWithoutNotify(false);
            autoAssignSoldierBee.SetIsOnWithoutNotify(false);
        }
        else if (value == false)
        {
            LevelManager.main.autoAssignBees = "unassigned";
        }
    }

    public void ToggleAutoSoldierBeeAssign()
    {
        bool value = autoAssignSoldierBee.isOn;
        if (value == true)
        {
            LevelManager.main.autoAssignBees = "Soldier";
            autoAssignNectarBee.SetIsOnWithoutNotify(false);
            autoAssignHoneyBee.SetIsOnWithoutNotify(false);
        }
        else if (value == false)
        {
            LevelManager.main.autoAssignBees = "unassigned";
        }
    }
}

