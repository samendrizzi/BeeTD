using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Attributes : MonoBehaviour
{
    public static Attributes main;

    [Header("References")]
    [Header("__________________________")]
    [SerializeField] public Rigidbody2D rb;

    [Header("Attributes")]
    [Header("__________________________")]
    [Header("All")]
    [SerializeField] public string sName = "";
    [SerializeField] public string type = "Tower";
    [SerializeField] public float cost = 0f;
    //
    [SerializeField] public float moveSpeed = 0f;
    [SerializeField] public float maxHP = 100f;
    [SerializeField] public float armor = 20f;
    [SerializeField] public float resistance = 20f;
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
    [SerializeField] public float[] effectPowerModifiers;
    [SerializeField] public float[] effectPierceModifiers;
    [SerializeField] public float[] effectRateModifiers;
    [SerializeField] public float[] effectRangeModifiers;
    [SerializeField] public float[] effectDurations;
    [SerializeField] public float[] effectExtraModifiers;

    [Header("Units Only")]
    [SerializeField] public bool willFly = false;
    [SerializeField] public int carryCapacity = 10;

    [Header("Towers Only")]
    [SerializeField] public string canHit = "All";
    [SerializeField] public bool ignoreTerrain = false;
    [SerializeField] public bool hasTargetSettings = true;
    [SerializeField] public string targetSetting;

    [Header("_____________________")]
    [Header("Trackers")]
    //trackers
    private Slider healthBar;
    public string[] prestige;
    public float hitPoints = 1f;
    public bool isDestroyed = false;
    public Transform target;
    public int pathIndex = 0;
    public bool inventoryFull = false;
    public bool frozen = false;
    public int frozenCount = 0;
    public float pausing = 0f;
    public float[] timeUntilActions;
    public float[] timeUntilEffects;
    public float wayPointDistance = .2f;
    public int onPath = 1;
    public Transform[] path;
    public string[] targetingOptions;
    public int rampCount = 0;
    public float moveSpeedBase;
    public float moveSpeedUncapped;
    public float targetingRangeBase;
    public float maxHPBase;
    public float armorBase;
    public float resistanceBase;
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
    public string work = "unassigned";
    public GameObject flower;
    public float nectar = 0f;
    public int targetingIndex = 0;


    private void Awake()
    {
        //initiate default values into trackers
        wayPointDistance = GlobalValues.main.wayPointDistance * wayPointRangeModifier;
        maxHP = maxHP * GlobalValues.main.difficultyMultiplier * (1 + (LevelManager.main.difficultyScaling * WaveSpawner.main.currentWave));
        hitPoints = maxHP;
        targetingOptions = GlobalValues.main.targetingOptions;
        //Set Base Stats
        moveSpeedBase = moveSpeed;
        moveSpeedUncapped = moveSpeed;
        targetingRangeBase = targetingRange;
        maxHPBase = maxHP;
        armorBase = armor;
        resistanceBase = resistance;
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
        //create action timers
        if (actions.Length > 0)
        {
            Array.Resize(ref timeUntilActions, actions.Length);
        }
        //create effect timers
        if (effects.Length > 0)
        {
            Array.Resize(ref timeUntilEffects, effects.Length);
        }
        if (hasTargetSettings == true)
        {
            targetingOptions = GlobalValues.main.targetingOptions;
        }
        PrestigeStats();
        if (healthBar != null)
        {
            healthBar.maxValue = hitPoints;
            healthBar.value = hitPoints;
            healthBar.gameObject.SetActive(true);
        }
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
        if (timeUntilActions.Length > 0)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                if (timeUntilActions[i] > 0)
                {
                    timeUntilActions[i] -= Time.deltaTime;
                }
            }        
        }
        if (timeUntilEffects.Length > 0)
        {
            for (int i = 0; i < effects.Length; i++)
            {
                if (timeUntilEffects[i] > 0)
                {
                    timeUntilEffects[i] -= Time.deltaTime;
                }
            }
        }
    }

    public void TakeDamage(float dmg, float armorPierce)
    {
        float armorBlock = (armor - armorPierce) / 100f;
        if (armorBlock < 0)
        {
            armorBlock = 0f;
        }
        else if (armorBlock >= 1f)
        {
            //no damage
            return;
        }
        hitPoints -= (1f - armorBlock) * dmg;
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

    public void SlowSpeed(float power, float pierce, float duration)
    {
        float resistanceBlock = (resistance - pierce) / 100f;
        if (resistanceBlock < 0)
        {
            resistanceBlock = 0f;
        }
        else if (resistanceBlock >= 1f || power < 1f)
        {
            //no slow
            return;
        }
        float resist = (power - 1f) * resistanceBlock;
        float powerAdjust = power - resist;
        moveSpeedUncapped = moveSpeedUncapped / powerAdjust;
        if (moveSpeedUncapped < GlobalValues.main.maxSlowDebuff * moveSpeedBase)
        {
            moveSpeed = GlobalValues.main.maxSlowDebuff * moveSpeedBase;
        }
        else
        {
            moveSpeed = moveSpeedUncapped;
        }
        StartCoroutine(ResetSpeed(powerAdjust, duration));
    }

    public void Freeze(float power, float pierce, float duration)
    {
        float resistanceBlock = (resistance - pierce) / 100f;
        if (resistanceBlock < 0)
        {
            resistanceBlock = 0f;
        }
        else if (resistanceBlock >= 1f || power < 1f)
        {
            //no slow
            return;
        }
        float resist = (power - 1f) * resistanceBlock;
        float freezeChance = ((power - 1f) - resist) * GlobalValues.main.freezePowerModifier;
        System.Random RandomGen = new System.Random();
        int freezeRoll = RandomGen.Next(100);
        if (freezeChance * 100 > freezeRoll)
        {
            HaltMovement();
            frozen = true;
            frozenCount++;
            StartCoroutine(Unfreeze(duration));
        }
        else
        {
            SlowSpeed(power, pierce, duration);
        }
    }

    private IEnumerator ResetSpeed(float power, float duration)
    {
        yield return new WaitForSeconds(duration);
        moveSpeedUncapped = moveSpeedUncapped * power;
        if (moveSpeed < moveSpeedUncapped)
        {
            moveSpeed = moveSpeedUncapped;
        }
    }

    private IEnumerator Unfreeze(float duration)
    {
        yield return new WaitForSeconds(duration);
        frozenCount--;
        if (frozenCount <= 0)
        {
            frozen = false;
            frozenCount = 0;
        }
    }

    public void Die()
    {
        if (isDestroyed == false)
        {
            isDestroyed = true;
            if (inventoryFull && type == "Enemy Unit")
            {
                gameObject.GetComponent<Enemy>().ReturnHoney();
                WaveSpawner.main.EnemyDestroyed();
            }
            else if (type == "Enemy Boss")
            {
                WaveSpawner.main.EnemyDestroyed();
            }
            Destroy(gameObject);
        }
    }

    private void PrestigeStats()
    {
        if (prestige.Length > 0)
        {

        }
    }

    public void RollPrestige(float power)
    {
        //Chance to become prestige
        //
        PrestigeStats();
    }

    public void HaltMovement()
    {
        if (type != "Tower")
        {
            rb.velocity = gameObject.transform.forward * 0;
        }
    }

    public void Pause(float duration)
    {
        HaltMovement();
        pausing = duration;
    }
}
