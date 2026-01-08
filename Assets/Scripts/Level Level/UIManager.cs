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
    [SerializeField] public GameObject controlUI;
    [SerializeField] public GameObject enemyUI;
    [SerializeField] public GameObject menuUI;
    [SerializeField] public TextMeshProUGUI queenHealthNumbered;
    [SerializeField] public TextMeshProUGUI unitCost;
    [SerializeField] public TextMeshProUGUI unassignedBees;
    [SerializeField] public TextMeshProUGUI nectarBees;
    [SerializeField] public TextMeshProUGUI honeyBees;
    [SerializeField] public TextMeshProUGUI soldierBees;
    [SerializeField] public Toggle autoAssignHoneyBee;
    [SerializeField] public Toggle autoAssignNectarBee;
    [SerializeField] public Toggle autoAssignSoldierBee;
    [SerializeField] public SoundType clickPositive;
    [SerializeField] public SoundType clickNegative;
    [SerializeField] public SoundType clickToggle;
    [SerializeField] public SoundType clickInteresting;

    public string speed = "Normal";
    public bool pause = false;
    private Color normalColor = Color.white;
    private Color pressedColor = Color.grey;
    private int frameCounter = 0;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        if (frameCounter >= GlobalValues.main.UIFrameRatio)
        {
            nectarCounterUI.text = Mathf.FloorToInt(LevelManager.main.nectar).ToString();
            honeyCounterRequiredUI.text = Mathf.FloorToInt(LevelManager.main.honey).ToString() + " / " + LevelManager.main.honeyRequired;
            waveSpawnCounterUI.text = Mathf.FloorToInt(GameObject.Find("LevelManager").GetComponent<WaveSpawner>().waveCountdown).ToString();
            queenHealthNumbered.text = Mathf.Round(100 * LevelManager.main.queenBeeHP / LevelManager.main.queenBeeMaxHP).ToString() + "%";
            frameCounter = 0;
        }
        frameCounter++;
        //Pause
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
        //Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenuUI();
        }
    }


    public void WaveUpdate()
    {
        WaveTrackerUI.text = "DAY " + WaveSpawner.main.currentWave + " OF " + WaveSpawner.main.numberOfWaves;
    }

    public void VictoryUI()
    {
        SetVictoryUI(true);
    }

    public void DefeatUI()
    {
        SetDefeatUI(true);
    }

    public void ExitToMainMenu()
    {
        SoundManager.main.PlaySound(clickPositive);
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Additive);
        GlobalValues.main.SetUI(false);
        SceneManager.UnloadSceneAsync(LevelManager.main.gameObject.scene);
    }

    public void ExitToCampaignMenu()
    {
        SoundManager.main.PlaySound(clickPositive);
        SceneManager.LoadScene("Global Map", LoadSceneMode.Additive);
        GlobalValues.main.SetUI(false);
        SceneManager.UnloadSceneAsync(LevelManager.main.gameObject.scene);
    }

    public void TogglePause(bool sound = true)
    {
        if (sound == true)
        {
            SoundManager.main.PlaySound(clickToggle);
        }
        pause = !pause;
        LevelManager.main.ChangeTiming(speed, pause);
        if (pause == true)
        {
            pauseButton.GetComponent<Image>().color = pressedColor;
        }
        else
        {
            SetMenuUI(false);
            pauseButton.GetComponent<Image>().color = normalColor;
        }
    }

    public void NormalSpeed(bool sound = true)
    {
        if (sound == true)
        {
            SoundManager.main.PlaySound(clickToggle);
        }
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

    public void FastSpeed(bool sound = true)
    {
        if (sound == true)
        {
            SoundManager.main.PlaySound(clickToggle);
        }
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

    public void VeryFastSpeed(bool sound = true)
    {
        if (sound == true)
        {
            SoundManager.main.PlaySound(clickToggle);
        }
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
        SetMenuUI(false);
        SetVictoryUI(false);
        SetDefeatUI(false);
        controlUI.transform.position = controlUI.GetComponent<DraggableUI>().startingPosition;
        enemyUI.transform.position = enemyUI.GetComponent<DraggableUI>().startingPosition;
        //Set button colors
        normalSpeedButton.GetComponent<Image>().color = pressedColor;
        pauseButton.GetComponent<Image>().color = pressedColor;
        fastSpeedButton.GetComponent<Image>().color = normalColor;
        veryFastSpeedButton.GetComponent<Image>().color = normalColor;
        WaveUpdate();
    }

    public void BuyUnit(string work = "")
    {
        SoundManager.main.PlaySound(clickPositive);
        LevelManager.main.BuyUnit(work);
    }

    public void AssignNectarBee(bool add)
    {
        SoundManager.main.PlaySound(clickPositive);
        LevelManager.main.AssignBeeToNectar(add);
    }

    public void AssignHoneyBee(bool add)
    {
        SoundManager.main.PlaySound(clickPositive);
        LevelManager.main.AssignBeeToHoney(add);
    }

    public void AssignSoldierBee(bool add)
    {
        SoundManager.main.PlaySound(clickPositive);
        LevelManager.main.AssignBeeToSoldier(add);
    }

    public void ToggleAutoNectarBeeAssign() 
    {
        SoundManager.main.PlaySound(clickToggle);
        bool value = autoAssignNectarBee.isOn;
        if (value == true)
        {
            LevelManager.main.autoAssignBees = "Nectar";
            autoAssignHoneyBee.SetIsOnWithoutNotify(false);
            autoAssignSoldierBee.SetIsOnWithoutNotify(false);

        }
        else if (value == false)
        {
            LevelManager.main.autoAssignBees = "Unassigned";
        }
    }

    public void ToggleAutoHoneyBeeAssign() 
    {
        SoundManager.main.PlaySound(clickToggle);
        bool value = autoAssignHoneyBee.isOn;
        if (value == true)
        {
            LevelManager.main.autoAssignBees = "Honey";
            autoAssignNectarBee.SetIsOnWithoutNotify(false);
            autoAssignSoldierBee.SetIsOnWithoutNotify(false);
        }
        else if (value == false)
        {
            LevelManager.main.autoAssignBees = "Unassigned";
        }
    }

    public void ToggleAutoSoldierBeeAssign()
    {
        SoundManager.main.PlaySound(clickToggle);
        bool value = autoAssignSoldierBee.isOn;
        if (value == true)
        {
            LevelManager.main.autoAssignBees = "Soldier";
            autoAssignNectarBee.SetIsOnWithoutNotify(false);
            autoAssignHoneyBee.SetIsOnWithoutNotify(false);
        }
        else if (value == false)
        {
            LevelManager.main.autoAssignBees = "Unassigned";
        }
    }

    public void ToggleControlUI()
    {
        SoundManager.main.PlaySound(clickToggle);
        controlUI.SetActive(!controlUI.activeSelf);
    }

    public void ToggleEnemyUI()
    {
        SoundManager.main.PlaySound(clickToggle);
        enemyUI.SetActive(!enemyUI.activeSelf);
    }

    public void ToggleMenuUI()
    {
        SoundManager.main.PlaySound(clickToggle);
        menuUI.SetActive(!menuUI.activeSelf);
    }

    public void SetControlUI(bool state)
    {
        controlUI.SetActive(state);
    }

    public void SetEnemyUI(bool state)
    {
        enemyUI.SetActive(state);
    }

    public void SetMenuUI(bool state)
    {
        menuUI.SetActive(state);
    }

    public void SetVictoryUI(bool state)
    {
        victoryUI.SetActive(state);
    }

    public void SetDefeatUI(bool state)
    {
        defeatUI.SetActive(state);
    }

    public void UpdateQueensCommand()
    {
        //Add text to buttons
        unitCost.text = "Cost: " + LevelManager.main.workerBeeCost.ToString() + "n";
        unassignedBees.text = LevelManager.main.unassignedBees.Length.ToString();
        nectarBees.text = LevelManager.main.nectarBees.Length.ToString();
        honeyBees.text = LevelManager.main.honeyBees.Length.ToString();
        soldierBees.text = LevelManager.main.soldierBees.Length.ToString();
    }
}

