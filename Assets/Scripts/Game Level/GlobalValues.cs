using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using TMPro;
using System;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;

public class GlobalValues : MonoBehaviour
{

    public static GlobalValues main;

    public enum Difficulty 
    { 
        EASY,
        MEDIUM,
        HARD
    }


    [Header("_______________________")]
    [Header("Global")]
    [SerializeField] public string gameVersion;
    [SerializeField] public float maxDeltaTime = 0.1f;
    [SerializeField] public float normalTiming = 1f;
    [SerializeField] public float fastTiming = 2f;
    [SerializeField] public float veryFastTiming = 3f;
    [SerializeField] public float waveLength = 30f;
    [SerializeField] public float startingWaveCountdown = 10f;
    [SerializeField] public float waveSpawnRatio = 0.7f;
    [SerializeField] public float startingRadius = 50f;
    [SerializeField] public LayerMask plotMask;
    [SerializeField] public LayerMask honeyCombMask;
    [SerializeField] public LayerMask enemyMask;
    [SerializeField] public LayerMask unitMask;
    [SerializeField] public LayerMask flowerMask;
    [SerializeField] public LayerMask obstructionMask;
    [SerializeField] public LayerMask towerMask;
    [SerializeField] public LayerMask incomeMask;

    [Header("_______________________")]
    [Header("Audio")]
    [SerializeField] public float pitchChange = 0.1f;

    [Header("_______________________")]
    [Header("All Units")]
    [SerializeField] public float wayPointDistance = 0.2f;
    [SerializeField] public float maxSlowDebuff = 0.2f;
    [SerializeField] public float freezeImmuneRatio = 0.25f;
    [SerializeField] public float stealthModifier = 1f;
    [SerializeField] public float stealthTransparancy = 0.3f;
    [SerializeField] public float reviveCooldownModifier = 1f;
    [SerializeField] public float reviveDurationModifier = 1f;

    [Header("_______________________")]
    [Header("Friendly Units")]
    [SerializeField] public GameObject workerBeePrefab;
    [SerializeField] public float unitRotationSpeed = 150f;
    [SerializeField] public float queenBeeHitPoints = 100f;
    [SerializeField] public float queenBeeReturnDamage = 0.25f;
    [SerializeField] public float queenBeeArmor = 0f;
    [SerializeField] public float SoldierPauseTime = 1f;
    [SerializeField] public int[] startingBees = { 3, 2, 1 };
    [SerializeField] public float beeCost = 50f;
    [SerializeField] public float beeCostScaling = 0.1f;
    [SerializeField] public float beeHitPoints = 1f;
    [SerializeField] public float beeHeal = 1f;
    [SerializeField] public float beeShield = 1f;
    [SerializeField] public float beeShieldGeneration = 1f;
    [SerializeField] public float beeTargetingRange = 1f;
    [SerializeField] public float beeDamage = 1f;
    [SerializeField] public float beeAttackRate = 1f;
    [SerializeField] public float beeActionPower = 1f;
    [SerializeField] public float beeActionRate = 1f;
    [SerializeField] public float beeEffectPower = 1f;
    [SerializeField] public float beeEffectRate = 1f;
    [SerializeField] public float beeCarryCapacity = 1f;
    [SerializeField] public float beeMoveSpeed = 1f;
    [SerializeField] public float beeArmor = 0f;
    [SerializeField] public float beeResistance = 0f;
    [SerializeField] public float beeDodge = 0f;
    [SerializeField] public float beeArmorPierce = 0f;
    [SerializeField] public float beeResistancePierce = 0f;
    [SerializeField] public float beeDodgePierce = 0f;
    [SerializeField] public float beeStealthDetection = 0f;

    [Header("_______________________")]
    [Header("Mobs")]
    [SerializeField] public float enemyRotationSpeed = 150f;
    [SerializeField] public float[] enemyHitPoints = { 0.75f, 1f, 1.25f };
    [SerializeField] public float enemyHeal = 1f;
    [SerializeField] public float enemyShield = 1f;
    [SerializeField] public float enemyShieldGeneration = 1f;
    [SerializeField] public float enemyTargetingRange = 1f;
    [SerializeField] public float enemyDamage = 1f;
    [SerializeField] public float enemyAttackRate = 1f;
    [SerializeField] public float enemyActionPower = 1f;
    [SerializeField] public float enemyActionRate = 1f;
    [SerializeField] public float enemySlowPower = 0f;
    [SerializeField] public float enemySlowPierce = 0f;
    [SerializeField] public float enemyFreezePower = 0f;
    [SerializeField] public float enemyFreezePierce = 0f;
    [SerializeField] public float enemyEffectPower = 1f;
    [SerializeField] public float enemyEffectRate = 1f;
    [SerializeField] public float enemyCarryCapacity = 1f;
    [SerializeField] public float[] enemyMoveSpeed = { 0.9f, 1f, 1.1f };
    [SerializeField] public float enemyArmor = 0f;
    [SerializeField] public float enemyResistance = 0f;
    [SerializeField] public float enemyDodge = 0f;
    [SerializeField] public float enemyArmorPierce = 0f;
    [SerializeField] public float enemyResistancePierce = 0f;
    [SerializeField] public float enemyDodgePierce = 0f;
    [SerializeField] public float enemyStealthDetection = 0f;
    [SerializeField] public float[] enemyWaveScaling = { 0f, 1f, 2f };
    [SerializeField] public float enemyHatchTime = 1f;
    [SerializeField] public float enemySpawnTime = 1f;
    [SerializeField] public float enemyStealthTime = 1f;
    [SerializeField] public float honeyhealModifier = 0.5f;
    [SerializeField] public float hummingbirdRange = 100f;
    [SerializeField] public float hummingbirdSapTimeModifier = 1f;
    [SerializeField] public float hummingbirdWaitTime = 3f;

    [Header("_______________________")]
    [Header("Boss Mobs")]

    [Header("_______________________")]
    [Header("Towers")]
    [SerializeField] public GameObject[] buildableHive;
    [SerializeField] public GameObject[] buildableFlower;
    [SerializeField] public GameObject[] buildable;
    [SerializeField] public string[] targetingOptions = new string[] { "Near", "Far", "Weak", "Strong", "Ground", "Flying" };
    [SerializeField] public float sellNonrefund = 35f;
    [SerializeField] public float turretPauseTime = 0.25f;
    [SerializeField] public float buffPower = 1f;
    [SerializeField] public float buffTimer = 1f;
    [SerializeField] public float healTimer = 1f;
    [SerializeField] public float slowPower = 1f;
    [SerializeField] public float slowPierce = 0f;
    [SerializeField] public float freezePower = 1f;
    [SerializeField] public float freezePierce = 0f;
    [SerializeField] public float towerAoEArea = 1f;
    [SerializeField] public float towerAoEDamageDropOff = 0.25f;
    [SerializeField] public float towerRampingDamage = 1f;
    [SerializeField] public int towerExtraRampCount = 0;
    [SerializeField] public int towerExtraRicochetCount = 0;
    [SerializeField] public float ricochetRange = 2f;
    [SerializeField] public float towerSell = 0.5f;
    [SerializeField] public float towerHitPoints = 1f;
    [SerializeField] public float towerHeal = 1f;
    [SerializeField] public float towerShield = 1f;
    [SerializeField] public float towerShieldGeneration = 1f;
    [SerializeField] public float towerTargetingRange = 1f;
    [SerializeField] public float towerDamage = 1f;
    [SerializeField] public float towerAttackRate = 1f;
    [SerializeField] public float towerActionPower = 1f;
    [SerializeField] public float towerActionRate = 1f;
    [SerializeField] public float towerBuffPower = 1f;
    [SerializeField] public float towerEffectPower = 1f;
    [SerializeField] public float towerEffectRate = 1f;
    [SerializeField] public float towerCarryCapacity = 1f;
    [SerializeField] public float towerMoveSpeed = 1f;
    [SerializeField] public float towerArmor = 0f;
    [SerializeField] public float towerResistance = 0f;
    [SerializeField] public float towerDodge = 0f;
    [SerializeField] public float towerArmorPierce = 0f;
    [SerializeField] public float towerResistancePierce = 0f;
    [SerializeField] public float towerDodgePierce = 0f;
    [SerializeField] public float towerStealthDetection = 0f;

    [Header("_______________________")]
    [Header("UI")]
    [SerializeField] public GameObject UIPrefab;

    [Header("_______________________")]
    [Header("Resources")]
    [SerializeField] public float[] startingNectar = { 300f, 200f, 100f };
    [SerializeField] public float[] startingHoney = { 50f, 0f, 0f };
    [SerializeField] public float[] honeyDropRate = { 0.5f, 0.25f, 0f };
    [SerializeField] public float honeyinterest = 0f;
    [SerializeField] public float pollenBuff = 0f;
    [SerializeField] public float nectarGenerationRate = 3f;
    [SerializeField] public float pollenGenerationRate = 1f;
    [SerializeField] public int honeycombTicks = 60;
    [SerializeField] public float honeycombGeneration = 1f;
    [SerializeField] public float honeycombCheckTime = 1f;
    [SerializeField] public float honeycombFillTime = 3f;
    [SerializeField] public float flowerRange = 1f;

    [Header("_______________________")]
    [Header("")]

    [Header("_______________________")]
    [Header("")]

    [Header("_______________________")]
    [Header("Mob Prestige")]
    [SerializeField] public int prestigeMaxNumber = 5;
    [SerializeField] public float prestigeDuplicateModifier = 0.5f;
    [SerializeField] public float prestigeRed = .3f;     //Armor / HP Buff
    [SerializeField] public float prestigeBlue = .3f;     //Reistance / Speed Buff
    [SerializeField] public float prestigeGreen = 1f;     //Health Regen
    [SerializeField] public float prestigeGreenMaxRatio = 0.05f;
    [SerializeField] public float prestigeGreenRate = 0.2f;
    [SerializeField] public float prestigeYellow = 1f;      //Carry Capacity
    [SerializeField] public float prestigeWhite = .5f;       //Invert Flying
    [SerializeField] public float prestigePurple = 0.25f;      //Blink
    [SerializeField] public float prestigePurpleRate = 0.2f;      
    [SerializeField] public float prestigePurpleRange = 5f;      
    [SerializeField] public float prestigeBlack = 2f;        //Death Split
    [SerializeField] public float prestigeBlackDistance = 0.5f;
    [SerializeField] public float prestigeGold = .5f;        //Buff Everything
    [SerializeField] public float prestigeBrown = .25f;        //Dodge Buff
    [SerializeField] public float prestigeGrey = 0.25f;        //Stealth
    [SerializeField] public float prestigeGreyRate = 0.125f;
    [SerializeField] public float prestigeGreyDuration = 5f;
    [SerializeField] public float prestigePrismatic = 2;  //Alternating Buff
    [SerializeField] public float prestigePrismaticDuration = 10f;
    [SerializeField] public float prestigeSilver = 1f;        //Shield
    [SerializeField] public float prestigePlatinum = 1f;        //Revive
    [SerializeField] public float prestigePlatinumCooldown = 45f;
    [SerializeField] public float prestigeTeal = 1f;    //Slow & Freeze Immunity / Slower

    [Header("_______________________")]
    [Header("Performance Settings")]
    [SerializeField] public int UIFrameRatio = 5;

    [Header("_______________________")]
    [Header("Camera Controls")]
    [SerializeField] public GameObject UI;
    [SerializeField] public float zoomStep = 1f;
    [SerializeField] public float minCamSize = 1f;
    [SerializeField] public float maxCamSize = 10f;
    [SerializeField] public float moveStep = 1f;
    [SerializeField] public float keystrokesPerSecond = 20f;

    [Header("_______________________")]
    [Header("Flowers")]

    [Header("Closed Flower")]
    [Header("ID 0")]
    [Header("Attributes")]
    [SerializeField] public Sprite closedFlowerSprite;
    [SerializeField] public string closedFlowerText = "No Pollen Yet";
    [SerializeField] public float closedFlowerModifier = 0f; //not used
    [SerializeField] public float closedFlowerRarity = 1f;

    [Header("Blue Flower")]
    [Header("ID 1")]
    [Header("Attributes")]
    [SerializeField] public Sprite blueFlowerSprite;
    [SerializeField] public string blueFlowerText = "Pollen source that strengthens effects";
    [SerializeField] public float blueFlowerModifier = 1.3f; //effect multiplier
    [SerializeField] public float blueFlowerRarity = 1f;

    [Header("White Flower")]
    [Header("ID 2")]
    [Header("Attributes")]
    [SerializeField] public Sprite whiteFlowerSprite;
    [SerializeField] public string whiteFlowerText = "Pollen source that increases attack speed";
    [SerializeField] public float whiteFlowerModifier = 1.2f; //attack speed multiplier
    [SerializeField] public float whiteFlowerRarity = 1f;

    [Header("Pink Flower")]
    [Header("ID 3")]
    [Header("Attributes")]
    [SerializeField] public Sprite pinkFlowerSprite;
    [SerializeField] public string pinkFlowerText = "Pollen source with extra potency";
    [SerializeField] public float pinkFlowerModifier = 1.15f; //minor income multiplier
    [SerializeField] public float pinkFlowerRarity = 3f;

    [Header("Purple Flower")]
    [Header("ID 4")]
    [Header("Attributes")]
    [SerializeField] public Sprite purpleFlowerSprite;
    [SerializeField] public string purpleFlowerText = "Pollen source with a lot more potency";
    [SerializeField] public float purpleFlowerModifier = 1.3f; //major income multiplier
    [SerializeField] public float purpleFlowerRarity = 5f;

    [Header("Gold Flower")]
    [Header("ID 5")]
    [Header("Attributes")]
    [SerializeField] public Sprite goldFlowerSprite;
    [SerializeField] public string goldFlowerText = "Pollen source with bonus processed nectar";
    [SerializeField] public float goldFlowerModifier = 0.1f; //bonus stored nectar multiplier
    [SerializeField] public float goldFlowerRarity = 5f;

    [Header("Red Flower")]
    [Header("ID 6")]
    [Header("Attributes")]
    [SerializeField] public Sprite redFlowerSprite;
    [SerializeField] public string redFlowerText = "Pollen source that increases damage";
    [SerializeField] public float redFlowerModifier = 1.2f; //damage multiplier
    [SerializeField] public float redFlowerRarity = 2f;

    [Header("Yellow Flower")]
    [Header("ID 7")]
    [Header("Attributes")]
    [SerializeField] public Sprite yellowFlowerSprite;
    [SerializeField] public string yellowFlowerText = "Pollen source that increases range";
    [SerializeField] public float yellowFlowerModifier = 1.5f; //range multiplier
    [SerializeField] public float yellowFlowerRarity = 2f;

    [Header("_______________________")]
    [Header("Projectiles")]

    [Header("References")]

    [Header("Attributes")]
    [SerializeField] public float projectileCollisonDistance = 0.1f;

    [Header("_______________________")]
    [Header("Environment")]
    [Header("Hive Plot")]
    [Header("References")]
    [Header("Attributes")]

    [Header("_______________________")]
    [Header("UI")]
    [Header("Menu")]
    [Header("References")]
    [Header("Attributes")]
    [Header("UIManager")]
    [Header("References")]
    [Header("Attributes")]

    [Header("_______________________")]
    [Header("Levels")]
    [Header("_______________________")]
    [Header("Level 1")]
    [Header("References")]
    [Header("Attributes")]

    //Trackers
    [Header("Trackers")]
    public int levelIndex = 0;
    public string targetingOptionDefault;
    public bool gameLoaded = false;
    public Difficulty difficulty;

    // Define parameter arrays
    //Flowers
    public Sprite[] FLOWERSprite;
    public string[] FLOWERText;
    public float[] FLOWERModifier;
    public float[] FLOWERRarity;

    public void Start()
    {
        gameLoaded = true;
        //Setup Scenes
        UnloadAllBut("Global");
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Additive);
    }

    public void Awake()
    {
        main = this;
        difficulty = Difficulty.EASY;
        SetUI(false);
        //
        targetingOptionDefault = targetingOptions[0];
        //Build parameter arrays for other scripts to pull from
        //Flower Arrays
        FLOWERSprite = new Sprite[] { closedFlowerSprite, blueFlowerSprite, whiteFlowerSprite, pinkFlowerSprite, purpleFlowerSprite, goldFlowerSprite, redFlowerSprite, yellowFlowerSprite };
        FLOWERText = new string[] { closedFlowerText, blueFlowerText, whiteFlowerText, pinkFlowerText, purpleFlowerText, goldFlowerText, redFlowerText, yellowFlowerText };
        FLOWERModifier = new float[] { closedFlowerModifier, blueFlowerModifier, whiteFlowerModifier, pinkFlowerModifier, purpleFlowerModifier, goldFlowerModifier, redFlowerModifier, yellowFlowerModifier };
        FLOWERRarity = new float[] { closedFlowerRarity, blueFlowerRarity, whiteFlowerRarity, pinkFlowerRarity, purpleFlowerRarity, goldFlowerRarity, redFlowerRarity, yellowFlowerRarity };
    }

    public void SetUI(bool state)
    {
        UI.SetActive(state);
        if (state == true)
        {
            UIManager.main.Reset();
        }
    }

    public void UnloadAllBut(string sceneName)
    {
        int sceneCount = SceneManager.sceneCount;
        // Iterate through all loaded scenes.
        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            // Check if the current scene is NOT the one we want to keep.
            if (scene.name != sceneName)
            {
                // Unload the scene asynchronously.
                // It is important to use the Async version for smooth performance.
                StartCoroutine(DelayedUnload(scene));
            }
        }
    }

    public IEnumerator DelayedUnload(Scene scene)
    {
        yield return new WaitForSecondsRealtime(0.03f);
        SceneManager.UnloadSceneAsync(scene);
        SetUI(false);
    } 
}


