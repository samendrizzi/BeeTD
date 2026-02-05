using UnityEngine;

public class TechTreeManager : MonoBehaviour
{
    public static TechTreeManager main;

    //Environment
    public float startingNectar = 0f;
    public float startingHoney = 0f;
    public float queenBeeHitPoints = 1f;
    public float queenBeeReturnDamage = 0f;
    public float queenBeeArmor = 0f;
    public float honeyDropRate = 0f;
    public float honeyinterest = 0f;
    public float pollenBuff = 0f;
    public float nectarGenerationRate = 1f;
    public float pollenGenerationRate = 1f;
    public int honeycombTicks = 0;
    public float honeycombGeneration = 1f;
    public float honeycombFillTime = 1f;
    //Worker Bees + Towers
    public float slowPower = 1f;
    public float slowPierce = 1f;
    public float freezePower = 1f;
    public float freezePierce = 1f;
    //Worker Bees
    public int startingBees = 0;
    public float beeCost = 1f;
    public float beeCostScaling = 1f;
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
    public float beeArmor = 0f;
    public float beeResistance = 0f;
    public float beeDodge = 0f;
    public float beeArmorPierce = 1f;
    public float beeResistancePierce = 1f;
    public float beeDodgePierce = 1f;
    public float beeStealthDetection = 0f;
    //Towers
    public float towerSell = 0f;
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
    public float towerArmor = 0f;
    public float towerResistance = 0f;
    public float towerDodge = 0f;
    public float towerArmorPierce = 1f;
    public float towerResistancePierce = 1f;
    public float towerDodgePierce = 1f;
    public float towerStealthDetection = 0f;
    public float towerAoEArea = 1f;
    public float towerAoEDamageDropOff = 1f;
    public float towerRampingDamage = 1f;
    public int towerExtraRampCount = 0;
    public int towerExtraRicochetCount = 0;
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
    public float enemyArmor = 0f;
    public float enemyResistance = 0f;
    public float enemyDodge = 0f;
    public float enemyArmorPierce = 1f;
    public float enemyResistancePierce = 1f;
    public float enemyDodgePierce = 1f;
    public float enemyStealthDetection = 0f;
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

}
