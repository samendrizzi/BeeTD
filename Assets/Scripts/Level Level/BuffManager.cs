using System;
using UnityEngine;

public class BuffManager : MonoBehaviour
{

    public static BuffManager main;

    //Summed Buffs
    //Environment
    public float startingNectar;
    public float startingHoney;
    public float queenBeeHitPoints;
    public float queenBeeReturnDamage;
    public float queenBeeArmor;
    public float honeyDropRate;
    public float honeyinterest;
    public float pollenBuff;
    public float nectarGenerationRate;
    public float pollenGenerationRate;
    public int honeycombTicks;
    public float honeycombGeneration;
    public float honeycombFillTime;
    //Worker Bees + Towers
    public float slowPower;
    public float slowPierce;
    public float freezePower;
    public float freezePierce;
    //Worker Bees
    public int startingBees;
    public float beeCost;
    public float beeCostScaling;
    public float beeHitPoints;
    public float beeHeal;
    public float beeShield;
    public float beeShieldGeneration;
    public float beeTargetingRange;
    public float beeDamage;
    public float beeAttackRate;
    public float beeActionPower;
    public float beeActionRate;
    public float beeEffectPower;
    public float beeEffectRate;
    public float beeCarryCapacity;
    public float beeMoveSpeed;
    public float beeArmor;
    public float beeResistance;
    public float beeDodge;
    public float beeArmorPierce;
    public float beeResistancePierce;
    public float beeDodgePierce;
    public float beeStealthDetection;
    //Towers
    public float towerSell;
    public float towerHitPoints;
    public float towerHeal;
    public float towerShield;
    public float towerShieldGeneration;
    public float towerTargetingRange;
    public float towerDamage;
    public float towerAttackRate;
    public float towerActionPower;
    public float towerActionRate;
    public float towerEffectPower;
    public float towerEffectRate;
    public float towerCarryCapacity;
    public float towerMoveSpeed;
    public float towerArmor;
    public float towerResistance;
    public float towerDodge;
    public float towerArmorPierce;
    public float towerResistancePierce;
    public float towerDodgePierce;
    public float towerStealthDetection;
    public float towerAoEArea;
    public float towerAoEDamageDropOff;
    public int towerExtraRampCount;
    public int towerExtraRicochetCount;
    //Enemies
    public float enemyHitPoints;
    public float enemyHeal;
    public float enemyShield;
    public float enemyShieldGeneration;
    public float enemyTargetingRange;
    public float enemyDamage;
    public float enemyAttackRate;
    public float enemyActionPower;
    public float enemyActionRate;
    public float enemySlowPower;
    public float enemySlowPierce;
    public float enemyFreezePower;
    public float enemyFreezePierce;
    public float enemyEffectPower;
    public float enemyEffectRate;
    public float enemyCarryCapacity;
    public float enemyMoveSpeed;
    public float enemyArmor;
    public float enemyResistance;
    public float enemyDodge;
    public float enemyArmorPierce;
    public float enemyResistancePierce;
    public float enemyDodgePierce;
    public float enemyStealthDetection;
    public float enemyWaveScaling;
    public float enemyHatchTime;
    public float enemySpawnTime;
    public float enemyStealthTime;

    void Awake()
    {
        RefreshBuffs();
    }

    void RefreshBuffs()
    {
        //Environment
        startingNectar = GlobalValues.main.startingNectar + TechTreeManager.main.startingNectar;
        startingHoney = GlobalValues.main.startingHoney + TechTreeManager.main.startingHoney;
        queenBeeHitPoints = GlobalValues.main.queenBeeHitPoints * TechTreeManager.main.queenBeeHitPoints;
        queenBeeReturnDamage = GlobalValues.main.queenBeeReturnDamage + TechTreeManager.main.queenBeeReturnDamage;
        queenBeeArmor = GlobalValues.main.queenBeeArmor + TechTreeManager.main.queenBeeArmor;
        honeyDropRate = GlobalValues.main.honeyDropRate + TechTreeManager.main.honeyDropRate;
        honeyinterest = GlobalValues.main.honeyinterest + TechTreeManager.main.honeyinterest;
        pollenBuff = GlobalValues.main.pollenBuff + TechTreeManager.main.pollenBuff;
        nectarGenerationRate = GlobalValues.main.nectarGenerationRate + TechTreeManager.main.nectarGenerationRate;
        pollenGenerationRate = GlobalValues.main.pollenGenerationRate + TechTreeManager.main.pollenGenerationRate;
        honeycombTicks = GlobalValues.main.honeycombTicks + TechTreeManager.main.honeycombTicks;
        honeycombGeneration = GlobalValues.main.honeycombGeneration * TechTreeManager.main.honeycombGeneration;
        honeycombFillTime = GlobalValues.main.honeycombFillTime * TechTreeManager.main.honeycombFillTime;
        //Worker Bees + Towers
        slowPower = GlobalValues.main.slowPower * TechTreeManager.main.slowPower;
        slowPierce = GlobalValues.main.slowPierce * TechTreeManager.main.slowPierce;
        freezePower = GlobalValues.main.freezePower * TechTreeManager.main.freezePower;
        freezePierce = GlobalValues.main.freezePierce * TechTreeManager.main.freezePierce;
        //Worker Bees
        startingBees = GlobalValues.main.startingBees + TechTreeManager.main.startingBees;
        beeCost = GlobalValues.main.beeCost * TechTreeManager.main.beeCost;
        beeCostScaling = GlobalValues.main.beeCostScaling * TechTreeManager.main.beeCostScaling;
        beeHitPoints = GlobalValues.main.beeHitPoints * TechTreeManager.main.beeHitPoints;
        beeHeal = GlobalValues.main.beeHeal * TechTreeManager.main.beeHeal;
        beeShield = GlobalValues.main.beeShield + TechTreeManager.main.beeShield;
        beeShieldGeneration = GlobalValues.main.beeShieldGeneration;
        beeTargetingRange = GlobalValues.main.beeTargetingRange + TechTreeManager.main.beeTargetingRange;
        beeDamage = GlobalValues.main.beeDamage * TechTreeManager.main.beeDamage;
        beeAttackRate = GlobalValues.main.beeAttackRate * TechTreeManager.main.beeAttackRate;
        beeActionPower = GlobalValues.main.beeActionPower * TechTreeManager.main.beeActionPower;

        beeEffectPower = GlobalValues.main.beeEffectPower * TechTreeManager.main.beeEffectPower;
        beeEffectRate = GlobalValues.main.beeEffectRate * TechTreeManager.main.beeEffectRate;
        beeCarryCapacity = GlobalValues.main.beeCarryCapacity * TechTreeManager.main.beeCarryCapacity;
        beeMoveSpeed = GlobalValues.main.beeMoveSpeed * TechTreeManager.main.beeMoveSpeed;
        beeArmor = GlobalValues.main.beeArmor + TechTreeManager.main.beeArmor;
        beeResistance = GlobalValues.main.beeResistance + TechTreeManager.main.beeResistance;
        beeDodge = GlobalValues.main.beeDodge + TechTreeManager.main.beeDodge;
        beeArmorPierce = GlobalValues.main.beeArmorPierce + TechTreeManager.main.beeArmorPierce;
        beeResistancePierce = GlobalValues.main.beeResistancePierce + TechTreeManager.main.beeResistancePierce;
        beeDodgePierce = GlobalValues.main.beeDodgePierce + TechTreeManager.main.beeDodgePierce;
        beeStealthDetection = GlobalValues.main.beeStealthDetection + TechTreeManager.main.beeStealthDetection;
        //Towers
        towerSell = GlobalValues.main.towerSell + TechTreeManager.main.towerSell;
        towerHitPoints = GlobalValues.main.towerHitPoints * TechTreeManager.main.towerHitPoints;
        towerHeal = GlobalValues.main.towerHeal * TechTreeManager.main.towerHeal;
        towerShield = GlobalValues.main.towerShield + TechTreeManager.main.towerShield;
        towerShieldGeneration = GlobalValues.main.towerShieldGeneration;
        towerTargetingRange = GlobalValues.main.towerTargetingRange + TechTreeManager.main.towerTargetingRange;
        towerDamage = GlobalValues.main.towerDamage * TechTreeManager.main.towerDamage;
        towerAttackRate = GlobalValues.main.towerAttackRate * TechTreeManager.main.towerAttackRate;
        towerActionPower = GlobalValues.main.towerActionPower * TechTreeManager.main.towerActionPower;
        towerEffectPower = GlobalValues.main.towerEffectPower * TechTreeManager.main.towerEffectPower;
        towerEffectRate = GlobalValues.main.towerEffectRate * TechTreeManager.main.towerEffectRate;
        towerCarryCapacity = GlobalValues.main.towerCarryCapacity * TechTreeManager.main.towerCarryCapacity;
        towerMoveSpeed = GlobalValues.main.towerMoveSpeed * TechTreeManager.main.towerMoveSpeed;
        towerArmor = GlobalValues.main.towerArmor + TechTreeManager.main.towerArmor;
        towerResistance = GlobalValues.main.towerResistance + TechTreeManager.main.towerResistance;
        towerDodge = GlobalValues.main.towerDodge + TechTreeManager.main.towerDodge;
        towerArmorPierce = GlobalValues.main.towerArmorPierce + TechTreeManager.main.towerArmorPierce;
        towerResistancePierce = GlobalValues.main.towerResistancePierce + TechTreeManager.main.towerResistancePierce;
        towerDodgePierce = GlobalValues.main.towerDodgePierce + TechTreeManager.main.towerDodgePierce;
        towerStealthDetection = GlobalValues.main.towerStealthDetection + TechTreeManager.main.towerStealthDetection;
        towerAoEArea = GlobalValues.main.towerAoEArea * TechTreeManager.main.towerAoEArea;
        towerAoEDamageDropOff = GlobalValues.main.towerAoEDamageDropOff + TechTreeManager.main.towerAoEDamageDropOff;
        towerExtraRampCount = GlobalValues.main.towerExtraRampCount + TechTreeManager.main.towerExtraRampCount;
        towerExtraRicochetCount = GlobalValues.main.towerExtraRicochetCount + TechTreeManager.main.towerExtraRicochetCount;
        //Enemies
        enemyHitPoints = GlobalValues.main.enemyHitPoints * TechTreeManager.main.enemyHitPoints;
        enemyHeal = GlobalValues.main.enemyHeal * TechTreeManager.main.enemyHeal;
        enemyShield = GlobalValues.main.enemyShield + TechTreeManager.main.enemyShield;
        enemyShieldGeneration = GlobalValues.main.enemyShieldGeneration;
        enemyTargetingRange = GlobalValues.main.enemyTargetingRange + TechTreeManager.main.enemyTargetingRange;
        enemyDamage = GlobalValues.main.enemyDamage * TechTreeManager.main.enemyDamage;
        enemyAttackRate = GlobalValues.main.enemyAttackRate * TechTreeManager.main.enemyAttackRate;
        enemyActionPower = GlobalValues.main.enemyActionPower * TechTreeManager.main.enemyActionPower;
        enemySlowPower = GlobalValues.main.enemySlowPower * TechTreeManager.main.enemySlowPower;
        enemySlowPierce = GlobalValues.main.enemySlowPierce * TechTreeManager.main.enemySlowPierce;
        enemyFreezePower = GlobalValues.main.enemyFreezePower * TechTreeManager.main.enemyFreezePower;
        enemyFreezePierce = GlobalValues.main.enemyFreezePierce * TechTreeManager.main.enemyFreezePierce;
        enemyEffectPower = GlobalValues.main.enemyEffectPower * TechTreeManager.main.enemyEffectPower;
        enemyEffectRate = GlobalValues.main.enemyEffectRate * TechTreeManager.main.enemyEffectRate;
        enemyCarryCapacity = GlobalValues.main.enemyCarryCapacity * TechTreeManager.main.enemyCarryCapacity;
        enemyMoveSpeed = GlobalValues.main.enemyMoveSpeed * TechTreeManager.main.enemyMoveSpeed;
        enemyArmor = GlobalValues.main.enemyArmor + TechTreeManager.main.enemyArmor;
        enemyResistance = GlobalValues.main.enemyResistance + TechTreeManager.main.enemyResistance;
        enemyDodge = GlobalValues.main.enemyDodge + TechTreeManager.main.enemyDodge;
        enemyArmorPierce = GlobalValues.main.enemyArmorPierce + TechTreeManager.main.enemyArmorPierce;
        enemyResistancePierce = GlobalValues.main.enemyResistancePierce + TechTreeManager.main.enemyResistancePierce;
        enemyDodgePierce = GlobalValues.main.enemyDodgePierce + TechTreeManager.main.enemyDodgePierce;
        enemyStealthDetection = GlobalValues.main.enemyStealthDetection + TechTreeManager.main.enemyStealthDetection;
    }
}
