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
    public float towerBuffPower;
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
    public float towerRampingDamage;
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
        honeyinterest = GlobalValues.main.honeyinterest + TechTreeManager.main.honeyinterest; //Not Implemented
        pollenBuff = GlobalValues.main.pollenBuff + TechTreeManager.main.pollenBuff; //Not Implemented
        nectarGenerationRate = GlobalValues.main.nectarGenerationRate * TechTreeManager.main.nectarGenerationRate; //Plot
        pollenGenerationRate = GlobalValues.main.pollenGenerationRate * TechTreeManager.main.pollenGenerationRate; //Not Implentation
        honeycombTicks = GlobalValues.main.honeycombTicks + TechTreeManager.main.honeycombTicks; //Plot
        honeycombGeneration = GlobalValues.main.honeycombGeneration * TechTreeManager.main.honeycombGeneration; //Plot
        honeycombFillTime = GlobalValues.main.honeycombFillTime * TechTreeManager.main.honeycombFillTime; //Actions
        //Worker Bees + Towers
        slowPower = GlobalValues.main.slowPower * TechTreeManager.main.slowPower; //Attributes
        slowPierce = GlobalValues.main.slowPierce * TechTreeManager.main.slowPierce; //Attributes
        freezePower = GlobalValues.main.freezePower * TechTreeManager.main.freezePower; //Attributes
        freezePierce = GlobalValues.main.freezePierce * TechTreeManager.main.freezePierce; //Attributes
        //Worker Bees
        startingBees = GlobalValues.main.startingBees[(int)GlobalValues.main.difficulty] + TechTreeManager.main.startingBees; //LevelManager
        beeCost = GlobalValues.main.beeCost * TechTreeManager.main.beeCost; //LevelManager
        beeCostScaling = GlobalValues.main.beeCostScaling * TechTreeManager.main.beeCostScaling; //LevelManager
        beeHitPoints = GlobalValues.main.beeHitPoints * TechTreeManager.main.beeHitPoints; //Attributes
        beeHeal = GlobalValues.main.beeHeal * TechTreeManager.main.beeHeal; //Attributes
        beeShield = GlobalValues.main.beeShield + TechTreeManager.main.beeShield; //Attributes
        beeShieldGeneration = GlobalValues.main.beeShieldGeneration; //Attributes
        beeTargetingRange = GlobalValues.main.beeTargetingRange + TechTreeManager.main.beeTargetingRange; //Attributes
        beeDamage = GlobalValues.main.beeDamage * TechTreeManager.main.beeDamage; //Attributes
        beeAttackRate = GlobalValues.main.beeAttackRate * TechTreeManager.main.beeAttackRate; //Attributes
        beeActionPower = GlobalValues.main.beeActionPower * TechTreeManager.main.beeActionPower; //Attributes
        beeEffectPower = GlobalValues.main.beeEffectPower * TechTreeManager.main.beeEffectPower; //Attributes
        beeEffectRate = GlobalValues.main.beeEffectRate * TechTreeManager.main.beeEffectRate; //Attributes
        beeCarryCapacity = GlobalValues.main.beeCarryCapacity * TechTreeManager.main.beeCarryCapacity; //Attributes
        beeMoveSpeed = GlobalValues.main.beeMoveSpeed * TechTreeManager.main.beeMoveSpeed; //Attributes
        beeArmor = GlobalValues.main.beeArmor + TechTreeManager.main.beeArmor; //Attributes
        beeResistance = GlobalValues.main.beeResistance + TechTreeManager.main.beeResistance; //Attributes
        beeDodge = GlobalValues.main.beeDodge + TechTreeManager.main.beeDodge; //Attributes
        beeArmorPierce = GlobalValues.main.beeArmorPierce + TechTreeManager.main.beeArmorPierce; //Attributes
        beeResistancePierce = GlobalValues.main.beeResistancePierce + TechTreeManager.main.beeResistancePierce; //Attributes
        beeDodgePierce = GlobalValues.main.beeDodgePierce + TechTreeManager.main.beeDodgePierce; //Attributes
        beeStealthDetection = GlobalValues.main.beeStealthDetection + TechTreeManager.main.beeStealthDetection; //Attributes
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
        towerShield = GlobalValues.main.towerShield + TechTreeManager.main.towerShield; //Attributes
        towerShieldGeneration = GlobalValues.main.towerShieldGeneration; //Attributes
        towerTargetingRange = GlobalValues.main.towerTargetingRange + TechTreeManager.main.towerTargetingRange; //Attributes
        towerDamage = GlobalValues.main.towerDamage * TechTreeManager.main.towerDamage; //Attributes
        towerAttackRate = GlobalValues.main.towerAttackRate * TechTreeManager.main.towerAttackRate; //Attributes
        towerActionPower = GlobalValues.main.towerActionPower * TechTreeManager.main.towerActionPower; //Attributes
        towerBuffPower = GlobalValues.main.towerBuffPower * TechTreeManager.main.towerBuffPower; //Effects
        towerEffectPower = GlobalValues.main.towerEffectPower * TechTreeManager.main.towerEffectPower; //Attributes
        towerEffectRate = GlobalValues.main.towerEffectRate * TechTreeManager.main.towerEffectRate; //Attributes
        towerCarryCapacity = GlobalValues.main.towerCarryCapacity * TechTreeManager.main.towerCarryCapacity; //Attributes
        towerMoveSpeed = GlobalValues.main.towerMoveSpeed * TechTreeManager.main.towerMoveSpeed; //Attributes
        towerArmor = GlobalValues.main.towerArmor + TechTreeManager.main.towerArmor; //Attributes
        towerResistance = GlobalValues.main.towerResistance + TechTreeManager.main.towerResistance; //Attributes
        towerDodge = GlobalValues.main.towerDodge + TechTreeManager.main.towerDodge; //Attributes
        towerArmorPierce = GlobalValues.main.towerArmorPierce + TechTreeManager.main.towerArmorPierce; //Attributes
        towerResistancePierce = GlobalValues.main.towerResistancePierce + TechTreeManager.main.towerResistancePierce; //Attributes
        towerDodgePierce = GlobalValues.main.towerDodgePierce + TechTreeManager.main.towerDodgePierce; //Attributes
        towerStealthDetection = GlobalValues.main.towerStealthDetection + TechTreeManager.main.towerStealthDetection; //Attributes
        if (towerStealthDetection > 1f)
        {
            towerStealthDetection = 1f;
        }
        towerAoEArea = GlobalValues.main.towerAoEArea * TechTreeManager.main.towerAoEArea; //Projectile
        towerAoEDamageDropOff = GlobalValues.main.towerAoEDamageDropOff + TechTreeManager.main.towerAoEDamageDropOff; //Projectile
        if (towerAoEDamageDropOff > 1f)
        {
            towerAoEDamageDropOff = 1f;
        }
        towerRampingDamage = GlobalValues.main.towerRampingDamage * TechTreeManager.main.towerRampingDamage; //Actions
        towerExtraRampCount = GlobalValues.main.towerExtraRampCount + TechTreeManager.main.towerExtraRampCount; //Actions
        towerExtraRicochetCount = GlobalValues.main.towerExtraRicochetCount + TechTreeManager.main.towerExtraRicochetCount; //Projectile
        //Enemies
        enemyHitPoints = GlobalValues.main.enemyHitPoints[(int)GlobalValues.main.difficulty] * TechTreeManager.main.enemyHitPoints; //Attributes
        enemyHeal = GlobalValues.main.enemyHeal * TechTreeManager.main.enemyHeal; //Attributes
        enemyShield = GlobalValues.main.enemyShield + TechTreeManager.main.enemyShield; //Attributes
        enemyShieldGeneration = GlobalValues.main.enemyShieldGeneration; //Attributes
        enemyTargetingRange = GlobalValues.main.enemyTargetingRange + TechTreeManager.main.enemyTargetingRange; //Attributes
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
        enemyArmorPierce = GlobalValues.main.enemyArmorPierce + TechTreeManager.main.enemyArmorPierce; //Attributes
        enemyResistancePierce = GlobalValues.main.enemyResistancePierce + TechTreeManager.main.enemyResistancePierce; //Attributes
        enemyDodgePierce = GlobalValues.main.enemyDodgePierce + TechTreeManager.main.enemyDodgePierce; //Attributes
        enemyStealthDetection = GlobalValues.main.enemyStealthDetection + TechTreeManager.main.enemyStealthDetection; //Attributes
        if (enemyStealthDetection > 1f)
        {
            enemyStealthDetection = 1f;
        }
    }
}
