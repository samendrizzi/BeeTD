using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Prestige : MonoBehaviour
{
    //trackers
    private Attributes attributes;

    private void Awake()
    {
        attributes = gameObject.GetComponent<Attributes>();
    }
    
    private void Start()
    {
        SetPrestigeStats();
    }

    public void SetPrestigeStats()
    {
        if (attributes.prestige != null)
        {
            SetPrestigeRed(true, Regex.Matches(attributes.prestige, "Red").Count);
            SetPrestigeBlue(true, Regex.Matches(attributes.prestige, "Blue").Count);
            SetPrestigeGreen(true, Regex.Matches(attributes.prestige, "Green").Count);
            SetPrestigeYellow(true, Regex.Matches(attributes.prestige, "Yellow").Count);
            SetPrestigeWhite(true, Regex.Matches(attributes.prestige, "White").Count);
            SetPrestigePurple(true, Regex.Matches(attributes.prestige, "Purple").Count);
            SetPrestigeBlack(true, Regex.Matches(attributes.prestige, "Black").Count);
            SetPrestigeGold(true, Regex.Matches(attributes.prestige, "Gold").Count);
            SetPrestigeBrown(true, Regex.Matches(attributes.prestige, "Brown").Count);
            SetPrestigeGrey(true, Regex.Matches(attributes.prestige, "Grey").Count);
            SetPrestigePrismatic(true, Regex.Matches(attributes.prestige, "Prismatic").Count);
            SetPrestigeSilver(true, Regex.Matches(attributes.prestige, "Silver").Count);
            SetPrestigePlatinum(true, Regex.Matches(attributes.prestige, "Platinum").Count);
            SetPrestigeTeal(true, Regex.Matches(attributes.prestige, "Teal").Count);
        }
    }

    public void AddAction(string action, float powerMod, float rateMod, float rangeMod, float pierceMod, float duration, float extraMod)
    {
        int index = attributes.actions.Length;
        Array.Resize(ref attributes.actions, index + 1);
        Array.Resize(ref attributes.actionPowerModifiers, index + 1);
        Array.Resize(ref attributes.actionRateModifiers, index + 1);
        Array.Resize(ref attributes.actionRangeModifiers, index + 1);
        Array.Resize(ref attributes.actionPierceModifiers, index + 1);
        Array.Resize(ref attributes.actionDurations, index + 1);
        Array.Resize(ref attributes.actionExtraModifiers, index + 1);
        Array.Resize(ref attributes.actionPowerModifiersBase, index + 1);
        Array.Resize(ref attributes.actionRateModifiersBase, index + 1);
        Array.Resize(ref attributes.actionRangeModifiersBase, index + 1);
        Array.Resize(ref attributes.actionPierceModifiersBase, index + 1);
        Array.Resize(ref attributes.actionDurationsBase, index + 1);
        Array.Resize(ref attributes.actionExtraModifiersBase, index + 1);
        Array.Resize(ref attributes.timeUntilActions, index + 1);
        attributes.actions[index] = action;
        attributes.actionPowerModifiers[index] = powerMod;
        attributes.actionRateModifiers[index] = rateMod;
        attributes.actionRangeModifiers[index] = rangeMod;
        attributes.actionPierceModifiers[index] = pierceMod;
        attributes.actionDurations[index] = duration;
        attributes.actionExtraModifiers[index] = extraMod;
        attributes.actionPowerModifiersBase[index] = powerMod;
        attributes.actionRateModifiersBase[index] = rateMod;
        attributes.actionRangeModifiersBase[index] = rangeMod;
        attributes.actionPierceModifiersBase[index] = pierceMod;
        attributes.actionDurationsBase[index] = duration;
        attributes.actionExtraModifiersBase[index] = extraMod;
    }

    public void AddEffect(string effect, float powerMod, float rateMod, float rangeMod, float pierceMod, float duration, float extraMod)
    {
        int index = attributes.effects.Length;
        Array.Resize(ref attributes.effects, index + 1);
        Array.Resize(ref attributes.effectPowerModifiers, index + 1);
        Array.Resize(ref attributes.effectRateModifiers, index + 1);
        Array.Resize(ref attributes.effectRangeModifiers, index + 1);
        Array.Resize(ref attributes.effectPierceModifiers, index + 1);
        Array.Resize(ref attributes.effectDurations, index + 1);
        Array.Resize(ref attributes.effectExtraModifiers, index + 1);
        Array.Resize(ref attributes.effectPowerModifiersBase, index + 1);
        Array.Resize(ref attributes.effectRateModifiersBase, index + 1);
        Array.Resize(ref attributes.effectRangeModifiersBase, index + 1);
        Array.Resize(ref attributes.effectPierceModifiersBase, index + 1);
        Array.Resize(ref attributes.effectDurationsBase, index + 1);
        Array.Resize(ref attributes.effectExtraModifiersBase, index + 1);
        Array.Resize(ref attributes.timeUntilEffects, index + 1);
        attributes.effects[index] = effect;
        attributes.effectPowerModifiers[index] = powerMod;
        attributes.effectRateModifiers[index] = rateMod;
        attributes.effectRangeModifiers[index] = rangeMod;
        attributes.effectPierceModifiers[index] = pierceMod;
        attributes.effectDurations[index] = duration;
        attributes.effectExtraModifiers[index] = extraMod;
        attributes.effectPowerModifiersBase[index] = powerMod;
        attributes.effectRateModifiersBase[index] = rateMod;
        attributes.effectRangeModifiersBase[index] = rangeMod;
        attributes.effectPierceModifiersBase[index] = pierceMod;
        attributes.effectDurationsBase[index] = duration;
        attributes.effectExtraModifiersBase[index] = extraMod;
    }

    public void AddPassive(string passive, float powerMod, float rateMod, float rangeMod, float pierceMod, float duration, float extraMod)
    {
        int index = attributes.passives.Length;
        Array.Resize(ref attributes.passives, index + 1);
        Array.Resize(ref attributes.passivePowerModifiers, index + 1);
        Array.Resize(ref attributes.passiveRateModifiers, index + 1);
        Array.Resize(ref attributes.passiveRangeModifiers, index + 1);
        Array.Resize(ref attributes.passivePierceModifiers, index + 1);
        Array.Resize(ref attributes.passiveDurations, index + 1);
        Array.Resize(ref attributes.passiveExtraModifiers, index + 1);
        Array.Resize(ref attributes.passivePowerModifiersBase, index + 1);
        Array.Resize(ref attributes.passiveRateModifiersBase, index + 1);
        Array.Resize(ref attributes.passiveRangeModifiersBase, index + 1);
        Array.Resize(ref attributes.passivePierceModifiersBase, index + 1);
        Array.Resize(ref attributes.passiveDurationsBase, index + 1);
        Array.Resize(ref attributes.passiveExtraModifiersBase, index + 1);
        Array.Resize(ref attributes.timeUntilPassives, index + 1);
        attributes.passives[index] = passive;
        attributes.passivePowerModifiers[index] = powerMod;
        attributes.passiveRateModifiers[index] = rateMod;
        attributes.passiveRangeModifiers[index] = rangeMod;
        attributes.passivePierceModifiers[index] = pierceMod;
        attributes.passiveDurations[index] = duration;
        attributes.passiveExtraModifiers[index] = extraMod;
        attributes.passivePowerModifiersBase[index] = powerMod;
        attributes.passiveRateModifiersBase[index] = rateMod;
        attributes.passiveRangeModifiersBase[index] = rangeMod;
        attributes.passivePierceModifiersBase[index] = pierceMod;
        attributes.passiveDurationsBase[index] = duration;
        attributes.passiveExtraModifiersBase[index] = extraMod;
    }

    public void ModifyAction(string action, float powerMod, float rateMod, float rangeMod, float pierceMod, float duration, float extraMod)
    {
        int i = Array.IndexOf(attributes.actions, action);
        if (i > -1)
        {
            attributes.actionPowerModifiers[i] = attributes.actionPowerModifiers[i] + powerMod;
            attributes.actionRateModifiers[i] = attributes.actionRateModifiers[i] + rateMod;
            attributes.actionRangeModifiers[i] = attributes.actionRangeModifiers[i] + rangeMod;
            attributes.actionPierceModifiers[i] = attributes.actionPierceModifiers[i] + pierceMod;
            attributes.actionDurations[i] = attributes.actionDurations[i] + duration;
            attributes.actionExtraModifiers[i] = attributes.actionExtraModifiers[i] + extraMod;
            attributes.actionPowerModifiersBase[i] = attributes.actionPowerModifiersBase[i] + powerMod;
            attributes.actionRateModifiersBase[i] = attributes.actionRateModifiersBase[i] + rateMod;
            attributes.actionRangeModifiersBase[i] = attributes.actionRangeModifiersBase[i] + rangeMod;
            attributes.actionPierceModifiersBase[i] = attributes.actionPierceModifiersBase[i] + pierceMod;
            attributes.actionDurationsBase[i] = attributes.actionDurationsBase[i] + duration;
            attributes.actionExtraModifiersBase[i] = attributes.actionExtraModifiersBase[i] + extraMod;
        }
        else
        {
            Debug.Log("Modifying Action: Action " + action + " not found for " + attributes.sName);
        }
    }

    public void ModifyEffect(string effect, float powerMod, float rateMod, float rangeMod, float pierceMod, float duration, float extraMod)
    {
        int i = Array.IndexOf(attributes.effects, effect);
        if (i > -1)
        {
            attributes.effectPowerModifiers[i] = attributes.effectPowerModifiers[i] + powerMod;
            attributes.effectRateModifiers[i] = attributes.effectRateModifiers[i] + rateMod;
            attributes.effectRangeModifiers[i] = attributes.effectRangeModifiers[i] + rangeMod;
            attributes.effectPierceModifiers[i] = attributes.effectPierceModifiers[i] + pierceMod;
            attributes.effectDurations[i] = attributes.effectDurations[i] + duration;
            attributes.effectExtraModifiers[i] = attributes.effectExtraModifiers[i] + extraMod;
            attributes.effectPowerModifiersBase[i] = attributes.effectPowerModifiersBase[i] + powerMod;
            attributes.effectRateModifiersBase[i] = attributes.effectRateModifiersBase[i] + rateMod;
            attributes.effectRangeModifiersBase[i] = attributes.effectRangeModifiersBase[i] + rangeMod;
            attributes.effectPierceModifiersBase[i] = attributes.effectPierceModifiersBase[i] + pierceMod;
            attributes.effectDurationsBase[i] = attributes.effectDurationsBase[i] + duration;
            attributes.effectExtraModifiersBase[i] = attributes.effectExtraModifiersBase[i] + extraMod;
        }
        else
        {
            Debug.Log("Modifying Effect: Effect " + effect + " not found for " + attributes.sName);
        }
    }

    public void ModifyPassive(string passive, float powerMod, float rateMod, float rangeMod, float pierceMod, float duration, float extraMod)
    {
        int i = Array.IndexOf(attributes.passives, passive);
        if (i > -1)
        {
            attributes.passivePowerModifiers[i] = attributes.passivePowerModifiers[i] + powerMod;
            attributes.passiveRateModifiers[i] = attributes.passiveRateModifiers[i] + rateMod;
            attributes.passiveRangeModifiers[i] = attributes.passiveRangeModifiers[i] + rangeMod;
            attributes.passivePierceModifiers[i] = attributes.passivePierceModifiers[i] + pierceMod;
            attributes.passiveDurations[i] = attributes.passiveDurations[i] + duration;
            attributes.passiveExtraModifiers[i] = attributes.passiveExtraModifiers[i] + extraMod;
            attributes.passivePowerModifiersBase[i] = attributes.passivePowerModifiersBase[i] + powerMod;
            attributes.passiveRateModifiersBase[i] = attributes.passiveRateModifiersBase[i] + rateMod;
            attributes.passiveRangeModifiersBase[i] = attributes.passiveRangeModifiersBase[i] + rangeMod;
            attributes.passivePierceModifiersBase[i] = attributes.passivePierceModifiersBase[i] + pierceMod;
            attributes.passiveDurationsBase[i] = attributes.passiveDurationsBase[i] + duration;
            attributes.passiveExtraModifiersBase[i] = attributes.passiveExtraModifiersBase[i] + extraMod;
        }
        else
        {
            Debug.Log("Modifying Passive: Passive " + passive + " not found for " + attributes.sName);
        }
    }

    public bool CheckAction(string action)
    {
        int i = Array.IndexOf(attributes.actions, action);
        if (i > -1)
        {
            return true;
        }
        return false;
    }

    public bool CheckEffect(string effect)
    {
        int i = Array.IndexOf(attributes.effects, effect);
        if (i > -1)
        {
            return true;
        }
        return false;

    }

    public bool CheckPassive(string passive)
    {
        int i = Array.IndexOf(attributes.passives, passive);
        if (i > -1)
        {
            return true;
        }
        return false;
    }
    public int FindAction(string action)
    {
        int i = Array.IndexOf(attributes.actions, action);
        return i;
    }

    public int FindEffect(string effect)
    {
        int i = Array.IndexOf(attributes.effects, effect);
        return i;
    }

    public int FindPassive(string passive)
    {
        int i = Array.IndexOf(attributes.passives, passive);
        return i;
    }

    public void SetPrestigeRed(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        float hpIncrease = 1f;
        float armorIncrease = 0f;
        for (int i = 1; i <= count; i++)
        {
            hpIncrease += (GlobalValues.main.prestigeRed * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
            armorIncrease = armorIncrease + (GlobalValues.main.prestigeRed * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
        }
        if (add == true) 
        {
            attributes.hitPoints = attributes.hitPoints * hpIncrease;
            attributes.maxHP = attributes.maxHP * hpIncrease;
            attributes.maxHPBase = attributes.maxHPBase * hpIncrease;
            attributes.armorBase += armorIncrease * 100f;
            attributes.armor += armorIncrease * 100f;
        }
        else
        {
            attributes.hitPoints = attributes.hitPoints / hpIncrease;
            attributes.maxHP = attributes.maxHP / hpIncrease;
            attributes.maxHPBase = attributes.maxHPBase / hpIncrease;
            attributes.armorBase -= armorIncrease * 100f;
            attributes.armor -= armorIncrease * 100f;
        }
    }

    public void SetPrestigeBlue(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        float resistanceIncrease = 0f;
        float moveSpeedIncrease = 1f;
        for (int i = 1; i <= count; i++)
        {
            resistanceIncrease += (GlobalValues.main.prestigeBlue * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
            moveSpeedIncrease += (GlobalValues.main.prestigeBlue * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
        }
        if (add == true)
        {
            attributes.resistanceBase += resistanceIncrease * 100f;
            attributes.resistance += resistanceIncrease * 100f;
            attributes.moveSpeedBase = attributes.moveSpeedBase * moveSpeedIncrease;
            attributes.moveSpeed = attributes.moveSpeed * moveSpeedIncrease;
        }
        else
        {
            attributes.resistanceBase -= resistanceIncrease * 100f;
            attributes.resistance -= resistanceIncrease * 100f;
            attributes.moveSpeedBase = attributes.moveSpeedBase / moveSpeedIncrease;
            attributes.moveSpeed = attributes.moveSpeed / moveSpeedIncrease;
        }
    }

    public void SetPrestigeGreen(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        for (int i = 1; i <= count; i++)
        {
            if (CheckEffect("Health Regen"))
            {
                ModifyEffect("Health Regen", GlobalValues.main.prestigeGreen * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 0f, 0f, 0f, 0f, 0f + GlobalValues.main.prestigeGreen * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))));
            }
            else
            {
                AddEffect("Health Regen", GlobalValues.main.prestigeGreen * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 5f, 1f, 1f, 1f, 0.1f);
            }
        }
    }

    public void SetPrestigeYellow(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        float carryCapacityIncrease = 0f;
        for (int i = 1; i <= count; i++)
        {
            carryCapacityIncrease += GlobalValues.main.prestigeYellow * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
        }
        attributes.carryCapacity += (int)((float)attributes.carryCapacity * carryCapacityIncrease);
    }

    public void SetPrestigeWhite(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        attributes.willFly = !attributes.willFly;
        gameObject.GetComponent<Enemy>().SetPathSettings();
    }

    public void SetPrestigePurple(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        for (int i = 1; i <= count; i++)
        {
            if (CheckEffect("Blink"))
            {
                ModifyEffect("Blink", GlobalValues.main.prestigePurple * ((1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 0f, 0f, 0f, 0f, 0f);
            }
            else
            {
                AddEffect("Blink", GlobalValues.main.prestigePurple * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), GlobalValues.main.prestigePurpleRate, GlobalValues.main.prestigePurpleRange, 1f, 1f, 1f);
            }
        }
    }

    public void SetPrestigeBlack(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        for (int i = 1; i <= count; i++)
        {
            if (CheckPassive("Death Split"))
            {
                ModifyPassive("Death Split", GlobalValues.main.prestigeBlack * ((1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 0f, 0f, 0f, 0f, 0f);
            }
            else
            {
                AddPassive("Death Split", GlobalValues.main.prestigeBlack * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 1f, 1f, 1f, 1f, 1f);
            }
        }
    }

    public void SetPrestigeGold(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        float hpIncrease = 1f;
        float armorIncrease = 0f;
        float resistanceIncrease = 0f;
        float moveSpeedIncrease = 1f;
        float dodgeIncrease = 0f;
        for (int i = 1; i <= count; i++)
        {
            hpIncrease += (GlobalValues.main.prestigeRed * GlobalValues.main.prestigeGold * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
            armorIncrease += (GlobalValues.main.prestigeRed * GlobalValues.main.prestigeGold * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
            resistanceIncrease += (GlobalValues.main.prestigeBlue * GlobalValues.main.prestigeGold * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
            moveSpeedIncrease += (GlobalValues.main.prestigeBlue * GlobalValues.main.prestigeGold * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
            dodgeIncrease += (GlobalValues.main.prestigeBrown * GlobalValues.main.prestigeGold * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
        }
        if (add == true)
        {
            attributes.hitPoints = attributes.hitPoints * hpIncrease;
            attributes.maxHP = attributes.maxHP * hpIncrease;
            attributes.maxHPBase = attributes.maxHPBase * hpIncrease;
            attributes.armorBase += armorIncrease * 100f;
            attributes.armor += armorIncrease * 100f;
            attributes.resistanceBase += resistanceIncrease * 100f;
            attributes.resistance += resistanceIncrease * 100f;
            attributes.moveSpeedBase = attributes.moveSpeedBase * moveSpeedIncrease;
            attributes.moveSpeed = attributes.moveSpeed * moveSpeedIncrease;
            attributes.dodgeChanceBase += dodgeIncrease * 100f;
            attributes.dodgeChance += dodgeIncrease * 100f;
        }
        else
        {
            attributes.hitPoints = attributes.hitPoints / hpIncrease;
            attributes.maxHP = attributes.maxHP / hpIncrease;
            attributes.maxHPBase = attributes.maxHPBase / hpIncrease;
            attributes.armorBase -= armorIncrease * 100f;
            attributes.armor -= armorIncrease * 100f;
            attributes.resistanceBase -= resistanceIncrease * 100f;
            attributes.resistance -= resistanceIncrease * 100f;
            attributes.moveSpeedBase = attributes.moveSpeedBase / moveSpeedIncrease;
            attributes.moveSpeed = attributes.moveSpeed / moveSpeedIncrease;
            attributes.dodgeChanceBase -= dodgeIncrease * 100f;
            attributes.dodgeChance -= dodgeIncrease * 100f;
        }
    }

    public void SetPrestigeBrown(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        float dodgeIncrease = 0f;
        for (int i = 1; i <= count; i++)
        {
            dodgeIncrease += (GlobalValues.main.prestigeBrown * (1f + GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
        }
        if (add == true)
        {
            attributes.dodgeChanceBase += dodgeIncrease * 100f;
            attributes.dodgeChance += dodgeIncrease * 100f;
        }
        else
        {
            attributes.dodgeChanceBase -= dodgeIncrease * 100f;
            attributes.dodgeChance -= dodgeIncrease * 100f;
        }
    }

    public void SetPrestigeGrey(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        for (int i = 1; i <= count; i++)
        {
            if (CheckEffect("Invisibility"))
            {
                ModifyEffect("Invisibility", GlobalValues.main.prestigeGrey * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 0f, 0f, 0f, 0f, 0f);
            }
            else
            {
                AddEffect("Invisibility", GlobalValues.main.prestigeGrey * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), GlobalValues.main.prestigeGreyRate, 1f, 1f, GlobalValues.main.prestigeGreyDuration, 1f);
            }
        }
    }

    public void SetPrestigePrismatic(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        for (int i = 1; i <= count; i++)
        {
            if (CheckEffect("Prismatic Buff"))
            {
                ModifyEffect("Prismatic Buff", GlobalValues.main.prestigePrismatic * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 0f, 0f, 0f, 0f, 0f);
            }
            else
            {
                AddEffect("Prismatic Buff", GlobalValues.main.prestigePrismatic * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 1 / GlobalValues.main.prestigePrismaticDuration, 1f, 1f, GlobalValues.main.prestigePrismaticDuration, 1f);
            }
        }
    }

    public void SetPrestigeSilver(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        float shieldIncrease = 0f;
        for (int i = 1; i <= count; i++)
        {
            shieldIncrease += GlobalValues.main.prestigeSilver * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1)));
        }
        if (add == true)
        {
            attributes.shield += attributes.maxHP * shieldIncrease;
            attributes.maxShield += attributes.maxHP * shieldIncrease;
            attributes.maxShieldBase += attributes.maxHP * shieldIncrease;
        }
        else
        {
            attributes.shield -= attributes.maxHP * shieldIncrease;
            attributes.maxShield -= attributes.maxHP * shieldIncrease;
            attributes.maxShieldBase -= attributes.maxHP * shieldIncrease;
        }
    }

    public void SetPrestigePlatinum(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        for (int i = 1; i <= count; i++)
        {
            if (CheckPassive("Revive"))
            {
                ModifyPassive("Revive", GlobalValues.main.prestigePlatinum * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 0f, 0f, 0f, 0f, 0f);
            }
            else
            {
                AddPassive("Revive", GlobalValues.main.prestigePlatinum * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 1 / GlobalValues.main.prestigePlatinumCooldown, 1f, 1f, 1f, 1f);
            }
        }

    }

    public void SetPrestigeTeal(bool add, int count)
    {
        if (count == 0)
        {
            return;
        }
        for (int i = 1; i <= count; i++)
        {
            if (CheckPassive("Slow & Freeze Immunity"))
            {
                ModifyPassive("Slow & Freeze Immunity", GlobalValues.main.prestigeTeal * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 0f, 0f, 0f, 0f, 0f);
            }
            else
            {
                AddPassive("Slow & Freeze Immunity", GlobalValues.main.prestigeTeal * (1f + (GlobalValues.main.prestigeDuplicateModifier * (i - 1))), 1f, 1f, 1f, 1f, 1f);
            }
        }
    }


}
