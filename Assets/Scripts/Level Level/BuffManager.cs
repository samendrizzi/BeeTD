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
    public float honeyInterest = 0f;
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
        
    }

    public void RefreshBuffs()
    {
        //Environment
        startingNectar = GlobalValues.main.startingNectar[(int)GlobalValues.main.difficulty] + (TechTreeManager.main.startingNectar); //LevelManager
        startingHoney = GlobalValues.main.startingHoney[(int)GlobalValues.main.difficulty] + (TechTreeManager.main.startingHoney); //LevelManager
        queenBeeHitPoints = GlobalValues.main.queenBeeHitPoints * (TechTreeManager.main.queenBeeHitPoints); //LevelManager
        queenBeeReturnDamage = GlobalValues.main.queenBeeReturnDamage + (TechTreeManager.main.queenBeeReturnDamage + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.QUEENTHORNS]); //Actions
        queenBeeArmor = GlobalValues.main.queenBeeArmor * (TechTreeManager.main.queenBeeArmor + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.QUEENARMOR]); //LevelManager
        honeyDropRate = GlobalValues.main.honeyDropRate[(int)GlobalValues.main.difficulty] + (TechTreeManager.main.honeyDropRate + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.HONEYDROPRATE]); //Attributes
        if (honeyDropRate > 1f)
        {
            honeyDropRate = 1f;
        }
        honeyInterest = GlobalValues.main.honeyInterest + (TechTreeManager.main.honeyInterest + PollenManager.main.honeyInterest + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.HONEYINTEREST]); //LevelManager
        pollenGatherRatio = GlobalValues.main.pollenGatherRatio * (TechTreeManager.main.pollenGather + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.POLLENGATHERRATE]); //PollenManager
        pollenBuff = GlobalValues.main.pollenBuff + (TechTreeManager.main.pollenBuff + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.POLLENBUFF]); //Not Implemented
        nectarGenerationRate = GlobalValues.main.nectarGenerationRate * (TechTreeManager.main.nectarGenerationRate + PollenManager.main.nectarGenerationRate + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.NECTARGENERATIONRATE]); //Plot
        honeycombTicks = GlobalValues.main.honeycombTicks + (TechTreeManager.main.honeycombTicks + (int)HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.HONEYCOMBTICKS]); //Plot
        honeycombGeneration = GlobalValues.main.honeycombGeneration * (TechTreeManager.main.honeycombGeneration + PollenManager.main.honeycombGeneration + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.HONEYCOMBGENERATIONRATE]); //Plot
        honeycombFillTime = GlobalValues.main.honeycombFillTime * (TechTreeManager.main.honeycombFillTime + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.HONEYCOMBFILLTIME]); //Actions
        //Worker Bees + Towers
        slowPower = GlobalValues.main.slowPower * (TechTreeManager.main.slowPower + PollenManager.main.slowPower + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.SLOWPOWER]); //Attributes
        slowPierce = GlobalValues.main.slowPierce * (TechTreeManager.main.slowPierce + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.SLOWPIERCE]); //Attributes
        freezePower = GlobalValues.main.freezePower * (TechTreeManager.main.freezePower + PollenManager.main.freezePower + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.FREEZEPOWER]); //Attributes
        freezePierce = GlobalValues.main.freezePierce * (TechTreeManager.main.freezePierce + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.FREEZEPIERCE]); //Attributes
        //Worker Bees
        startingBees = GlobalValues.main.startingBees[(int)GlobalValues.main.difficulty] + (TechTreeManager.main.startingBees); //LevelManager
        beeCost = GlobalValues.main.beeCost * (TechTreeManager.main.beeCost); //LevelManager
        beeCostScaling = GlobalValues.main.beeCostScaling * (TechTreeManager.main.beeCostScaling); //LevelManager
        beeHitPoints = GlobalValues.main.beeHitPoints * (TechTreeManager.main.beeHitPoints); //Attributes
        beeHeal = GlobalValues.main.beeHeal * (TechTreeManager.main.beeHeal + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEHEAL]); //Attributes
        beeShield = GlobalValues.main.beeShield * (TechTreeManager.main.beeShield + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEESHIELD]); //Attributes
        beeShieldGeneration = GlobalValues.main.beeShieldGeneration * (TechTreeManager.main.beeShieldGeneration + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEESHIELDGENERATION]); //Attributes
        beeTargetingRange = GlobalValues.main.beeTargetingRange * (TechTreeManager.main.beeTargetingRange + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEETARGETINGRANGE]); //Attributes
        beeDamage = GlobalValues.main.beeDamage * (TechTreeManager.main.beeDamage + PollenManager.main.damage + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEDAMAGE]); //Attributes
        beeAttackRate = GlobalValues.main.beeAttackRate * (TechTreeManager.main.beeAttackRate + PollenManager.main.attackRate + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEATTACKRATE]); //Attributes
        beeActionPower = GlobalValues.main.beeActionPower * (TechTreeManager.main.beeActionPower + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEACTIONPOWER]); //Attributes
        beeActionRate = GlobalValues.main.beeActionRate * (TechTreeManager.main.beeActionRate + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEACTIONRATE]); //Attributes
        beeEffectPower = GlobalValues.main.beeEffectPower * (TechTreeManager.main.beeEffectPower + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEEFFECTPOWER]); //Attributes
        beeEffectRate = GlobalValues.main.beeEffectRate * (TechTreeManager.main.beeEffectRate + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEEFFECTRATE]); //Attributes
        beeCarryCapacity = GlobalValues.main.beeCarryCapacity * (TechTreeManager.main.beeCarryCapacity + PollenManager.main.beeCarryCapacity + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEECARRYCAPACITY]); //Attributes
        beeMoveSpeed = GlobalValues.main.beeMoveSpeed * (TechTreeManager.main.beeMoveSpeed + PollenManager.main.beeMoveSpeed + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEMOVESPEED]); //Attributes
        beeArmor = GlobalValues.main.beeArmor * (TechTreeManager.main.beeArmor + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEARMOR]); //Attributes
        beeResistance = GlobalValues.main.beeResistance * (TechTreeManager.main.beeResistance + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEREISTANCE]); //Attributes
        beeDodge = GlobalValues.main.beeDodge * (TechTreeManager.main.beeDodge + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEDODGE]); //Attributes
        beeArmorPierce = GlobalValues.main.beeArmorPierce * (TechTreeManager.main.beeArmorPierce + PollenManager.main.armorPierce + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEARMORPIERCE]); //Attributes
        beeResistancePierce = GlobalValues.main.beeResistancePierce * (TechTreeManager.main.beeResistancePierce + PollenManager.main.resistancePierce + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEERESISTANCEPIERCE]); //Attributes
        beeDodgePierce = GlobalValues.main.beeDodgePierce * (TechTreeManager.main.beeDodgePierce + PollenManager.main.dodgePierce + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEEDODGEPIERCE]); //Attributes
        beeStealthDetection = GlobalValues.main.beeStealthDetection * (TechTreeManager.main.beeStealthDetection + PollenManager.main.stealthDetection + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.BEESTEALTHDETECTION]); //Attributes
        if (beeStealthDetection > 1f)
        {
            beeStealthDetection = 1f;
        }
        //Towers
        towerSell = GlobalValues.main.towerSell * (TechTreeManager.main.towerSell); //StructureUIHandler
        if (towerSell > 1f)
        {
            towerSell = 1f;
        }
        towerHitPoints = GlobalValues.main.towerHitPoints * (TechTreeManager.main.towerHitPoints); //Attributes
        towerHeal = GlobalValues.main.towerHeal * (TechTreeManager.main.towerHeal + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERHEAL]); //Attributes
        towerShield = GlobalValues.main.towerShield * (TechTreeManager.main.towerShield + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERSHIELD]); //Attributes
        towerShieldGeneration = GlobalValues.main.towerShieldGeneration * (TechTreeManager.main.towerShieldGeneration + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERSHIELDGENERATION]); //Attributes
        towerTargetingRange = GlobalValues.main.towerTargetingRange * (TechTreeManager.main.towerTargetingRange + PollenManager.main.towerTargetingRange + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERTARGETINGRANGE]); //Attributes
        towerDamage = GlobalValues.main.towerDamage * (TechTreeManager.main.towerDamage + PollenManager.main.damage + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERDAMAGE]); //Attributes
        towerAttackRate = GlobalValues.main.towerAttackRate * (TechTreeManager.main.towerAttackRate + PollenManager.main.attackRate + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERATTACKRATE]); //Attributes
        towerActionPower = GlobalValues.main.towerActionPower * (TechTreeManager.main.towerActionPower + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERACTIONPOWER]); //Attributes
        towerActionRate = GlobalValues.main.towerActionRate * (TechTreeManager.main.towerActionRate + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERACTIONRATE]); //Attributes
        towerBuffPower = GlobalValues.main.towerBuffPower * (TechTreeManager.main.towerBuffPower + PollenManager.main.buffPower + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERBUFFPOWER]); //Effects
        towerEffectPower = GlobalValues.main.towerEffectPower * (TechTreeManager.main.towerEffectPower + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWEREFFECTPOWER]); //Attributes
        towerEffectRate = GlobalValues.main.towerEffectRate * (TechTreeManager.main.towerEffectRate + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWEREFFECTRATE]); //Attributes
        towerCarryCapacity = GlobalValues.main.towerCarryCapacity * (TechTreeManager.main.towerCarryCapacity + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERCARRYCAPACITY]); //Attributes
        towerMoveSpeed = GlobalValues.main.towerMoveSpeed * (TechTreeManager.main.towerMoveSpeed + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERMOVESPEED]); //Attributes
        towerArmor = GlobalValues.main.towerArmor * (TechTreeManager.main.towerArmor + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERARMOR]); //Attributes
        towerResistance = GlobalValues.main.towerResistance * (TechTreeManager.main.towerResistance + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERREISTANCE]); //Attributes
        towerDodge = GlobalValues.main.towerDodge * (TechTreeManager.main.towerDodge + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERDODGE]); //Attributes
        towerArmorPierce = GlobalValues.main.towerArmorPierce * (TechTreeManager.main.towerArmorPierce + PollenManager.main.armorPierce + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERARMORPIERCE]); //Attributes
        towerResistancePierce = GlobalValues.main.towerResistancePierce * (TechTreeManager.main.towerResistancePierce + PollenManager.main.resistancePierce + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERRESISTANCEPIERCE]); //Attributes
        towerDodgePierce = GlobalValues.main.towerDodgePierce * (TechTreeManager.main.towerDodgePierce + PollenManager.main.dodgePierce + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERDODGEPIERCE]); //Attributes
        towerStealthDetection = GlobalValues.main.towerStealthDetection * (TechTreeManager.main.towerStealthDetection + PollenManager.main.stealthDetection + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERSTEALTHDETECTION]); //Attributes
        if (towerStealthDetection > 1f)
        {
            towerStealthDetection = 1f;
        }
        towerAoEArea = GlobalValues.main.towerAoEArea * (TechTreeManager.main.towerAoEArea + PollenManager.main.towerAoEArea + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERAOEAREA]); //Projectile
        towerAoEDamageDropOff = GlobalValues.main.towerAoEDamageDropOff * (TechTreeManager.main.towerAoEDamageDropOff + PollenManager.main.towerAoEDamageDropOff + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERAOEDAMAGEDROPOFF]); //Projectile
        if (towerAoEDamageDropOff > 1f)
        {
            towerAoEDamageDropOff = 1f;
        }
        towerRampingDamage = GlobalValues.main.towerRampingDamage * (TechTreeManager.main.towerRampingDamage + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWERRAMPINGDAMAGE]); //Actions
        towerExtraRampCount = (int)(GlobalValues.main.towerExtraRampCount * (TechTreeManager.main.towerExtraRampCount + PollenManager.main.towerExtraRampCount + (int)HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWEREXTRARAMPCOUNT])); //Actions
        towerExtraRicochetCount = (int)(GlobalValues.main.towerExtraRicochetCount * (TechTreeManager.main.towerExtraRicochetCount + PollenManager.main.towerExtraRicochetCount + (int)HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.TOWEREXTRARICOCHETCOUNT])); //Projectile
        //Enemies
        enemyHitPoints = GlobalValues.main.enemyHitPoints[(int)GlobalValues.main.difficulty] * (TechTreeManager.main.enemyHitPoints); //Attributes
        enemyHeal = GlobalValues.main.enemyHeal * (TechTreeManager.main.enemyHeal + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYHEAL]); //Attributes
        enemyShield = GlobalValues.main.enemyShield * (TechTreeManager.main.enemyShield + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYSHIELD]); //Attributes
        enemyShieldGeneration = GlobalValues.main.enemyShieldGeneration * (TechTreeManager.main.enemyShieldGeneration + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYSHIELDGENERATION]); //Attributes
        enemyTargetingRange = GlobalValues.main.enemyTargetingRange * (TechTreeManager.main.enemyTargetingRange + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYTARGETINGRANGE]); //Attributes
        enemyDamage = GlobalValues.main.enemyDamage * (TechTreeManager.main.enemyDamage + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYDAMAGE]); //Attributes
        enemyAttackRate = GlobalValues.main.enemyAttackRate * (TechTreeManager.main.enemyAttackRate + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYATTACKRATE]); //Attributes
        enemyActionPower = GlobalValues.main.enemyActionPower * (TechTreeManager.main.enemyActionPower + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYACTIONPOWER]); //Attributes
        enemyActionRate = GlobalValues.main.enemyActionRate * (TechTreeManager.main.enemyActionRate + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYACTIONRATE]); //Attributes
        enemySlowPower = GlobalValues.main.enemySlowPower * (TechTreeManager.main.enemySlowPower); //Attributes
        enemySlowPierce = GlobalValues.main.enemySlowPierce * (TechTreeManager.main.enemySlowPierce); //Attributes
        enemyFreezePower = GlobalValues.main.enemyFreezePower * (TechTreeManager.main.enemyFreezePower); //Attributes
        enemyFreezePierce = GlobalValues.main.enemyFreezePierce * (TechTreeManager.main.enemyFreezePierce); //Attributes
        enemyEffectPower = GlobalValues.main.enemyEffectPower * (TechTreeManager.main.enemyEffectPower + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYEFFECTPOWER]); //Attributes
        enemyEffectRate = GlobalValues.main.enemyEffectRate * (TechTreeManager.main.enemyEffectRate + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYEFFECTRATE]); //Attributes
        enemyCarryCapacity = GlobalValues.main.enemyCarryCapacity * (TechTreeManager.main.enemyCarryCapacity + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYCARRYCAPACITY]); //Attributes
        enemyMoveSpeed = GlobalValues.main.enemyMoveSpeed[(int)GlobalValues.main.difficulty] * (TechTreeManager.main.enemyMoveSpeed + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYMOVESPEED]); //Attributes
        enemyArmor = GlobalValues.main.enemyArmor * (TechTreeManager.main.enemyArmor + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYARMOR]); //Attributes
        enemyResistance = GlobalValues.main.enemyResistance * (TechTreeManager.main.enemyResistance + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYREISTANCE]); //Attributes
        enemyDodge = GlobalValues.main.enemyDodge * (TechTreeManager.main.enemyDodge + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYDODGE]); //Attributes
        enemyArmorPierce = GlobalValues.main.enemyArmorPierce * (TechTreeManager.main.enemyArmorPierce + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYARMORPIERCE]); //Attributes
        enemyResistancePierce = GlobalValues.main.enemyResistancePierce * (TechTreeManager.main.enemyResistancePierce + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYRESISTANCEPIERCE]); //Attributes
        enemyDodgePierce = GlobalValues.main.enemyDodgePierce * (TechTreeManager.main.enemyDodgePierce + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYDODGEPIERCE]); //Attributes
        enemyStealthDetection = GlobalValues.main.enemyStealthDetection * (TechTreeManager.main.enemyStealthDetection + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYSTEALTHDETECTION]); //Attributes
        if (enemyStealthDetection > 1f)
        {
            enemyStealthDetection = 1f;
        }
        enemyHatchTime = GlobalValues.main.enemyHatchTime * (TechTreeManager.main.enemyHatchTime + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYHATCHTIME]); //Effects
        enemySpawnTime = GlobalValues.main.enemySpawnTime * (TechTreeManager.main.enemySpawnTime + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYSPAWNTIME]); //Effects
        enemyStealthTime = GlobalValues.main.enemyStealthTime * (TechTreeManager.main.enemyStealthTime + HoneyBuffManager.main.honeyBuffs[(int)HoneyBuff.ENEMYSTEALTHTIME]); //Effects
        Attributes[] allAttributes = FindObjectsByType<Attributes>(FindObjectsSortMode.None);
        foreach (Attributes attributes in allAttributes)
        {
            attributes.SetBuffedAttributes();
        }
    }
}
