using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static Enemy main;

    [Header("References")]

    [Header("Attributes")]

    //trackers
    private Attributes attributes;

    private void Start()
    {
        //setup
        WaveSpawner.main.EnemySpawned();
        attributes = gameObject.GetComponent<Attributes>();
        SetPathSettings();
    }

    private void Update()
    {
        if (attributes.frozen == true || attributes.pausing > 0f)
        {
            return;
        }
        if (attributes.sName == "Hummingbird")
        {
            UniqueMove();
        }
        else if (attributes.sName != "Skunk Spray")
        {
            Move();
        }
    }

    private void Move()
    {
        //check pathing
        if (Vector2.Distance(attributes.target.position, transform.position) <= attributes.wayPointDistance)
        {
            if ((attributes.inventoryFull == false) && (attributes.pathIndex != (attributes.path.Length - 1)))
            {
                attributes.pathIndex++;
            }
            else if (attributes.inventoryFull == true && attributes.pathIndex != 0)
            {
                attributes.pathIndex--;
            }
            attributes.target = attributes.path[attributes.pathIndex];
        }
        //set velocity
        Vector2 direction = (attributes.target.position - transform.position).normalized;
        attributes.rb.linearVelocity = direction * attributes.moveSpeed;
        //set rotation
        if (attributes.rotationSpeed > 0)
        {
        float angle = Mathf.Atan2(attributes.target.position.y - transform.position.y, attributes.target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, attributes.rotationSpeed * attributes.moveSpeed * Time.deltaTime);
        }
    }

    private void UniqueMove()
    {
        Hummingbird();
    }

    private void Actions(int i)
    {
        string action = attributes.actions[i];
        //float actionPower = attributes.actionPowerModifiers[i] * attributes.actionPower;
        float actionRate = attributes.actionRateModifiers[i] * attributes.actionRate;
        //float actionRange = attributes.actionRangeModifiers[i] * attributes.targetingRange;
        //float actionPierce = attributes.actionPierceModifiers[i] * attributes.armorPierce;
        //float actionDuration = attributes.actionDurations[i];
        //GameObject prefab = attributes.actionPrefabs[i];
        attributes.timeUntilActions[i] = 1 / (actionRate);
        if (action == "Steal Honey")
        {
            float actionPower = attributes.actionPowerModifiers[i] * attributes.actionPower;
            float actionPierce = attributes.actionPierceModifiers[i] * attributes.armorPierce;
            //pause instead of action timer
            attributes.timeUntilActions[i] = 0.1f;
            //attributes.Pause(1 / (actionRate));
            StealHoney(actionPower, actionPierce);
            return;
        }
        else if (action == "Attack Queen")
        {
            float actionPower = attributes.actionPowerModifiers[i] * attributes.actionPower;
            float actionPierce = attributes.actionPierceModifiers[i] * attributes.armorPierce;
            //pause instead of action timer
            //attributes.timeUntilActions[i] = 0f;
            //attributes.Pause(1 / (actionRate));
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

    private void Effects(int i)
    {
        string effect = attributes.effects[i];
        //float effectPower = attributes.effectPowerModifiers[i] * attributes.effectPower;
        float effectRate = attributes.effectRateModifiers[i] * attributes.effectRate;
        //float effectRange = attributes.effectRangeModifiers[i] * attributes.targetingRange;
        //float effectPierce = attributes.effectPierceModifiers[i] * attributes.resistancePierce;
        //float effectDuration = attributes.effectDurations[i];
        //GameObject prefab = attributes.effectPrefabs[i];
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

    private void Spawn(GameObject prefab, float power, float duration)
    {
        Transform start = attributes.path[attributes.pathIndex];
        Transform nextPoint = attributes.target;
        GameObject spawn = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
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

    private void SapFlower(float power, float duration, float range)
    {
        if (Vector2.Distance(attributes.target.transform.position, gameObject.transform.position) <= range)
        {
            attributes.target.gameObject.GetComponent<Plot>().SapFlower(duration * GlobalValues.main.hummingbirdSapTimeModifier);
            attributes.target = null;
            attributes.Pause(GlobalValues.main.hummingbirdWaitTime / power);
        }
    }

    private void Hummingbird()
    {
        if (attributes.target == null)
        {
            //find new flower
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, GlobalValues.main.hummingbirdRange, (Vector2)transform.position, 0f, GlobalValues.main.flowerMask);
            System.Random RandomGen = new System.Random();
            int randompick = RandomGen.Next(hits.Length - 1);
            attributes.target = hits[randompick].transform;
        }
        if (attributes.target != null)
        {
            //move towards flower
            Vector2 direction = (attributes.target.position - transform.position).normalized;
            attributes.rb.linearVelocity = direction * attributes.moveSpeed;
            float angle = Mathf.Atan2(attributes.target.position.y - transform.position.y, attributes.target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 150 * attributes.moveSpeed * Time.deltaTime);
        }
        else
        {
            attributes.rb.linearVelocity = attributes.rb.linearVelocity * 0;
            Debug.Log("Hummingbird unable to find flower.");
        }
    }

    public void SetPathSettings()
    {
        if (attributes.willFly == true)
        {
            attributes.path = LevelManager.main.flyingPaths[attributes.onPath];
        }
        else
        {
            attributes.path = LevelManager.main.paths[attributes.onPath];
        }
        if (attributes.path != null)
        {
            attributes.target = attributes.path[attributes.pathIndex];
        }
        if (attributes.sName == "Hummingbird")
        {
            attributes.target = null;
        }
    }
}
