using System;
using UnityEngine;

public class PollenManager : MonoBehaviour
{

    public static PollenManager main;

    //Environment
    public float honeyInterest = 0f;
    public float nectarGenerationRate = 0f;
    public float honeycombGeneration = 0f;
    //Worker Bees + Towers
    public float slowPower = 0f;
    public float freezePower = 0f;
    public float damage = 0f;
    public float attackRate = 0f;
    public float buffPower = 0f;
    public float armorPierce = 0f;
    public float resistancePierce = 0f;
    public float dodgePierce = 0f;
    public float stealthDetection = 0f;
    //Worker Bees
    public float beeCarryCapacity = 0f;
    public float beeMoveSpeed = 0f;
    //Towers
    public float towerTargetingRange = 0f;
    public float towerAoEArea = 0f;
    public float towerAoEDamageDropOff = 0f;
    public float towerExtraRampCount = 0f;
    public float towerExtraRicochetCount = 0f;

    public FlowerType[] flowersAvailable;
    public int[] flowerLevel;
    public float[] flowerPollen;

    void Awake()
    {
        main = this;
    }

    public void AddFlower(GameObject flower)
    {
        FlowerType type = flower.GetComponent<Plot>().flowerType;
        if (FindFlowerIndex(type) == -1)
        {
            Array.Resize(ref flowersAvailable, flowersAvailable.Length + 1);
            Array.Resize(ref flowerLevel, flowersAvailable.Length);
            Array.Resize(ref flowerPollen, flowersAvailable.Length);
            flowersAvailable[flowersAvailable.Length - 1] = type;
            flowerLevel[flowersAvailable.Length - 1] = 0;
            flowerPollen[flowersAvailable.Length - 1] = 0f;
            CreateUI(type);
        }
    }

    public void AddPollen(float amount, FlowerType type)
    {
        int index = FindFlowerIndex(type);
        flowerPollen[index] += amount * BuffManager.main.pollenGatherRatio;
        CheckBuffLevels(type, index);
    }

    public void CheckBuffLevels(FlowerType type, int index)
    {
        int level = 0;
        if (flowerPollen[index] >= GlobalValues.main.FLOWERPollenThreshold[(int)type][level])
        {
            level++;
            if (flowerPollen[index] >= GlobalValues.main.FLOWERPollenThreshold[(int)type][level])
            {
                level++;
                if (flowerPollen[index] >= GlobalValues.main.FLOWERPollenThreshold[(int)type][level])
                {
                    level++;
                    if (flowerPollen[index] >= GlobalValues.main.FLOWERPollenThreshold[(int)type][level])
                    {
                        level++;
                        if (flowerPollen[index] >= GlobalValues.main.FLOWERPollenThreshold[(int)type][level])
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
        }
        RefreshUI(type);
    }

    private int FindFlowerIndex(FlowerType type)
    {
        return Array.IndexOf(flowersAvailable, type);
    }

    private void RefreshBuffs()
    {
        for (int i = 0; i < flowersAvailable.Length; i++)
        {
            if (flowerLevel[i] > 0)
            {
                if (flowersAvailable[i] == FlowerType.ORANGE)
                {
                    //Orange Buff
                    beeCarryCapacity = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.ORANGE][flowerLevel[i] - 1];
                    beeMoveSpeed = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.ORANGE][flowerLevel[i] - 1]; 
                }
                else if (flowersAvailable[i] == FlowerType.BROWN)
                {
                    //Brown Buff
                    armorPierce = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.BROWN][flowerLevel[i] - 1];
                    resistancePierce = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.BROWN][flowerLevel[i] - 1]; 
                }
                else if (flowersAvailable[i] == FlowerType.GREEN)
                {
                    //Green Buff
                    nectarGenerationRate = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.GREEN][flowerLevel[i] - 1];
                }
                else if (flowersAvailable[i] == FlowerType.BLUE)
                {
                    //Blue Buff
                    slowPower = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.BLUE][flowerLevel[i] - 1];
                    freezePower = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.BLUE][flowerLevel[i] - 1]; 
                }
                else if (flowersAvailable[i] == FlowerType.YELLOW)
                {
                    //Yellow Buff
                    honeycombGeneration = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.YELLOW][flowerLevel[i] - 1];
                }
                else if (flowersAvailable[i] == FlowerType.RED)
                {
                    //Red Buff
                    damage = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.RED][flowerLevel[i] - 1];
                    attackRate = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.RED][flowerLevel[i] - 1]; 
                }
                else if (flowersAvailable[i] == FlowerType.BLACK)
                {
                    //Black Buff
                    stealthDetection = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.BLACK][flowerLevel[i] - 1];
                    dodgePierce = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.BLACK][flowerLevel[i] - 1]; 
                }
                else if (flowersAvailable[i] == FlowerType.WHITE)
                {
                    //White Buff
                    towerTargetingRange = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.WHITE][flowerLevel[i] - 1];
                }
                else if (flowersAvailable[i] == FlowerType.PURPLE)
                {
                    //Purple Buff
                    towerAoEArea = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.PURPLE][flowerLevel[i] - 1];
                    towerAoEDamageDropOff = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.PURPLE][flowerLevel[i] - 1]; 
                    towerExtraRampCount = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.PURPLE][flowerLevel[i] - 1]; 
                    towerExtraRicochetCount = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.PURPLE][flowerLevel[i] - 1];
                }
                else if (flowersAvailable[i] == FlowerType.PINK)
                {
                    //Pink Buff
                    buffPower = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.PINK][flowerLevel[i] - 1];
                }
                else if (flowersAvailable[i] == FlowerType.GOLD)
                {
                    //Gold Buff
                    honeyInterest = GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.GOLD][flowerLevel[i] - 1];
                }
            }            
        }       
        BuffManager.main.RefreshBuffs();
    }

    public void RefreshUI(FlowerType type)
    {
        UIManager.main.RefreshPollenPanel(type);
    }

    public void CreateUI(FlowerType type)
    {
        UIManager.main.CreatePollenPanel(type);
    }
}
