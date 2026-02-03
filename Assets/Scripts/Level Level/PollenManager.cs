using System;
using UnityEngine;

public class PollenManager : MonoBehaviour
{

    public static PollenManager main;

    //Environment
    public float queenBeeReturnDamage;
    public float queenBeeArmor;
    public float honeyDropRate;
    public float honeyinterest;
    public float nectarGenerationRate;
    public float honeycombGeneration;
    //Worker Bees + Towers
    public float slowPower;
    public float freezePower;
    //Worker Bees
    public float beeCarryCapacity;
    public float beeMoveSpeed;
    //Towers
    public float towerTargetingRange;
    public float towerDamage;
    public float towerAttackRate;
    public float towerBuffPower;
    public float towerCarryCapacity;
    public float towerArmorPierce;
    public float towerResistancePierce;
    public float towerDodgePierce;
    public float towerStealthDetection;
    public float towerAoEArea;
    public float towerAoEDamageDropOff;
    public int towerExtraRampCount;
    public int towerExtraRicochetCount;

    public FlowerType[] flowersAvailable;
    public int[] flowerLevel;
    public float[] flowerPollen;

    private void Start()
    {
        foreach (GameObject flower in LevelManager.main.flowers)
        {
            AddFlower(flower);
        }
    }

    private void AddFlower(GameObject flower)
    {
        FlowerType type = flower.GetComponent<Plot>().flowerType;
        if (FindFlowerIndex(type) == -1)
        {
            Array.Resize(ref flowersAvailable, flowersAvailable.Length + 1);
            Array.Resize(ref flowerLevel, flowersAvailable.Length);
            Array.Resize(ref flowerPollen, flowersAvailable.Length);
            flowersAvailable[flowersAvailable.Length - 1] = type;
            flowerLevel[flowersAvailable.Length - 1] = -1;
            flowerPollen[flowersAvailable.Length - 1] = 0f;
        }
    }

    public void AddPollen(float amount, FlowerType type)
    {
        int index = FindFLowerIndex(type);
        flowerPollen[index] += amount;
        int level = 0;
        if (flowerPollen[index] >= GlobalValues.main.flowerPollenThresholds[(int)type][level + 1])
        {
            level++;
            if (flowerPollen[index] >= GlobalValues.main.flowerPollenThresholds[(int)type][level + 1])
            {
                level++;
                if (flowerPollen[index] >= GlobalValues.main.flowerPollenThresholds[(int)type][level + 1])
                {
                    level++;
                    if (flowerPollen[index] >= GlobalValues.main.flowerPollenThresholds[(int)type][level + 1])
                    {
                        level++;
                        if (flowerPollen[index] >= GlobalValues.main.flowerPollenThresholds[(int)type][level + 1])
                        {
                            level++;
                        }
                    }
                }
            }
        }
        flowerLevel[index] = level;
        RefreshBuffs();
    }

    private int FindFlowerIndex(FlowerType type)
    {
        return Array.IndexOf(flowersAvailable, type);
    }

    private void RefreshBuffs()
    {
        
    }
}
