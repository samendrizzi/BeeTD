using System;
using UnityEngine;

public class PollenManager : MonoBehaviour
{

    public static PollenManager main;

    //Environment
    public float honeyInterest = 0f;
    public float nectarGenerationRate = 1f;
    public float honeycombGeneration = 1f;
    //Worker Bees + Towers
    public float slowPower = 1f;
    public float freezePower = 1f;
    public float damage = 1f;
    public float attackRate = 1f;
    public float buffPower = 1f;
    public float armorPierce = 1f;
    public float resistancePierce = 1f;
    public float dodgePierce = 1f;
    public float stealthDetection = 0f;
    //Worker Bees
    public float beeCarryCapacity = 1f;
    public float beeMoveSpeed = 1f;
    //Towers
    public float towerTargetingRange = 1f;
    public float towerAoEArea = 1f;
    public float towerAoEDamageDropOff = 1f;
    public float towerExtraRampCount = 1f;
    public float towerExtraRicochetCount = 1f;

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
                    beeCarryCapacity = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.ORANGE][flowerLevel[i] - 1];
                    beeMoveSpeed = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.ORANGE][flowerLevel[i] - 1]; 
                }
                else if (flowersAvailable[i] == FlowerType.BROWN)
                {
                    //Brown Buff
                    armorPierce = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.BROWN][flowerLevel[i] - 1];
                    resistancePierce = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.BROWN][flowerLevel[i] - 1]; 
                }
                else if (flowersAvailable[i] == FlowerType.GREEN)
                {
                    //Green Buff
                    nectarGenerationRate = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.GREEN][flowerLevel[i] - 1];
                }
                else if (flowersAvailable[i] == FlowerType.BLUE)
                {
                    //Blue Buff
                    slowPower = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.BLUE][flowerLevel[i] - 1];
                    freezePower = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.BLUE][flowerLevel[i] - 1]; 
                }
                else if (flowersAvailable[i] == FlowerType.YELLOW)
                {
                    //Yellow Buff
                    honeycombGeneration = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.YELLOW][flowerLevel[i] - 1];
                }
                else if (flowersAvailable[i] == FlowerType.RED)
                {
                    //Red Buff
                    damage = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.RED][flowerLevel[i] - 1];
                    attackRate = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.RED][flowerLevel[i] - 1]; 
                }
                else if (flowersAvailable[i] == FlowerType.BLACK)
                {
                    //Black Buff
                    stealthDetection = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.BLACK][flowerLevel[i] - 1];
                    dodgePierce = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.BLACK][flowerLevel[i] - 1]; 
                }
                else if (flowersAvailable[i] == FlowerType.WHITE)
                {
                    //White Buff
                    towerTargetingRange = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.WHITE][flowerLevel[i] - 1];
                }
                else if (flowersAvailable[i] == FlowerType.PURPLE)
                {
                    //Purple Buff
                    towerAoEArea = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.PURPLE][flowerLevel[i] - 1];
                    towerAoEDamageDropOff = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.PURPLE][flowerLevel[i] - 1]; 
                    towerExtraRampCount = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.PURPLE][flowerLevel[i] - 1]; 
                    towerExtraRicochetCount = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.PURPLE][flowerLevel[i] - 1];
                }
                else if (flowersAvailable[i] == FlowerType.PINK)
                {
                    //Pink Buff
                    buffPower = 1f + GlobalValues.main.FLOWERBuffModifier[(int)FlowerType.PINK][flowerLevel[i] - 1];
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
