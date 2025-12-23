using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Actions : MonoBehaviour
{
    public static Actions main;

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
        //Actions
        if (attributes.actions.Length > 0)
        {
            //iterate through all actions
            for (int i = 0; i < attributes.actions.Length; i++)
            {
                attributes.timeUntilActions[i] -= Time.deltaTime;
                if (attributes.timeUntilActions[i] <= 0f)
                {
                    Action(i);
                }
            }
        }
    }

    private void Action(int i)
    {
        string action = attributes.actions[i];
        float actionRate = attributes.actionRateModifiers[i] * attributes.actionRate;
        attributes.timeUntilActions[i] = 1 / (actionRate);
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
            float actionRange = attributes.actionRangeModifiers[i] * attributes.targetingRange;
            SapFlower(actionPower, actionDuration, actionRange);
            return;
        }
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
            attributes.TakeDamage(damage * GlobalValues.main.queenThornModifier, armorPierce);
        }
    }

    private void SapFlower(float power, float duration, float range)
    {
        if (Vector2.Distance(attributes.target.transform.position, gameObject.transform.position) <= range)
        {
            attributes.target.gameObject.GetComponent<Plot>().SapFlower(duration * GlobalValues.main.hummingbirdSapTimeModifier);
            attributes.target = null;
            attributes.Pause(GlobalValues.main.hummingbirdWaitTime / power);
        }
    }
}
