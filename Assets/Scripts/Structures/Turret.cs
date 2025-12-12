using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using System.Threading;
using System;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private Transform firingPoint;
    [SerializeField] public GameObject[] upgradeMatrix;

    [Header("Attribute")]
    private StructureUIHandler UI;
    private Attributes attributes;

    private void Start()
    {
        attributes = gameObject.GetComponent<Attributes>();
        UI = gameObject.GetComponent<StructureUIHandler>();
        //Set starting rotation
        if (turretRotationPoint != null)
        {
            float angle;
            if (Vector2.Distance(LevelManager.main.path1[0].position, transform.position) < Vector2.Distance(LevelManager.main.path2[0].position, transform.position))
            {
                angle = Mathf.Atan2(LevelManager.main.path1[0].position.y - transform.position.y, LevelManager.main.path1[0].position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            }
            else
            {
                angle = Mathf.Atan2(LevelManager.main.path2[0].position.y - transform.position.y, LevelManager.main.path2[0].position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            }
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            turretRotationPoint.rotation = targetRotation;
        }
    }

    private void Update()
    {
        if (attributes.frozen == true || attributes.pausing > 0f)
        {
            return;
        }
        //Actions
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
        RotateTowardsTarget();
    }

    private void Actions(int i)
    {
        string action = attributes.actions[i];
        float actionPower = attributes.actionPowerModifiers[i] * attributes.actionPower;
        float actionRate = attributes.actionRateModifiers[i] * attributes.actionRate;
        float effectPower = attributes.actionPowerModifiers[i] * attributes.effectPower;
        float actionPierce = attributes.actionPierceModifiers[i] * attributes.armorPierce;
        float effectPierce = attributes.actionPierceModifiers[i] * attributes.resistancePierce;
        float actionRange = attributes.actionRangeModifiers[i] * attributes.targetingRange;
        float actionDuration = attributes.actionDurations[i];
        if (action.Substring(0,5) == "Shoot")
        {
            CheckTarget(actionRange);
            if (attributes.target == null)
            {
                attributes.Pause(GlobalValues.main.turretPauseTime);
                return;
            }
            float actionAoE = attributes.actionExtraModifiers[i] * GlobalValues.main.projectileAoEModifier;
            Shoot(action, attributes.actionPrefabs[i], actionPower, effectPower, actionPierce, effectPierce, actionDuration, actionAoE);
            attributes.timeUntilActions[i] = 1 / (actionRate);
        }
    }

    private void Effects(int i)
    {
        string effect = attributes.effects[i];
        float effectPower = attributes.effectPowerModifiers[i] * attributes.effectPower;
        float effectDuration = attributes.effectDurations[i];
        float effectPierce = attributes.effectPierceModifiers[i] * attributes.resistancePierce;
        float effectRange = attributes.effectRangeModifiers[i] * attributes.targetingRange;
        if (effect.Substring(0, 5) == "Pulse")
        {
            if (attributes.hasTargetSettings)
            {
                CheckTarget(effectRange);
                if (attributes.target == null)
                {
                    attributes.Pause(GlobalValues.main.turretPauseTime);
                    return;
                }
            }
            Pulse(effect, effectPower, effectDuration, effectPierce, effectRange);
            attributes.timeUntilEffects[i] = 1 / attributes.effectRate;
        }
        else if (effect == "Heal Queen")
        {
            LevelManager.main.HealQueen(effectPower);
            attributes.timeUntilEffects[i] = 1 / attributes.effectRate;
        }
        else
        {
            CheckTarget(effectRange);
            if (attributes.target == null)
            {
                attributes.Pause(GlobalValues.main.turretPauseTime);
                return;
            }
            OtherEffects(effect, effectPower, effectDuration, effectPierce, effectRange);
            attributes.timeUntilEffects[i] = 1 / attributes.effectRate;
        }
    }

    private void Shoot(string action, GameObject projectilePrefab, float actionPower, float effectPower, float actionPierce, float effectPierce, float actionDuration, float actionAoE)
    {

        float angle = Mathf.Atan2(attributes.target.position.y - transform.position.y, attributes.target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        GameObject projectileObj = Instantiate(projectilePrefab, firingPoint.position, targetRotation);
        Projectile projectileScript = projectileObj.GetComponent<Projectile>();
        if (action == "Shoot Ramping")
        {
            projectileScript.SetTarget(attributes.target, (actionPower * (1 + ((float)attributes.rampCount) * GlobalValues.main.rampPowerGain)), effectPower, actionPierce, effectPierce, attributes.canHit, attributes.ignoreTerrain, action, actionDuration, actionAoE);
            if (attributes.rampCount < 10)
            {
                attributes.rampCount++;
            }
        }
        else
        {
            projectileScript.SetTarget(attributes.target, actionPower, effectPower, actionPierce, effectPierce, attributes.canHit, attributes.ignoreTerrain, action, actionDuration, actionAoE);
        }
    }

    private void CheckTarget(float range)
    {
        if ((attributes.target != null && !CheckTargetIsInRange(range)) || (attributes.ignoreTerrain == false && isTargetObstructed() == true))
        {
            attributes.target = null;
        }
        if (attributes.target == null)
        {
            FindTarget(range);
        }
    }

    private void FindTarget(float range)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, GlobalValues.main.enemyMask);
        attributes.rampCount = 0;
        //Adjust for allowed targets
        if (attributes.canHit != "All")
        {
            RaycastHit2D[] hitsNew = new RaycastHit2D[] { };
            if (attributes.canHit == "Ground")
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (!hits[i].transform.gameObject.GetComponent<Attributes>().willFly)
                    {
                        Array.Resize(ref hitsNew, hitsNew.Length + 1);
                        hitsNew[hitsNew.Length - 1] = hits[i];
                    }
                }
            }
            else if (attributes.canHit == "Flying")
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform.gameObject.GetComponent<Attributes>().willFly)
                    {
                        Array.Resize(ref hitsNew, hitsNew.Length + 1);
                        hitsNew[hitsNew.Length - 1] = hits[i];
                    }
                }
            }
            hits = hitsNew;
        }
        if (hits.Length > 0)
        {
            if (attributes.targetSetting == attributes.targetingOptions[0])
            {
                //Near
                attributes.target = hits[0].transform;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (attributes.ignoreTerrain == true || !Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))
                    {
                        if (Vector2.Distance(attributes.target.position, transform.position) > Vector2.Distance(hits[i].transform.position, transform.position))
                        {
                            attributes.target = hits[i].transform;
                        }
                    }
                }
            }
            else if (attributes.targetSetting == attributes.targetingOptions[1])
            {
                //Far
                attributes.target = hits[hits.Length - 1].transform;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (attributes.ignoreTerrain == true || !Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))
                    {
                        if (Vector2.Distance(attributes.target.position, transform.position) < Vector2.Distance(hits[i].transform.position, transform.position))
                        {
                            attributes.target = hits[i].transform;
                        }
                    }
                }
            }
            else if (attributes.targetSetting == attributes.targetingOptions[2])
            {
                //Weak
                attributes.target = hits[0].transform;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (attributes.target.GetComponent<Attributes>().maxHP > hits[i].transform.gameObject.GetComponent<Attributes>().maxHP && (attributes.ignoreTerrain == true || (attributes.ignoreTerrain == false && !Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))))
                    {
                        attributes.target = hits[i].transform;
                    }
                }
            }
            else if (attributes.targetSetting == attributes.targetingOptions[3])
            {
                //Strong
                attributes.target = hits[0].transform;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (attributes.target.GetComponent<Attributes>().maxHP < hits[i].transform.gameObject.GetComponent<Attributes>().maxHP && (attributes.ignoreTerrain == true || (attributes.ignoreTerrain == false && !Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))))
                    {
                        attributes.target = hits[i].transform;
                    }
                }
            }
            else if (attributes.targetSetting == attributes.targetingOptions[4])
            {
                //Ground
                attributes.target = hits[0].transform;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform.gameObject.GetComponent<Attributes>().willFly == false && (attributes.ignoreTerrain == true || (attributes.ignoreTerrain == false && !Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))))
                    {
                        attributes.target = hits[i].transform;
                        return;
                    }
                }
            }
            else if (attributes.targetSetting == attributes.targetingOptions[5])
            {
                //Flying
                attributes.target = hits[0].transform;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform.gameObject.GetComponent<Attributes>().willFly == true && (attributes.ignoreTerrain == true || (attributes.ignoreTerrain == false && !Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))))
                    {
                        attributes.target = hits[i].transform;
                        return;
                    }
                }
            }
            else
            {
                attributes.target = hits[0].transform;
                Debug.Log("No Targeting Settings Found for " + gameObject.name);
            }
        }
    }

    private bool isTargetObstructed()
    {
        return attributes.target != null && (Physics2D.Linecast(transform.position, attributes.target.transform.position, GlobalValues.main.obstructionMask));
    }

    private void RotateTowardsTarget()
    {
        if (attributes.target != null && turretRotationPoint != null)
        {
            float angle = Mathf.Atan2(attributes.target.position.y - transform.position.y, attributes.target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, attributes.rotationSpeed * Time.deltaTime);
        }
    }

    private bool CheckTargetIsInRange(float range)
    {
        return Vector2.Distance(attributes.target.position, transform.position) <= range;
    }

    private void Pulse(string effect, float effectPower, float effectDuration, float effectPierce, float effectRange)
    {
        if (effect == "Pulse Slow")
        {
            SendSlowPulse(effectPower, effectDuration, effectPierce, effectRange);
        }
        else if (effect == "Pulse Freeze")
        {
            SendFreezePulse(effectPower, effectDuration, effectPierce, effectRange);
        }
        else if (effect == "Pulse Power Buff")
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
    }

    private void OtherEffects(string effect, float effectPower, float effectDuration, float effectPierce, float effectRange)
    {
        if (effect == "Heal Queen")
        {
            LevelManager.main.HealQueen(effectPower);
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
                Turret tur = hit.transform.GetComponent<Turret>();
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
                Turret tur = hit.transform.GetComponent<Turret>();
                tur.ReceiveRateBuff(buff, duration);
            }
        }
    }

    private void SendSlowPulse(float effectPower, float effectDuration, float effectPierce, float effectRange)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, effectRange, (Vector2)transform.position, 0f, GlobalValues.main.enemyMask);
        float slowPower = 1f + (effectPower * GlobalValues.main.slowPowerModifier);
        float slowDuration = effectDuration * GlobalValues.main.slowDurationModifier;
        float slowPierce = effectPierce * GlobalValues.main.slowPierceModifier;
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                hits[i].transform.gameObject.GetComponent<Attributes>().SlowSpeed(slowPower, slowPierce, slowDuration);
            }
        }
    }

    private void SendFreezePulse(float effectPower, float effectDuration, float effectPierce, float effectRange)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, effectRange, (Vector2)transform.position, 0f, GlobalValues.main.enemyMask);
        float freezePower = effectPower * GlobalValues.main.freezePowerModifier;
        float freezeDuration = effectDuration * GlobalValues.main.freezeDurationModifier;
        float freezePierce = effectPower * GlobalValues.main.freezePierceModifier;
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                hit.transform.gameObject.gameObject.GetComponent<Attributes>().Freeze(freezePower, freezePierce, freezeDuration);;
            }
        }
    }

    private IEnumerator RemovePowerBuff(float buff, float duration)
    {
        yield return new WaitForSeconds(duration);
        attributes.actionPower = attributes.actionPower / buff;
    }

    private IEnumerator RemoveRateBuff(float buff, float duration)
    {
        yield return new WaitForSeconds(duration);
        attributes.actionRate = attributes.actionRate / buff;
    }

    public void ReceivePowerBuff(float buff, float duration)
    {
        attributes.actionPower = attributes.actionPower * buff;
        StartCoroutine(RemovePowerBuff(buff, duration));           
    }

    public void ReceiveRateBuff(float buff, float duration)
    {
        attributes.actionRate = attributes.actionRate * buff;      
        StartCoroutine(RemoveRateBuff(buff, duration));
    }

    private void OnMouseDown()
    {
        if (!(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()))
        {
            return;
        }
        UI.OpenUI();
    }

    private void OnMouseEnter()
    {
        UI.OpenRangeUI();
    }

    private void OnMouseExit()
    {
        UI.CloseRangeUI();
    }
}
