using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using TMPro;
using System;
using static UnityEngine.GraphicsBuffer;

public class GlobalValues : MonoBehaviour
{

    public static GlobalValues main;

    [Header("Global")]
    [SerializeField] public int levelIndex = 0;
    [SerializeField] public string difficultySetting = "Medium";
    [SerializeField] public float difficultyMultiplier = 1f;
    [SerializeField] public float easyMultiplier = 0.7f;
    [SerializeField] public float mediumMultiplier = 1f;
    [SerializeField] public float hardMultiplier = 1.5f;
    [SerializeField] public float normalTiming = 1f;
    [SerializeField] public float fastTiming = 2f;
    [SerializeField] public float veryFastTiming = 3f;
    [SerializeField] public float influenceModifier = 0.5f;
    [SerializeField] public float basenectarGeneration = 0.25f;
    [SerializeField] public float flowerRange = 1f;
    [SerializeField] public float globalFertility = 3f;
    [SerializeField] public float honeyDropModifier = 0.75f;
    [SerializeField] public float investmentMultiplier = 0.25f;
    [SerializeField] public float investmentCost = 2f;
    [SerializeField] public float sellRatio = 0.75f;
    [SerializeField] public float healRatio = 1f;
    [SerializeField] public float damageBuffRatio = 0.1f;
    [SerializeField] public float apsBuffRatio = 0.1f;
    [SerializeField] public float slowRatio = 1f;
    [SerializeField] public float slowDurationRatio = 1f;
    [SerializeField] public float maxSlowDebuff = 0.2f;
    [SerializeField] public float freezeRatio = 1f;
    [SerializeField] public int freezeChance = 10;
    [SerializeField] public float slowTimer = 3f;
    [SerializeField] public float AoeDropOffFloor = 0.25f;
    [SerializeField] public float buffTimer = 10f;
    [SerializeField] public float healTimer = 5f;
    [SerializeField] public float ricochetRange = 2f;
    [SerializeField] public int rampRatio = 10;
    [SerializeField] public int rampCap = 10;
    [SerializeField] public float honeyHealRatio = 0.25f;
    [SerializeField] public float queenThornRatio = 1f;
    [SerializeField] public float eggLayRatio = 1f;
    [SerializeField] public float healAuraRatio = 1f;
    [SerializeField] public float healAuraTimer = 2f;
    [SerializeField] public float enemyHealRange = 2f;
    [SerializeField] public float eggLayingTimer = 5f;
    [SerializeField] public float eggHatchingTimer = 5f;
    [SerializeField] public float hummingbirdRange = 100f;
    [SerializeField] public float hummingbirdSapTime = 10f;
    [SerializeField] public float hummingbirdWaitTime = 3f;
    [SerializeField] public float chickenTimer = 3f;
    [SerializeField] public float skunkSprayTimer = 10f;
    [SerializeField] public float skunkSprayDuration = 3f;
    [SerializeField] public float waveLength = 30f;
    [SerializeField] public float waveSpawnRatio = 0.7f;
    [SerializeField] public float enemyWaypointDistance = 0.2f;
    [SerializeField] public float enemyRotationSpeed = 150f;
    [SerializeField] public string[] targetingOptions = new string[] { "Near", "Far", "Weak", "Strong", "Ground", "Flying" };
    [SerializeField] public LayerMask plotMask;
    [SerializeField] public LayerMask enemyMask;
    [SerializeField] public LayerMask flowerMask;
    [SerializeField] public LayerMask obstructionMask;
    [SerializeField] public LayerMask towerMask;
    [SerializeField] public LayerMask investmentMask;
    [SerializeField] public LayerMask incomeMask;

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
    [Header("Mobs")]

    [Header("Ant")]
    [Header("ID 0")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY0prefab;
    [SerializeField] public GameObject ENEMY0prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY0name = "Ant";
    [SerializeField] public float ENEMY0hitPoints = 3f;
    [SerializeField] public float ENEMY0armor = 0f;
    [SerializeField] public bool ENEMY0healthBar = false;
    [SerializeField] public float ENEMY0bounty = 0f;
    [SerializeField] public float ENEMY0moveSpeed = 1f;
    [SerializeField] public bool ENEMY0willFly = false;
    [SerializeField] public int ENEMY0carryCapacity = 20;
    [SerializeField] public bool ENEMY0willStealNectar = true;
    [SerializeField] public bool ENEMY0willStealHoney = false;
    [SerializeField] public bool ENEMY0willAttack = true;
    [SerializeField] public string ENEMY0effect = "none";
    [SerializeField] public float ENEMY0effectModifier = 0f;
    [SerializeField] public float ENEMY0attackDamage = 1f;
    [SerializeField] public float ENEMY0attackRate = 1f;
    [Header("Ant Egg")]
    [Header("ID 1")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY1prefab;
    [SerializeField] public GameObject ENEMY1prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY1name = "Ant Egg";
    [SerializeField] public float ENEMY1hitPoints = 2f;
    [SerializeField] public float ENEMY1armor = 20f;
    [SerializeField] public bool ENEMY1healthBar = false;
    [SerializeField] public float ENEMY1bounty = 0f;
    [SerializeField] public float ENEMY1moveSpeed = 0f;
    [SerializeField] public bool ENEMY1willFly = false;
    [SerializeField] public int ENEMY1carryCapacity = 0;
    [SerializeField] public bool ENEMY1willStealNectar = false;
    [SerializeField] public bool ENEMY1willStealHoney = false;
    [SerializeField] public bool ENEMY1willAttack = true;
    [SerializeField] public string ENEMY1effect = "Hatch Ant";
    [SerializeField] public float ENEMY1effectModifier = 1f;
    [SerializeField] public float ENEMY1attackDamage = 0f;
    [SerializeField] public float ENEMY1attackRate = 1f;
    [Header("Ant Queen")]
    [Header("ID 2")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY2prefab;
    [SerializeField] public GameObject ENEMY2prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY2name = "Ant Queen";
    [SerializeField] public float ENEMY2hitPoints = 10f;
    [SerializeField] public float ENEMY2armor = 20f;
    [SerializeField] public bool ENEMY2healthBar = true;
    [SerializeField] public float ENEMY2bounty = 0f;
    [SerializeField] public float ENEMY2moveSpeed = 0.5f;
    [SerializeField] public bool ENEMY2willFly = false;
    [SerializeField] public int ENEMY2carryCapacity = 50;
    [SerializeField] public bool ENEMY2willStealNectar = true;
    [SerializeField] public bool ENEMY2willStealHoney = false;
    [SerializeField] public bool ENEMY2willAttack = true;
    [SerializeField] public string ENEMY2effect = "Lay Ant Eggs";
    [SerializeField] public float ENEMY2effectModifier = 2f;
    [SerializeField] public float ENEMY2attackDamage = 2f;
    [SerializeField] public float ENEMY2attackRate = 1f;
    [Header("Fire Ant")]
    [Header("ID 3")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY3prefab;
    [SerializeField] public GameObject ENEMY3prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY3name = "Fire Ant";
    [SerializeField] public float ENEMY3hitPoints = 2f;
    [SerializeField] public float ENEMY3armor = 30f;
    [SerializeField] public bool ENEMY3healthBar = false;
    [SerializeField] public float ENEMY3bounty = 0f;
    [SerializeField] public float ENEMY3moveSpeed = 2f;
    [SerializeField] public bool ENEMY3willFly = false;
    [SerializeField] public int ENEMY3carryCapacity = 10;
    [SerializeField] public bool ENEMY3willStealNectar = false;
    [SerializeField] public bool ENEMY3willStealHoney = false;
    [SerializeField] public bool ENEMY3willAttack = true;
    [SerializeField] public string ENEMY3effect = "none";
    [SerializeField] public float ENEMY3effectModifier = 0f;
    [SerializeField] public float ENEMY3attackDamage = 1f;
    [SerializeField] public float ENEMY3attackRate = 1f;
    [Header("Beatle")]
    [Header("ID 4")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY4prefab;
    [SerializeField] public GameObject ENEMY4prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY4name = "Beatle";
    [SerializeField] public float ENEMY4hitPoints = 20f;
    [SerializeField] public float ENEMY4armor = 80f;
    [SerializeField] public bool ENEMY4healthBar = true;
    [SerializeField] public float ENEMY4bounty = 0f;
    [SerializeField] public float ENEMY4moveSpeed = 0.5f;
    [SerializeField] public bool ENEMY4willFly = false;
    [SerializeField] public int ENEMY4carryCapacity = 50;
    [SerializeField] public bool ENEMY4willStealNectar = true;
    [SerializeField] public bool ENEMY4willStealHoney = false;
    [SerializeField] public bool ENEMY4willAttack = true;
    [SerializeField] public string ENEMY4effect = "none";
    [SerializeField] public float ENEMY4effectModifier = 0f;
    [SerializeField] public float ENEMY4attackDamage = 5f;
    [SerializeField] public float ENEMY4attackRate = 0.5f;
    [Header("Catapillar")]
    [Header("ID 5")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY5prefab;
    [SerializeField] public GameObject ENEMY5prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY5name = "Catapillar";
    [SerializeField] public float ENEMY5hitPoints = 30f;
    [SerializeField] public float ENEMY5armor = 60f;
    [SerializeField] public bool ENEMY5healthBar = true;
    [SerializeField] public float ENEMY5bounty = 0f;
    [SerializeField] public float ENEMY5moveSpeed = 0.5f;
    [SerializeField] public bool ENEMY5willFly = false;
    [SerializeField] public int ENEMY5carryCapacity = 50;
    [SerializeField] public bool ENEMY5willStealNectar = true;
    [SerializeField] public bool ENEMY5willStealHoney = false;
    [SerializeField] public bool ENEMY5willAttack = true;
    [SerializeField] public string ENEMY5effect = "none";
    [SerializeField] public float ENEMY5effectModifier = 0f;
    [SerializeField] public float ENEMY5attackDamage = 2f;
    [SerializeField] public float ENEMY5attackRate = 1f;
    [Header("Ladybug")]
    [Header("ID 6")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY6prefab;
    [SerializeField] public GameObject ENEMY6prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY6name = "Ladybug";
    [SerializeField] public float ENEMY6hitPoints = 10f;
    [SerializeField] public float ENEMY6armor = 50f;
    [SerializeField] public bool ENEMY6healthBar = true;
    [SerializeField] public float ENEMY6bounty = 0f;
    [SerializeField] public float ENEMY6moveSpeed = 0.5f;
    [SerializeField] public bool ENEMY6willFly = false;
    [SerializeField] public int ENEMY6carryCapacity = 30;
    [SerializeField] public bool ENEMY6willStealNectar = true;
    [SerializeField] public bool ENEMY6willStealHoney = false;
    [SerializeField] public bool ENEMY6willAttack = true;
    [SerializeField] public string ENEMY6effect = "Heal Aura";
    [SerializeField] public float ENEMY6effectModifier = 2f;
    [SerializeField] public float ENEMY6attackDamage = 1f;
    [SerializeField] public float ENEMY6attackRate = 1f;
    [Header("Fly")]
    [Header("ID 7")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY7prefab;
    [SerializeField] public GameObject ENEMY7prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY7name = "Fly";
    [SerializeField] public float ENEMY7hitPoints = 2f;
    [SerializeField] public float ENEMY7armor = 0f;
    [SerializeField] public bool ENEMY7healthBar = false;
    [SerializeField] public float ENEMY7bounty = 0f;
    [SerializeField] public float ENEMY7moveSpeed = 2f;
    [SerializeField] public bool ENEMY7willFly = true;
    [SerializeField] public int ENEMY7carryCapacity = 10;
    [SerializeField] public bool ENEMY7willStealNectar = true;
    [SerializeField] public bool ENEMY7willStealHoney = false;
    [SerializeField] public bool ENEMY7willAttack = true;
    [SerializeField] public string ENEMY7effect = "none";
    [SerializeField] public float ENEMY7effectModifier = 0f;
    [SerializeField] public float ENEMY7attackDamage = 0.5f;
    [SerializeField] public float ENEMY7attackRate = 1f;
    [Header("Wasp")]
    [Header("ID 8")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY8prefab;
    [SerializeField] public GameObject ENEMY8prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY8name = "Wasp";
    [SerializeField] public float ENEMY8hitPoints = 5f;
    [SerializeField] public float ENEMY8armor = 20f;
    [SerializeField] public bool ENEMY8healthBar = false;
    [SerializeField] public float ENEMY8bounty = 0f;
    [SerializeField] public float ENEMY8moveSpeed = 1f;
    [SerializeField] public bool ENEMY8willFly = true;
    [SerializeField] public int ENEMY8carryCapacity = 20;
    [SerializeField] public bool ENEMY8willStealNectar = false;
    [SerializeField] public bool ENEMY8willStealHoney = false;
    [SerializeField] public bool ENEMY8willAttack = true;
    [SerializeField] public string ENEMY8effect = "none";
    [SerializeField] public float ENEMY8effectModifier = 0f;
    [SerializeField] public float ENEMY8attackDamage = 1f;
    [SerializeField] public float ENEMY8attackRate = 1f;
    [Header("Wasp Egg")]
    [Header("ID 9")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY9prefab;
    [SerializeField] public GameObject ENEMY9prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY9name = "Wasp Egg";
    [SerializeField] public float ENEMY9hitPoints = 3f;
    [SerializeField] public float ENEMY9armor = 40f;
    [SerializeField] public bool ENEMY9healthBar = false;
    [SerializeField] public float ENEMY9bounty = 0f;
    [SerializeField] public float ENEMY9moveSpeed = 0f;
    [SerializeField] public bool ENEMY9willFly = true;
    [SerializeField] public int ENEMY9carryCapacity = 0;
    [SerializeField] public bool ENEMY9willStealNectar = false;
    [SerializeField] public bool ENEMY9willStealHoney = false;
    [SerializeField] public bool ENEMY9willAttack = true;
    [SerializeField] public string ENEMY9effect = "none";
    [SerializeField] public float ENEMY9effectModifier = 1f;
    [SerializeField] public float ENEMY9attackDamage = 0f;
    [SerializeField] public float ENEMY9attackRate = 1f;
    [Header("Wasp Queen")]
    [Header("ID 10")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY10prefab;
    [SerializeField] public GameObject ENEMY10prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY10name = "Wasp Queen";
    [SerializeField] public float ENEMY10hitPoints = 20f;
    [SerializeField] public float ENEMY10armor = 40f;
    [SerializeField] public bool ENEMY10healthBar = true;
    [SerializeField] public float ENEMY10bounty = 0f;
    [SerializeField] public float ENEMY10moveSpeed = 0.5f;
    [SerializeField] public bool ENEMY10willFly = true;
    [SerializeField] public int ENEMY10carryCapacity = 20;
    [SerializeField] public bool ENEMY10willStealNectar = false;
    [SerializeField] public bool ENEMY10willStealHoney = false;
    [SerializeField] public bool ENEMY10willAttack = true;
    [SerializeField] public string ENEMY10effect = "Lay Wasp Eggs";
    [SerializeField] public float ENEMY10effectModifier = 2f;
    [SerializeField] public float ENEMY10attackDamage = 2f;
    [SerializeField] public float ENEMY10attackRate = 1f;
    [Header("Mite")]
    [Header("ID 11")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY11prefab;
    [SerializeField] public GameObject ENEMY11prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY11name = "Mite";
    [SerializeField] public float ENEMY11hitPoints = 1f;
    [SerializeField] public float ENEMY11armor = 10f;
    [SerializeField] public bool ENEMY11healthBar = false;
    [SerializeField] public float ENEMY11bounty = 0f;
    [SerializeField] public float ENEMY11moveSpeed = 3f;
    [SerializeField] public bool ENEMY11willFly = false;
    [SerializeField] public int ENEMY11carryCapacity = 0;
    [SerializeField] public bool ENEMY11willStealNectar = false;
    [SerializeField] public bool ENEMY11willStealHoney = false;
    [SerializeField] public bool ENEMY11willAttack = true;
    [SerializeField] public string ENEMY11effect = "none";
    [SerializeField] public float ENEMY11effectModifier = 0f;
    [SerializeField] public float ENEMY11attackDamage = 1f;
    [SerializeField] public float ENEMY11attackRate = 1f;
    [Header("")]
    [Header("ID 12")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY12prefab;
    [SerializeField] public GameObject ENEMY12prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY12name = "";
    [SerializeField] public float ENEMY12hitPoints = 1f;
    [SerializeField] public float ENEMY12armor = 0f;
    [SerializeField] public bool ENEMY12healthBar = false;
    [SerializeField] public float ENEMY12bounty = 0f;
    [SerializeField] public float ENEMY12moveSpeed = 3f;
    [SerializeField] public bool ENEMY12willFly = false;
    [SerializeField] public int ENEMY12carryCapacity = 0;
    [SerializeField] public bool ENEMY12willStealNectar = false;
    [SerializeField] public bool ENEMY12willStealHoney = false;
    [SerializeField] public bool ENEMY12willAttack = true;
    [SerializeField] public string ENEMY12effect = "none";
    [SerializeField] public float ENEMY12effectModifier = 0f;
    [SerializeField] public float ENEMY12attackDamage = 1f;
    [SerializeField] public float ENEMY12attackRate = 1f;
    [Header("")]
    [Header("ID 13")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY13prefab;
    [SerializeField] public GameObject ENEMY13prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY13name = "";
    [SerializeField] public float ENEMY13hitPoints = 1f;
    [SerializeField] public float ENEMY13armor = 0f;
    [SerializeField] public bool ENEMY13healthBar = false;
    [SerializeField] public float ENEMY13bounty = 0f;
    [SerializeField] public float ENEMY13moveSpeed = 3f;
    [SerializeField] public bool ENEMY13willFly = false;
    [SerializeField] public int ENEMY13carryCapacity = 0;
    [SerializeField] public bool ENEMY13willStealNectar = false;
    [SerializeField] public bool ENEMY13willStealHoney = false;
    [SerializeField] public bool ENEMY13willAttack = true;
    [SerializeField] public string ENEMY13effect = "none";
    [SerializeField] public float ENEMY13effectModifier = 0f;
    [SerializeField] public float ENEMY13attackDamage = 1f;
    [SerializeField] public float ENEMY13attackRate = 1f;
    [Header("")]
    [Header("ID 14")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY14prefab;
    [SerializeField] public GameObject ENEMY14prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY14name = "";
    [SerializeField] public float ENEMY14hitPoints = 1f;
    [SerializeField] public float ENEMY14armor = 0f;
    [SerializeField] public bool ENEMY14healthBar = false;
    [SerializeField] public float ENEMY14bounty = 0f;
    [SerializeField] public float ENEMY14moveSpeed = 3f;
    [SerializeField] public bool ENEMY14willFly = false;
    [SerializeField] public int ENEMY14carryCapacity = 0;
    [SerializeField] public bool ENEMY14willStealNectar = false;
    [SerializeField] public bool ENEMY14willStealHoney = false;
    [SerializeField] public bool ENEMY14willAttack = true;
    [SerializeField] public string ENEMY14effect = "none";
    [SerializeField] public float ENEMY14effectModifier = 0f;
    [SerializeField] public float ENEMY14attackDamage = 1f;
    [SerializeField] public float ENEMY14attackRate = 1f;
    [Header("")]
    [Header("ID 15")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY15prefab;
    [SerializeField] public GameObject ENEMY15prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY15name = "";
    [SerializeField] public float ENEMY15hitPoints = 1f;
    [SerializeField] public float ENEMY15armor = 0f;
    [SerializeField] public bool ENEMY15healthBar = false;
    [SerializeField] public float ENEMY15bounty = 0f;
    [SerializeField] public float ENEMY15moveSpeed = 3f;
    [SerializeField] public bool ENEMY15willFly = false;
    [SerializeField] public int ENEMY15carryCapacity = 0;
    [SerializeField] public bool ENEMY15willStealNectar = false;
    [SerializeField] public bool ENEMY15willStealHoney = false;
    [SerializeField] public bool ENEMY15willAttack = true;
    [SerializeField] public string ENEMY15effect = "none";
    [SerializeField] public float ENEMY15effectModifier = 0f;
    [SerializeField] public float ENEMY15attackDamage = 1f;
    [SerializeField] public float ENEMY15attackRate = 1f;
    [Header("")]
    [Header("ID 16")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY16prefab;
    [SerializeField] public GameObject ENEMY16prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY16name = "";
    [SerializeField] public float ENEMY16hitPoints = 1f;
    [SerializeField] public float ENEMY16armor = 0f;
    [SerializeField] public bool ENEMY16healthBar = false;
    [SerializeField] public float ENEMY16bounty = 0f;
    [SerializeField] public float ENEMY16moveSpeed = 3f;
    [SerializeField] public bool ENEMY16willFly = false;
    [SerializeField] public int ENEMY16carryCapacity = 0;
    [SerializeField] public bool ENEMY16willStealNectar = false;
    [SerializeField] public bool ENEMY16willStealHoney = false;
    [SerializeField] public bool ENEMY16willAttack = true;
    [SerializeField] public string ENEMY16effect = "none";
    [SerializeField] public float ENEMY16effectModifier = 0f;
    [SerializeField] public float ENEMY16attackDamage = 1f;
    [SerializeField] public float ENEMY16attackRate = 1f;
    [Header("")]
    [Header("ID 17")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY17prefab;
    [SerializeField] public GameObject ENEMY17prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY17name = "";
    [SerializeField] public float ENEMY17hitPoints = 1f;
    [SerializeField] public float ENEMY17armor = 0f;
    [SerializeField] public bool ENEMY17healthBar = false;
    [SerializeField] public float ENEMY17bounty = 0f;
    [SerializeField] public float ENEMY17moveSpeed = 3f;
    [SerializeField] public bool ENEMY17willFly = false;
    [SerializeField] public int ENEMY17carryCapacity = 0;
    [SerializeField] public bool ENEMY17willStealNectar = false;
    [SerializeField] public bool ENEMY17willStealHoney = false;
    [SerializeField] public bool ENEMY17willAttack = true;
    [SerializeField] public string ENEMY17effect = "none";
    [SerializeField] public float ENEMY17effectModifier = 0f;
    [SerializeField] public float ENEMY17attackDamage = 1f;
    [SerializeField] public float ENEMY17attackRate = 1f;
    [Header("")]
    [Header("ID 18")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY18prefab;
    [SerializeField] public GameObject ENEMY18prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY18name = "";
    [SerializeField] public float ENEMY18hitPoints = 1f;
    [SerializeField] public float ENEMY18armor = 0f;
    [SerializeField] public bool ENEMY18healthBar = false;
    [SerializeField] public float ENEMY18bounty = 0f;
    [SerializeField] public float ENEMY18moveSpeed = 3f;
    [SerializeField] public bool ENEMY18willFly = false;
    [SerializeField] public int ENEMY18carryCapacity = 0;
    [SerializeField] public bool ENEMY18willStealNectar = false;
    [SerializeField] public bool ENEMY18willStealHoney = false;
    [SerializeField] public bool ENEMY18willAttack = true;
    [SerializeField] public string ENEMY18effect = "none";
    [SerializeField] public float ENEMY18effectModifier = 0f;
    [SerializeField] public float ENEMY18attackDamage = 1f;
    [SerializeField] public float ENEMY18attackRate = 1f;
    [Header("")]
    [Header("ID 19")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY19prefab;
    [SerializeField] public GameObject ENEMY19prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY19name = "";
    [SerializeField] public float ENEMY19hitPoints = 1f;
    [SerializeField] public float ENEMY19armor = 0f;
    [SerializeField] public bool ENEMY19healthBar = false;
    [SerializeField] public float ENEMY19bounty = 0f;
    [SerializeField] public float ENEMY19moveSpeed = 3f;
    [SerializeField] public bool ENEMY19willFly = false;
    [SerializeField] public int ENEMY19carryCapacity = 0;
    [SerializeField] public bool ENEMY19willStealNectar = false;
    [SerializeField] public bool ENEMY19willStealHoney = false;
    [SerializeField] public bool ENEMY19willAttack = true;
    [SerializeField] public string ENEMY19effect = "none";
    [SerializeField] public float ENEMY19effectModifier = 0f;
    [SerializeField] public float ENEMY19attackDamage = 1f;
    [SerializeField] public float ENEMY19attackRate = 1f;
    [Header("")]
    [Header("ID 20")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY20prefab;
    [SerializeField] public GameObject ENEMY20prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY20name = "";
    [SerializeField] public float ENEMY20hitPoints = 1f;
    [SerializeField] public float ENEMY20armor = 0f;
    [SerializeField] public bool ENEMY20healthBar = false;
    [SerializeField] public float ENEMY20bounty = 0f;
    [SerializeField] public float ENEMY20moveSpeed = 3f;
    [SerializeField] public bool ENEMY20willFly = false;
    [SerializeField] public int ENEMY20carryCapacity = 0;
    [SerializeField] public bool ENEMY20willStealNectar = false;
    [SerializeField] public bool ENEMY20willStealHoney = false;
    [SerializeField] public bool ENEMY20willAttack = true;
    [SerializeField] public string ENEMY20effect = "none";
    [SerializeField] public float ENEMY20effectModifier = 0f;
    [SerializeField] public float ENEMY20attackDamage = 1f;
    [SerializeField] public float ENEMY20attackRate = 1f;
    [Header("")]
    [Header("ID 21")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY21prefab;
    [SerializeField] public GameObject ENEMY21prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY21name = "";
    [SerializeField] public float ENEMY21hitPoints = 1f;
    [SerializeField] public float ENEMY21armor = 0f;
    [SerializeField] public bool ENEMY21healthBar = false;
    [SerializeField] public float ENEMY21bounty = 0f;
    [SerializeField] public float ENEMY21moveSpeed = 3f;
    [SerializeField] public bool ENEMY21willFly = false;
    [SerializeField] public int ENEMY21carryCapacity = 0;
    [SerializeField] public bool ENEMY21willStealNectar = false;
    [SerializeField] public bool ENEMY21willStealHoney = false;
    [SerializeField] public bool ENEMY21willAttack = true;
    [SerializeField] public string ENEMY21effect = "none";
    [SerializeField] public float ENEMY21effectModifier = 0f;
    [SerializeField] public float ENEMY21attackDamage = 1f;
    [SerializeField] public float ENEMY21attackRate = 1f;
    [Header("")]
    [Header("ID 22")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY22prefab;
    [SerializeField] public GameObject ENEMY22prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY22name = "";
    [SerializeField] public float ENEMY22hitPoints = 1f;
    [SerializeField] public float ENEMY22armor = 0f;
    [SerializeField] public bool ENEMY22healthBar = false;
    [SerializeField] public float ENEMY22bounty = 0f;
    [SerializeField] public float ENEMY22moveSpeed = 3f;
    [SerializeField] public bool ENEMY22willFly = false;
    [SerializeField] public int ENEMY22carryCapacity = 0;
    [SerializeField] public bool ENEMY22willStealNectar = false;
    [SerializeField] public bool ENEMY22willStealHoney = false;
    [SerializeField] public bool ENEMY22willAttack = true;
    [SerializeField] public string ENEMY22effect = "none";
    [SerializeField] public float ENEMY22effectModifier = 0f;
    [SerializeField] public float ENEMY22attackDamage = 1f;
    [SerializeField] public float ENEMY22attackRate = 1f;
    [Header("")]
    [Header("ID 23")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY23prefab;
    [SerializeField] public GameObject ENEMY23prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY23name = "";
    [SerializeField] public float ENEMY23hitPoints = 1f;
    [SerializeField] public float ENEMY23armor = 0f;
    [SerializeField] public bool ENEMY23healthBar = false;
    [SerializeField] public float ENEMY23bounty = 0f;
    [SerializeField] public float ENEMY23moveSpeed = 3f;
    [SerializeField] public bool ENEMY23willFly = false;
    [SerializeField] public int ENEMY23carryCapacity = 0;
    [SerializeField] public bool ENEMY23willStealNectar = false;
    [SerializeField] public bool ENEMY23willStealHoney = false;
    [SerializeField] public bool ENEMY23willAttack = true;
    [SerializeField] public string ENEMY23effect = "none";
    [SerializeField] public float ENEMY23effectModifier = 0f;
    [SerializeField] public float ENEMY23attackDamage = 1f;
    [SerializeField] public float ENEMY23attackRate = 1f;
    [Header("")]
    [Header("ID 24")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY24prefab;
    [SerializeField] public GameObject ENEMY24prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY24name = "";
    [SerializeField] public float ENEMY24hitPoints = 1f;
    [SerializeField] public float ENEMY24armor = 0f;
    [SerializeField] public bool ENEMY24healthBar = false;
    [SerializeField] public float ENEMY24bounty = 0f;
    [SerializeField] public float ENEMY24moveSpeed = 3f;
    [SerializeField] public bool ENEMY24willFly = false;
    [SerializeField] public int ENEMY24carryCapacity = 0;
    [SerializeField] public bool ENEMY24willStealNectar = false;
    [SerializeField] public bool ENEMY24willStealHoney = false;
    [SerializeField] public bool ENEMY24willAttack = true;
    [SerializeField] public string ENEMY24effect = "none";
    [SerializeField] public float ENEMY24effectModifier = 0f;
    [SerializeField] public float ENEMY24attackDamage = 1f;
    [SerializeField] public float ENEMY24attackRate = 1f;
    [Header("")]
    [Header("ID 25")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY25prefab;
    [SerializeField] public GameObject ENEMY25prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY25name = "";
    [SerializeField] public float ENEMY25hitPoints = 1f;
    [SerializeField] public float ENEMY25armor = 0f;
    [SerializeField] public bool ENEMY25healthBar = false;
    [SerializeField] public float ENEMY25bounty = 0f;
    [SerializeField] public float ENEMY25moveSpeed = 3f;
    [SerializeField] public bool ENEMY25willFly = false;
    [SerializeField] public int ENEMY25carryCapacity = 0;
    [SerializeField] public bool ENEMY25willStealNectar = false;
    [SerializeField] public bool ENEMY25willStealHoney = false;
    [SerializeField] public bool ENEMY25willAttack = true;
    [SerializeField] public string ENEMY25effect = "none";
    [SerializeField] public float ENEMY25effectModifier = 0f;
    [SerializeField] public float ENEMY25attackDamage = 1f;
    [SerializeField] public float ENEMY25attackRate = 1f;
    [Header("")]
    [Header("ID 26")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY26prefab;
    [SerializeField] public GameObject ENEMY26prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY26name = "";
    [SerializeField] public float ENEMY26hitPoints = 1f;
    [SerializeField] public float ENEMY26armor = 0f;
    [SerializeField] public bool ENEMY26healthBar = false;
    [SerializeField] public float ENEMY26bounty = 0f;
    [SerializeField] public float ENEMY26moveSpeed = 3f;
    [SerializeField] public bool ENEMY26willFly = false;
    [SerializeField] public int ENEMY26carryCapacity = 0;
    [SerializeField] public bool ENEMY26willStealNectar = false;
    [SerializeField] public bool ENEMY26willStealHoney = false;
    [SerializeField] public bool ENEMY26willAttack = true;
    [SerializeField] public string ENEMY26effect = "none";
    [SerializeField] public float ENEMY26effectModifier = 0f;
    [SerializeField] public float ENEMY26attackDamage = 1f;
    [SerializeField] public float ENEMY26attackRate = 1f;
    [Header("")]
    [Header("ID 27")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY27prefab;
    [SerializeField] public GameObject ENEMY27prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY27name = "";
    [SerializeField] public float ENEMY27hitPoints = 1f;
    [SerializeField] public float ENEMY27armor = 0f;
    [SerializeField] public bool ENEMY27healthBar = false;
    [SerializeField] public float ENEMY27bounty = 0f;
    [SerializeField] public float ENEMY27moveSpeed = 3f;
    [SerializeField] public bool ENEMY27willFly = false;
    [SerializeField] public int ENEMY27carryCapacity = 0;
    [SerializeField] public bool ENEMY27willStealNectar = false;
    [SerializeField] public bool ENEMY27willStealHoney = false;
    [SerializeField] public bool ENEMY27willAttack = true;
    [SerializeField] public string ENEMY27effect = "none";
    [SerializeField] public float ENEMY27effectModifier = 0f;
    [SerializeField] public float ENEMY27attackDamage = 1f;
    [SerializeField] public float ENEMY27attackRate = 1f;
    [Header("")]
    [Header("ID 28")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY28prefab;
    [SerializeField] public GameObject ENEMY28prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY28name = "";
    [SerializeField] public float ENEMY28hitPoints = 1f;
    [SerializeField] public float ENEMY28armor = 0f;
    [SerializeField] public bool ENEMY28healthBar = false;
    [SerializeField] public float ENEMY28bounty = 0f;
    [SerializeField] public float ENEMY28moveSpeed = 3f;
    [SerializeField] public bool ENEMY28willFly = false;
    [SerializeField] public int ENEMY28carryCapacity = 0;
    [SerializeField] public bool ENEMY28willStealNectar = false;
    [SerializeField] public bool ENEMY28willStealHoney = false;
    [SerializeField] public bool ENEMY28willAttack = true;
    [SerializeField] public string ENEMY28effect = "none";
    [SerializeField] public float ENEMY28effectModifier = 0f;
    [SerializeField] public float ENEMY28attackDamage = 1f;
    [SerializeField] public float ENEMY28attackRate = 1f;
    [Header("")]
    [Header("ID 29")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY29prefab;
    [SerializeField] public GameObject ENEMY29prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY29name = "";
    [SerializeField] public float ENEMY29hitPoints = 1f;
    [SerializeField] public float ENEMY29armor = 0f;
    [SerializeField] public bool ENEMY29healthBar = false;
    [SerializeField] public float ENEMY29bounty = 0f;
    [SerializeField] public float ENEMY29moveSpeed = 3f;
    [SerializeField] public bool ENEMY29willFly = false;
    [SerializeField] public int ENEMY29carryCapacity = 0;
    [SerializeField] public bool ENEMY29willStealNectar = false;
    [SerializeField] public bool ENEMY29willStealHoney = false;
    [SerializeField] public bool ENEMY29willAttack = true;
    [SerializeField] public string ENEMY29effect = "none";
    [SerializeField] public float ENEMY29effectModifier = 0f;
    [SerializeField] public float ENEMY29attackDamage = 1f;
    [SerializeField] public float ENEMY29attackRate = 1f;
    [Header("")]
    [Header("ID 30")]
    [Header("References")]
    [SerializeField] public GameObject ENEMY30prefab;
    [SerializeField] public GameObject ENEMY30prefab2;
    [Header("Attributes")]
    [SerializeField] public string ENEMY30name = "";
    [SerializeField] public float ENEMY30hitPoints = 1f;
    [SerializeField] public float ENEMY30armor = 0f;
    [SerializeField] public bool ENEMY30healthBar = false;
    [SerializeField] public float ENEMY30bounty = 0f;
    [SerializeField] public float ENEMY30moveSpeed = 3f;
    [SerializeField] public bool ENEMY30willFly = false;
    [SerializeField] public int ENEMY30carryCapacity = 0;
    [SerializeField] public bool ENEMY30willStealNectar = false;
    [SerializeField] public bool ENEMY30willStealHoney = false;
    [SerializeField] public bool ENEMY30willAttack = true;
    [SerializeField] public string ENEMY30effect = "none";
    [SerializeField] public float ENEMY30effectModifier = 0f;
    [SerializeField] public float ENEMY30attackDamage = 1f;
    [SerializeField] public float ENEMY30attackRate = 1f;

    [Header("_______________________")]
    [Header("Boss Mobs")]

    [Header("Hummingbird")]
    [Header("ID 0")]
    [Header("References")]
    [SerializeField] public GameObject BOSS0prefab;
    [SerializeField] public GameObject BOSS0prefab2;
    [SerializeField] public GameObject BOSS0prefab3;
    [Header("Attributes")]
    [SerializeField] public string BOSS0name = "Hummingbird";
    [SerializeField] public float BOSS0hitPoints = 100f;
    [SerializeField] public float BOSS0armor = 0f;
    [SerializeField] public bool BOSS0healthBar = true;
    [SerializeField] public float BOSS0bounty = 0f;
    [SerializeField] public float BOSS0moveSpeed = 5f;
    [SerializeField] public bool BOSS0willFly = true;
    [SerializeField] public int BOSS0carryCapacity = 0;
    [SerializeField] public bool BOSS0willStealNectar = false;
    [SerializeField] public bool BOSS0willStealHoney = false;
    [SerializeField] public bool BOSS0willAttack = true;
    [SerializeField] public string BOSS0effect = "Hummingbird";
    [SerializeField] public float BOSS0effectModifier = 1f;
    [SerializeField] public float BOSS0attackDamage = 1f;
    [SerializeField] public float BOSS0attackRate = 1f;
    [Header("Chicken")]
    [Header("ID 1")]
    [Header("References")]
    [SerializeField] public GameObject BOSS1prefab;
    [SerializeField] public GameObject BOSS1prefab2;
    [SerializeField] public GameObject BOSS1prefab3;
    [Header("Attributes")]
    [SerializeField] public string BOSS1name = "Chicken";
    [SerializeField] public float BOSS1hitPoints = 200f;
    [SerializeField] public float BOSS1armor = 10f;
    [SerializeField] public bool BOSS1healthBar = true;
    [SerializeField] public float BOSS1bounty = 0f;
    [SerializeField] public float BOSS1moveSpeed = 3f;
    [SerializeField] public bool BOSS1willFly = false;
    [SerializeField] public int BOSS1carryCapacity = 100;
    [SerializeField] public bool BOSS1willStealNectar = false;
    [SerializeField] public bool BOSS1willStealHoney = false;
    [SerializeField] public bool BOSS1willAttack = true;
    [SerializeField] public string BOSS1effect = "Chicken";
    [SerializeField] public float BOSS1effectModifier = 1f;
    [SerializeField] public float BOSS1attackDamage = 1f;
    [SerializeField] public float BOSS1attackRate = 1f;
    [Header("Skunk")]
    [Header("ID 2")]
    [Header("References")]
    [SerializeField] public GameObject BOSS2prefab;
    [SerializeField] public GameObject BOSS2prefab2;
    [SerializeField] public GameObject BOSS2prefab3;
    [Header("Attributes")]
    [SerializeField] public string BOSS2name = "Skunk";
    [SerializeField] public float BOSS2hitPoints = 300f;
    [SerializeField] public float BOSS2armor = 10f;
    [SerializeField] public bool BOSS2healthBar = true;
    [SerializeField] public float BOSS2bounty = 0f;
    [SerializeField] public float BOSS2moveSpeed = 3f;
    [SerializeField] public bool BOSS2willFly = false;
    [SerializeField] public int BOSS2carryCapacity = 100;
    [SerializeField] public bool BOSS2willStealNectar = false;
    [SerializeField] public bool BOSS2willStealHoney = false;
    [SerializeField] public bool BOSS2willAttack = true;
    [SerializeField] public string BOSS2effect = "Skunk";
    [SerializeField] public float BOSS2effectModifier = 1f;
    [SerializeField] public float BOSS2attackDamage = 1f;
    [SerializeField] public float BOSS2attackRate = 1f;
    [Header("")]
    [Header("ID 3")]
    [Header("References")]
    [SerializeField] public GameObject BOSS3prefab;
    [SerializeField] public GameObject BOSS3prefab2;
    [SerializeField] public GameObject BOSS3prefab3;
    [Header("Attributes")]
    [SerializeField] public string BOSS3name = "";
    [SerializeField] public float BOSS3hitPoints = 300f;
    [SerializeField] public float BOSS3armor = 0f;
    [SerializeField] public bool BOSS3healthBar = true;
    [SerializeField] public float BOSS3bounty = 0f;
    [SerializeField] public float BOSS3moveSpeed = 3f;
    [SerializeField] public bool BOSS3willFly = false;
    [SerializeField] public int BOSS3carryCapacity = 100;
    [SerializeField] public bool BOSS3willStealNectar = false;
    [SerializeField] public bool BOSS3willStealHoney = false;
    [SerializeField] public bool BOSS3willAttack = true;
    [SerializeField] public string BOSS3effect = "Skunk";
    [SerializeField] public float BOSS3effectModifier = 1f;
    [SerializeField] public float BOSS3attackDamage = 1f;
    [SerializeField] public float BOSS3attackRate = 1f;
    [Header("")]
    [Header("ID 4")]
    [Header("References")]
    [SerializeField] public GameObject BOSS4prefab;
    [SerializeField] public GameObject BOSS4prefab2;
    [SerializeField] public GameObject BOSS4prefab3;
    [Header("Attributes")]
    [SerializeField] public string BOSS4name = "";
    [SerializeField] public float BOSS4hitPoints = 300f;
    [SerializeField] public float BOSS4armor = 0f;
    [SerializeField] public bool BOSS4healthBar = true;
    [SerializeField] public float BOSS4bounty = 0f;
    [SerializeField] public float BOSS4moveSpeed = 3f;
    [SerializeField] public bool BOSS4willFly = false;
    [SerializeField] public int BOSS4carryCapacity = 100;
    [SerializeField] public bool BOSS4willStealNectar = false;
    [SerializeField] public bool BOSS4willStealHoney = false;
    [SerializeField] public bool BOSS4willAttack = true;
    [SerializeField] public string BOSS4effect = "Skunk";
    [SerializeField] public float BOSS4effectModifier = 1f;
    [SerializeField] public float BOSS4attackDamage = 1f;
    [SerializeField] public float BOSS4attackRate = 1f;
    [Header("")]
    [Header("ID 5")]
    [Header("References")]
    [SerializeField] public GameObject BOSS5prefab;
    [SerializeField] public GameObject BOSS5prefab2;
    [SerializeField] public GameObject BOSS5prefab3;
    [Header("Attributes")]
    [SerializeField] public string BOSS5name = "";
    [SerializeField] public float BOSS5hitPoints = 300f;
    [SerializeField] public float BOSS5armor = 0f;
    [SerializeField] public bool BOSS5healthBar = true;
    [SerializeField] public float BOSS5bounty = 0f;
    [SerializeField] public float BOSS5moveSpeed = 3f;
    [SerializeField] public bool BOSS5willFly = false;
    [SerializeField] public int BOSS5carryCapacity = 100;
    [SerializeField] public bool BOSS5willStealNectar = false;
    [SerializeField] public bool BOSS5willStealHoney = false;
    [SerializeField] public bool BOSS5willAttack = true;
    [SerializeField] public string BOSS5effect = "Skunk";
    [SerializeField] public float BOSS5effectModifier = 1f;
    [SerializeField] public float BOSS5attackDamage = 1f;
    [SerializeField] public float BOSS5attackRate = 1f;
    [Header("")]
    [Header("ID 6")]
    [Header("References")]
    [SerializeField] public GameObject BOSS6prefab;
    [SerializeField] public GameObject BOSS6prefab2;
    [SerializeField] public GameObject BOSS6prefab3;
    [Header("Attributes")]
    [SerializeField] public string BOSS6name = "";
    [SerializeField] public float BOSS6hitPoints = 300f;
    [SerializeField] public float BOSS6armor = 0f;
    [SerializeField] public bool BOSS6healthBar = true;
    [SerializeField] public float BOSS6bounty = 0f;
    [SerializeField] public float BOSS6moveSpeed = 3f;
    [SerializeField] public bool BOSS6willFly = false;
    [SerializeField] public int BOSS6carryCapacity = 100;
    [SerializeField] public bool BOSS6willStealNectar = false;
    [SerializeField] public bool BOSS6willStealHoney = false;
    [SerializeField] public bool BOSS6willAttack = true;
    [SerializeField] public string BOSS6effect = "Skunk";
    [SerializeField] public float BOSS6effectModifier = 1f;
    [SerializeField] public float BOSS6attackDamage = 1f;
    [SerializeField] public float BOSS6attackRate = 1f;
    [Header("")]
    [Header("ID 7")]
    [Header("References")]
    [SerializeField] public GameObject BOSS7prefab;
    [SerializeField] public GameObject BOSS7prefab2;
    [SerializeField] public GameObject BOSS7prefab3;
    [Header("Attributes")]
    [SerializeField] public string BOSS7name = "";
    [SerializeField] public float BOSS7hitPoints = 300f;
    [SerializeField] public float BOSS7armor = 0f;
    [SerializeField] public bool BOSS7healthBar = true;
    [SerializeField] public float BOSS7bounty = 0f;
    [SerializeField] public float BOSS7moveSpeed = 3f;
    [SerializeField] public bool BOSS7willFly = false;
    [SerializeField] public int BOSS7carryCapacity = 100;
    [SerializeField] public bool BOSS7willStealNectar = false;
    [SerializeField] public bool BOSS7willStealHoney = false;
    [SerializeField] public bool BOSS7willAttack = true;
    [SerializeField] public string BOSS7effect = "Skunk";
    [SerializeField] public float BOSS7effectModifier = 1f;
    [SerializeField] public float BOSS7attackDamage = 1f;
    [SerializeField] public float BOSS7attackRate = 1f;
    [Header("")]
    [Header("ID 8")]
    [Header("References")]
    [SerializeField] public GameObject BOSS8prefab;
    [SerializeField] public GameObject BOSS8prefab2;
    [SerializeField] public GameObject BOSS8prefab3;
    [Header("Attributes")]
    [SerializeField] public string BOSS8name = "";
    [SerializeField] public float BOSS8hitPoints = 300f;
    [SerializeField] public float BOSS8armor = 0f;
    [SerializeField] public bool BOSS8healthBar = true;
    [SerializeField] public float BOSS8bounty = 0f;
    [SerializeField] public float BOSS8moveSpeed = 3f;
    [SerializeField] public bool BOSS8willFly = false;
    [SerializeField] public int BOSS8carryCapacity = 100;
    [SerializeField] public bool BOSS8willStealNectar = false;
    [SerializeField] public bool BOSS8willStealHoney = false;
    [SerializeField] public bool BOSS8willAttack = true;
    [SerializeField] public string BOSS8effect = "Skunk";
    [SerializeField] public float BOSS8effectModifier = 1f;
    [SerializeField] public float BOSS8attackDamage = 1f;
    [SerializeField] public float BOSS8attackRate = 1f;
    [Header("")]
    [Header("ID 9")]
    [Header("References")]
    [SerializeField] public GameObject BOSS9prefab;
    [SerializeField] public GameObject BOSS9prefab2;
    [SerializeField] public GameObject BOSS9prefab3;
    [Header("Attributes")]
    [SerializeField] public string BOSS9name = "";
    [SerializeField] public float BOSS9hitPoints = 300f;
    [SerializeField] public float BOSS9armor = 0f;
    [SerializeField] public bool BOSS9healthBar = true;
    [SerializeField] public float BOSS9bounty = 0f;
    [SerializeField] public float BOSS9moveSpeed = 3f;
    [SerializeField] public bool BOSS9willFly = false;
    [SerializeField] public int BOSS9carryCapacity = 100;
    [SerializeField] public bool BOSS9willStealNectar = false;
    [SerializeField] public bool BOSS9willStealHoney = false;
    [SerializeField] public bool BOSS9willAttack = true;
    [SerializeField] public string BOSS9effect = "Skunk";
    [SerializeField] public float BOSS9effectModifier = 1f;
    [SerializeField] public float BOSS9attackDamage = 1f;
    [SerializeField] public float BOSS9attackRate = 1f;
    [Header("")]
    [Header("ID 10")]
    [Header("References")]
    [SerializeField] public GameObject BOSS10prefab;
    [SerializeField] public GameObject BOSS10prefab2;
    [SerializeField] public GameObject BOSS10prefab3;
    [Header("Attributes")]
    [SerializeField] public string BOSS10name = "";
    [SerializeField] public float BOSS10hitPoints = 300f;
    [SerializeField] public float BOSS10armor = 0f;
    [SerializeField] public bool BOSS10healthBar = true;
    [SerializeField] public float BOSS10bounty = 0f;
    [SerializeField] public float BOSS10moveSpeed = 3f;
    [SerializeField] public bool BOSS10willFly = false;
    [SerializeField] public int BOSS10carryCapacity = 100;
    [SerializeField] public bool BOSS10willStealNectar = false;
    [SerializeField] public bool BOSS10willStealHoney = false;
    [SerializeField] public bool BOSS10willAttack = true;
    [SerializeField] public string BOSS10effect = "Skunk";
    [SerializeField] public float BOSS10effectModifier = 1f;
    [SerializeField] public float BOSS10attackDamage = 1f;
    [SerializeField] public float BOSS10attackRate = 1f;

    [Header("_______________________")]
    [Header("Towers")]

    [SerializeField] public int[] buildable;
    [SerializeField] public int[] buildableHive;
    [SerializeField] public int[] buildableFlower;

    [Header("Scout")]
    [Header("ID 0")]
    [Header("References")]
    [SerializeField] public GameObject TOWER0prefab;
    [SerializeField] public LayerMask TOWER0enemyMask;
    [SerializeField] public GameObject TOWER0projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER0name;
    [SerializeField] public float TOWER0targetingRange = 3f;
    [SerializeField] public float TOWER0rotationSpeed = 200f;
    [SerializeField] public float TOWER0damage = 0f;
    [SerializeField] public float TOWER0armorPierce = 0f;
    [SerializeField] public float TOWER0aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER0effect = "none";
    [SerializeField] public float TOWER0effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER0effectRatio = 0f;
    [SerializeField] public int[] TOWER0upgradeIndex;
    [SerializeField] public float TOWER0efficiency = 0f;
    [SerializeField] public bool TOWER0hasTargetSettings = false;
    [SerializeField] public bool TOWER0ignoreTerrain = false;
    [SerializeField] public float TOWER0cost = 35f;
    [SerializeField] public string TOWER0canHit = "None";

    [Header("Single-Target T1")]
    [Header("ID 1")]
    [Header("References")]
    [SerializeField] public GameObject TOWER1prefab;
    [SerializeField] public LayerMask TOWER1enemyMask;
    [SerializeField] public GameObject TOWER1projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER1name;
    [SerializeField] public float TOWER1targetingRange = 3f;
    [SerializeField] public float TOWER1rotationSpeed = 200f;
    [SerializeField] public float TOWER1damage = 1f;
    [SerializeField] public float TOWER1armorPierce = 10f;
    [SerializeField] public float TOWER1aps = 1f; // Attacks Per Second
    [SerializeField] public string TOWER1effect = "none";
    [SerializeField] public float TOWER1effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER1effectRatio = 0f;
    [SerializeField] public int[] TOWER1upgradeIndex;
    [SerializeField] public float TOWER1efficiency = 0f;
    [SerializeField] public bool TOWER1hasTargetSettings = true;
    [SerializeField] public bool TOWER1ignoreTerrain = false;
    [SerializeField] public float TOWER1cost = 100f;
    [SerializeField] public string TOWER1canHit = "All";

    [Header("Single-Target T2-1")]
    [Header("ID 2")]
    [Header("References")]
    [SerializeField] public GameObject TOWER2prefab;
    [SerializeField] public LayerMask TOWER2enemyMask;
    [SerializeField] public GameObject TOWER2projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER2name;
    [SerializeField] public float TOWER2targetingRange = 5f;
    [SerializeField] public float TOWER2rotationSpeed = 200f;
    [SerializeField] public float TOWER2damage = 2f;
    [SerializeField] public float TOWER2armorPierce = 30f;
    [SerializeField] public float TOWER2aps = 1f; // Attacks Per Second
    [SerializeField] public string TOWER2effect = "none";
    [SerializeField] public float TOWER2effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER2effectRatio = 0f;
    [SerializeField] public int[] TOWER2upgradeIndex;
    [SerializeField] public float TOWER2efficiency = 0f;
    [SerializeField] public bool TOWER2hasTargetSettings = true;
    [SerializeField] public bool TOWER2ignoreTerrain = false;
    [SerializeField] public float TOWER2cost = 200f;
    [SerializeField] public string TOWER2canHit = "All";

    [Header("Single-Target T2-2")]
    [Header("ID 3")]
    [Header("References")]
    [SerializeField] public GameObject TOWER3prefab;
    [SerializeField] public LayerMask TOWER3enemyMask;
    [SerializeField] public GameObject TOWER3projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER3name;
    [SerializeField] public float TOWER3targetingRange = 3f;
    [SerializeField] public float TOWER3rotationSpeed = 200f;
    [SerializeField] public float TOWER3armorPierce = 20f;
    [SerializeField] public float TOWER3damage = 1f;
    [SerializeField] public float TOWER3aps = 3f; // Attacks Per Second
    [SerializeField] public string TOWER3effect = "none";
    [SerializeField] public float TOWER3effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER3effectRatio = 0f;
    [SerializeField] public int[] TOWER3upgradeIndex;
    [SerializeField] public float TOWER3efficiency = 0f;
    [SerializeField] public bool TOWER3hasTargetSettings = true;
    [SerializeField] public bool TOWER3ignoreTerrain = false;
    [SerializeField] public float TOWER3cost = 200f;
    [SerializeField] public string TOWER3canHit = "All";

    [Header("Single-Target T3-1")]
    [Header("ID 4")]
    [Header("References")]
    [SerializeField] public GameObject TOWER4prefab;
    [SerializeField] public LayerMask TOWER4enemyMask;
    [SerializeField] public GameObject TOWER4projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER4name;
    [SerializeField] public float TOWER4targetingRange = 7f;
    [SerializeField] public float TOWER4rotationSpeed = 200f;
    [SerializeField] public float TOWER4damage = 5f;
    [SerializeField] public float TOWER4armorPierce = 50f;
    [SerializeField] public float TOWER4aps = 1f; // Attacks Per Second
    [SerializeField] public string TOWER4effect = "none";
    [SerializeField] public float TOWER4effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER4effectRatio = 0f;
    [SerializeField] public int[] TOWER4upgradeIndex;
    [SerializeField] public float TOWER4efficiency = 0f;
    [SerializeField] public bool TOWER4hasTargetSettings = true;
    [SerializeField] public bool TOWER4ignoreTerrain = true;
    [SerializeField] public float TOWER4cost = 450f;
    [SerializeField] public string TOWER4canHit = "All";

    [Header("Single-Target T3-2")]
    [Header("ID 5")]
    [Header("References")]
    [SerializeField] public GameObject TOWER5prefab;
    [SerializeField] public LayerMask TOWER5enemyMask;
    [SerializeField] public GameObject TOWER5projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER5name;
    [SerializeField] public float TOWER5targetingRange = 3f;
    [SerializeField] public float TOWER5rotationSpeed = 300f;
    [SerializeField] public float TOWER5damage = 1f;
    [SerializeField] public float TOWER5armorPierce = 30f;
    [SerializeField] public float TOWER5aps = 5f; // Attacks Per Second
    [SerializeField] public string TOWER5effect = "none";
    [SerializeField] public float TOWER5effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER5effectRatio = 0f;
    [SerializeField] public int[] TOWER5upgradeIndex;
    [SerializeField] public float TOWER5efficiency = 0f;
    [SerializeField] public bool TOWER5hasTargetSettings = true;
    [SerializeField] public bool TOWER5ignoreTerrain = false;
    [SerializeField] public float TOWER5cost = 450f;
    [SerializeField] public string TOWER5canHit = "All";

    [Header("Multi-Target T1")]
    [Header("ID 6")]
    [Header("References")]
    [SerializeField] public GameObject TOWER6prefab;
    [SerializeField] public LayerMask TOWER6enemyMask;
    [SerializeField] public GameObject TOWER6projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER6name;
    [SerializeField] public float TOWER6targetingRange = 3f;
    [SerializeField] public float TOWER6rotationSpeed = 200f;
    [SerializeField] public float TOWER6damage = 1f;
    [SerializeField] public float TOWER6armorPierce = 0f;
    [SerializeField] public float TOWER6aps = 1f; // Attacks Per Second
    [SerializeField] public string TOWER6effect = "none";
    [SerializeField] public float TOWER6effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER6effectRatio = 0f;
    [SerializeField] public int[] TOWER6upgradeIndex;
    [SerializeField] public float TOWER6efficiency = 0f;
    [SerializeField] public bool TOWER6hasTargetSettings = true;
    [SerializeField] public bool TOWER6ignoreTerrain = false;
    [SerializeField] public float TOWER6cost = 150f;
    [SerializeField] public string TOWER6canHit = "Ground";

    [Header("Multi-Target T2-1")]
    [Header("ID 7")]
    [Header("References")]
    [SerializeField] public GameObject TOWER7prefab;
    [SerializeField] public LayerMask TOWER7enemyMask;
    [SerializeField] public GameObject TOWER7projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER7name;
    [SerializeField] public float TOWER7targetingRange = 3f;
    [SerializeField] public float TOWER7rotationSpeed = 200f;
    [SerializeField] public float TOWER7damage = 2f;
    [SerializeField] public float TOWER7armorPierce = 0f;
    [SerializeField] public float TOWER7aps = 1f; // Attacks Per Second
    [SerializeField] public string TOWER7effect = "none";
    [SerializeField] public float TOWER7effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER7effectRatio = 0f;
    [SerializeField] public int[] TOWER7upgradeIndex;
    [SerializeField] public float TOWER7efficiency = 0f;
    [SerializeField] public bool TOWER7hasTargetSettings = true;
    [SerializeField] public bool TOWER7ignoreTerrain = false;
    [SerializeField] public float TOWER7cost = 250f;
    [SerializeField] public string TOWER7canHit = "Ground";

    [Header("Multi-Target T2-2")]
    [Header("ID 8")]
    [Header("References")]
    [SerializeField] public GameObject TOWER8prefab;
    [SerializeField] public LayerMask TOWER8enemyMask;
    [SerializeField] public GameObject TOWER8projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER8name;
    [SerializeField] public float TOWER8targetingRange = 3f;
    [SerializeField] public float TOWER8rotationSpeed = 200f;
    [SerializeField] public float TOWER8damage = 1f;
    [SerializeField] public float TOWER8armorPierce = 0f;
    [SerializeField] public float TOWER8aps = 1f; // Attacks Per Second
    [SerializeField] public string TOWER8effect = "none";
    [SerializeField] public float TOWER8effectDuration = 3f; // Duration of Effect
    [SerializeField] public float TOWER8effectRatio = 0f;
    [SerializeField] public int[] TOWER8upgradeIndex;
    [SerializeField] public float TOWER8efficiency = 0f;
    [SerializeField] public bool TOWER8hasTargetSettings = true;
    [SerializeField] public bool TOWER8ignoreTerrain = false;
    [SerializeField] public float TOWER8cost = 150f;
    [SerializeField] public string TOWER8canHit = "All";

    [Header("Multi-Target T3-1")]
    [Header("ID 9")]
    [Header("References")]
    [SerializeField] public GameObject TOWER9prefab;
    [SerializeField] public LayerMask TOWER9enemyMask;
    [SerializeField] public GameObject TOWER9projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER9name;
    [SerializeField] public float TOWER9targetingRange = 4f;
    [SerializeField] public float TOWER9rotationSpeed = 200f;
    [SerializeField] public float TOWER9damage = 3f;
    [SerializeField] public float TOWER9armorPierce = 0f;
    [SerializeField] public float TOWER9aps = 1f; // Attacks Per Second
    [SerializeField] public string TOWER9effect = "none";
    [SerializeField] public float TOWER9effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER9effectRatio = 0f;
    [SerializeField] public int[] TOWER9upgradeIndex;
    [SerializeField] public float TOWER9efficiency = 0f;
    [SerializeField] public bool TOWER9hasTargetSettings = true;
    [SerializeField] public bool TOWER9ignoreTerrain = true;
    [SerializeField] public float TOWER9cost = 500f;
    [SerializeField] public string TOWER9canHit = "Ground";

    [Header("Multi-Target T3-2")]
    [Header("ID 10")]
    [Header("References")]
    [SerializeField] public GameObject TOWER10prefab;
    [SerializeField] public LayerMask TOWER10enemyMask;
    [SerializeField] public GameObject TOWER10projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER10name;
    [SerializeField] public float TOWER10targetingRange = 4f;
    [SerializeField] public float TOWER10rotationSpeed = 200f;
    [SerializeField] public float TOWER10damage = 2f;
    [SerializeField] public float TOWER10armorPierce = 0f;
    [SerializeField] public float TOWER10aps = 1.5f; // Attacks Per Second
    [SerializeField] public string TOWER10effect = "none";
    [SerializeField] public float TOWER10effectDuration = 5f; // Duration of Effect
    [SerializeField] public float TOWER10effectRatio = 0f;
    [SerializeField] public int[] TOWER10upgradeIndex;
    [SerializeField] public float TOWER10efficiency = 0f;
    [SerializeField] public bool TOWER10hasTargetSettings = true;
    [SerializeField] public bool TOWER10ignoreTerrain = false;
    [SerializeField] public float TOWER10cost = 500f;
    [SerializeField] public string TOWER10canHit = "All";

    [Header("Effect T1")]
    [Header("ID 11")]
    [Header("References")]
    [SerializeField] public GameObject TOWER11prefab;
    [SerializeField] public LayerMask TOWER11enemyMask;
    [SerializeField] public GameObject TOWER11projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER11name;
    [SerializeField] public float TOWER11targetingRange = 3f;
    [SerializeField] public float TOWER11rotationSpeed = 200f;
    [SerializeField] public float TOWER11damage = 0f;
    [SerializeField] public float TOWER11armorPierce = 0f;
    [SerializeField] public float TOWER11aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER11effect = "Slow Pulse";
    [SerializeField] public float TOWER11effectDuration = 1f; // Duration of Effect
    [SerializeField] public float TOWER11effectRatio = 1f;
    [SerializeField] public int[] TOWER11upgradeIndex;
    [SerializeField] public float TOWER11efficiency = 0f;
    [SerializeField] public bool TOWER11hasTargetSettings = false;
    [SerializeField] public bool TOWER11ignoreTerrain = false;
    [SerializeField] public float TOWER11cost = 175f;
    [SerializeField] public string TOWER11canHit = "All";

    [Header("Effect T2-1")]
    [Header("ID 12")]
    [Header("References")]
    [SerializeField] public GameObject TOWER12prefab;
    [SerializeField] public LayerMask TOWER12enemyMask;
    [SerializeField] public GameObject TOWER12projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER12name;
    [SerializeField] public float TOWER12targetingRange = 4f;
    [SerializeField] public float TOWER12rotationSpeed = 200f;
    [SerializeField] public float TOWER12damage = 0f;
    [SerializeField] public float TOWER12armorPierce = 0f;
    [SerializeField] public float TOWER12aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER12effect = "Slow Pulse";
    [SerializeField] public float TOWER12effectDuration = 2f; // Duration of Effect
    [SerializeField] public float TOWER12effectRatio = 2f;
    [SerializeField] public int[] TOWER12upgradeIndex;
    [SerializeField] public float TOWER12efficiency = 0f;
    [SerializeField] public bool TOWER12hasTargetSettings = false;
    [SerializeField] public bool TOWER12ignoreTerrain = false;
    [SerializeField] public float TOWER12cost = 300f;
    [SerializeField] public string TOWER12canHit = "All";

    [Header("Effect T2-2")]
    [Header("ID 13")]
    [Header("References")]
    [SerializeField] public GameObject TOWER13prefab;
    [SerializeField] public LayerMask TOWER13enemyMask;
    [SerializeField] public GameObject TOWER13projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER13name;
    [SerializeField] public float TOWER13targetingRange = 3f;
    [SerializeField] public float TOWER13rotationSpeed = 200f;
    [SerializeField] public float TOWER13damage = 0f;
    [SerializeField] public float TOWER13armorPierce = 0f;
    [SerializeField] public float TOWER13aps = 1f; // Attacks Per Second
    [SerializeField] public string TOWER13effect = "none";
    [SerializeField] public float TOWER13effectDuration = 2f; // Duration of Effect
    [SerializeField] public float TOWER13effectRatio = 2f;
    [SerializeField] public int[] TOWER13upgradeIndex;
    [SerializeField] public float TOWER13efficiency = 0f;
    [SerializeField] public bool TOWER13hasTargetSettings = true;
    [SerializeField] public bool TOWER13ignoreTerrain = false;
    [SerializeField] public float TOWER13cost = 300f;
    [SerializeField] public string TOWER13canHit = "All";

    [Header("Effect T3-1")]
    [Header("ID 14")]
    [Header("References")]
    [SerializeField] public GameObject TOWER14prefab;
    [SerializeField] public LayerMask TOWER14enemyMask;
    [SerializeField] public GameObject TOWER14projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER14name;
    [SerializeField] public float TOWER14targetingRange = 5f;
    [SerializeField] public float TOWER14rotationSpeed = 200f;
    [SerializeField] public float TOWER14damage = 0f;
    [SerializeField] public float TOWER14armorPierce = 0f;
    [SerializeField] public float TOWER14aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER14effect = "Slow Pulse with Freeze";
    [SerializeField] public float TOWER14effectDuration = 3f; // Duration of Effect
    [SerializeField] public float TOWER14effectRatio = 3f;
    [SerializeField] public int[] TOWER14upgradeIndex;
    [SerializeField] public float TOWER14efficiency = 1f;
    [SerializeField] public bool TOWER14hasTargetSettings = false;
    [SerializeField] public bool TOWER14ignoreTerrain = false;
    [SerializeField] public float TOWER14cost = 500f;
    [SerializeField] public string TOWER14canHit = "All";

    [Header("Effect T3-2")]
    [Header("ID 15")]
    [Header("References")]
    [SerializeField] public GameObject TOWER15prefab;
    [SerializeField] public LayerMask TOWER15enemyMask;
    [SerializeField] public GameObject TOWER15projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER15name;
    [SerializeField] public float TOWER15targetingRange = 4f;
    [SerializeField] public float TOWER15rotationSpeed = 200f;
    [SerializeField] public float TOWER15damage = 0f;
    [SerializeField] public float TOWER15armorPierce = 0f;
    [SerializeField] public float TOWER15aps = 2f; // Attacks Per Second
    [SerializeField] public string TOWER15effect = "none";
    [SerializeField] public float TOWER15effectDuration = 5f; // Duration of Effect
    [SerializeField] public float TOWER15effectRatio = 5f;
    [SerializeField] public int[] TOWER15upgradeIndex;
    [SerializeField] public float TOWER15efficiency = 0f;
    [SerializeField] public bool TOWER15hasTargetSettings = true;
    [SerializeField] public bool TOWER15ignoreTerrain = false;
    [SerializeField] public float TOWER15cost = 500f;
    [SerializeField] public string TOWER15canHit = "All";

    [Header("Economy T1")]
    [Header("ID 16")]
    [Header("References")]
    [SerializeField] public GameObject TOWER16prefab;
    [SerializeField] public LayerMask TOWER16enemyMask;
    [SerializeField] public GameObject TOWER16projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER16name;
    [SerializeField] public float TOWER16targetingRange = 3f;
    [SerializeField] public float TOWER16rotationSpeed = 200f;
    [SerializeField] public float TOWER16damage = 0f;
    [SerializeField] public float TOWER16armorPierce = 0f;
    [SerializeField] public float TOWER16aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER16effect = "none";
    [SerializeField] public float TOWER16effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER16effectRatio = 0f;
    [SerializeField] public int[] TOWER16upgradeIndex;
    [SerializeField] public float TOWER16efficiency = 1f;
    [SerializeField] public bool TOWER16hasTargetSettings = false;
    [SerializeField] public bool TOWER16ignoreTerrain = false;
    [SerializeField] public float TOWER16cost = 200f;
    [SerializeField] public string TOWER16canHit = "None";

    [Header("Economy T2-1")]
    [Header("ID 17")]
    [Header("References")]
    [SerializeField] public GameObject TOWER17prefab;
    [SerializeField] public LayerMask TOWER17enemyMask;
    [SerializeField] public GameObject TOWER17projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER17name;
    [SerializeField] public float TOWER17targetingRange = 3f;
    [SerializeField] public float TOWER17rotationSpeed = 200f;
    [SerializeField] public float TOWER17damage = 0f;
    [SerializeField] public float TOWER17armorPierce = 0f;
    [SerializeField] public float TOWER17aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER17effect = "none";
    [SerializeField] public float TOWER17effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER17effectRatio = 0f;
    [SerializeField] public int[] TOWER17upgradeIndex;
    [SerializeField] public float TOWER17efficiency = 1.5f;
    [SerializeField] public bool TOWER17hasTargetSettings = false;
    [SerializeField] public bool TOWER17ignoreTerrain = false;
    [SerializeField] public float TOWER17cost = 350f;
    [SerializeField] public string TOWER17canHit = "None";

    [Header("Economy T2-2")]
    [Header("ID 18")]
    [Header("References")]
    [SerializeField] public GameObject TOWER18prefab;
    [SerializeField] public LayerMask TOWER18enemyMask;
    [SerializeField] public GameObject TOWER18projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER18name;
    [SerializeField] public float TOWER18targetingRange = 3f;
    [SerializeField] public float TOWER18rotationSpeed = 200f;
    [SerializeField] public float TOWER18damage = 0f;
    [SerializeField] public float TOWER18armorPierce = 0f;
    [SerializeField] public float TOWER18aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER18effect = "Damage Buff";
    [SerializeField] public float TOWER18effectDuration = 1f; // Duration of Effect
    [SerializeField] public float TOWER18effectRatio = 0.2f;
    [SerializeField] public int[] TOWER18upgradeIndex;
    [SerializeField] public float TOWER18efficiency = 1f;
    [SerializeField] public bool TOWER18hasTargetSettings = false;
    [SerializeField] public bool TOWER18ignoreTerrain = false;
    [SerializeField] public float TOWER18cost = 350f;
    [SerializeField] public string TOWER18canHit = "None";

    [Header("Economy T3-1")]
    [Header("ID 19")]
    [Header("References")]
    [SerializeField] public GameObject TOWER19prefab;
    [SerializeField] public LayerMask TOWER19enemyMask;
    [SerializeField] public GameObject TOWER19projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER19name;
    [SerializeField] public float TOWER19targetingRange = 3f;
    [SerializeField] public float TOWER19rotationSpeed = 200f;
    [SerializeField] public float TOWER19damage = 0f;
    [SerializeField] public float TOWER19armorPierce = 0f;
    [SerializeField] public float TOWER19aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER19effect = "none";
    [SerializeField] public float TOWER19effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER19effectRatio = 0f;
    [SerializeField] public int[] TOWER19upgradeIndex;
    [SerializeField] public float TOWER19efficiency = 2f;
    [SerializeField] public bool TOWER19hasTargetSettings = false;
    [SerializeField] public bool TOWER19ignoreTerrain = false;
    [SerializeField] public float TOWER19cost = 600f;
    [SerializeField] public string TOWER19canHit = "None";

    [Header("Economy T3-2")]
    [Header("ID 20")]
    [Header("References")]
    [SerializeField] public GameObject TOWER20prefab;
    [SerializeField] public LayerMask TOWER20enemyMask;
    [SerializeField] public GameObject TOWER20projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER20name;
    [SerializeField] public float TOWER20targetingRange = 3f;
    [SerializeField] public float TOWER20rotationSpeed = 200f;
    [SerializeField] public float TOWER20damage = 0f;
    [SerializeField] public float TOWER20armorPierce = 0f;
    [SerializeField] public float TOWER20aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER20effect = "Damage and Speed Buff";
    [SerializeField] public float TOWER20effectDuration = 1f; // Duration of Effect
    [SerializeField] public float TOWER20effectRatio = 0.3f;
    [SerializeField] public int[] TOWER20upgradeIndex;
    [SerializeField] public float TOWER20efficiency = 1f;
    [SerializeField] public bool TOWER20hasTargetSettings = false;
    [SerializeField] public bool TOWER20ignoreTerrain = false;
    [SerializeField] public float TOWER20cost = 600f;
    [SerializeField] public string TOWER20canHit = "None";

    [Header("Investment T1")]
    [Header("ID 21")]
    [Header("References")]
    [SerializeField] public GameObject TOWER21prefab;
    [SerializeField] public LayerMask TOWER21enemyMask;
    [SerializeField] public GameObject TOWER21projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER21name;
    [SerializeField] public float TOWER21targetingRange = 3f;
    [SerializeField] public float TOWER21rotationSpeed = 200f;
    [SerializeField] public float TOWER21damage = 0f;
    [SerializeField] public float TOWER21armorPierce = 0f;
    [SerializeField] public float TOWER21aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER21effect = "none";
    [SerializeField] public float TOWER21effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER21effectRatio = 0f;
    [SerializeField] public int[] TOWER21upgradeIndex;
    [SerializeField] public float TOWER21efficiency = 1f;
    [SerializeField] public bool TOWER21hasTargetSettings = false;
    [SerializeField] public bool TOWER21ignoreTerrain = false;
    [SerializeField] public float TOWER21cost = 50f;
    [SerializeField] public string TOWER21canHit = "None";

    [Header("Investment T2-1")]
    [Header("ID 22")]
    [Header("References")]
    [SerializeField] public GameObject TOWER22prefab;
    [SerializeField] public LayerMask TOWER22enemyMask;
    [SerializeField] public GameObject TOWER22projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER22name;
    [SerializeField] public float TOWER22targetingRange = 3f;
    [SerializeField] public float TOWER22rotationSpeed = 200f;
    [SerializeField] public float TOWER22damage = 0f;
    [SerializeField] public float TOWER22armorPierce = 0f;
    [SerializeField] public float TOWER22aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER22effect = "none";
    [SerializeField] public float TOWER22effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER22effectRatio = 0f;
    [SerializeField] public int[] TOWER22upgradeIndex;
    [SerializeField] public float TOWER22efficiency = 1.5f;
    [SerializeField] public bool TOWER22hasTargetSettings = false;
    [SerializeField] public bool TOWER22ignoreTerrain = false;
    [SerializeField] public float TOWER22cost = 100f;
    [SerializeField] public string TOWER22canHit = "None";

    [Header("Investment T2-2")]
    [Header("ID 23")]
    [Header("References")]
    [SerializeField] public GameObject TOWER23prefab;
    [SerializeField] public LayerMask TOWER23enemyMask;
    [SerializeField] public GameObject TOWER23projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER23name;
    [SerializeField] public float TOWER23targetingRange = 3f;
    [SerializeField] public float TOWER23rotationSpeed = 200f;
    [SerializeField] public float TOWER23damage = 0f;
    [SerializeField] public float TOWER23armorPierce = 0f;
    [SerializeField] public float TOWER23aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER23effect = "Heal Queen";
    [SerializeField] public float TOWER23effectDuration = 1f; // Duration of Effect
    [SerializeField] public float TOWER23effectRatio = 1f;
    [SerializeField] public int[] TOWER23upgradeIndex;
    [SerializeField] public float TOWER23efficiency = 1f;
    [SerializeField] public bool TOWER23hasTargetSettings = false;
    [SerializeField] public bool TOWER23ignoreTerrain = false;
    [SerializeField] public float TOWER23cost = 100f;
    [SerializeField] public string TOWER23canHit = "None";

    [Header("Investment T3-1")]
    [Header("ID 24")]
    [Header("References")]
    [SerializeField] public GameObject TOWER24prefab;
    [SerializeField] public LayerMask TOWER24enemyMask;
    [SerializeField] public GameObject TOWER24projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER24name;
    [SerializeField] public float TOWER24targetingRange = 3f;
    [SerializeField] public float TOWER24rotationSpeed = 200f;
    [SerializeField] public float TOWER24damage = 0f;
    [SerializeField] public float TOWER24armorPierce = 0f;
    [SerializeField] public float TOWER24aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER24effect = "none";
    [SerializeField] public float TOWER24effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER24effectRatio = 0f;
    [SerializeField] public int[] TOWER24upgradeIndex;
    [SerializeField] public float TOWER24efficiency = 2.5f;
    [SerializeField] public bool TOWER24hasTargetSettings = false;
    [SerializeField] public bool TOWER24ignoreTerrain = false;
    [SerializeField] public float TOWER24cost = 200f;
    [SerializeField] public string TOWER24canHit = "None";

    [Header("Investment T3-2")]
    [Header("ID 25")]
    [Header("References")]
    [SerializeField] public GameObject TOWER25prefab;
    [SerializeField] public LayerMask TOWER25enemyMask;
    [SerializeField] public GameObject TOWER25projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER25name;
    [SerializeField] public float TOWER25targetingRange = 3f;
    [SerializeField] public float TOWER25rotationSpeed = 200f;
    [SerializeField] public float TOWER25damage = 0f;
    [SerializeField] public float TOWER25armorPierce = 0f;
    [SerializeField] public float TOWER25aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER25effect = "Heal Queen";
    [SerializeField] public float TOWER25effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER25effectRatio = 0f;
    [SerializeField] public int[] TOWER25upgradeIndex;
    [SerializeField] public float TOWER25efficiency = 1.5f;
    [SerializeField] public bool TOWER25hasTargetSettings = false;
    [SerializeField] public bool TOWER25ignoreTerrain = false;
    [SerializeField] public float TOWER25cost = 200f;
    [SerializeField] public string TOWER25canHit = "None";

    [Header("")]
    [Header("ID 26")]
    [Header("References")]
    [SerializeField] public GameObject TOWER26prefab;
    [SerializeField] public LayerMask TOWER26enemyMask;
    [SerializeField] public GameObject TOWER26projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER26name;
    [SerializeField] public float TOWER26targetingRange = 3f;
    [SerializeField] public float TOWER26rotationSpeed = 200f;
    [SerializeField] public float TOWER26damage = 0f;
    [SerializeField] public float TOWER26armorPierce = 0f;
    [SerializeField] public float TOWER26aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER26effect = "none";
    [SerializeField] public float TOWER26effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER26effectRatio = 0f;
    [SerializeField] public int[] TOWER26upgradeIndex;
    [SerializeField] public float TOWER26efficiency = 0f;
    [SerializeField] public bool TOWER26hasTargetSettings = false;
    [SerializeField] public bool TOWER26ignoreTerrain = false;
    [SerializeField] public float TOWER26cost = 50f;
    [SerializeField] public string TOWER26canHit = "None";

    [Header("")]
    [Header("ID 27")]
    [Header("References")]
    [SerializeField] public GameObject TOWER27prefab;
    [SerializeField] public LayerMask TOWER27enemyMask;
    [SerializeField] public GameObject TOWER27projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER27name;
    [SerializeField] public float TOWER27targetingRange = 3f;
    [SerializeField] public float TOWER27rotationSpeed = 200f;
    [SerializeField] public float TOWER27damage = 0f;
    [SerializeField] public float TOWER27armorPierce = 0f;
    [SerializeField] public float TOWER27aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER27effect = "none";
    [SerializeField] public float TOWER27effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER27effectRatio = 0f;
    [SerializeField] public int[] TOWER27upgradeIndex;
    [SerializeField] public float TOWER27efficiency = 0f;
    [SerializeField] public bool TOWER27hasTargetSettings = false;
    [SerializeField] public bool TOWER27ignoreTerrain = false;
    [SerializeField] public float TOWER27cost = 100f;
    [SerializeField] public string TOWER27canHit = "None";

    [Header("")]
    [Header("ID 28")]
    [Header("References")]
    [SerializeField] public GameObject TOWER28prefab;
    [SerializeField] public LayerMask TOWER28enemyMask;
    [SerializeField] public GameObject TOWER28projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER28name;
    [SerializeField] public float TOWER28targetingRange = 3f;
    [SerializeField] public float TOWER28rotationSpeed = 200f;
    [SerializeField] public float TOWER28damage = 0f;
    [SerializeField] public float TOWER28armorPierce = 0f;
    [SerializeField] public float TOWER28aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER28effect = "none";
    [SerializeField] public float TOWER28effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER28effectRatio = 0f;
    [SerializeField] public int[] TOWER28upgradeIndex;
    [SerializeField] public float TOWER28efficiency = 0.5f;
    [SerializeField] public bool TOWER28hasTargetSettings = false;
    [SerializeField] public bool TOWER28ignoreTerrain = false;
    [SerializeField] public float TOWER28cost = 100f;
    [SerializeField] public string TOWER28canHit = "None";

    [Header("")]
    [Header("ID 29")]
    [Header("References")]
    [SerializeField] public GameObject TOWER29prefab;
    [SerializeField] public LayerMask TOWER29enemyMask;
    [SerializeField] public GameObject TOWER29projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER29name;
    [SerializeField] public float TOWER29targetingRange = 3f;
    [SerializeField] public float TOWER29rotationSpeed = 200f;
    [SerializeField] public float TOWER29damage = 0f;
    [SerializeField] public float TOWER29armorPierce = 0f;
    [SerializeField] public float TOWER29aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER29effect = "none";
    [SerializeField] public float TOWER29effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER29effectRatio = 0f;
    [SerializeField] public int[] TOWER29upgradeIndex;
    [SerializeField] public float TOWER29efficiency = 0f;
    [SerializeField] public bool TOWER29hasTargetSettings = false;
    [SerializeField] public bool TOWER29ignoreTerrain = false;
    [SerializeField] public float TOWER29cost = 250f;
    [SerializeField] public string TOWER29canHit = "None";

    [Header("")]
    [Header("ID 30")]
    [Header("References")]
    [SerializeField] public GameObject TOWER30prefab;
    [SerializeField] public LayerMask TOWER30enemyMask;
    [SerializeField] public GameObject TOWER30projectilePrefab;
    [Header("Attributes")]
    [SerializeField] public string TOWER30name;
    [SerializeField] public float TOWER30targetingRange = 3f;
    [SerializeField] public float TOWER30rotationSpeed = 200f;
    [SerializeField] public float TOWER30damage = 0f;
    [SerializeField] public float TOWER30armorPierce = 0f;
    [SerializeField] public float TOWER30aps = 0f; // Attacks Per Second
    [SerializeField] public string TOWER30effect = "none";
    [SerializeField] public float TOWER30effectDuration = 0f; // Duration of Effect
    [SerializeField] public float TOWER30effectRatio = 0f;
    [SerializeField] public int[] TOWER30upgradeIndex;
    [SerializeField] public float TOWER30efficiency = 1f;
    [SerializeField] public bool TOWER30hasTargetSettings = false;
    [SerializeField] public bool TOWER30ignoreTerrain = false;
    [SerializeField] public float TOWER30cost = 250f;
    [SerializeField] public string TOWER30canHit = "None";

    [Header("_______________________")]
    [Header("Projectiles")]

    [Header("Bullet")]
    [Header("Index 0")]
    [Header("References")]
    [SerializeField] public LayerMask PROJECTILE0enemyMask;
    [Header("Attributes")]
    [SerializeField] public float PROJECTILE0projectileSpeed;
    [SerializeField] public float PROJECTILE0AoE;
    [SerializeField] public bool PROJECTILE0AoEDropOff = false;

    [Header("Fast Bullet")]
    [Header("Index 1")]
    [Header("References")]
    [SerializeField] public LayerMask PROJECTILE1enemyMask;
    [Header("Attributes")]
    [SerializeField] public float PROJECTILE1projectileSpeed;
    [SerializeField] public float PROJECTILE1AoE;
    [SerializeField] public bool PROJECTILE1AoEDropOff = false;

    [Header("Rocket")]
    [Header("Index 2")]
    [Header("References")]
    [SerializeField] public LayerMask PROJECTILE2enemyMask;
    [Header("Attributes")]
    [SerializeField] public float PROJECTILE2projectileSpeed;
    [SerializeField] public float PROJECTILE2AoE;
    [SerializeField] public bool PROJECTILE2AoEDropOff = false;

    [Header("Ricochet")]
    [Header("Index 3")]
    [Header("References")]
    [SerializeField] public LayerMask PROJECTILE3enemyMask;
    [Header("Attributes")]
    [SerializeField] public float PROJECTILE3projectileSpeed;
    [SerializeField] public float PROJECTILE3AoE;
    [SerializeField] public bool PROJECTILE3AoEDropOff = false;

    [Header("AoE Slow")]
    [Header("Index 4")]
    [Header("References")]
    [SerializeField] public LayerMask PROJECTILE4enemyMask;
    [Header("Attributes")]
    [SerializeField] public float PROJECTILE4projectileSpeed;
    [SerializeField] public float PROJECTILE4AoE;
    [SerializeField] public bool PROJECTILE4AoEDropOff = false;

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

    // Define parameter arrays
    //Flowers
    public Sprite[] FLOWERSprite;
    public string[] FLOWERText;
    public float[] FLOWERModifier;
    public float[] FLOWERRarity;
    //Mobs
    public GameObject[] ENEMYprefab;
    public GameObject[] ENEMYprefab2;
    public string[] ENEMYname;
    public float[] ENEMYhitPoints;
    public float[] ENEMYarmor;
    public bool[] ENEMYhealthBar;
    public float[] ENEMYbounty;
    public float[] ENEMYmoveSpeed;
    public bool[] ENEMYwillFly;
    public int[] ENEMYcarryCapacity;
    public bool[] ENEMYwillStealNectar;
    public bool[] ENEMYwillStealHoney;
    public bool[] ENEMYwillAttack;
    public string[] ENEMYeffect;
    public float[] ENEMYeffectModifier;
    public float[] ENEMYattackDamage;
    public float[] ENEMYattackRate;
    //Boss Mobs
    public GameObject[] BOSSprefab;
    public GameObject[] BOSSprefab2;
    public GameObject[] BOSSprefab3;
    public string[] BOSSname;
    public float[] BOSShitPoints;
    public float[] BOSSarmor;
    public bool[] BOSShealthBar;
    public float[] BOSSbounty;
    public float[] BOSSmoveSpeed;
    public bool[] BOSSwillFly;
    public int[] BOSScarryCapacity;
    public bool[] BOSSwillStealNectar;
    public bool[] BOSSwillStealHoney;
    public bool[] BOSSwillAttack;
    public string[] BOSSeffect;
    public float[] BOSSeffectModifier;
    public float[] BOSSattackDamage;
    public float[] BOSSattackRate;
    //Towers
    public GameObject[] TOWERprefab;
    public LayerMask[] TOWERenemyMask;
    public GameObject[] TOWERprojectilePrefab;
    public string[] TOWERname;
    public float[] TOWERtargetingRange;
    public float[] TOWERrotationSpeed;
    public float[] TOWERdamage;
    public float[] TOWERarmorPierce;
    public float[] TOWERaps;
    public string[] TOWEReffect;
    public float[] TOWEReffectDuration;
    public float[] TOWEReffectRatio;
    public int[][] TOWERupgradeIndex;
    public float[] TOWERefficiency;
    public bool[] TOWERhasTargetSettings;
    public bool[] TOWERignoreTerrain;
    public float[] TOWERcost;
    public string[] TOWERcanHit;
    //Projectiles
    public LayerMask[] PROJECTILEenemyMask;
    public float[] PROJECTILEprojectileSpeed;
    public float[] PROJECTILEAoE;
    public bool[] PROJECTILEAoEDropOff;
    public string[] PROJECTILEcanHit;


    public void Start()
    {
    }

    public void Awake()
    {
        main = this;

        //Build parameter arrays for other scripts to pull from
        //Flower Arrays
        FLOWERSprite = new Sprite[] { closedFlowerSprite, blueFlowerSprite, whiteFlowerSprite, pinkFlowerSprite, purpleFlowerSprite, goldFlowerSprite, redFlowerSprite, yellowFlowerSprite };
        FLOWERText = new string[] { closedFlowerText, blueFlowerText, whiteFlowerText, pinkFlowerText, purpleFlowerText, goldFlowerText, redFlowerText, yellowFlowerText };
        FLOWERModifier = new float[] { closedFlowerModifier, blueFlowerModifier, whiteFlowerModifier, pinkFlowerModifier, purpleFlowerModifier, goldFlowerModifier, redFlowerModifier, yellowFlowerModifier };
        FLOWERRarity = new float[] { closedFlowerRarity, blueFlowerRarity, whiteFlowerRarity, pinkFlowerRarity, purpleFlowerRarity, goldFlowerRarity, redFlowerRarity, yellowFlowerRarity };
        //Mob Arrays
        ENEMYprefab = new GameObject[] { ENEMY0prefab, ENEMY1prefab, ENEMY2prefab, ENEMY3prefab, ENEMY4prefab, ENEMY5prefab, ENEMY6prefab, ENEMY7prefab, ENEMY8prefab, ENEMY9prefab, ENEMY10prefab, ENEMY11prefab, ENEMY12prefab, ENEMY13prefab, ENEMY14prefab, ENEMY15prefab, ENEMY16prefab, ENEMY17prefab, ENEMY18prefab, ENEMY19prefab, ENEMY20prefab, ENEMY21prefab, ENEMY22prefab, ENEMY23prefab, ENEMY24prefab, ENEMY25prefab, ENEMY26prefab, ENEMY27prefab, ENEMY28prefab, ENEMY29prefab, ENEMY30prefab };
        ENEMYprefab2 = new GameObject[] { ENEMY0prefab2, ENEMY1prefab2, ENEMY2prefab2, ENEMY3prefab2, ENEMY4prefab2, ENEMY5prefab2, ENEMY6prefab2, ENEMY7prefab2, ENEMY8prefab2, ENEMY9prefab2, ENEMY10prefab2, ENEMY11prefab2, ENEMY12prefab2, ENEMY13prefab2, ENEMY14prefab2, ENEMY15prefab2, ENEMY16prefab2, ENEMY17prefab2, ENEMY18prefab2, ENEMY19prefab2, ENEMY20prefab2, ENEMY21prefab2, ENEMY22prefab2, ENEMY23prefab2, ENEMY24prefab2, ENEMY25prefab2, ENEMY26prefab2, ENEMY27prefab2, ENEMY28prefab2, ENEMY29prefab2, ENEMY30prefab2 };
        ENEMYname = new string[] { ENEMY0name, ENEMY1name, ENEMY2name, ENEMY3name, ENEMY4name, ENEMY5name, ENEMY6name, ENEMY7name, ENEMY8name, ENEMY9name, ENEMY10name, ENEMY11name, ENEMY12name, ENEMY13name, ENEMY14name, ENEMY15name, ENEMY16name, ENEMY17name, ENEMY18name, ENEMY19name, ENEMY20name, ENEMY21name, ENEMY22name, ENEMY23name, ENEMY24name, ENEMY25name, ENEMY26name, ENEMY27name, ENEMY28name, ENEMY29name, ENEMY30name};
        ENEMYhitPoints = new float[] { ENEMY0hitPoints, ENEMY1hitPoints, ENEMY2hitPoints, ENEMY3hitPoints, ENEMY4hitPoints, ENEMY5hitPoints, ENEMY6hitPoints, ENEMY7hitPoints, ENEMY8hitPoints, ENEMY9hitPoints, ENEMY10hitPoints, ENEMY11hitPoints, ENEMY12hitPoints, ENEMY13hitPoints, ENEMY14hitPoints, ENEMY15hitPoints, ENEMY16hitPoints, ENEMY17hitPoints, ENEMY18hitPoints, ENEMY19hitPoints, ENEMY20hitPoints, ENEMY21hitPoints, ENEMY22hitPoints, ENEMY23hitPoints, ENEMY24hitPoints, ENEMY25hitPoints, ENEMY26hitPoints, ENEMY27hitPoints, ENEMY28hitPoints, ENEMY29hitPoints, ENEMY30hitPoints };
        ENEMYarmor = new float[] { ENEMY0armor, ENEMY1armor, ENEMY2armor, ENEMY3armor, ENEMY4armor, ENEMY5armor, ENEMY6armor, ENEMY7armor, ENEMY8armor, ENEMY9armor, ENEMY10armor, ENEMY11armor, ENEMY12armor, ENEMY13armor, ENEMY14armor, ENEMY15armor, ENEMY16armor, ENEMY17armor, ENEMY18armor, ENEMY19armor, ENEMY20armor, ENEMY21armor, ENEMY22armor, ENEMY23armor, ENEMY24armor, ENEMY25armor, ENEMY26armor, ENEMY27armor, ENEMY28armor, ENEMY29armor, ENEMY30armor };
        ENEMYhealthBar = new bool[] { ENEMY0healthBar, ENEMY1healthBar, ENEMY2healthBar, ENEMY3healthBar, ENEMY4healthBar, ENEMY5healthBar, ENEMY6healthBar, ENEMY7healthBar, ENEMY8healthBar, ENEMY9healthBar, ENEMY10healthBar, ENEMY11healthBar, ENEMY12healthBar, ENEMY13healthBar, ENEMY14healthBar, ENEMY15healthBar, ENEMY16healthBar, ENEMY17healthBar, ENEMY18healthBar, ENEMY19healthBar, ENEMY20healthBar, ENEMY21healthBar, ENEMY22healthBar, ENEMY23healthBar, ENEMY24healthBar, ENEMY25healthBar, ENEMY26healthBar, ENEMY27healthBar, ENEMY28healthBar, ENEMY29healthBar, ENEMY30healthBar };
        ENEMYbounty = new float[] { ENEMY0bounty, ENEMY1bounty, ENEMY2bounty, ENEMY3bounty, ENEMY4bounty, ENEMY5bounty, ENEMY6bounty, ENEMY7bounty, ENEMY8bounty, ENEMY9bounty, ENEMY10bounty, ENEMY11bounty, ENEMY12bounty, ENEMY13bounty, ENEMY14bounty, ENEMY15bounty, ENEMY16bounty, ENEMY17bounty, ENEMY18bounty, ENEMY19bounty, ENEMY20bounty, ENEMY21bounty, ENEMY22bounty, ENEMY23bounty, ENEMY24bounty, ENEMY25bounty, ENEMY26bounty, ENEMY27bounty, ENEMY28bounty, ENEMY29bounty, ENEMY30bounty };
        ENEMYmoveSpeed = new float[] { ENEMY0moveSpeed, ENEMY1moveSpeed, ENEMY2moveSpeed, ENEMY3moveSpeed, ENEMY4moveSpeed, ENEMY5moveSpeed, ENEMY6moveSpeed, ENEMY7moveSpeed, ENEMY8moveSpeed, ENEMY9moveSpeed, ENEMY10moveSpeed, ENEMY11moveSpeed, ENEMY12moveSpeed, ENEMY13moveSpeed, ENEMY14moveSpeed, ENEMY15moveSpeed, ENEMY16moveSpeed, ENEMY17moveSpeed, ENEMY18moveSpeed, ENEMY19moveSpeed, ENEMY20moveSpeed, ENEMY21moveSpeed, ENEMY22moveSpeed, ENEMY23moveSpeed, ENEMY24moveSpeed, ENEMY25moveSpeed, ENEMY26moveSpeed, ENEMY27moveSpeed, ENEMY28moveSpeed, ENEMY29moveSpeed, ENEMY30moveSpeed };
        ENEMYwillFly = new bool[] { ENEMY0willFly, ENEMY1willFly, ENEMY2willFly, ENEMY3willFly, ENEMY4willFly, ENEMY5willFly, ENEMY6willFly, ENEMY7willFly, ENEMY8willFly, ENEMY9willFly, ENEMY10willFly, ENEMY11willFly, ENEMY12willFly, ENEMY13willFly, ENEMY14willFly, ENEMY15willFly, ENEMY16willFly, ENEMY17willFly, ENEMY18willFly, ENEMY19willFly, ENEMY20willFly, ENEMY21willFly, ENEMY22willFly, ENEMY23willFly, ENEMY24willFly, ENEMY25willFly, ENEMY26willFly, ENEMY27willFly, ENEMY28willFly, ENEMY29willFly, ENEMY30willFly };
        ENEMYcarryCapacity = new int[] { ENEMY0carryCapacity, ENEMY1carryCapacity, ENEMY2carryCapacity, ENEMY3carryCapacity, ENEMY4carryCapacity, ENEMY5carryCapacity, ENEMY6carryCapacity, ENEMY7carryCapacity, ENEMY8carryCapacity, ENEMY9carryCapacity, ENEMY10carryCapacity, ENEMY11carryCapacity, ENEMY12carryCapacity, ENEMY13carryCapacity, ENEMY14carryCapacity, ENEMY15carryCapacity, ENEMY16carryCapacity, ENEMY17carryCapacity, ENEMY18carryCapacity, ENEMY19carryCapacity, ENEMY20carryCapacity, ENEMY21carryCapacity, ENEMY22carryCapacity, ENEMY23carryCapacity, ENEMY24carryCapacity, ENEMY25carryCapacity, ENEMY26carryCapacity, ENEMY27carryCapacity, ENEMY28carryCapacity, ENEMY29carryCapacity, ENEMY30carryCapacity };
        ENEMYwillStealNectar = new bool[] { ENEMY0willStealNectar, ENEMY1willStealNectar, ENEMY2willStealNectar, ENEMY3willStealNectar, ENEMY4willStealNectar, ENEMY5willStealNectar, ENEMY6willStealNectar, ENEMY7willStealNectar, ENEMY8willStealNectar, ENEMY9willStealNectar, ENEMY10willStealNectar, ENEMY11willStealNectar, ENEMY12willStealNectar, ENEMY13willStealNectar, ENEMY14willStealNectar, ENEMY15willStealNectar, ENEMY16willStealNectar, ENEMY17willStealNectar, ENEMY18willStealNectar, ENEMY19willStealNectar, ENEMY20willStealNectar, ENEMY21willStealNectar, ENEMY22willStealNectar, ENEMY23willStealNectar, ENEMY24willStealNectar, ENEMY25willStealNectar, ENEMY26willStealNectar, ENEMY27willStealNectar, ENEMY28willStealNectar, ENEMY29willStealNectar, ENEMY30willStealNectar };
        ENEMYwillStealHoney = new bool[] { ENEMY0willStealHoney, ENEMY1willStealHoney, ENEMY2willStealHoney, ENEMY3willStealHoney, ENEMY4willStealHoney, ENEMY5willStealHoney, ENEMY6willStealHoney, ENEMY7willStealHoney, ENEMY8willStealHoney, ENEMY9willStealHoney, ENEMY10willStealHoney, ENEMY11willStealHoney, ENEMY12willStealHoney, ENEMY13willStealHoney, ENEMY14willStealHoney, ENEMY15willStealHoney, ENEMY16willStealHoney, ENEMY17willStealHoney, ENEMY18willStealHoney, ENEMY19willStealHoney, ENEMY20willStealHoney, ENEMY21willStealHoney, ENEMY22willStealHoney, ENEMY23willStealHoney, ENEMY24willStealHoney, ENEMY25willStealHoney, ENEMY26willStealHoney, ENEMY27willStealHoney, ENEMY28willStealHoney, ENEMY29willStealHoney, ENEMY30willStealHoney };
        ENEMYwillAttack = new bool[] { ENEMY0willAttack, ENEMY1willAttack, ENEMY2willAttack, ENEMY3willAttack, ENEMY4willAttack, ENEMY5willAttack, ENEMY6willAttack, ENEMY7willAttack, ENEMY8willAttack, ENEMY9willAttack, ENEMY10willAttack, ENEMY11willAttack, ENEMY12willAttack, ENEMY13willAttack, ENEMY14willAttack, ENEMY15willAttack, ENEMY16willAttack, ENEMY17willAttack, ENEMY18willAttack, ENEMY19willAttack, ENEMY20willAttack, ENEMY21willAttack, ENEMY22willAttack, ENEMY23willAttack, ENEMY24willAttack, ENEMY25willAttack, ENEMY26willAttack, ENEMY27willAttack, ENEMY28willAttack, ENEMY29willAttack, ENEMY30willAttack };
        ENEMYeffect = new string[] { ENEMY0effect, ENEMY1effect, ENEMY2effect, ENEMY3effect, ENEMY4effect, ENEMY5effect, ENEMY6effect, ENEMY7effect, ENEMY8effect, ENEMY9effect, ENEMY10effect, ENEMY11effect, ENEMY12effect, ENEMY13effect, ENEMY14effect, ENEMY15effect, ENEMY16effect, ENEMY17effect, ENEMY18effect, ENEMY19effect, ENEMY20effect, ENEMY21effect, ENEMY22effect, ENEMY23effect, ENEMY24effect, ENEMY25effect, ENEMY26effect, ENEMY27effect, ENEMY28effect, ENEMY29effect, ENEMY30effect };
        ENEMYeffectModifier = new float[] { ENEMY0effectModifier, ENEMY1effectModifier, ENEMY2effectModifier, ENEMY3effectModifier, ENEMY4effectModifier, ENEMY5effectModifier, ENEMY6effectModifier, ENEMY7effectModifier, ENEMY8effectModifier, ENEMY9effectModifier, ENEMY10effectModifier, ENEMY11effectModifier, ENEMY12effectModifier, ENEMY13effectModifier, ENEMY14effectModifier, ENEMY15effectModifier, ENEMY16effectModifier, ENEMY17effectModifier, ENEMY18effectModifier, ENEMY19effectModifier, ENEMY20effectModifier, ENEMY21effectModifier, ENEMY22effectModifier, ENEMY23effectModifier, ENEMY24effectModifier, ENEMY25effectModifier, ENEMY26effectModifier, ENEMY27effectModifier, ENEMY28effectModifier, ENEMY29effectModifier, ENEMY30effectModifier };
        ENEMYattackDamage = new float[] { ENEMY0attackDamage, ENEMY1attackDamage, ENEMY2attackDamage, ENEMY3attackDamage, ENEMY4attackDamage, ENEMY5attackDamage, ENEMY6attackDamage, ENEMY7attackDamage, ENEMY8attackDamage, ENEMY9attackDamage, ENEMY10attackDamage, ENEMY11attackDamage, ENEMY12attackDamage, ENEMY13attackDamage, ENEMY14attackDamage, ENEMY15attackDamage, ENEMY16attackDamage, ENEMY17attackDamage, ENEMY18attackDamage, ENEMY19attackDamage, ENEMY20attackDamage, ENEMY21attackDamage, ENEMY22attackDamage, ENEMY23attackDamage, ENEMY24attackDamage, ENEMY25attackDamage, ENEMY26attackDamage, ENEMY27attackDamage, ENEMY28attackDamage, ENEMY29attackDamage, ENEMY30attackDamage };
        ENEMYattackRate = new float[] { ENEMY0attackRate, ENEMY1attackRate, ENEMY2attackRate, ENEMY3attackRate, ENEMY4attackRate, ENEMY5attackRate, ENEMY6attackRate, ENEMY7attackRate, ENEMY8attackRate, ENEMY9attackRate, ENEMY10attackRate, ENEMY11attackRate, ENEMY12attackRate, ENEMY13attackRate, ENEMY14attackRate, ENEMY15attackRate, ENEMY16attackRate, ENEMY17attackRate, ENEMY18attackRate, ENEMY19attackRate, ENEMY20attackRate, ENEMY21attackRate, ENEMY22attackRate, ENEMY23attackRate, ENEMY24attackRate, ENEMY25attackRate, ENEMY26attackRate, ENEMY27attackRate, ENEMY28attackRate, ENEMY29attackRate, ENEMY30attackRate };

        //Boss Mob Arrays
        BOSSprefab = new GameObject[] { BOSS0prefab, BOSS1prefab, BOSS2prefab, BOSS3prefab, BOSS4prefab, BOSS5prefab, BOSS6prefab, BOSS7prefab, BOSS8prefab, BOSS9prefab, BOSS10prefab };
        BOSSprefab2 = new GameObject[] { BOSS0prefab2, BOSS1prefab2, BOSS2prefab2, BOSS3prefab2, BOSS4prefab2, BOSS5prefab2, BOSS6prefab2, BOSS7prefab2, BOSS8prefab2, BOSS9prefab2, BOSS10prefab2 };
        BOSSprefab3 = new GameObject[] { BOSS0prefab3, BOSS1prefab3, BOSS2prefab3, BOSS3prefab3, BOSS4prefab3, BOSS5prefab3, BOSS6prefab3, BOSS7prefab3, BOSS8prefab3, BOSS9prefab3, BOSS10prefab3 };
        BOSSname = new string[] { BOSS0name, BOSS1name, BOSS2name, BOSS3name, BOSS4name, BOSS5name, BOSS6name, BOSS7name, BOSS8name, BOSS9name, BOSS10name };
        BOSShitPoints = new float[] { BOSS0hitPoints, BOSS1hitPoints, BOSS2hitPoints, BOSS3hitPoints, BOSS4hitPoints, BOSS5hitPoints, BOSS6hitPoints, BOSS7hitPoints, BOSS8hitPoints, BOSS9hitPoints, BOSS10hitPoints };
        BOSSarmor = new float[] { BOSS0armor, BOSS1armor, BOSS2armor, BOSS3armor, BOSS4armor, BOSS5armor, BOSS6armor, BOSS7armor, BOSS8armor, BOSS9armor, BOSS10armor };
        BOSShealthBar = new bool[] { BOSS0healthBar, BOSS1healthBar, BOSS2healthBar, BOSS3healthBar, BOSS4healthBar, BOSS5healthBar, BOSS6healthBar, BOSS7healthBar, BOSS8healthBar, BOSS9healthBar, BOSS10healthBar };
        BOSSbounty = new float[] { BOSS0bounty, BOSS1bounty, BOSS2bounty, BOSS3bounty, BOSS4bounty, BOSS5bounty, BOSS6bounty, BOSS7bounty, BOSS8bounty, BOSS9bounty, BOSS10bounty };
        BOSSmoveSpeed = new float[] { BOSS0moveSpeed, BOSS1moveSpeed, BOSS2moveSpeed, BOSS3moveSpeed, BOSS4moveSpeed, BOSS5moveSpeed, BOSS6moveSpeed, BOSS7moveSpeed, BOSS8moveSpeed, BOSS9moveSpeed, BOSS10moveSpeed };
        BOSSwillFly = new bool[] { BOSS0willFly, BOSS1willFly, BOSS2willFly, BOSS3willFly, BOSS4willFly, BOSS5willFly, BOSS6willFly, BOSS7willFly, BOSS8willFly, BOSS9willFly, BOSS10willFly };
        BOSScarryCapacity = new int[] { BOSS0carryCapacity, BOSS1carryCapacity, BOSS2carryCapacity, BOSS3carryCapacity, BOSS4carryCapacity, BOSS5carryCapacity, BOSS6carryCapacity, BOSS7carryCapacity, BOSS8carryCapacity, BOSS9carryCapacity, BOSS10carryCapacity };
        BOSSwillStealNectar = new bool[] { BOSS0willStealNectar, BOSS1willStealNectar, BOSS2willStealNectar, BOSS3willStealNectar, BOSS4willStealNectar, BOSS5willStealNectar, BOSS6willStealNectar, BOSS7willStealNectar, BOSS8willStealNectar, BOSS9willStealNectar, BOSS10willStealNectar };
        BOSSwillStealHoney = new bool[] { BOSS0willStealHoney, BOSS1willStealHoney, BOSS2willStealHoney, BOSS3willStealHoney, BOSS4willStealHoney, BOSS5willStealHoney, BOSS6willStealHoney, BOSS7willStealHoney, BOSS8willStealHoney, BOSS9willStealHoney, BOSS10willStealHoney };
        BOSSwillAttack = new bool[] { BOSS0willAttack, BOSS1willAttack, BOSS2willAttack, BOSS3willAttack, BOSS4willAttack, BOSS5willAttack, BOSS6willAttack, BOSS7willAttack, BOSS8willAttack, BOSS9willAttack, BOSS10willAttack};
        BOSSeffect = new string[] { BOSS0effect, BOSS1effect, BOSS2effect, BOSS3effect, BOSS4effect, BOSS5effect, BOSS6effect, BOSS7effect, BOSS8effect, BOSS9effect, BOSS10effect };
        BOSSeffectModifier = new float[] { BOSS0effectModifier, BOSS1effectModifier, BOSS2effectModifier, BOSS3effectModifier, BOSS4effectModifier, BOSS5effectModifier, BOSS6effectModifier, BOSS7effectModifier, BOSS8effectModifier, BOSS9effectModifier, BOSS10effectModifier };
        BOSSattackDamage = new float[] { BOSS0attackDamage, BOSS1attackDamage, BOSS2attackDamage, BOSS3attackDamage, BOSS4attackDamage, BOSS5attackDamage, BOSS6attackDamage, BOSS7attackDamage, BOSS8attackDamage, BOSS9attackDamage, BOSS10attackDamage };
        BOSSattackRate = new float[] { BOSS0attackRate, BOSS1attackRate, BOSS2attackRate, BOSS3attackRate, BOSS4attackRate, BOSS5attackRate, BOSS6attackRate, BOSS7attackRate, BOSS8attackRate, BOSS9attackRate, BOSS10attackRate };

        //Tower Arrays
        TOWERprefab = new GameObject[] { TOWER0prefab, TOWER1prefab, TOWER2prefab, TOWER3prefab, TOWER4prefab, TOWER5prefab, TOWER6prefab, TOWER7prefab, TOWER8prefab, TOWER9prefab, TOWER10prefab, TOWER11prefab, TOWER12prefab, TOWER13prefab, TOWER14prefab, TOWER15prefab, TOWER16prefab, TOWER17prefab, TOWER18prefab, TOWER19prefab, TOWER20prefab, TOWER21prefab, TOWER22prefab, TOWER23prefab, TOWER24prefab, TOWER25prefab, TOWER26prefab, TOWER27prefab, TOWER28prefab, TOWER29prefab, TOWER30prefab };
        TOWERenemyMask = new LayerMask[] { TOWER0enemyMask, TOWER1enemyMask, TOWER2enemyMask, TOWER3enemyMask, TOWER4enemyMask, TOWER5enemyMask, TOWER6enemyMask, TOWER7enemyMask, TOWER8enemyMask, TOWER9enemyMask, TOWER10enemyMask, TOWER11enemyMask, TOWER12enemyMask, TOWER13enemyMask, TOWER14enemyMask, TOWER15enemyMask, TOWER16enemyMask, TOWER17enemyMask, TOWER18enemyMask, TOWER19enemyMask, TOWER20enemyMask, TOWER21enemyMask, TOWER22enemyMask, TOWER23enemyMask, TOWER24enemyMask, TOWER25enemyMask, TOWER26enemyMask, TOWER27enemyMask, TOWER28enemyMask, TOWER29enemyMask, TOWER30enemyMask };
        TOWERprojectilePrefab = new GameObject[] { TOWER0projectilePrefab, TOWER1projectilePrefab, TOWER2projectilePrefab, TOWER3projectilePrefab, TOWER4projectilePrefab, TOWER5projectilePrefab, TOWER6projectilePrefab, TOWER7projectilePrefab, TOWER8projectilePrefab, TOWER9projectilePrefab, TOWER10projectilePrefab, TOWER11projectilePrefab, TOWER12projectilePrefab, TOWER13projectilePrefab, TOWER14projectilePrefab, TOWER15projectilePrefab, TOWER16projectilePrefab, TOWER17projectilePrefab, TOWER18projectilePrefab, TOWER19projectilePrefab, TOWER20projectilePrefab, TOWER21projectilePrefab, TOWER22projectilePrefab, TOWER23projectilePrefab, TOWER24projectilePrefab, TOWER25projectilePrefab, TOWER26projectilePrefab, TOWER27projectilePrefab, TOWER28projectilePrefab, TOWER29projectilePrefab, TOWER30projectilePrefab };
        TOWERname = new string[] { TOWER0name, TOWER1name, TOWER2name, TOWER3name, TOWER4name, TOWER5name, TOWER6name, TOWER7name, TOWER8name, TOWER9name, TOWER10name, TOWER11name, TOWER12name, TOWER13name, TOWER14name, TOWER15name, TOWER16name, TOWER17name, TOWER18name, TOWER19name, TOWER20name, TOWER21name, TOWER22name, TOWER23name, TOWER24name, TOWER25name, TOWER26name, TOWER27name, TOWER28name, TOWER29name, TOWER30name };
        TOWERtargetingRange = new float[] { TOWER0targetingRange, TOWER1targetingRange, TOWER2targetingRange, TOWER3targetingRange, TOWER4targetingRange, TOWER5targetingRange, TOWER6targetingRange, TOWER7targetingRange, TOWER8targetingRange, TOWER9targetingRange, TOWER10targetingRange, TOWER11targetingRange, TOWER12targetingRange, TOWER13targetingRange, TOWER14targetingRange, TOWER15targetingRange, TOWER16targetingRange, TOWER17targetingRange, TOWER18targetingRange, TOWER19targetingRange, TOWER20targetingRange, TOWER21targetingRange, TOWER22targetingRange, TOWER23targetingRange, TOWER24targetingRange, TOWER25targetingRange, TOWER26targetingRange, TOWER27targetingRange, TOWER28targetingRange, TOWER29targetingRange, TOWER30targetingRange };
        TOWERrotationSpeed = new float[] { TOWER0rotationSpeed, TOWER1rotationSpeed, TOWER2rotationSpeed, TOWER3rotationSpeed, TOWER4rotationSpeed, TOWER5rotationSpeed, TOWER6rotationSpeed, TOWER7rotationSpeed, TOWER8rotationSpeed, TOWER9rotationSpeed, TOWER10rotationSpeed, TOWER11rotationSpeed, TOWER12rotationSpeed, TOWER13rotationSpeed, TOWER14rotationSpeed, TOWER15rotationSpeed, TOWER16rotationSpeed, TOWER17rotationSpeed, TOWER18rotationSpeed, TOWER19rotationSpeed, TOWER20rotationSpeed, TOWER21rotationSpeed, TOWER22rotationSpeed, TOWER23rotationSpeed, TOWER24rotationSpeed, TOWER25rotationSpeed, TOWER26rotationSpeed, TOWER27rotationSpeed, TOWER28rotationSpeed, TOWER29rotationSpeed, TOWER30rotationSpeed };
        TOWERdamage = new float[] { TOWER0damage, TOWER1damage, TOWER2damage, TOWER3damage, TOWER4damage, TOWER5damage, TOWER6damage, TOWER7damage, TOWER8damage, TOWER9damage, TOWER10damage, TOWER11damage, TOWER12damage, TOWER13damage, TOWER14damage, TOWER15damage, TOWER16damage, TOWER17damage, TOWER18damage, TOWER19damage, TOWER20damage, TOWER21damage, TOWER22damage, TOWER23damage, TOWER24damage, TOWER25damage, TOWER26damage, TOWER27damage, TOWER28damage, TOWER29damage, TOWER30damage };
        TOWERarmorPierce = new float[] { TOWER0armorPierce, TOWER1armorPierce, TOWER2armorPierce, TOWER3armorPierce, TOWER4armorPierce, TOWER5armorPierce, TOWER6armorPierce, TOWER7armorPierce, TOWER8armorPierce, TOWER9armorPierce, TOWER10armorPierce, TOWER11armorPierce, TOWER12armorPierce, TOWER13armorPierce, TOWER14armorPierce, TOWER15armorPierce, TOWER16armorPierce, TOWER17armorPierce, TOWER18armorPierce, TOWER19armorPierce, TOWER20armorPierce, TOWER21armorPierce, TOWER22armorPierce, TOWER23armorPierce, TOWER24armorPierce, TOWER25armorPierce, TOWER26armorPierce, TOWER27armorPierce, TOWER28armorPierce, TOWER29armorPierce, TOWER30armorPierce };
        TOWERaps = new float[] { TOWER0aps, TOWER1aps, TOWER2aps, TOWER3aps, TOWER4aps, TOWER5aps, TOWER6aps, TOWER7aps, TOWER8aps, TOWER9aps, TOWER10aps, TOWER11aps, TOWER12aps, TOWER13aps, TOWER14aps, TOWER15aps, TOWER16aps, TOWER17aps, TOWER18aps, TOWER19aps, TOWER20aps, TOWER21aps, TOWER22aps, TOWER23aps, TOWER24aps, TOWER25aps, TOWER26aps, TOWER27aps, TOWER28aps, TOWER29aps, TOWER30aps };
        TOWEReffect = new string[] { TOWER0effect, TOWER1effect, TOWER2effect, TOWER3effect, TOWER4effect, TOWER5effect, TOWER6effect, TOWER7effect, TOWER8effect, TOWER9effect, TOWER10effect, TOWER11effect, TOWER12effect, TOWER13effect, TOWER14effect, TOWER15effect, TOWER16effect, TOWER17effect, TOWER18effect, TOWER19effect, TOWER20effect, TOWER21effect, TOWER22effect, TOWER23effect, TOWER24effect, TOWER25effect, TOWER26effect, TOWER27effect, TOWER28effect, TOWER29effect, TOWER30effect };
        TOWEReffectDuration = new float[] { TOWER0effectDuration, TOWER1effectDuration, TOWER2effectDuration, TOWER3effectDuration, TOWER4effectDuration, TOWER5effectDuration, TOWER6effectDuration, TOWER7effectDuration, TOWER8effectDuration, TOWER9effectDuration, TOWER10effectDuration, TOWER11effectDuration, TOWER12effectDuration, TOWER13effectDuration, TOWER14effectDuration, TOWER15effectDuration, TOWER16effectDuration, TOWER17effectDuration, TOWER18effectDuration, TOWER19effectDuration, TOWER20effectDuration, TOWER21effectDuration, TOWER22effectDuration, TOWER23effectDuration, TOWER24effectDuration, TOWER25effectDuration, TOWER26effectDuration, TOWER27effectDuration, TOWER28effectDuration, TOWER29effectDuration, TOWER30effectDuration };
        TOWEReffectRatio = new float[] { TOWER0effectRatio, TOWER1effectRatio, TOWER2effectRatio, TOWER3effectRatio, TOWER4effectRatio, TOWER5effectRatio, TOWER6effectRatio, TOWER7effectRatio, TOWER8effectRatio, TOWER9effectRatio, TOWER10effectRatio, TOWER11effectRatio, TOWER12effectRatio, TOWER13effectRatio, TOWER14effectRatio, TOWER15effectRatio, TOWER16effectRatio, TOWER17effectRatio, TOWER18effectRatio, TOWER19effectRatio, TOWER20effectRatio, TOWER21effectRatio, TOWER22effectRatio, TOWER23effectRatio, TOWER24effectRatio, TOWER25effectRatio, TOWER26effectRatio, TOWER27effectRatio, TOWER28effectRatio, TOWER29effectRatio, TOWER30effectRatio };
        TOWERupgradeIndex = new int[][] { TOWER0upgradeIndex, TOWER1upgradeIndex, TOWER2upgradeIndex, TOWER3upgradeIndex, TOWER4upgradeIndex, TOWER5upgradeIndex, TOWER6upgradeIndex, TOWER7upgradeIndex, TOWER8upgradeIndex, TOWER9upgradeIndex, TOWER10upgradeIndex, TOWER11upgradeIndex, TOWER12upgradeIndex, TOWER13upgradeIndex, TOWER14upgradeIndex, TOWER15upgradeIndex, TOWER16upgradeIndex, TOWER17upgradeIndex, TOWER18upgradeIndex, TOWER19upgradeIndex, TOWER20upgradeIndex, TOWER21upgradeIndex, TOWER22upgradeIndex, TOWER23upgradeIndex, TOWER24upgradeIndex, TOWER25upgradeIndex, TOWER26upgradeIndex, TOWER27upgradeIndex, TOWER28upgradeIndex, TOWER29upgradeIndex, TOWER30upgradeIndex };
        TOWERefficiency = new float[] { TOWER0efficiency, TOWER1efficiency, TOWER2efficiency, TOWER3efficiency, TOWER4efficiency, TOWER5efficiency, TOWER6efficiency, TOWER7efficiency, TOWER8efficiency, TOWER9efficiency, TOWER10efficiency, TOWER11efficiency, TOWER12efficiency, TOWER13efficiency, TOWER14efficiency, TOWER15efficiency, TOWER16efficiency, TOWER17efficiency, TOWER18efficiency, TOWER19efficiency, TOWER20efficiency, TOWER21efficiency, TOWER22efficiency, TOWER23efficiency, TOWER24efficiency, TOWER25efficiency, TOWER26efficiency, TOWER27efficiency, TOWER28efficiency, TOWER29efficiency, TOWER30efficiency };
        TOWERhasTargetSettings = new bool[] { TOWER0hasTargetSettings, TOWER1hasTargetSettings, TOWER2hasTargetSettings, TOWER3hasTargetSettings, TOWER4hasTargetSettings, TOWER5hasTargetSettings, TOWER6hasTargetSettings, TOWER7hasTargetSettings, TOWER8hasTargetSettings, TOWER9hasTargetSettings, TOWER10hasTargetSettings, TOWER11hasTargetSettings, TOWER12hasTargetSettings, TOWER13hasTargetSettings, TOWER14hasTargetSettings, TOWER15hasTargetSettings, TOWER16hasTargetSettings, TOWER17hasTargetSettings, TOWER18hasTargetSettings, TOWER19hasTargetSettings, TOWER20hasTargetSettings, TOWER21hasTargetSettings, TOWER22hasTargetSettings, TOWER23hasTargetSettings, TOWER24hasTargetSettings, TOWER25hasTargetSettings, TOWER26hasTargetSettings, TOWER27hasTargetSettings, TOWER28hasTargetSettings, TOWER29hasTargetSettings, TOWER30hasTargetSettings };
        TOWERignoreTerrain = new bool[] { TOWER0ignoreTerrain, TOWER1ignoreTerrain, TOWER2ignoreTerrain, TOWER3ignoreTerrain, TOWER4ignoreTerrain, TOWER5ignoreTerrain, TOWER6ignoreTerrain, TOWER7ignoreTerrain, TOWER8ignoreTerrain, TOWER9ignoreTerrain, TOWER10ignoreTerrain, TOWER11ignoreTerrain, TOWER12ignoreTerrain, TOWER13ignoreTerrain, TOWER14ignoreTerrain, TOWER15ignoreTerrain, TOWER16ignoreTerrain, TOWER17ignoreTerrain, TOWER18ignoreTerrain, TOWER19ignoreTerrain, TOWER20ignoreTerrain, TOWER21ignoreTerrain, TOWER22ignoreTerrain, TOWER23ignoreTerrain, TOWER24ignoreTerrain, TOWER25ignoreTerrain, TOWER26ignoreTerrain, TOWER27ignoreTerrain, TOWER28ignoreTerrain, TOWER29ignoreTerrain, TOWER30ignoreTerrain };
        TOWERcost = new float[] { TOWER0cost, TOWER1cost, TOWER2cost, TOWER3cost, TOWER4cost, TOWER5cost, TOWER6cost, TOWER7cost, TOWER8cost, TOWER9cost, TOWER10cost, TOWER11cost, TOWER12cost, TOWER13cost, TOWER14cost, TOWER15cost, TOWER16cost, TOWER17cost, TOWER18cost, TOWER19cost, TOWER20cost, TOWER21cost, TOWER22cost, TOWER23cost, TOWER24cost, TOWER25cost, TOWER26cost, TOWER27cost, TOWER28cost, TOWER29cost, TOWER30cost };
        TOWERcanHit = new string[] { TOWER0canHit, TOWER1canHit, TOWER2canHit, TOWER3canHit, TOWER4canHit, TOWER5canHit, TOWER6canHit, TOWER7canHit, TOWER8canHit, TOWER9canHit, TOWER10canHit, TOWER11canHit, TOWER12canHit, TOWER13canHit, TOWER14canHit, TOWER15canHit, TOWER16canHit, TOWER17canHit, TOWER18canHit, TOWER19canHit, TOWER20canHit, TOWER21canHit, TOWER22canHit, TOWER23canHit, TOWER24canHit, TOWER25canHit, TOWER26canHit, TOWER27canHit, TOWER28canHit, TOWER29canHit, TOWER30canHit };

        //Projectile Arrays
        PROJECTILEenemyMask = new LayerMask[] { PROJECTILE0enemyMask, PROJECTILE1enemyMask, PROJECTILE2enemyMask, PROJECTILE3enemyMask, PROJECTILE4enemyMask };
        PROJECTILEprojectileSpeed = new float[] { PROJECTILE0projectileSpeed, PROJECTILE1projectileSpeed, PROJECTILE2projectileSpeed, PROJECTILE3projectileSpeed, PROJECTILE4projectileSpeed };
        PROJECTILEAoE = new float[] { PROJECTILE0AoE, PROJECTILE1AoE, PROJECTILE2AoE, PROJECTILE3AoE, PROJECTILE4AoE };
        PROJECTILEAoEDropOff = new bool[] { PROJECTILE0AoEDropOff, PROJECTILE1AoEDropOff, PROJECTILE2AoEDropOff, PROJECTILE3AoEDropOff, PROJECTILE4AoEDropOff };
    }

    public void SetUI(bool state)
    {
        UI.SetActive(state);
        UIManager.main.Reset();
    }

    public void SetDifficulty(string setting)
    {
        if (setting == "Easy")
        {
            difficultyMultiplier = easyMultiplier;
            difficultySetting = setting;
        }
        else if (setting == "Medium")
        {
            difficultyMultiplier = mediumMultiplier;
            difficultySetting = setting;
        }
        else if (setting == "Hard")
        {
            difficultyMultiplier = hardMultiplier;
            difficultySetting = setting;
        }
    }

}


