using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Passives : MonoBehaviour
{
    public static Passives main;

    [Header("References")]

    [Header("Attributes")]

    //trackers
    private Attributes attributes;

    private void Awake()
    {
        //setup
        attributes = gameObject.GetComponent<Attributes>();
    }

    private void Update()
    {
        if (attributes.frozen == true)
        {
            return;
        }
        //Passives
        if (attributes.passives.Length > 0)
        {
            //iterate through all passives
            for (int i = 0; i < attributes.passives.Length; i++)
            {
                attributes.timeUntilPassives[i] -= GlobalValues.main.deltaTime;
            }
        }
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

    public bool CheckPassive(string passive)
    {
        int i = Array.IndexOf(attributes.passives, passive);
        if (i > -1)
        {
            return true;
        }
        return false;
    }

    public int FindPassive(string passive)
    {
        int i = Array.IndexOf(attributes.passives, passive);
        return i;
    }

    public void DeathSplit()
    {
        int index = FindPassive("Death Split");
        if (index != -1)
        {
            int count = (int)(attributes.passivePowerModifiers[index] * attributes.passivePower);
            float power = (attributes.passiveExtraModifiers[index] * attributes.passivePower);
            RemovePassive("Death Split");
            if (attributes.prestige.IndexOf("Black") != -1)
            {
                attributes.prestige = attributes.prestige.Remove(attributes.prestige.IndexOf("Black"), "Black".Length);
            }
            attributes.hitPoints = attributes.maxHP;
            attributes.shield = attributes.maxShield;
            attributes.isDestroyed = false;
            float size = gameObject.GetComponent<Renderer>().bounds.size.x;
            for (int i = 1; i <= count; i++)
            {
                GameObject newSpawn = Library.main.Spawn(gameObject, gameObject.transform);
                newSpawn.transform.position = Library.main.ShiftPosition(gameObject.transform.position, (i * 360 / count), GlobalValues.main.prestigeBlackDistance * size);
                newSpawn.GetComponent<Attributes>().actionPower = attributes.actionPower * power;
                newSpawn.GetComponent<Attributes>().effectPower = attributes.effectPower * power;
                newSpawn.GetComponent<Attributes>().passivePower = attributes.passivePower * power;
            }
        }
    }

    public void Revive()
    {
        int index = FindPassive("Revive");
        attributes.timeUntilPassives[index] = 1 / (attributes.passiveRateModifiers[index] * attributes.passiveRate * GlobalValues.main.reviveCooldownModifier);
        float power = attributes.passivePowerModifiers[index] * attributes.passivePower;
        if (power >= 1f)
        {
            power = 1f;
        }
        attributes.hitPoints = attributes.maxHP * power;
        attributes.shield = attributes.maxShield * power;
        attributes.Pause(attributes.passiveDurations[index] * GlobalValues.main.reviveDurationModifier);
    }

    public void RemovePassive(string passive)
    {
        int index = FindPassive(passive);
        if (index < 0)
        {
            Debug.Log(attributes.sName + " does not have passive " + passive + " to remove.");
            return;
        }
        if (attributes.passives.Length > index + 1)
        {
            for (int i = index; i < attributes.passives.Length - 1; i++)
            {
                attributes.passives[i] =  attributes.passives[i + 1];
                attributes.passivePowerModifiers[i] = attributes.passivePowerModifiers[i + 1];
                attributes.passiveRateModifiers[i] = attributes.passiveRateModifiers[i + 1];
                attributes.passiveRangeModifiers[i] =  attributes.passiveRangeModifiers[i + 1];
                attributes.passivePierceModifiers[i] = attributes.passivePierceModifiers[i + 1];
                attributes.passiveDurations[i] =  attributes.passiveDurations[i + 1];
                attributes.passiveExtraModifiers[i] = attributes.passiveExtraModifiers[i + 1];
                attributes.passivePowerModifiersBase[i] = attributes.passivePowerModifiersBase[i + 1];
                attributes.passiveRateModifiersBase[i] =  attributes.passiveRateModifiersBase[i + 1];
                attributes.passiveRangeModifiersBase[i] = attributes.passiveRangeModifiersBase[i + 1] ;
                attributes.passivePierceModifiersBase[i] = attributes.passivePierceModifiersBase[i + 1];
                attributes.passiveDurationsBase[i] =  attributes.passiveDurationsBase[i + 1];
                attributes.passiveExtraModifiersBase[i] = attributes.passiveExtraModifiersBase[i + 1];
                attributes.timeUntilPassives[i] = attributes.timeUntilPassives[i + 1];
            }
        }
        Array.Resize(ref attributes.passivePowerModifiers, attributes.passives.Length - 1);
        Array.Resize(ref attributes.passiveRateModifiers, attributes.passives.Length - 1);
        Array.Resize(ref attributes.passiveRangeModifiers, attributes.passives.Length - 1);
        Array.Resize(ref attributes.passivePierceModifiers, attributes.passives.Length - 1);
        Array.Resize(ref attributes.passiveDurations, attributes.passives.Length - 1);
        Array.Resize(ref attributes.passiveExtraModifiers, attributes.passives.Length - 1);
        Array.Resize(ref attributes.passivePowerModifiersBase, attributes.passives.Length - 1);
        Array.Resize(ref attributes.passiveRateModifiersBase, attributes.passives.Length - 1);
        Array.Resize(ref attributes.passiveRangeModifiersBase, attributes.passives.Length - 1);
        Array.Resize(ref attributes.passivePierceModifiersBase, attributes.passives.Length - 1);
        Array.Resize(ref attributes.passiveDurationsBase, attributes.passives.Length - 1);
        Array.Resize(ref attributes.passiveExtraModifiersBase, attributes.passives.Length - 1);
        Array.Resize(ref attributes.timeUntilPassives, attributes.passives.Length - 1);
        Array.Resize(ref attributes.passives, attributes.passives.Length - 1);
    }
}
