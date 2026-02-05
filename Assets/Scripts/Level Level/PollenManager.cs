using System;
using UnityEngine;

public class PollenManager : MonoBehaviour
{

    public static PollenManager main;

    //Environment
    public float honeyInterest;
    public float nectarGenerationRate;
    public float honeycombGeneration;
    //Worker Bees + Towers
    public float slowPower;
    public float freezePower;
    public float damage;
    public float attackRate;
    public float buffPower;
    public float armorPierce;
    public float resistancePierce;
    public float dodgePierce;
    public float stealthDetection;
    //Worker Bees
    public float beeCarryCapacity;
    public float beeMoveSpeed;
    //Towers
    public float towerTargetingRange;
    public float towerAoEArea;
    public float towerAoEDamageDropOff;
    public float towerExtraRampCount;
    public float towerExtraRicochetCount;

    public FlowerType[] flowersAvailable;
    public int[] flowerLevel;
    public float[] flowerPollen;

    void Awake()
    {
        main = this;
    }

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
        int index = FindFlowerIndex(type);
        flowerPollen[index] += amount;
        CheckBuffLevels(type, index);
    }

    public void CheckBuffLevels(FlowerType type, int index)
    {
        int level = 0;
        if (flowerPollen[index] >= GlobalValues.main.FLOWERPollenThreshold[(int)type][level + 1])
        {
            level++;
            if (flowerPollen[index] >= GlobalValues.main.FLOWERPollenThreshold[(int)type][level + 1])
            {
                level++;
                if (flowerPollen[index] >= GlobalValues.main.FLOWERPollenThreshold[(int)type][level + 1])
                {
                    level++;
                    if (flowerPollen[index] >= GlobalValues.main.FLOWERPollenThreshold[(int)type][level + 1])
                    {
                        level++;
                        if (flowerPollen[index] >= GlobalValues.main.FLOWERPollenThreshold[(int)type][level + 1])
                        {
                            level++;
                        }
                    }
                }
            }
        }
        if (flowerLevel[index] != level)
        {
            flowerLevel[index] = level;
            RefreshBuffs();
            CreateUI();
        }
        RefreshUI();
    }

    private int FindFlowerIndex(FlowerType type)
    {
        return Array.IndexOf(flowersAvailable, type);
    }

    private void RefreshBuffs()
    {
        int index = 1;
        //Orange Buff
        beeCarryCapacity = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]];
        beeMoveSpeed = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]]; 
        index++;
        //Brown Buff
        armorPierce = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]];
        resistancePierce = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]]; 
        index++;
        //Green Buff
        nectarGenerationRate = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]];
        index++;
        //Blue Buff
        slowPower = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]];
        freezePower = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]]; 
        index++;
        //Yellow Buff
        honeycombGeneration = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]];
        index++;
        //Red Buff
        damage = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]];
        attackRate = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]]; 
        index++;
        //Black Buff
        stealthDetection = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]];
        dodgePierce = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]]; 
        index++;
        //White Buff
        towerTargetingRange = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]];
        index++;
        //Purple Buff
        towerAoEArea = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]];
        towerAoEDamageDropOff = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]]; 
        towerExtraRampCount = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]]; 
        towerExtraRicochetCount = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]]; 
        index++;
        //Pink Buff
        buffPower = 1f + GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]];
        index++;
        //Gold Buff
        honeyInterest = GlobalValues.main.FLOWERBuffModifier[index][flowerLevel[index]];
    }

    public void RefreshUI()
    {
        UIManager.main.RefreshPollenPanel();
    }

    public void CreateUI()
    {
        UIManager.main.CreatePollenPanel();
    }
}
