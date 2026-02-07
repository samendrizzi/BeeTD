using System;
using UnityEngine;

public class BuffManager : MonoBehaviour
{

    public static BuffManager main;

    //Summed Buffs
    //Environment
    public float startingNectar = 0f;
    public float startingHoney = 0f;
    public float queenBeeHitPoints = 100f;
    public float queenBeeReturnDamage = 0f;
    public float queenBeeArmor = 0f;
    public float honeyDropRate = 0f;
    public float honeyinterest = 0f;
    public float pollenGatherRatio = 0.25f;
    public float pollenBuff = 1f;
    public float nectarGenerationRate = 1f;
    public int honeycombTicks = 60;
    public float honeycombGeneration = 1f;
    public float honeycombFillTime = 1f;
    //Worker Bees + Towers
    public float slowPower = 1f;
    public float slowPierce = 1f;
    public float freezePower = 1f;
    public float freezePierce = 1f;
    //Worker Bees
    public int startingBees = 1;
    public float beeCost = 50f;
    public float beeCostScaling = 0.05f;
    public float beeHitPoints = 1f;
    public float beeHeal = 1f;
    public float beeShield = 1f;
    public float beeShieldGeneration = 1f;
    public float beeTargetingRange = 1f;
    public float beeDamage = 1f;
    public float beeAttackRate = 1f;
    public float beeActionPower = 1f;
    public float beeActionRate = 1f;
    public float beeEffectPower = 1f;
    public float beeEffectRate = 1f;
    public float beeCarryCapacity = 1f;
    public float beeMoveSpeed = 1f;
    public float beeArmor = 1f;
    public float beeResistance = 1f;
    public float beeDodge = 1f;
    public float beeArmorPierce = 1f;
    public float beeResistancePierce = 1f;
    public float beeDodgePierce = 1f;
    public float beeStealthDetection = 1f;
    //Towers
    public float towerSell = 0.5f;
    public float towerHitPoints = 1f;
    public float towerHeal = 1f;
    public float towerShield = 1f;
    public float towerShieldGeneration = 1f;
    public float towerTargetingRange = 1f;
    public float towerDamage = 1f;
    public float towerAttackRate = 1f;
    public float towerActionPower = 1f;
    public float towerActionRate = 1f;
    public float towerBuffPower = 1f;
    public float towerEffectPower = 1f;
    public float towerEffectRate = 1f;
    public float towerCarryCapacity = 1f;
    public float towerMoveSpeed = 1f;
    public float towerArmor = 1f;
    public float towerResistance = 1f;
    public float towerDodge = 1f;
    public float towerArmorPierce = 1f;
    public float towerResistancePierce = 1f;
    public float towerDodgePierce = 1f;
    public float towerStealthDetection = 1f;
    public float towerAoEArea = 1f;
    public float towerAoEDamageDropOff = 1f;
    public float towerRampingDamage = 1f;
    public int towerExtraRampCount = 1;
    public int towerExtraRicochetCount = 1;
    //Enemies
    public float enemyHitPoints = 1f;
    public float enemyHeal = 1f;
    public float enemyShield = 1f;
    public float enemyShieldGeneration = 1f;
    public float enemyTargetingRange = 1f;
    public float enemyDamage = 1f;
    public float enemyAttackRate = 1f;
    public float enemyActionPower = 1f;
    public float enemyActionRate = 1f;
    public float enemySlowPower = 1f;
    public float enemySlowPierce = 1f;
    public float enemyFreezePower = 1f;
    public float enemyFreezePierce = 1f;
    public float enemyEffectPower = 1f;
    public float enemyEffectRate = 1f;
    public float enemyCarryCapacity = 1f;
    public float enemyMoveSpeed = 1f;
    public float enemyArmor = 1f;
    public float enemyResistance = 1f;
    public float enemyDodge = 1f;
    public float enemyArmorPierce = 1f;
    public float enemyResistancePierce = 1f;
    public float enemyDodgePierce = 1f;
    public float enemyStealthDetection = 1f;
    public float enemyWaveScaling = 1f;
    public float enemyHatchTime = 1f;
    public float enemySpawnTime = 1f;
    public float enemyStealthTime = 1f;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        RefreshBuffs();
    }

    public void RefreshBuffs()
    {
        //Environment
        startingNectar = GlobalValues.main.startingNectar[(int)GlobalValues.main.difficulty] + TechTreeManager.main.startingNectar; //LevelManager
        startingHoney = GlobalValues.main.startingHoney[(int)GlobalValues.main.difficulty] + TechTreeManager.main.startingHoney; //LevelManager
        queenBeeHitPoints = GlobalValues.main.queenBeeHitPoints * TechTreeManager.main.queenBeeHitPoints; //LevelManager
        queenBeeReturnDamage = GlobalValues.main.queenBeeReturnDamage + TechTreeManager.main.queenBeeReturnDamage; //Actions
        queenBeeArmor = GlobalValues.main.queenBeeArmor + TechTreeManager.main.queenBeeArmor; //LevelManager
        honeyDropRate = GlobalValues.main.honeyDropRate[(int)GlobalValues.main.difficulty] + TechTreeManager.main.honeyDropRate; //Attributes
        if (honeyDropRate > 1f)
        {
            honeyDropRate = 1f;
        }
        honeyinterest = GlobalValues.main.honeyinterest + TechTreeManager.main.honeyinterest + PollenManager.main.honeyInterest; //Not Implemented
        pollenGatherRatio = GlobalValues.main.pollenGatherRatio * TechTreeManager.main.pollenGather; //PollenManager
        pollenBuff = GlobalValues.main.pollenBuff + TechTreeManager.main.pollenBuff; //Not Implemented
        nectarGenerationRate = GlobalValues.main.nectarGenerationRate * TechTreeManager.main.nectarGenerationRate * PollenManager.main.nectarGenerationRate; //Plot
        honeycombTicks = GlobalValues.main.honeycombTicks + TechTreeManager.main.honeycombTicks; //Plot
        honeycombGeneration = GlobalValues.main.honeycombGeneration * TechTreeManager.main.honeycombGeneration * PollenManager.main.honeycombGeneration; //Plot
        honeycombFillTime = GlobalValues.main.honeycombFillTime * TechTreeManager.main.honeycombFillTime; //Actions
        //Worker Bees + Towers
        slowPower = GlobalValues.main.slowPower * TechTreeManager.main.slowPower * PollenManager.main.slowPower; //Attributes
        slowPierce = GlobalValues.main.slowPierce * TechTreeManager.main.slowPierce; //Attributes
        freezePower = GlobalValues.main.freezePower * TechTreeManager.main.freezePower * PollenManager.main.freezePower; //Attributes
        freezePierce = GlobalValues.main.freezePierce * TechTreeManager.main.freezePierce; //Attributes
        //Worker Bees
        startingBees = GlobalValues.main.startingBees[(int)GlobalValues.main.difficulty] + TechTreeManager.main.startingBees; //LevelManager
        beeCost = GlobalValues.main.beeCost * TechTreeManager.main.beeCost; //LevelManager
        beeCostScaling = GlobalValues.main.beeCostScaling * TechTreeManager.main.beeCostScaling; //LevelManager
        beeHitPoints = GlobalValues.main.beeHitPoints * TechTreeManager.main.beeHitPoints; //Attributes
        beeHeal = GlobalValues.main.beeHeal * TechTreeManager.main.beeHeal; //Attributes
        beeShield = GlobalValues.main.beeShield * TechTreeManager.main.beeShield; //Attributes
        beeShieldGeneration = GlobalValues.main.beeShieldGeneration; //Attributes
        beeTargetingRange = GlobalValues.main.beeTargetingRange * TechTreeManager.main.beeTargetingRange; //Attributes
        beeDamage = GlobalValues.main.beeDamage * TechTreeManager.main.beeDamage * PollenManager.main.damage; //Attributes
        beeAttackRate = GlobalValues.main.beeAttackRate * TechTreeManager.main.beeAttackRate * PollenManager.main.attackRate; //Attributes
        beeActionPower = GlobalValues.main.beeActionPower * TechTreeManager.main.beeActionPower; //Attributes
        beeEffectPower = GlobalValues.main.beeEffectPower * TechTreeManager.main.beeEffectPower; //Attributes
        beeEffectRate = GlobalValues.main.beeEffectRate * TechTreeManager.main.beeEffectRate; //Attributes
        beeCarryCapacity = GlobalValues.main.beeCarryCapacity * TechTreeManager.main.beeCarryCapacity * PollenManager.main.beeCarryCapacity; //Attributes
        beeMoveSpeed = GlobalValues.main.beeMoveSpeed * TechTreeManager.main.beeMoveSpeed * PollenManager.main.beeMoveSpeed; //Attributes
        beeArmor = GlobalValues.main.beeArmor + TechTreeManager.main.beeArmor; //Attributes
        beeResistance = GlobalValues.main.beeResistance + TechTreeManager.main.beeResistance; //Attributes
        beeDodge = GlobalValues.main.beeDodge + TechTreeManager.main.beeDodge; //Attributes
        beeArmorPierce = GlobalValues.main.beeArmorPierce * TechTreeManager.main.beeArmorPierce * PollenManager.main.armorPierce; //Attributes
        beeResistancePierce = GlobalValues.main.beeResistancePierce * TechTreeManager.main.beeResistancePierce * PollenManager.main.resistancePierce; //Attributes
        beeDodgePierce = GlobalValues.main.beeDodgePierce * TechTreeManager.main.beeDodgePierce * PollenManager.main.dodgePierce; //Attributes
        beeStealthDetection = GlobalValues.main.beeStealthDetection + TechTreeManager.main.beeStealthDetection + PollenManager.main.stealthDetection; //Attributes
        if (beeStealthDetection > 1f)
        {
            beeStealthDetection = 1f;
        }
        //Towers
        towerSell = GlobalValues.main.towerSell + TechTreeManager.main.towerSell; //StructureUIHandler
        if (towerSell > 1f)
        {
            towerSell = 1f;
        }
        towerHitPoints = GlobalValues.main.towerHitPoints * TechTreeManager.main.towerHitPoints; //Attributes
        towerHeal = GlobalValues.main.towerHeal * TechTreeManager.main.towerHeal; //Attributes
        towerShield = GlobalValues.main.towerShield * TechTreeManager.main.towerShield; //Attributes
        towerShieldGeneration = GlobalValues.main.towerShieldGeneration; //Attributes
        towerTargetingRange = GlobalValues.main.towerTargetingRange * TechTreeManager.main.towerTargetingRange * PollenManager.main.towerTargetingRange; //Attributes
        towerDamage = GlobalValues.main.towerDamage * TechTreeManager.main.towerDamage * PollenManager.main.damage; //Attributes
        towerAttackRate = GlobalValues.main.towerAttackRate * TechTreeManager.main.towerAttackRate * PollenManager.main.attackRate; //Attributes
        towerActionPower = GlobalValues.main.towerActionPower * TechTreeManager.main.towerActionPower; //Attributes
        towerBuffPower = GlobalValues.main.towerBuffPower * TechTreeManager.main.towerBuffPower * PollenManager.main.buffPower; //Effects
        towerEffectPower = GlobalValues.main.towerEffectPower * TechTreeManager.main.towerEffectPower; //Attributes
        towerEffectRate = GlobalValues.main.towerEffectRate * TechTreeManager.main.towerEffectRate; //Attributes
        towerCarryCapacity = GlobalValues.main.towerCarryCapacity * TechTreeManager.main.towerCarryCapacity; //Attributes
        towerMoveSpeed = GlobalValues.main.towerMoveSpeed * TechTreeManager.main.towerMoveSpeed; //Attributes
        towerArmor = GlobalValues.main.towerArmor + TechTreeManager.main.towerArmor; //Attributes
        towerResistance = GlobalValues.main.towerResistance + TechTreeManager.main.towerResistance; //Attributes
        towerDodge = GlobalValues.main.towerDodge + TechTreeManager.main.towerDodge; //Attributes
        towerArmorPierce = GlobalValues.main.towerArmorPierce * TechTreeManager.main.towerArmorPierce * PollenManager.main.armorPierce; //Attributes
        towerResistancePierce = GlobalValues.main.towerResistancePierce * TechTreeManager.main.towerResistancePierce * PollenManager.main.resistancePierce; //Attributes
        towerDodgePierce = GlobalValues.main.towerDodgePierce * TechTreeManager.main.towerDodgePierce * PollenManager.main.dodgePierce; //Attributes
        towerStealthDetection = GlobalValues.main.towerStealthDetection + TechTreeManager.main.towerStealthDetection * PollenManager.main.stealthDetection; //Attributes
        if (towerStealthDetection > 1f)
        {
            towerStealthDetection = 1f;
        }
        towerAoEArea = GlobalValues.main.towerAoEArea * TechTreeManager.main.towerAoEArea * PollenManager.main.towerAoEArea; //Projectile
        towerAoEDamageDropOff = GlobalValues.main.towerAoEDamageDropOff * TechTreeManager.main.towerAoEDamageDropOff * PollenManager.main.towerAoEDamageDropOff; //Projectile
        if (towerAoEDamageDropOff > 1f)
        {
            towerAoEDamageDropOff = 1f;
        }
        towerRampingDamage = GlobalValues.main.towerRampingDamage * TechTreeManager.main.towerRampingDamage; //Actions
        towerExtraRampCount = (int)(GlobalValues.main.towerExtraRampCount + TechTreeManager.main.towerExtraRampCount * PollenManager.main.towerExtraRampCount); //Actions
        towerExtraRicochetCount = (int)(GlobalValues.main.towerExtraRicochetCount + TechTreeManager.main.towerExtraRicochetCount * PollenManager.main.towerExtraRicochetCount); //Projectile
        //Enemies
        enemyHitPoints = GlobalValues.main.enemyHitPoints[(int)GlobalValues.main.difficulty] * TechTreeManager.main.enemyHitPoints; //Attributes
        enemyHeal = GlobalValues.main.enemyHeal * TechTreeManager.main.enemyHeal; //Attributes
        enemyShield = GlobalValues.main.enemyShield * TechTreeManager.main.enemyShield; //Attributes
        enemyShieldGeneration = GlobalValues.main.enemyShieldGeneration; //Attributes
        enemyTargetingRange = GlobalValues.main.enemyTargetingRange * TechTreeManager.main.enemyTargetingRange; //Attributes
        enemyDamage = GlobalValues.main.enemyDamage * TechTreeManager.main.enemyDamage; //Attributes
        enemyAttackRate = GlobalValues.main.enemyAttackRate * TechTreeManager.main.enemyAttackRate; //Attributes
        enemyActionPower = GlobalValues.main.enemyActionPower * TechTreeManager.main.enemyActionPower; //Attributes
        enemySlowPower = GlobalValues.main.enemySlowPower * TechTreeManager.main.enemySlowPower; //Attributes
        enemySlowPierce = GlobalValues.main.enemySlowPierce * TechTreeManager.main.enemySlowPierce; //Attributes
        enemyFreezePower = GlobalValues.main.enemyFreezePower * TechTreeManager.main.enemyFreezePower; //Attributes
        enemyFreezePierce = GlobalValues.main.enemyFreezePierce * TechTreeManager.main.enemyFreezePierce; //Attributes
        enemyEffectPower = GlobalValues.main.enemyEffectPower * TechTreeManager.main.enemyEffectPower; //Attributes
        enemyEffectRate = GlobalValues.main.enemyEffectRate * TechTreeManager.main.enemyEffectRate; //Attributes
        enemyCarryCapacity = GlobalValues.main.enemyCarryCapacity * TechTreeManager.main.enemyCarryCapacity; //Attributes
        enemyMoveSpeed = GlobalValues.main.enemyMoveSpeed[(int)GlobalValues.main.difficulty] * TechTreeManager.main.enemyMoveSpeed; //Attributes
        enemyArmor = GlobalValues.main.enemyArmor + TechTreeManager.main.enemyArmor; //Attributes
        enemyResistance = GlobalValues.main.enemyResistance + TechTreeManager.main.enemyResistance; //Attributes
        enemyDodge = GlobalValues.main.enemyDodge + TechTreeManager.main.enemyDodge; //Attributes
        enemyArmorPierce = GlobalValues.main.enemyArmorPierce * TechTreeManager.main.enemyArmorPierce; //Attributes
        enemyResistancePierce = GlobalValues.main.enemyResistancePierce * TechTreeManager.main.enemyResistancePierce; //Attributes
        enemyDodgePierce = GlobalValues.main.enemyDodgePierce * TechTreeManager.main.enemyDodgePierce; //Attributes
        enemyStealthDetection = GlobalValues.main.enemyStealthDetection + TechTreeManager.main.enemyStealthDetection; //Attributes
        if (enemyStealthDetection > 1f)
        {
            enemyStealthDetection = 1f;
        }
        Attributes[] allAttributes = FindObjectsByType<Attributes>(FindObjectsSortMode.None);
        foreach (Attributes attributes in allAttributes)
        {
            attributes.SetBuffedAttributes();
        }
    }
}
