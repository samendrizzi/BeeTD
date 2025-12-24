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

    private void Start()
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
                attributes.timeUntilPassives[i] -= Time.deltaTime;
            }
        }
    }

    public void DeathSplit()
    {
        int index = gameObject.GetComponent<Prestige>().FindPassive("Death Split");
        if (index != -1)
        {
            int count = (int)(attributes.passivePowerModifiers[index] * attributes.passivePower);
            float power = (attributes.passiveExtraModifiers[index] * attributes.passivePower);
            RemovePassive("Death Split");
            attributes.prestige = attributes.prestige.Remove(attributes.prestige.IndexOf("Black"), "Black".Length);
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
        int index = gameObject.GetComponent<Prestige>().FindPassive("Revive");
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
        int index = gameObject.GetComponent<Prestige>().FindPassive(passive);
        if (index < 0)
        {
            Debug.Log(attributes.sName + " does not have passive " + passive + " to remove.");
            return;
        }
        if (attributes.passives.Length > index + 1)
        {
            for (int i = index + 1; i <= attributes.passives.Length; i++)
            {
                Debug.Log("test");
                attributes.passives[i - 1] =  attributes.passives[i - 1];
                attributes.passivePowerModifiers[i - 1] = attributes.passivePowerModifiers[i];
                attributes.passiveRateModifiers[i - 1] = attributes.passiveRateModifiers[i];
                attributes.passiveRangeModifiers[i - 1] =  attributes.passiveRangeModifiers[i];
                attributes.passivePierceModifiers[i - 1] = attributes.passivePierceModifiers[i];
                attributes.passiveDurations[i - 1] =  attributes.passiveDurations[i];
                attributes.passiveExtraModifiers[i - 1] = attributes.passiveExtraModifiers[i];
                attributes.passivePowerModifiersBase[i - 1] = attributes.passivePowerModifiersBase[i];
                attributes.passiveRateModifiersBase[i - 1] =  attributes.passiveRateModifiersBase[i];
                attributes.passiveRangeModifiersBase[i - 1] = attributes.passiveRangeModifiersBase[i] ;
                attributes.passivePierceModifiersBase[i - 1] = attributes.passivePierceModifiersBase[i];
                attributes.passiveDurationsBase[i - 1] =  attributes.passiveDurationsBase[i];
                attributes.passiveExtraModifiersBase[i - 1] = attributes.passiveExtraModifiersBase[i];
                attributes.timeUntilPassives[i - 1] = attributes.timeUntilPassives[i];
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
