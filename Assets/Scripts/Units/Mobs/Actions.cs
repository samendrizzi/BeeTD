using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class Actions : MonoBehaviour
{
    public static Actions main;

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
        //Actions
        if (attributes.actions.Length > 0)
        {
            //iterate through all actions
            for (int i = 0; i < attributes.actions.Length; i++)
            {
                attributes.timeUntilActions[i] -= Time.deltaTime;
                if (attributes.target != null)
                {
                    if (Vector2.Distance(attributes.target.position, gameObject.transform.position) <= attributes.targetingRange && attributes.timeUntilActions[i] <= 0f)
                    {
                        Action(i);
                    }
                }
            }   
        }
    }

    private void Action(int i)
    {
        string action = attributes.actions[i];
        float actionRate = attributes.actionRateModifiers[i] * attributes.actionRate;
        attributes.timeUntilActions[i] = 1 / (actionRate);
        if (attributes.actionSounds.Length >= i + 1 && attributes.actionSounds[i] != SoundType.EMPTY)
        {
            SoundManager.main.PlaySound(attributes.actionSounds[i]);
        }
        if (attributes.type == "Tower")
        {
            float actionPower = attributes.actionPowerModifiers[i] * attributes.actionPower;
            float actionDuration = attributes.actionDurations[i];
            float actionRange = attributes.actionRangeModifiers[i] * attributes.targetingRange;
            float actionPierce = attributes.actionPierceModifiers[i] * attributes.armorPierce;
            if (action.Substring(0, 5) == "Shoot")
            {
                float effectPower = attributes.actionPowerModifiers[i] * attributes.effectPower;
                float effectPierce = attributes.actionPierceModifiers[i] * attributes.resistancePierce;                
                gameObject.GetComponent<Turret>().CheckTarget(actionRange);
                if (attributes.target == null)
                {
                    attributes.Pause(GlobalValues.main.turretPauseTime);
                    return;
                }
                float actionExtraMod = attributes.actionExtraModifiers[i];
                Shoot(action, attributes.actionPrefabs[i], attributes.actionSounds[i], actionPower, effectPower, actionPierce, effectPierce, actionDuration, actionExtraMod);
                attributes.timeUntilActions[i] = 1 / (actionRate);
            }
            else if (action == "Pulse Slow")
            {
                SendSlowPulse(actionPower, actionDuration, actionPierce, actionRange);
            }
            else if (action == "Pulse Freeze")
            {
                SendFreezePulse(actionPower, actionDuration, actionPierce, actionRange);
            }
        }
        else if (attributes.type == "Friendly Unit")
        {
            if (action == "Nectar" && attributes.work == "Nectar")
            {
                if (attributes.inventoryFull == true && Vector2.Distance(attributes.target.position, transform.position) <= attributes.wayPointDistance)
                {
                    DepositNectar();
                    return;
                }
                else if (attributes.inventoryFull == false && Vector2.Distance(attributes.target.position, transform.position) <= attributes.wayPointDistance)
                {
                    CollectNectar();
                    return;
                }
            }
            else if (action == "Honey" && attributes.work == "Honey")
            {
                float actionPower = attributes.actionPowerModifiers[i] * attributes.actionPower;
                CreateHoneyComb(actionPower);
                return;
            }
            else if (action == "Attack" && attributes.work == "Soldier")
            {
                float actionPower = attributes.actionPowerModifiers[i] * attributes.actionPower;
                float actionRange = attributes.actionRangeModifiers[i] * attributes.targetingRange;
                float actionPierce = attributes.actionPierceModifiers[i] * attributes.armorPierce;
                AttackTarget(actionPower, actionRange, actionPierce);
            }
        }
        else
        {
            if (action == "Steal Honey")
            {
                float actionPower = attributes.actionPowerModifiers[i] * attributes.actionPower;
                float actionPierce = attributes.actionPierceModifiers[i] * attributes.armorPierce;
                //pause instead of action timer
                attributes.timeUntilActions[i] = 0.1f;
                StealHoney(actionPower, actionPierce);
                return;
            }
            else if (action == "Attack Queen")
            {
                float actionPower = attributes.actionPowerModifiers[i] * attributes.actionPower;
                float actionPierce = attributes.actionPierceModifiers[i] * attributes.armorPierce;
                //pause instead of action timer
                AttackQueen(actionPower, actionPierce);
                return;
            }
            else if (action == "Sap Flower")
            {
                float actionPower = attributes.actionPowerModifiers[i] * attributes.actionPower;
                float actionDuration = attributes.actionDurations[i];
                SapFlower(actionPower, actionDuration);
                return;
            }
        }
    }

    public void AddAction(string action, GameObject prefab, SoundType sound, float powerMod, float rateMod, float rangeMod, float pierceMod, float duration, float extraMod)
    {
        int index = attributes.actions.Length;
        Array.Resize(ref attributes.actions, index + 1);
        Array.Resize(ref attributes.actionPrefabs, index + 1);
        Array.Resize(ref attributes.actionSounds, index + 1);
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
        attributes.actionPrefabs[index] = prefab;
        attributes.actionSounds[index] = sound;
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

    public void RemoveAction(string action)
    {
        int index = FindAction(action);
        if (index < 0)
        {
            Debug.Log(attributes.sName + " does not have action " + action + " to remove.");
            return;
        }
        if (attributes.actions.Length > index + 1)
        {
            for (int i = index; i < attributes.actions.Length - 1; i++)
            {
                attributes.actions[i] = attributes.actions[i + 1];
                attributes.actionPrefabs[i] = attributes.actionPrefabs[i + 1];
                attributes.actionSounds[i] = attributes.actionSounds[i + 1];
                attributes.actionPowerModifiers[i] = attributes.actionPowerModifiers[i + 1];
                attributes.actionRateModifiers[i] = attributes.actionRateModifiers[i + 1];
                attributes.actionRangeModifiers[i] = attributes.actionRangeModifiers[i + 1];
                attributes.actionPierceModifiers[i] = attributes.actionPierceModifiers[i + 1];
                attributes.actionDurations[i] = attributes.actionDurations[i + 1];
                attributes.actionExtraModifiers[i] = attributes.actionExtraModifiers[i + 1];
                attributes.actionPowerModifiersBase[i] = attributes.actionPowerModifiersBase[i + 1];
                attributes.actionRateModifiersBase[i] = attributes.actionRateModifiersBase[i + 1];
                attributes.actionRangeModifiersBase[i] = attributes.actionRangeModifiersBase[i + 1];
                attributes.actionPierceModifiersBase[i] = attributes.actionPierceModifiersBase[i + 1];
                attributes.actionDurationsBase[i] = attributes.actionDurationsBase[i + 1];
                attributes.actionExtraModifiersBase[i] = attributes.actionExtraModifiersBase[i + 1];
                attributes.timeUntilActions[i] = attributes.timeUntilActions[i + 1];
            }
        }
        Array.Resize(ref attributes.actionPrefabs, attributes.actions.Length - 1);
        Array.Resize(ref attributes.actionSounds, attributes.actions.Length - 1);
        Array.Resize(ref attributes.actionPowerModifiers, attributes.actions.Length - 1);
        Array.Resize(ref attributes.actionRateModifiers, attributes.actions.Length - 1);
        Array.Resize(ref attributes.actionRangeModifiers, attributes.actions.Length - 1);
        Array.Resize(ref attributes.actionPierceModifiers, attributes.actions.Length - 1);
        Array.Resize(ref attributes.actionDurations, attributes.actions.Length - 1);
        Array.Resize(ref attributes.actionExtraModifiers, attributes.actions.Length - 1);
        Array.Resize(ref attributes.actionPowerModifiersBase, attributes.actions.Length - 1);
        Array.Resize(ref attributes.actionRateModifiersBase, attributes.actions.Length - 1);
        Array.Resize(ref attributes.actionRangeModifiersBase, attributes.actions.Length - 1);
        Array.Resize(ref attributes.actionPierceModifiersBase, attributes.actions.Length - 1);
        Array.Resize(ref attributes.actionDurationsBase, attributes.actions.Length - 1);
        Array.Resize(ref attributes.actionExtraModifiersBase, attributes.actions.Length - 1);
        Array.Resize(ref attributes.timeUntilActions, attributes.actions.Length - 1);
        Array.Resize(ref attributes.actions, attributes.actions.Length - 1);
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

    public int FindAction(string action)
    {
        int i = Array.IndexOf(attributes.actions, action);
        return i;
    }

    private void StealHoney(float power, float pierce)
    {
        if (Vector2.Distance(LevelManager.main.queenBee.transform.position, transform.position) <= attributes.wayPointDistance)
        {
            if (attributes.pathIndex == attributes.path.Length - 1)
            {
                if (LevelManager.main.honey < attributes.carryCapacity)
                {
                    AttackQueen(power, pierce);
                    return;
                }
                else if (LevelManager.main.honey >= attributes.carryCapacity)
                {
                    attributes.inventoryFull = true;
                    LevelManager.main.honey = LevelManager.main.honey - attributes.carryCapacity;
                }
            }
        }
        else if (Vector2.Distance(attributes.path[0].position, transform.position) <= attributes.wayPointDistance)
        {
            if (attributes.inventoryFull == true && attributes.pathIndex == 0)
            {
                attributes.Heal(attributes.carryCapacity * GlobalValues.main.honeyhealModifier);
                attributes.inventoryFull = false;
                return;
            }
        }
    }

    private void AttackQueen(float damage, float armorPierce)
    {
        if (Vector2.Distance(LevelManager.main.queenBee.transform.position, transform.position) <= attributes.wayPointDistance)
        {
            LevelManager.main.HitQueen(damage, armorPierce);
            attributes.TakeDamage(damage * BuffManager.main.queenBeeReturnDamage, armorPierce);
        }
    }

    private void SapFlower(float power, float duration)
    {
        attributes.target.gameObject.GetComponent<Plot>().SapFlower(duration * GlobalValues.main.hummingbirdSapTimeModifier);
        attributes.target = null;
        attributes.Pause(GlobalValues.main.hummingbirdWaitTime / power);      
    }

    private void Shoot(string action, GameObject projectilePrefab, SoundType sound, float actionPower, float effectPower, float actionPierce, float effectPierce, float actionDuration, float actionExtra)
    {
        float angle = Mathf.Atan2(attributes.target.position.y - transform.position.y, attributes.target.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        GameObject projectileObj = Instantiate(projectilePrefab, gameObject.GetComponent<Turret>().firingPoint.position, targetRotation);
        Projectile projectileScript = projectileObj.GetComponent<Projectile>();
        if (action == "Shoot Ramping")
        {
            projectileScript.SetTarget(attributes.target, (actionPower * (1 + ((float)attributes.rampCount) * BuffManager.main.towerRampingDamage * actionExtra)), effectPower, actionPierce, effectPierce, attributes.canHit, attributes.ignoreTerrain, action, actionDuration, actionExtra);
            if (attributes.rampCount < attributes.actionDurations[FindAction("Shoot Ramping")] + BuffManager.main.towerExtraRampCount)
            {
                attributes.rampCount++;
            }
        }
        else
        {
            projectileScript.SetTarget(attributes.target, actionPower, effectPower, actionPierce, effectPierce, attributes.canHit, attributes.ignoreTerrain, action, actionDuration, actionExtra);
        }
        SoundManager.main.PlaySound(sound);
    }

    private void SendSlowPulse(float effectPower, float effectDuration, float effectPierce, float effectRange)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, effectRange, (Vector2)transform.position, 0f, GlobalValues.main.enemyMask);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                hit.transform.gameObject.GetComponent<Attributes>().SlowSpeed(effectPower, effectPierce, effectDuration);
            }
        }
    }

    private void SendFreezePulse(float effectPower, float effectDuration, float effectPierce, float effectRange)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, effectRange, (Vector2)transform.position, 0f, GlobalValues.main.enemyMask);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                hit.transform.gameObject.GetComponent<Attributes>().Freeze(effectPower, effectPierce, effectDuration);
            }
        }
    }

    private void AttackTarget(float power, float range, float pierce)
    {
        if (attributes.target != null)
        {
            if (Vector2.Distance(attributes.target.position, transform.position) <= range)
            {
                attributes.target.gameObject.GetComponent<Attributes>().TakeDamage(power, pierce);
            }
        }
    }

    public void DepositNectar()
    {
        attributes.inventoryFull = false;
        LevelManager.main.IncreaseNectar(attributes.nectar);
        attributes.nectar = 0f;
        attributes.target = attributes.flower.transform;
    }

    public void CollectNectar()
    {
        if (attributes.flower.GetComponent<Plot>().isSapped == false)
        {
            attributes.nectar = attributes.carryCapacity;
            attributes.flower.GetComponent<Plot>().SapFlower(attributes.carryCapacity / LevelManager.main.nectarGenerationRate);
            attributes.inventoryFull = true;
            attributes.target = LevelManager.main.queenBee.gameObject.transform;
        }
    }

    public void CreateHoneyComb(float power)
    {
        if (attributes.target == null)
        {
            return;
        }
        if (Vector2.Distance(attributes.target.position, transform.position) <= attributes.wayPointDistance)
        {
            if (attributes.inventoryFull)
            {
                if (attributes.target.gameObject.GetComponent<Plot>().honeyTicks > 0)
                {
                    //another bee filled
                    attributes.target = null;
                    return;
                }
                attributes.target.gameObject.GetComponent<Plot>().HoneyFill();
                attributes.inventoryFull = false;
                attributes.target = null;
                attributes.Pause(BuffManager.main.honeycombFillTime);
            }
            else
            {
                attributes.inventoryFull = true;
                attributes.target = null;
            }
        }
    }
}
