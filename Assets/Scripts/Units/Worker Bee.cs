using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class WorkerBee : MonoBehaviour
{
    public static WorkerBee main;

    //trackers
    //private Plot F;
    private Attributes attributes;

    private void Start()
    {
        //setup
        attributes = gameObject.GetComponent<Attributes>();
        //track bees
        LevelManager.main.OrganizeBees();
    }

    private void Update()
    {    
        if (LevelManager.main.levelStarted == false || attributes.work == "unassigned" || attributes.frozen == true || attributes.pausing > 0f)
        {
            //Bee to do nothing
            attributes.HaltMovement();
            return;
        }
        FindTarget();
        Move();
        if (attributes.actions.Length > 0)
        {
            //iterate through all actions
            for (int i = 0; i < attributes.actions.Length; i++)
            {
                if (attributes.timeUntilActions[i] <= 0f)
                {
                    Actions(i);
                }
            }
        }
        //Effects
        if (attributes.effects.Length > 0)
        {
            //iterate through all effects
            for (int i = 0; i < attributes.effects.Length; i++)
            {
                if (attributes.timeUntilEffects[i] <= 0f)
                {
                    Effects(i);
                }
            }
        }
    }

    private void Move()
    {
        if (attributes.target != null)
        {
            Vector2 direction = (attributes.target.position - transform.position).normalized;
            attributes.rb.velocity = direction * attributes.moveSpeed;
            float angle = Mathf.Atan2(attributes.target.position.y - transform.position.y, attributes.target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, attributes.rotationSpeed * attributes.moveSpeed * Time.deltaTime);
        }
        else
        {
            attributes.HaltMovement();
        }
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
            else
            {
                Debug.Log("worker Bee done be confused where to go.");
            }
        }
        else if (action == "Honey" && attributes.work == "Honey")
        {
            float actionPower = attributes.actionPowerModifiers[i] * attributes.actionPower;
            CreateHoney(actionPower);
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
   

    private void FindTarget()
    {
        //Check if bee has target
        if (attributes.work == "Soldier" && attributes.target == null)
        {
            //find enemy nearest to queen
            RaycastHit2D[] hits = Physics2D.CircleCastAll(LevelManager.main.queenBee.transform.position, 300f, (Vector2)LevelManager.main.queenBee.transform.position, 0f, GlobalValues.main.enemyMask);
            if (hits.Length > 0)
            {
                attributes.target = hits[0].transform;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (Vector2.Distance(attributes.target.position, LevelManager.main.queenBee.transform.position) > Vector2.Distance(hits[i].transform.position, LevelManager.main.queenBee.transform.position))
                    {
                        attributes.target = hits[i].transform;
                    }
                }
            }
            else
            {
                //pause checking for targets
                attributes.Pause(GlobalValues.main.SoldierPauseTime);
                return;
            }
        }
    }

    private void Effects(int i)
    {
        //currently none
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

    public void CreateHoney(float power)
    {
        if (LevelManager.main.finalWave == false)
        {
            LevelManager.main.IncreaseHoney(power * GlobalValues.main.honeyGenerationRateModifier);
        }
    }
}
