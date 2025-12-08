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

    [Header("Attribute")]
    private StructureUIHandler UI;
    private Attributes attributes;

    private void Start()
    {
        attributes = gameObject.GetComponent<Attributes>();
        //Set starting rotation
        if (turretRotationPoint != null)
        {
            if (Vector2.Distance(LevelManager.main.path1[0].position, transform.position) < Vector2.Distance(LevelManager.main.path2[0].position, transform.position))
            {
                attributes.target = LevelManager.main.path1[0];
            }
            else
            {
                attributes.target = LevelManager.main.path2[0];
            }
            float angle = Mathf.Atan2(attributes.target.position.y - transform.position.y, attributes.target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
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
        float actionPowerModifier = attributes.actionPowerModifiers[i] * attributes.actionPower;
        float actionPierce = attributes.actionPierceModifiers[i] * attributes.armorPierce;
        float actionRange = attributes.actionRangeModifiers[i] * attributes.targetingRange;
        if (action.Substring(0,5) == "Shoot")
        {
            CheckTarget(actionRange);
            if (attributes.target == null)
            {
                attributes.Pause(GlobalValues.main.turretPauseTime);
                return;
            }
            Shoot(action, actionPowerModifier, actionPierce);
            attributes.timeUntilActions[i] = attributes.actionRateModifiers[i] * attributes.actionRate;
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
            CheckTarget(effectRange);
            if (attributes.target == null)
            {
                attributes.Pause(GlobalValues.main.turretPauseTime);
                return;
            }
            Pulse(effect, effectPower, effectDuration, effectPierce, effectRange);
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

    private void Shoot(string action, float actionPower, float actionPierce)
    {

        GameObject projectilePrefab;
        if (action == "Shoot Basic")
        {
            projectilePrefab = GlobalValues.main.shootBasicPrefab;
        }
        else if (action == "Shoot Ramping")
        {
            projectilePrefab = GlobalValues.main.shootRampingPrefab;
        }
        else if (action == "Shoot Slowing")
        {
            projectilePrefab = GlobalValues.main.shootSlowPrefab;
        }
        else if (action == "Shoot Freezing")
        {
            projectilePrefab = GlobalValues.main.shootFreezingPrefab;
        }
        else if (action == "Shoot Ricochet")
        {
            projectilePrefab = GlobalValues.main.shootRicochetPrefab;
        }
        else if (action == "Shoot AoE Slowing")
        {
            projectilePrefab = GlobalValues.main.shootAoESlowingPrefab;
        }
        else if (action == "Shoot AoE Freezing")
        {
            projectilePrefab = GlobalValues.main.shootAoEFreezingPrefab;
        }
        else if (action == "Shoot AoE")
        {
            projectilePrefab = GlobalValues.main.shootAoEPrefab;
        }
        else
        {
            Debug.Log(gameObject.name + " is not properly assigned a projectile on action -> " + action.ToString());
            return;
        }
        float angle = Mathf.Atan2(attributes.target.position.y - transform.position.y, attributes.target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        GameObject projectileObj = Instantiate(projectilePrefab, firingPoint.position, targetRotation);
        Projectile projectileScript = projectileObj.GetComponent<Projectile>();
        if (action == "Shoot Ramping")
        {
            projectileScript.SetTarget(attributes.target, (attributes.actionPower + attributes.actionPower * (1 + (float)attributes.rampCount * actionPowerModifier)), attributes.armorPierce, attributes.canHit, attributes.ignoreTerrain, action, actionPowerModifier, actionDuration);
            if (attributes.rampCount < 10)
            {
                attributes.rampCount++;
            }
        }
        else
        {
            projectileScript.SetTarget(attributes.target, attributes.actionPower, attributes.armorPierce, attributes.resistancePierce, attributes.canHit, attributes.ignoreTerrain, action, actionPowerModifier, actionDuration);
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
        if (attributes.effect == "Ramping")
        {
            attributes.rampCount = 0;
        }
        //Adjust for allowed targets
        if (attributes.canHit != "All")
        {
            RaycastHit2D[] hitsNew = new RaycastHit2D[] { };
            if (attributes.canHit == "Ground")
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (!hits[i].transform.gameObject.getComponent<Attributes>().willFly)
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
                    if (hits[i].transform.gameObject.getComponent<Attributes>().willFly)
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
                    if (attributes.target().getComponent<Attributes>().maxHP > hits[i].transform.gameObject.getComponent<Attributes>().maxHP && (attributes.ignoreTerrain == true || (attributes.ignoreTerrain == false && !Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))))
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
                    if (attributes.target().getComponent<Attributes>().maxHP < hits[i].transform.gameObject.getComponent<Attributes>().maxHP && (attributes.ignoreTerrain == true || (attributes.ignoreTerrain == false && !Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))))
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
                    if (hits[i].transform.gameObject.getComponent<Attributes>().willFly == false && (attributes.ignoreTerrain == true || (attributes.ignoreTerrain == false && !Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))))
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
                    if (hits[i].transform.gameObject.getComponent<Attributes>().willFly == true && (attributes.ignoreTerrain == true || (attributes.ignoreTerrain == false && !Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))))
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
        if (attributes.effect == "Pulse Slow")
        {
            SendSlowPulse(effectPower, effectDuration, effectPierce, effectRange);
        }
        else if (attributes.effect == "Pulse Freeze")
        {
            SendFreezePulse(effectPower, effectDuration, effectPierce, effectRange);
        }
        else if (attributes.effect == "Pulse Damage Buff")
        {
            SendDamageBuff(effectPower, effectDuration, effectRange);
        }
        else if (attributes.effect == "Pulse Damage and Speed Buff")
        {
            SendDamageBuff(effectPower, effectDuration, effectRange);
            SendRateBuff(effectPower, effectDuration, effectRange);
        }
    }

    private void OtherEffects(string effect, float effectPower, float effectDuration, float effectPierce, float effectRange)
    {
        if (attributes.effect == "Heal Queen")
        {
            LevelManager.main.HealQueen(effectPower);
        }
    }

    private void SendDamageBuff(float effectPower, float effectDuration, float effectRange)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, effectRange, (Vector2)transform.position, 0f, GlobalValues.main.towerMask);
        float buff = 1 + (GlobalValues.main.effectPowerBuffRatio * effectPower);
        float duration = GlobalValues.main.buffTimerModifier * effectDuration;
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                Turret tur = hit.transform.GetComponent<Turret>();
                tur.ReceiveDamageBuff(buff, duration);
            }
        }
    }

    private void SendRateBuff(float effectPower, float effectDuration, float effectRange)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, effectRange, (Vector2)transform.position, 0f, GlobalValues.main.towerMask);
        float buff = 1 + (GlobalValues.main.effectRateBuffRatio * effectPower);
        float duration = GlobalValues.main.buffTimer * effectDuration;
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
        float slowPierce = effectPierce * GlobalValues.main.slowPiercenModifier;
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                hits[i].transform.gameObject.attributes.SlowSpeed(slowPower, slowDuration, slowPierce);
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
                hit.transform.gameObject.attributes.Freeze(freezePower, freezeDuration, freezePierce);;
            }
        }
    }

    private IEnumerator RemovePowerBuff(float buff, float duration)
    {
        yield return new WaitForSeconds(duration);
        attributes.effectPower = attributes.effectPower / buff;
    }

    private IEnumerator RemoveRateBuff(float buff, float duration)
    {
        yield return new WaitForSeconds(duration);
        attributes.effectRate = attributes.effectRate / buff;
    }

    public void ReceivePowerBuff(float buff, float duration)
    {
        attributes.effectPower = attributes.effectPower * buff;
        StartCoroutine(RemovePowerBuff(buff, duration));           
    }

    public void ReceiveRateBuff(float buff, float duration)
    {
        attributes.effectRate = attributes.effectRate * buff;      
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
