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
        if (LevelManager.main.state == Level.NOTSTARTED || LevelManager.main.state == Level.VICTORY || LevelManager.main.state == Level.DEFEAT || attributes.work == "Unassigned" || attributes.frozen == true || attributes.pausing > 0f)
        {
            //Bee to do nothing
            attributes.HaltMovement();
            return;
        }
        FindTarget();
        Move();
    }

    private void Move()
    {
        if (attributes.target != null)
        {
            //set veolcity
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
        else if (attributes.work == "Honey" && attributes.target == null)
        {
            if (LevelManager.main.state == Level.FINALWAVE)
            {
                LevelManager.main.AssignBeeToHoney(false);
                LevelManager.main.AssignBeeToSoldier(true);
                return;
            }
            //Find Empty Comb
            if (attributes.inventoryFull)
            {
                if (LevelManager.main.emptyHoneyCombs.Length == 0)
                {
                    return;
                }
                attributes.target = LevelManager.main.emptyHoneyCombs[LevelManager.main.emptyHoneyCombs.Length - 1];
                Array.Resize(ref LevelManager.main.emptyHoneyCombs, LevelManager.main.emptyHoneyCombs.Length - 1);
            }
            //Return to Queen
            else
            {
                attributes.target = LevelManager.main.queenBee.transform;
            }

        }
    }

}
