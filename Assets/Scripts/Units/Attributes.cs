using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Xml.Linq;

public class Attributes : MonoBehaviour
{
    public static Attributes main;

    [Header("References")]
    [Header("__________________________")]
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Slider healthBar;

    [Header("Attributes")]
    [Header("__________________________")]
    [Header("All")]
    [SerializeField] public string sName = "";
    [SerializeField] public string type = "";
    [SerializeField] public float cost = 0f;
    [SerializeField] private SoundType deathSound;
    //
    [SerializeField] public float moveSpeed = 0f;
    [SerializeField] public float maxHP = 100f;
    [SerializeField] public float maxShield = 0f;
    [SerializeField] public float armor = 20f;
    [SerializeField] public float resistance = 20f;
    [SerializeField] public float dodgeChance = 0f;
    //Targeting
    [SerializeField] public float targetingRange = 5f;
    [SerializeField] public float rotationSpeed = 150f;
    [SerializeField] public float wayPointRangeModifier = 1f;
    //Actions
    [SerializeField] public float actionPower = 1f;
    [SerializeField] public float actionRate = 1f;
    [SerializeField] public float armorPierce = 0f;
    [SerializeField] public string[] actions;
    [SerializeField] public GameObject[] actionPrefabs;
    [SerializeField] public SoundType[] actionSounds;
    [SerializeField] public float[] actionPowerModifiers;
    [SerializeField] public float[] actionRateModifiers;
    [SerializeField] public float[] actionRangeModifiers;
    [SerializeField] public float[] actionPierceModifiers;
    [SerializeField] public float[] actionDurations;
    [SerializeField] public float[] actionExtraModifiers;
    //Effects
    [SerializeField] public float effectPower = 1f;
    [SerializeField] public float effectRate = 1f;
    [SerializeField] public float resistancePierce = 0f;
    [SerializeField] public string[] effects;
    [SerializeField] public GameObject[] effectPrefabs;
    [SerializeField] public SoundType[] effectSounds;
    [SerializeField] public float[] effectPowerModifiers;
    [SerializeField] public float[] effectPierceModifiers;
    [SerializeField] public float[] effectRateModifiers;
    [SerializeField] public float[] effectRangeModifiers;
    [SerializeField] public float[] effectDurations;
    [SerializeField] public float[] effectExtraModifiers;
    //Passives
    [SerializeField] public float passivePower = 1f;
    [SerializeField] public float passiveRate = 1f;
    [SerializeField] public float passivePierce = 0f;
    [SerializeField] public string[] passives;
    [SerializeField] public GameObject[] passivePrefabs;
    [SerializeField] public SoundType[] passiveSounds;
    [SerializeField] public float[] passivePowerModifiers;
    [SerializeField] public float[] passivePierceModifiers;
    [SerializeField] public float[] passiveRateModifiers;
    [SerializeField] public float[] passiveRangeModifiers;
    [SerializeField] public float[] passiveDurations;
    [SerializeField] public float[] passiveExtraModifiers;

    [Header("Units Only")]
    [SerializeField] public bool willFly = false;
    [SerializeField] public int carryCapacity = 10;

    [Header("Towers Only")]
    [SerializeField] public string canHit = "All";
    [SerializeField] public bool ignoreTerrain = false;
    [SerializeField] public bool hasTargetSettings = false;
    [SerializeField] public string targetSetting;

    [Header("_____________________")]
    [Header("Trackers")]
    //trackers
    public VariantType variant;
    public string variantName;
    public string prestige;
    public float hitPoints = 1f;
    public float shield = 0f;
    public bool isDestroyed = false;
    public Transform target;
    public int pathIndex = 0;
    public bool inventoryFull = false;
    public bool frozen = false;
    public int Stealth = 0;
    public float freezeImmune = 0f;
    public float pausing = 0f;
    public float[] timeUntilActions;
    public float[] timeUntilEffects;
    public float[] timeUntilPassives;
    public float wayPointDistance = .2f;
    public int onPath = 1;
    public Transform[] path;
    public string[] targetingOptions;
    public int rampCount = 0;
    public float moveSpeedBase;
    public float moveSpeedUncapped;
    public float targetingRangeBase;
    public float maxHPBase;
    public float maxShieldBase;
    public float armorBase;
    public float resistanceBase;
    public float dodgeChanceBase;
    public float actionPowerBase;
    public float actionRateBase;
    public float armorPierceBase;
    public float[] actionPowerModifiersBase;
    public float[] actionRateModifiersBase;
    public float[] actionRangeModifiersBase;
    public float[] actionPierceModifiersBase;
    public float[] actionDurationsBase;
    public float[] actionExtraModifiersBase;
    public float effectPowerBase;
    public float effectRateBase;
    public float resistancePierceBase;
    public float[] effectPowerModifiersBase;
    public float[] effectPierceModifiersBase;
    public float[] effectRateModifiersBase;
    public float[] effectRangeModifiersBase;
    public float[] effectDurationsBase;
    public float[] effectExtraModifiersBase;
    public float passivePowerBase;
    public float passiveRateBase;
    public float passivePierceBase;
    public float[] passivePowerModifiersBase;
    public float[] passivePierceModifiersBase;
    public float[] passiveRateModifiersBase;
    public float[] passiveRangeModifiersBase;
    public float[] passiveDurationsBase;
    public float[] passiveExtraModifiersBase;
    public string work = "Unassigned";
    public GameObject flower;
    public float nectar = 0f;
    public int targetingIndex = 0;

    private void Awake()
    {
        //initiate default values into trackers
        maxHP = maxHP * GlobalValues.main.difficultyMultiplier * (1 + (LevelManager.main.difficultyScaling * WaveSpawner.main.currentWave));
        maxShield = maxShield * (1 + (LevelManager.main.difficultyScaling * WaveSpawner.main.currentWave)) * GlobalValues.main.shieldModifier;
        wayPointDistance = GlobalValues.main.wayPointDistance * wayPointRangeModifier;
        targetingOptions = GlobalValues.main.targetingOptions;
        if (hasTargetSettings == true)
        {
            targetingOptions = GlobalValues.main.targetingOptions;
        }
        SetBaseAttributes();
        SetHealthBar();
    }

    private void Update()
    {
        if (pausing > 0f)
        {
            pausing -= Time.deltaTime;
        }
        if (frozen == true)
        {
            return;
        }
        //Update Counters
        if (freezeImmune > 0)
        {
            freezeImmune -= Time.deltaTime;
        }
    }

    public void SetBaseAttributes()
    {
        //Set Base Stats
        hitPoints = maxHP;
        shield = maxShield;
        moveSpeedBase = moveSpeed;
        moveSpeedUncapped = moveSpeed;
        targetingRangeBase = targetingRange;
        maxHPBase = maxHP;
        maxShieldBase = maxShield;
        armorBase = armor;
        resistanceBase = resistance;
        dodgeChanceBase = dodgeChance;
        actionPowerBase = actionPower;
        actionRateBase = actionRate;
        armorPierceBase = armorPierce;
        actionPowerModifiersBase = actionPowerModifiers;
        actionRateModifiersBase = actionRateModifiers;
        actionRangeModifiersBase = actionRangeModifiers;
        actionPierceModifiersBase = actionPierceModifiers;
        actionDurationsBase = actionDurations;
        actionExtraModifiersBase = actionExtraModifiers;
        effectPowerBase = effectPower;
        effectRateBase = effectRate;
        resistancePierceBase = resistancePierce;
        effectPowerModifiersBase = effectPowerModifiers;
        effectPierceModifiersBase = effectPierceModifiers;
        effectRateModifiersBase = effectRateModifiers;
        effectRangeModifiersBase = effectRangeModifiers;
        effectDurationsBase = effectDurations;
        effectExtraModifiersBase = effectExtraModifiers;
        passivePowerBase = passivePower;
        passiveRateBase = passiveRate;
        passivePierceBase = passivePierce;
        passivePowerModifiersBase = passivePowerModifiers;
        passivePierceModifiersBase = passivePierceModifiers;
        passiveRateModifiersBase = passiveRateModifiers;
        passiveRangeModifiersBase = passiveRangeModifiers;
        passiveDurationsBase = passiveDurations;
        passiveExtraModifiersBase = passiveExtraModifiers;
        //create action timers
        if (actions.Length > 0)
        {
            timeUntilActions = (float[])actionRateModifiers.Clone();
        }
        //create effect timers
        if (effects.Length > 0)
        {
            timeUntilEffects = (float[])effectRateModifiers.Clone();
        }
        //create passive timers
        if (passives.Length > 0)
        {
            Array.Resize(ref timeUntilPassives, passives.Length);
        }
    }

    public void SetHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = maxHP;
            healthBar.value = hitPoints;
            healthBar.gameObject.SetActive(true);
        }
    }

    public void TakeDamage(float dmg, float armorPierce)
    {
        //check dodge
        if (dodgeChance > 0)
        {
            System.Random RandomGen = new System.Random();
            int dodgeRoll = RandomGen.Next(100);
            if (dodgeRoll < dodgeChance)
            {
                return;
            }
        }
        else if (dmg <= 0f)
        {
            return;
        }
        //check armor reduction
        float armorAdjusted = (armor - armor * (armorPierce / 100f));
        if (armorAdjusted < 0)
        {
            armorAdjusted = 0f;
        }
        float dmgBlocked;
        if (armorAdjusted <= 90)
        {
            dmgBlocked = (dmg * (armorAdjusted / 100));
        }
        else
        {
            dmgBlocked = dmg - (dmg * 10f * (float)Math.Pow(1f + armorAdjusted, -1f));
        }
        float dmgAdjust = dmg - dmgBlocked;
        //check shield
        if (shield > 0)
        {
            if (shield < dmgAdjust)
            {
                hitPoints -= dmgAdjust + shield;
                shield = 0f;
            }
            else
            {
                shield -= dmgAdjust;
            }
        }
        else
        {
            hitPoints -= dmgAdjust;
        }
        HealthBar();
        if (hitPoints <= 0 && !isDestroyed)
        {
            Die();
        }
    }

    public void HealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = hitPoints;
        }
    }

    public void Heal(float heal)
    {
        if (heal == 0f || (heal + hitPoints) >= maxHP)
        {
            hitPoints = maxHP;
        }
        else
        {
            hitPoints += heal;
        }
        HealthBar();
    }

    public void Shield(float shieldAmount)
    {
        shield += shieldAmount;
        if (shield > maxShield)
        {
            shield = maxShield;
        }
    }

    public void SlowSpeed(float power, float pierce, float duration)
    {
        if (moveSpeedBase == 0 || power <= 0f)
        {
            return;
        }
        if (prestige.Length > 0)
        {
            if (gameObject.GetComponent<Passives>().CheckPassive("Slow & Freeze Immunity") )
            {
                return;
            }
        }
        float resistanceAdjusted = (resistance - resistance * (pierce / 100));
        if (resistanceAdjusted < 0)
        {
            resistanceAdjusted = 0f;
        }
        float resist;
        float powerAdjusted = power * GlobalValues.main.slowPowerModifier;
        if (resistanceAdjusted <= 90)
        {
            resist = powerAdjusted * (resistanceAdjusted / 100);
        }
        else
        {
            resist = powerAdjusted - powerAdjusted * 10f * (float)Math.Pow(1f + resistanceAdjusted, -1f);
        }
        float slowAdjusted = powerAdjusted - resist;
        float speedLoss = moveSpeedBase * slowAdjusted;
        moveSpeedUncapped -= speedLoss;
        if (moveSpeedUncapped < GlobalValues.main.maxSlowDebuff * moveSpeedBase)
        {
            moveSpeed = GlobalValues.main.maxSlowDebuff * moveSpeedBase;
        }
        else
        {
            moveSpeed = moveSpeedUncapped;
        }
        StartCoroutine(ResetSpeed(speedLoss, duration * GlobalValues.main.slowDurationModifier));
    }

    public void Freeze(float power, float pierce, float duration)
    {
        if (prestige.Length > 0)
        {
            if (gameObject.GetComponent<Passives>().CheckPassive("Slow & Freeze Immunity"))
            {
                return;
            }
        }
        if (power <= 0f)
        {
            return;
        }
        else if (freezeImmune > 0)
        {
            SlowSpeed(power, pierce, duration);
            return;
        }
        float resistanceAdjusted = (resistance - resistance * (pierce / 100f));
        if (resistanceAdjusted < 0)
        {
            resistanceAdjusted = 0f;
        }
        float resist;
        float powerAdjusted = power * GlobalValues.main.freezePowerModifier;
        if (resistanceAdjusted <= 90)
        {
            resist = powerAdjusted * (resistanceAdjusted / 100);
        }
        else
        {
            resist = powerAdjusted - powerAdjusted * 10f * (float)Math.Pow(1f + resistanceAdjusted, -1f);
        }
        float freezeChance = ((power * GlobalValues.main.freezePowerModifier) - resist);
        System.Random RandomGen = new System.Random();
        int freezeRoll = RandomGen.Next(100);
        if (freezeChance * 100 > freezeRoll)
        {
            HaltMovement();
            frozen = true;
            freezeImmune = (duration * GlobalValues.main.freezeImmuneRatio);
            StartCoroutine(Unfreeze(duration * GlobalValues.main.freezeDurationModifier));
        }
        else
        {
            SlowSpeed(power, pierce, duration);
        }
    }

    private IEnumerator ResetSpeed(float speedLoss, float duration)
    {
        yield return new WaitForSeconds(duration);
        moveSpeedUncapped += speedLoss;
        if (moveSpeed < moveSpeedUncapped)
        {
            moveSpeed = moveSpeedUncapped;
        }
    }

    private IEnumerator Unfreeze(float duration)
    {
        yield return new WaitForSeconds(duration);
        frozen = false;
    }

    public void Die()
    {
        if (isDestroyed == false)
        {
            isDestroyed = true;
            if (prestige.Length > 0)
            {
                if (gameObject.GetComponent<Passives>().CheckPassive("Revive") == true && timeUntilPassives[gameObject.GetComponent<Passives>().FindPassive("Revive")] <= 0f)
                {
                    gameObject.GetComponent<Passives>().Revive();
                    isDestroyed = false;
                    return;
                }
                else if (gameObject.GetComponent<Passives>().CheckPassive("Death Split") == true)
                {
                    gameObject.GetComponent<Passives>().DeathSplit();
                }
            }
            if (type.Substring(0,5) == "Enemy")
            {
                ReturnHoney();
                WaveSpawner.main.EnemyDestroyed();
            }
            if (deathSound != SoundType.EMPTY)
            {
                SoundManager.main.PlaySound(deathSound);
            }
            Destroy(gameObject);
        }
    }

    public void RollPrestige(float power)
    {
        //Chance to become prestige
        //
        if (type.Substring(0, 5) == "Enemy")
        {
            gameObject.GetComponent<Prestige>().SetPrestigeStats();
        }
    }

    public void HaltMovement()
    {
        if (type != "Tower")
        {
            rb.linearVelocity = gameObject.transform.forward * 0;
        }
    }

    public void Pause(float duration)
    {
        HaltMovement();
        pausing = duration;
    }

    public void ReturnHoney()
    {
        if (inventoryFull)
        {
            LevelManager.main.honey += carryCapacity * GlobalValues.main.honeyDropReturnModifier;
        }
    }

    public void AddStealth(float invisDuration)
    {
        Stealth++;
        //add code
        {
            ToggleStealth(true);
        }
        StartCoroutine(RemoveStealth(invisDuration));
    }

    private IEnumerator RemoveStealth(float duration)
    {
        yield return new WaitForSeconds(duration);
        Stealth--;
        if (Stealth < 1)
        {
            ToggleStealth(false);
        }
        else
        {
            Stealth = 0;
        }
    }

    private void ToggleStealth(bool state)
    {
        Color tempColor = gameObject.GetComponent<SpriteRenderer>().color;
        if (state == true)
        {
            tempColor.a = GlobalValues.main.StealthTransparancy;
            gameObject.GetComponent<SpriteRenderer>().color = tempColor;
        }
        else
        {
            tempColor.a = 1f;
            gameObject.GetComponent<SpriteRenderer>().color = tempColor;
        }
    }

    public IEnumerator RemovePowerBuff(float buff, float duration)
    {
        yield return new WaitForSeconds(duration);
        actionPower = actionPower / buff;
    }

    public IEnumerator RemoveRateBuff(float buff, float duration)
    {
        yield return new WaitForSeconds(duration);
        actionRate = actionRate / buff;
    }

    public void ReceivePowerBuff(float buff, float duration)
    {
        actionPower = actionPower * buff;
        StartCoroutine(RemovePowerBuff(buff, duration));
    }

    public void ReceiveRateBuff(float buff, float duration)
    {
        actionRate = actionRate * buff;
        StartCoroutine(RemoveRateBuff(buff, duration));
    }
}
