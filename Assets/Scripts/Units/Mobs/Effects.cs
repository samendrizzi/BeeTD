using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Effects : MonoBehaviour
{
    public static Effects main;

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
        Debug.Log(durationAdjusted);
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
            Debug.Log("Adding Red");
            prestige.SetPrestigeRed(true, (int)(power * GlobalValues.main.prestigePrismatic));
            StartCoroutine(RemovePrismaticBuff("Red", (int)(power * GlobalValues.main.prestigePrismatic), duration));
        }
        else if (randompick == 1)
        {
            Debug.Log("Adding Blue");
            prestige.SetPrestigeBlue(true, (int)(power * GlobalValues.main.prestigePrismatic));
            StartCoroutine(RemovePrismaticBuff("Blue", (int)(power * GlobalValues.main.prestigePrismatic), duration));
        }
        else if (randompick == 2)
        {
            Debug.Log("Adding Gold");
            prestige.SetPrestigeGold(true, (int)(power * GlobalValues.main.prestigePrismatic));
            StartCoroutine(RemovePrismaticBuff("Gold", (int)(power * GlobalValues.main.prestigePrismatic), duration));
        }
        else if (randompick == 3)
        {
            Debug.Log("Adding Brown");
            prestige.SetPrestigeBrown(true, (int)(power * GlobalValues.main.prestigePrismatic));
            StartCoroutine(RemovePrismaticBuff("Brown", (int)(power * GlobalValues.main.prestigePrismatic), duration));
        }
        else if (randompick == 4)
        {
            Debug.Log("Adding Silver");
            prestige.SetPrestigeSilver(true, (int)(power * GlobalValues.main.prestigePrismatic));
            StartCoroutine(RemovePrismaticBuff("Silver", (int)(power * GlobalValues.main.prestigePrismatic), duration));
        }
    }

    private IEnumerator RemovePrismaticBuff(string buff, float power, float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("Removing " + buff);
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
}
