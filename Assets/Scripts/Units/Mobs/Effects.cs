using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class Effects : MonoBehaviour
{
    public static Effects main;

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
        //Effects
        if (attributes.effects.Length > 0)
        {
            //iterate through all effects
            for (int i = 0; i<attributes.effects.Length; i++)
            {
                attributes.timeUntilEffects[i] -= Time.deltaTime;
                if (attributes.timeUntilEffects[i] <= 0f)
                {
                    Effect(i);
                }
            }
        }
    }

    private void Effect(int i)
    {
        string effect = attributes.effects[i];
        float effectRate = attributes.effectRateModifiers[i] * attributes.effectRate;
        attributes.timeUntilEffects[i] = 1 / (effectRate);
        if (attributes.type == "Tower")
        {
            float effectRange = attributes.effectRangeModifiers[i] * attributes.targetingRange;
            float effectPower = attributes.effectPowerModifiers[i] * attributes.effectPower;
            float effectDuration = attributes.effectDurations[i] * GlobalValues.main.spawnTimerModifier;
            if (effect == "Pulse Power Buff")
            {
                SendPowerBuff(effectPower, effectDuration, effectRange);
            }
            else if (effect == "Pulse Rate Buff")
            {
                SendPowerBuff(effectPower, effectDuration, effectRange);
                SendRateBuff(effectPower, effectDuration, effectRange);
            }
            else if (effect == "Pulse Power and Rate Buff")
            {
                SendPowerBuff(effectPower, effectDuration, effectRange);
                SendRateBuff(effectPower, effectDuration, effectRange);
            }
            else if (effect == "Heal Queen")
            {
                LevelManager.main.HealQueen(effectPower);
            }
            else
            {
                Debug.Log("Calling invalid effect: " + effect);
            }
        }
        else if (attributes.type == "Friendly Unit")
        {

        }
        else
        {
            if (effect == "Spawn")
            {
                float effectPower = attributes.effectPowerModifiers[i] * attributes.effectPower;
                float effectDuration = attributes.effectDurations[i] * GlobalValues.main.spawnTimerModifier;
                GameObject prefab = attributes.effectPrefabs[i];
                Spawn(prefab, effectPower, effectDuration);
            }
            else if (effect == "Heal Aura")
            {
                float effectPower = attributes.effectPowerModifiers[i] * attributes.effectPower;
                float effectRange = attributes.effectRangeModifiers[i] * attributes.targetingRange;
                HealAura(effectPower, effectRange);
            }
            else if (effect == "Health Regen")
            {
                float effectPower = attributes.effectPowerModifiers[i] * attributes.effectPower;
                float effectExtra = attributes.effectExtraModifiers[i] * attributes.effectPower;
                HealthRegen(effectPower, effectExtra);
            }
            else if (effect == "Blink")
            {
                float effectPower = attributes.effectPowerModifiers[i] * attributes.effectPower;
                float effectRange = attributes.effectRangeModifiers[i] * attributes.targetingRange;
                Blink(effectPower, effectRange);
            }
            else if (effect == "Invisibility")
            {
                float effectPower = attributes.effectPowerModifiers[i] * attributes.effectPower;
                float effectDuration = attributes.effectDurations[i] * GlobalValues.main.invisibilityModifier;
                Invisibility(effectPower, effectDuration);
            }
            else if (effect == "Prismatic Buff")
            {
                //Not effected by base effect power
                float effectPower = attributes.effectPowerModifiers[i];
                float effectDuration = attributes.effectDurations[i];
                //Not effected by base effect rate
                attributes.timeUntilEffects[i] = 1 / (attributes.effectRateModifiers[i]);
                PrismaticBuff(effectPower, effectDuration);
            }
            else if (effect == "Hatch")
            {
                GameObject prefab = attributes.effectPrefabs[i];
                float effectPower = attributes.effectPowerModifiers[i] * attributes.effectPower;
                Hatch(prefab, effectPower);
            }
            else if (effect == "Smoke Screen")
            {
                attributes.Die();
            }
            else
            {
                Debug.Log("Calling invalid effect: " + effect);
            }
        }
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

    public bool CheckEffect(string effect)
    {
        int i = Array.IndexOf(attributes.effects, effect);
        if (i > -1)
        {
            return true;
        }
        return false;
    }

    public int FindEffect(string effect)
    {
        int i = Array.IndexOf(attributes.effects, effect);
        return i;
    }

    public void Spawn(GameObject prefab, float power, float duration)
    {
        Transform start = attributes.path[attributes.pathIndex];
        Transform nextPoint = attributes.target;
        GameObject spawn = Library.main.Spawn(prefab, gameObject.transform);
        Attributes spawnAtt = spawn.GetComponent<Attributes>();
        spawnAtt.onPath = attributes.onPath;
        spawnAtt.path = attributes.path;
        spawnAtt.pathIndex = attributes.pathIndex;
        spawnAtt.target = attributes.target;
        if (spawnAtt.effects.Length <= 0)
        {
            spawnAtt.RollPrestige(power);
        }
        else if (spawnAtt.effects[0] == "Hatch")
        {
            spawnAtt.timeUntilEffects[0] = duration * GlobalValues.main.eggHatchingTimerModifier;
            spawnAtt.effectPowerModifiers[0] = power;
        }
        else if (spawnAtt.effects[0] == "Smoke Screen")
        {
            spawnAtt.transform.localScale = spawnAtt.transform.localScale * power;
            spawnAtt.timeUntilEffects[0] = duration;
            spawnAtt.effectPowerModifiers[0] = power;
        }
    }

    public void Hatch(GameObject prefab, float power)
    {
        GameObject spawn = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
        Attributes spawnAtt = spawn.GetComponent<Attributes>();
        spawnAtt.onPath = attributes.onPath;
        spawnAtt.path = attributes.path;
        spawnAtt.pathIndex = attributes.pathIndex;
        spawnAtt.target = attributes.target;
        spawnAtt.RollPrestige(power);
        attributes.Die();
    }

    private void HealAura(float power, float range)
    {
        //find objects of same type in range
        float rangeAdjusted = range * GlobalValues.main.enemyHealRangeModifier;
        float powerAdjusted = power * GlobalValues.main.enemyHealModifier;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, rangeAdjusted, (Vector2)transform.position, 0f, (1 << gameObject.layer));
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].transform.gameObject.GetComponent<Attributes>().Heal(powerAdjusted);
            }
        }
    }

    private void HealthRegen(float power, float extraMod)
    {
        float healAmount = power + extraMod * attributes.maxHP;
        attributes.Heal(healAmount);
    }

    private void Blink(float power, float range)
    {
        if (attributes.target == null)
        {
            return;
        }
        float distanceToTravel = Vector2.Distance(attributes.target.position, gameObject.transform.position);
        System.Random RandomGen = new System.Random();
        int randompick = RandomGen.Next(100);
        float distanceAllowed = range * 100f / (float)randompick;
        if (distanceAllowed >= distanceToTravel)
        {
            gameObject.transform.position = attributes.target.position;
        }
        else
        {
            gameObject.transform.position = Library.main.FindPositionBetweenPoints(gameObject.transform, attributes.target, distanceAllowed);
        }
    }

    private void Invisibility(float power, float duration)
    {
        System.Random RandomGen = new System.Random();
        int randompick = RandomGen.Next(100);
        float durationAdjusted = duration;
        if ((power * 100) <= (float)randompick)
        {
            durationAdjusted = duration * power;
        }
        attributes.AddInvisibility(durationAdjusted);
    }

    private void PrismaticBuff(float power, float duration)
    {
        Prestige prestige = gameObject.GetComponent<Prestige>();
        if (prestige == null)
        {
            Debug.Log(attributes.sName + " does not contain prestige script and cannot utilize Prismatic Buff.");
            return;
        }
        System.Random RandomGen = new System.Random();
        int randompick = RandomGen.Next(4);
        if (randompick == 0)
        {
            prestige.SetPrestigeRed(true, (int)(power * GlobalValues.main.prestigePrismatic));
            StartCoroutine(RemovePrismaticBuff("Red", (int)(power * GlobalValues.main.prestigePrismatic), duration));
        }
        else if (randompick == 1)
        {
            prestige.SetPrestigeBlue(true, (int)(power * GlobalValues.main.prestigePrismatic));
            StartCoroutine(RemovePrismaticBuff("Blue", (int)(power * GlobalValues.main.prestigePrismatic), duration));
        }
        else if (randompick == 2)
        {
            prestige.SetPrestigeGold(true, (int)(power * GlobalValues.main.prestigePrismatic));
            StartCoroutine(RemovePrismaticBuff("Gold", (int)(power * GlobalValues.main.prestigePrismatic), duration));
        }
        else if (randompick == 3)
        {
            prestige.SetPrestigeBrown(true, (int)(power * GlobalValues.main.prestigePrismatic));
            StartCoroutine(RemovePrismaticBuff("Brown", (int)(power * GlobalValues.main.prestigePrismatic), duration));
        }
        else if (randompick == 4)
        {
            prestige.SetPrestigeSilver(true, (int)(power * GlobalValues.main.prestigePrismatic));
            StartCoroutine(RemovePrismaticBuff("Silver", (int)(power * GlobalValues.main.prestigePrismatic), duration));
        }
    }

    private IEnumerator RemovePrismaticBuff(string buff, float power, float duration)
    {
        yield return new WaitForSeconds(duration);
        Prestige prestige = gameObject.GetComponent<Prestige>();
        if (buff == "Red")
        {
            prestige.SetPrestigeRed(false, (int)(power * GlobalValues.main.prestigePrismatic));
        }
        else if (buff == "Blue")
        {
            prestige.SetPrestigeBlue(false, (int)(power * GlobalValues.main.prestigePrismatic));
        }
        else if (buff == "Gold")
        {
            prestige.SetPrestigeGold(false, (int)(power * GlobalValues.main.prestigePrismatic));
        }
        else if (buff == "Brown")
        {
            prestige.SetPrestigeBrown(false, (int)(power * GlobalValues.main.prestigePrismatic));
        }
        else if (buff == "Silver")
        {
            prestige.SetPrestigeSilver(false, (int)(power * GlobalValues.main.prestigePrismatic));
        }
    }

    private void SendPowerBuff(float effectPower, float effectDuration, float effectRange)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, effectRange, (Vector2)transform.position, 0f, GlobalValues.main.towerMask);
        float buff = 1 + (GlobalValues.main.buffPowerModifier * effectPower);
        float duration = GlobalValues.main.buffTimerModifier * effectDuration;
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                Attributes tur = hit.transform.GetComponent<Attributes>();
                tur.ReceivePowerBuff(buff, duration);
            }
        }
    }
    private void SendRateBuff(float effectPower, float effectDuration, float effectRange)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, effectRange, (Vector2)transform.position, 0f, GlobalValues.main.towerMask);
        float buff = 1 + (GlobalValues.main.buffPowerModifier * effectPower);
        float duration = GlobalValues.main.buffTimerModifier * effectDuration;
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                Attributes tur = hit.transform.GetComponent<Attributes>();
                tur.ReceiveRateBuff(buff, duration);
            }
        }
    }
}
