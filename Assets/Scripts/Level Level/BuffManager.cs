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
        startingNectar = GlobalValues.main.startingNectar[(int)GlobalValues.main.difficulty] * (TechTreeManager.main.startingNectar+ 1f); //LevelManager
        startingHoney = GlobalValues.main.startingHoney[(int)GlobalValues.main.difficulty] * (TechTreeManager.main.startingHoney+ 1f); //LevelManager
        queenBeeHitPoints = GlobalValues.main.queenBeeHitPoints * (TechTreeManager.main.queenBeeHitPoints+ 1f); //LevelManager
        queenBeeReturnDamage = GlobalValues.main.queenBeeReturnDamage * (TechTreeManager.main.queenBeeReturnDamage + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.QUEENTHORNS]+ 1f); //Actions
        queenBeeArmor = GlobalValues.main.queenBeeArmor * (TechTreeManager.main.queenBeeArmor + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.QUEENARMOR]+ 1f); //LevelManager
        honeyDropRate = GlobalValues.main.honeyDropRate[(int)GlobalValues.main.difficulty] * (TechTreeManager.main.honeyDropRate + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.HONEYDROPRATE]+ 1f); //Attributes
        if (honeyDropRate > 1f)
        {
            honeyDropRate = 1f;
        }
        honeyinterest = GlobalValues.main.honeyinterest * (TechTreeManager.main.honeyinterest + PollenManager.main.honeyInterest + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.HONEYINTEREST]+ 1f); //Not Implemented
        pollenGatherRatio = GlobalValues.main.pollenGatherRatio * (TechTreeManager.main.pollenGather * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.POLLENGATHERRATE]+ 1f); //PollenManager
        pollenBuff = GlobalValues.main.pollenBuff * (TechTreeManager.main.pollenBuff + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.POLLENBUFF]+ 1f); //Not Implemented
        nectarGenerationRate = GlobalValues.main.nectarGenerationRate * (TechTreeManager.main.nectarGenerationRate * PollenManager.main.nectarGenerationRate * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.NECTARGENERATIONRATE]+ 1f); //Plot
        honeycombTicks = GlobalValues.main.honeycombTicks * (TechTreeManager.main.honeycombTicks + (int)HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.HONEYCOMBTICKS]+ 1); //Plot
        honeycombGeneration = GlobalValues.main.honeycombGeneration * (TechTreeManager.main.honeycombGeneration * PollenManager.main.honeycombGeneration * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.HONEYCOMBGENERATIONRATE]+ 1f); //Plot
        honeycombFillTime = GlobalValues.main.honeycombFillTime * (TechTreeManager.main.honeycombFillTime * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.HONEYCOMBFILLTIME]+ 1f); //Actions
        //Worker Bees + Towers
        slowPower = GlobalValues.main.slowPower * (TechTreeManager.main.slowPower * PollenManager.main.slowPower * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.SLOWPOWER]+ 1f); //Attributes
        slowPierce = GlobalValues.main.slowPierce * (TechTreeManager.main.slowPierce * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.SLOWPIERCE]+ 1f); //Attributes
        freezePower = GlobalValues.main.freezePower * (TechTreeManager.main.freezePower * PollenManager.main.freezePower * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.FREEZEPOWER]+ 1f); //Attributes
        freezePierce = GlobalValues.main.freezePierce * (TechTreeManager.main.freezePierce * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.FREEZEPIERCE]+ 1f); //Attributes
        //Worker Bees
        startingBees = GlobalValues.main.startingBees[(int)GlobalValues.main.difficulty] * (TechTreeManager.main.startingBees+ 1); //LevelManager
        beeCost = GlobalValues.main.beeCost * (TechTreeManager.main.beeCost+ 1f); //LevelManager
        beeCostScaling = GlobalValues.main.beeCostScaling * (TechTreeManager.main.beeCostScaling+ 1f); //LevelManager
        beeHitPoints = GlobalValues.main.beeHitPoints * (TechTreeManager.main.beeHitPoints+ 1f); //Attributes
        beeHeal = GlobalValues.main.beeHeal * (TechTreeManager.main.beeHeal * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEHEAL]+ 1f); //Attributes
        beeShield = GlobalValues.main.beeShield * (TechTreeManager.main.beeShield * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEESHIELD]+ 1f); //Attributes
        beeShieldGeneration = GlobalValues.main.beeShieldGeneration * (TechTreeManager.main.beeShieldGeneration * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEESHIELDGENERATION]+ 1f); //Attributes
        beeTargetingRange = GlobalValues.main.beeTargetingRange * (TechTreeManager.main.beeTargetingRange * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEETARGETINGRANGE]+ 1f); //Attributes
        beeDamage = GlobalValues.main.beeDamage * (TechTreeManager.main.beeDamage * PollenManager.main.damage * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEDAMAGE]+ 1f); //Attributes
        beeAttackRate = GlobalValues.main.beeAttackRate * (TechTreeManager.main.beeAttackRate * PollenManager.main.attackRate * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEATTACKRATE]+ 1f); //Attributes
        beeActionPower = GlobalValues.main.beeActionPower * (TechTreeManager.main.beeActionPower * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEACTIONPOWER]+ 1f); //Attributes
        beeActionRate = GlobalValues.main.beeActionRate * (TechTreeManager.main.beeActionRate * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEACTIONRATE]+ 1f); //Attributes
        beeEffectPower = GlobalValues.main.beeEffectPower * (TechTreeManager.main.beeEffectPower * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEEFFECTPOWER]+ 1f); //Attributes
        beeEffectRate = GlobalValues.main.beeEffectRate * (TechTreeManager.main.beeEffectRate * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEEFFECTRATE]+ 1f); //Attributes
        beeCarryCapacity = GlobalValues.main.beeCarryCapacity * (TechTreeManager.main.beeCarryCapacity * PollenManager.main.beeCarryCapacity * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEECARRYCAPACITY]+ 1f); //Attributes
        beeMoveSpeed = GlobalValues.main.beeMoveSpeed * (TechTreeManager.main.beeMoveSpeed * PollenManager.main.beeMoveSpeed * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEMOVESPEED]+ 1f); //Attributes
        beeArmor = GlobalValues.main.beeArmor * (TechTreeManager.main.beeArmor + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEARMOR]+ 1f); //Attributes
        beeResistance = GlobalValues.main.beeResistance * (TechTreeManager.main.beeResistance + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEREISTANCE]+ 1f); //Attributes
        beeDodge = GlobalValues.main.beeDodge * (TechTreeManager.main.beeDodge + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEDODGE]+ 1f); //Attributes
        beeArmorPierce = GlobalValues.main.beeArmorPierce * (TechTreeManager.main.beeArmorPierce * PollenManager.main.armorPierce * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEARMORPIERCE]+ 1f); //Attributes
        beeResistancePierce = GlobalValues.main.beeResistancePierce * (TechTreeManager.main.beeResistancePierce * PollenManager.main.resistancePierce * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEERESISTANCEPIERCE]+ 1f); //Attributes
        beeDodgePierce = GlobalValues.main.beeDodgePierce * (TechTreeManager.main.beeDodgePierce * PollenManager.main.dodgePierce * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEDODGEPIERCE]+ 1f); //Attributes
        beeStealthDetection = GlobalValues.main.beeStealthDetection * (TechTreeManager.main.beeStealthDetection + PollenManager.main.stealthDetection + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEESTEALTHDETECTION]+ 1f); //Attributes
        if (beeStealthDetection > 1f)
        {
            beeStealthDetection = 1f;
        }
        //Towers
        towerSell = GlobalValues.main.towerSell * (TechTreeManager.main.towerSell+ 1f); //StructureUIHandler
        if (towerSell > 1f)
        {
            towerSell = 1f;
        }
        towerHitPoints = GlobalValues.main.towerHitPoints * (TechTreeManager.main.towerHitPoints+ 1f); //Attributes
        towerHeal = GlobalValues.main.towerHeal * (TechTreeManager.main.towerHeal * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERHEAL]+ 1f); //Attributes
        towerShield = GlobalValues.main.towerShield * (TechTreeManager.main.towerShield * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERSHIELD]+ 1f); //Attributes
        towerShieldGeneration = GlobalValues.main.towerShieldGeneration * (TechTreeManager.main.towerShieldGeneration * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERSHIELDGENERATION]+ 1f); //Attributes
        towerTargetingRange = GlobalValues.main.towerTargetingRange * (TechTreeManager.main.towerTargetingRange * PollenManager.main.towerTargetingRange * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERTARGETINGRANGE]+ 1f); //Attributes
        towerDamage = GlobalValues.main.towerDamage * (TechTreeManager.main.towerDamage * PollenManager.main.damage * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERDAMAGE]+ 1f); //Attributes
        towerAttackRate = GlobalValues.main.towerAttackRate * (TechTreeManager.main.towerAttackRate * PollenManager.main.attackRate * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERATTACKRATE]+ 1f); //Attributes
        towerActionPower = GlobalValues.main.towerActionPower * (TechTreeManager.main.towerActionPower * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERACTIONPOWER]+ 1f); //Attributes
        towerActionRate = GlobalValues.main.towerActionRate * (TechTreeManager.main.towerActionRate * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERACTIONRATE]+ 1f); //Attributes
        towerBuffPower = GlobalValues.main.towerBuffPower * (TechTreeManager.main.towerBuffPower * PollenManager.main.buffPower * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERBUFFPOWER]+ 1f); //Effects
        towerEffectPower = GlobalValues.main.towerEffectPower * (TechTreeManager.main.towerEffectPower * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWEREFFECTPOWER]+ 1f); //Attributes
        towerEffectRate = GlobalValues.main.towerEffectRate * (TechTreeManager.main.towerEffectRate * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWEREFFECTRATE]+ 1f); //Attributes
        towerCarryCapacity = GlobalValues.main.towerCarryCapacity * (TechTreeManager.main.towerCarryCapacity * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERCARRYCAPACITY]+ 1f); //Attributes
        towerMoveSpeed = GlobalValues.main.towerMoveSpeed * (TechTreeManager.main.towerMoveSpeed * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERMOVESPEED]+ 1f); //Attributes
        towerArmor = GlobalValues.main.towerArmor * (TechTreeManager.main.towerArmor + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERARMOR]+ 1f); //Attributes
        towerResistance = GlobalValues.main.towerResistance * (TechTreeManager.main.towerResistance + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERREISTANCE]+ 1f); //Attributes
        towerDodge = GlobalValues.main.towerDodge * (TechTreeManager.main.towerDodge + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERDODGE]+ 1f); //Attributes
        towerArmorPierce = GlobalValues.main.towerArmorPierce * (TechTreeManager.main.towerArmorPierce * PollenManager.main.armorPierce * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERARMORPIERCE]+ 1f); //Attributes
        towerResistancePierce = GlobalValues.main.towerResistancePierce * (TechTreeManager.main.towerResistancePierce * PollenManager.main.resistancePierce * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERRESISTANCEPIERCE]+ 1f); //Attributes
        towerDodgePierce = GlobalValues.main.towerDodgePierce * (TechTreeManager.main.towerDodgePierce * PollenManager.main.dodgePierce * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERDODGEPIERCE]+ 1f); //Attributes
        towerStealthDetection = GlobalValues.main.towerStealthDetection * (TechTreeManager.main.towerStealthDetection * PollenManager.main.stealthDetection + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERSTEALTHDETECTION]+ 1f); //Attributes
        if (towerStealthDetection > 1f)
        {
            towerStealthDetection = 1f;
        }
        towerAoEArea = GlobalValues.main.towerAoEArea * (TechTreeManager.main.towerAoEArea * PollenManager.main.towerAoEArea * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERAOEAREA]+ 1f); //Projectile
        towerAoEDamageDropOff = GlobalValues.main.towerAoEDamageDropOff * (TechTreeManager.main.towerAoEDamageDropOff * PollenManager.main.towerAoEDamageDropOff * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERAOEDAMAGEDROPOFF]+ 1f); //Projectile
        if (towerAoEDamageDropOff > 1f)
        {
            towerAoEDamageDropOff = 1f;
        }
        towerRampingDamage = GlobalValues.main.towerRampingDamage * (TechTreeManager.main.towerRampingDamage * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERRAMPINGDAMAGE]+ 1f); //Actions
        towerExtraRampCount = (int)(GlobalValues.main.towerExtraRampCount * (TechTreeManager.main.towerExtraRampCount * PollenManager.main.towerExtraRampCount + (int)HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWEREXTRARAMPCOUNT])+ 1f); //Actions
        towerExtraRicochetCount = (int)(GlobalValues.main.towerExtraRicochetCount * (TechTreeManager.main.towerExtraRicochetCount * PollenManager.main.towerExtraRicochetCount + (int)HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWEREXTRARICOCHETCOUNT])+ 1f); //Projectile
        //Enemies
        enemyHitPoints = GlobalValues.main.enemyHitPoints[(int)GlobalValues.main.difficulty] * (TechTreeManager.main.enemyHitPoints+ 1f); //Attributes
        enemyHeal = GlobalValues.main.enemyHeal * (TechTreeManager.main.enemyHeal * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYHEAL]+ 1f); //Attributes
        enemyShield = GlobalValues.main.enemyShield * (TechTreeManager.main.enemyShield * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYSHIELD]+ 1f); //Attributes
        enemyShieldGeneration = GlobalValues.main.enemyShieldGeneration * (TechTreeManager.main.enemyShieldGeneration * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYSHIELDGENERATION]+ 1f); //Attributes
        enemyTargetingRange = GlobalValues.main.enemyTargetingRange * (TechTreeManager.main.enemyTargetingRange * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYTARGETINGRANGE]+ 1f); //Attributes
        enemyDamage = GlobalValues.main.enemyDamage * (TechTreeManager.main.enemyDamage * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYDAMAGE]+ 1f); //Attributes
        enemyAttackRate = GlobalValues.main.enemyAttackRate * (TechTreeManager.main.enemyAttackRate * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYATTACKRATE]+ 1f); //Attributes
        enemyActionPower = GlobalValues.main.enemyActionPower * (TechTreeManager.main.enemyActionPower * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYACTIONPOWER]+ 1f); //Attributes
        enemyActionRate = GlobalValues.main.enemyActionRate * (TechTreeManager.main.enemyActionRate * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYACTIONRATE]+ 1f); //Attributes
        enemySlowPower = GlobalValues.main.enemySlowPower * (TechTreeManager.main.enemySlowPower+ 1f); //Attributes
        enemySlowPierce = GlobalValues.main.enemySlowPierce * (TechTreeManager.main.enemySlowPierce+ 1f); //Attributes
        enemyFreezePower = GlobalValues.main.enemyFreezePower * (TechTreeManager.main.enemyFreezePower+ 1f); //Attributes
        enemyFreezePierce = GlobalValues.main.enemyFreezePierce * (TechTreeManager.main.enemyFreezePierce+ 1f); //Attributes
        enemyEffectPower = GlobalValues.main.enemyEffectPower * (TechTreeManager.main.enemyEffectPower * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYEFFECTPOWER]+ 1f); //Attributes
        enemyEffectRate = GlobalValues.main.enemyEffectRate * (TechTreeManager.main.enemyEffectRate * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYEFFECTRATE]+ 1f); //Attributes
        enemyCarryCapacity = GlobalValues.main.enemyCarryCapacity * (TechTreeManager.main.enemyCarryCapacity * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYCARRYCAPACITY]+ 1f); //Attributes
        enemyMoveSpeed = GlobalValues.main.enemyMoveSpeed[(int)GlobalValues.main.difficulty] * (TechTreeManager.main.enemyMoveSpeed * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYMOVESPEED]+ 1f); //Attributes
        enemyArmor = GlobalValues.main.enemyArmor * (TechTreeManager.main.enemyArmor + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYARMOR]+ 1f); //Attributes
        enemyResistance = GlobalValues.main.enemyResistance * (TechTreeManager.main.enemyResistance + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYREISTANCE]+ 1f); //Attributes
        enemyDodge = GlobalValues.main.enemyDodge * (TechTreeManager.main.enemyDodge + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYDODGE]+ 1f); //Attributes
        enemyArmorPierce = GlobalValues.main.enemyArmorPierce * (TechTreeManager.main.enemyArmorPierce * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYARMORPIERCE]+ 1f); //Attributes
        enemyResistancePierce = GlobalValues.main.enemyResistancePierce * (TechTreeManager.main.enemyResistancePierce * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYRESISTANCEPIERCE]+ 1f); //Attributes
        enemyDodgePierce = GlobalValues.main.enemyDodgePierce * (TechTreeManager.main.enemyDodgePierce * HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYDODGEPIERCE]+ 1f); //Attributes
        enemyStealthDetection = GlobalValues.main.enemyStealthDetection * (TechTreeManager.main.enemyStealthDetection + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYSTEALTHDETECTION]+ 1f); //Attributes
        if (enemyStealthDetection > 1f)
        {
            enemyStealthDetection = 1f;
        }
        enemyHatchTime = GlobalValues.main.enemyHatchTime * (TechTreeManager.main.enemyHatchTime + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYHATCHTIME]+ 1f); //Effects
        enemySpawnTime = GlobalValues.main.enemySpawnTime * (TechTreeManager.main.enemySpawnTime + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYSPAWNTIME]+ 1f); //Effects
        enemyStealthTime = GlobalValues.main.enemyStealthTime * (TechTreeManager.main.enemyStealthTime + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYSTEALTHTIME]+ 1f); //Effects
        Attributes[] allAttributes = FindObjectsByType<Attributes>(FindObjectsSortMode.None);
        foreach (Attributes attributes in allAttributes)
        {
            attributes.SetBuffedAttributes();
        }
    }
}
