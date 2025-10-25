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
    private LayerMask enemyMask;
    private GameObject projectilePrefab;
    private LayerMask flowerMask;
    private LayerMask towerMask;


    [Header("Attribute")]
    public string tName;
    public float targetingRange;
    public float rotationSpeed;
    public float damage;
    public float armorPierce;
    public float aps;
    public float effectDuration;
    public float effectRatio;
    public float efficiency;
    public int flowerIndex;
    public float flowerMultiplier;
    public float apsBase;
    public float damageBase;
    public float targetingRangeBase;
    public string effect;
    public float effectDurationBase;
    public float effectRatioBase;
    public float efficiencyBase;
    public bool hasTargetSettings;
    public string targetSetting = "Near";
    private string[] targetingOptions;
    public bool ignoreTerrain;
    public float honeyRate;  //bonus
    public float generationRate;
    private float honeyRateBase;
    private float generationRateBase;
    private int rampCount = 0;
    private float cost;
    public bool isDestroyed = false;

    public Transform target;
    private float timeUntilFire;
    public float debuffTargetingRange = 0f;
    public float debuffaps = 0f;
    public float debuffDamage = 0f;
    public float debuffEfficiency = 0f;
    public float debuffEffectRatio = 0f;
    public float debuffEffectDuration = 0f;
    public float debuffFlower = 0f;
    public float debuffTimer = 0f;
    public bool debuffed = false;
    private float timeUntilEffect = 0f;
    public float investmentRate = 0f;
    public float investmentRateBase = 0f;
    private StructureUIHandler UI;
    private string canHit = "None";

    private void Start()
    {
        //import values
        UI = gameObject.GetComponent<StructureUIHandler>();
        flowerIndex = -1;
        targetingOptions = GlobalValues.main.targetingOptions;
        int index = GetComponent<Identify>().ID;
        tName = GlobalValues.main.TOWERname[index];
        enemyMask = GlobalValues.main.TOWERenemyMask[index];
        towerMask = GlobalValues.main.towerMask;
        projectilePrefab = GlobalValues.main.TOWERprojectilePrefab[index];
        cost = GlobalValues.main.TOWERcost[index];
        targetingRange = GlobalValues.main.TOWERtargetingRange[index];
        rotationSpeed = GlobalValues.main.TOWERrotationSpeed[index];
        damage = GlobalValues.main.TOWERdamage[index];
        armorPierce = GlobalValues.main.TOWERarmorPierce[index];
        aps = GlobalValues.main.TOWERaps[index];
        hasTargetSettings = GlobalValues.main.TOWERhasTargetSettings[index];
        effect = GlobalValues.main.TOWEReffect[index];
        effectDuration = GlobalValues.main.TOWEReffectDuration[index];
        effectRatio = GlobalValues.main.TOWEReffectRatio[index];
        efficiency = GlobalValues.main.TOWERefficiency[index];
        ignoreTerrain = GlobalValues.main.TOWERignoreTerrain[index];
        canHit = GlobalValues.main.TOWERcanHit[index];
        apsBase = aps;
        damageBase = damage;
        targetingRangeBase = targetingRange;
        effectDurationBase = effectDuration;
        effectRatioBase = effectRatio;
        efficiencyBase = efficiency;
        if ((GlobalValues.main.investmentMask & (1 << gameObject.layer)) != 0)
        {
            investmentRate = efficiency * GlobalValues.main.investmentMultiplier;
            investmentRateBase = investmentRate;
            LevelManager.main.CalculateInvestment();
        }
        else if ((GlobalValues.main.incomeMask & (1 << gameObject.layer)) != 0)
        {
            generationRate = efficiency * GlobalValues.main.globalFertility;
            generationRateBase = generationRate;
        }
        //check for flower buffs
        flowerMask = GlobalValues.main.flowerMask;
        IdentifyFlower();
        //Set starting rotation
        if (turretRotationPoint != null)
        {
            if (Vector2.Distance(LevelManager.main.path1[0].position, transform.position) < Vector2.Distance(LevelManager.main.path2[0].position, transform.position))
            {
                target = LevelManager.main.path1[0];
            }
            else
            {
                target = LevelManager.main.path2[0];
            }
            float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            turretRotationPoint.rotation = targetRotation;
        }
    }

    private void Update()
    {
        //does tower shoot
        if (hasTargetSettings == true)
        {
            if (target != null && !CheckTargetIsInRange())
            {
                target = null;
            }
            else if (target != null)
            {
                timeUntilFire += Time.deltaTime * LevelManager.main.timing;

                if (timeUntilFire >= 1f / aps)
                {
                    Shoot();
                    timeUntilFire = 0f;
                }
            }

            if (target == null)
            {
                FindTarget();
                return;
            }

            RotateTowardsTarget();
        }
        if (effect != "none" && timeUntilEffect <= 0f)
        {
            Effect();
        }
        timeUntilEffect -= Time.deltaTime * LevelManager.main.timing;

        
        if (ignoreTerrain == false && isTargetObstructed() == true)
        {
            target = null;
        }
    }

    private void ApplyFlowerBuff(bool buff)
    {
        if (buff == true)
        {
            generationRate = generationRateBase;
        }
        else
        {
            generationRate = 0f;
        }
        if (flowerIndex == 0) //closed flower
        {
            //no current effect
            if (buff == true)
            {
                generationRate = 0f;
            }
            else
            {
                generationRate = 0f;

            }
        }
        else if (flowerIndex == 1) //blue flower effect
        {
            if (buff == true)
            {
                effectRatio = effectRatio * flowerMultiplier;
                effectDuration = effectDuration * flowerMultiplier;
            }
            else
            {
                effectRatio = effectRatio / flowerMultiplier;
                effectDuration = effectDuration / flowerMultiplier;
            }
        }
        else if (flowerIndex == 2) //white flower attack speed
        {
            if (buff == true)
            {
                aps = aps * flowerMultiplier;
            }
            else
            {
                aps = aps / flowerMultiplier;
            }
            //placeholder
        }
        else if (flowerIndex == 3) //pink flower minor income
        {
            if (buff == true)
            {
                generationRate = generationRate * flowerMultiplier;
            }
        }
        else if (flowerIndex == 4) //purple flower major income
        {
            if (buff == true)
            {
                generationRate = generationRate * flowerMultiplier;
            }
        }
        else if (flowerIndex == 5) //gold flower bonus stored nectar
        {
            if (buff == true)
            {
                honeyRate = generationRate * flowerMultiplier;
            }
            else
            {
                honeyRate = generationRate / flowerMultiplier;
            }
        }
        else if (flowerIndex == 6) //red flower damage
        {
            if (buff == true)
            {
                damage = damage * flowerMultiplier;
            }
            else
            {
                damage = damage / flowerMultiplier;
            }
        }
        else if (flowerIndex == 7) //yellow flower range
        {
            if (buff == true)
            {
                targetingRange = targetingRange * flowerMultiplier;
            }
            else
            {
                targetingRange = targetingRange / flowerMultiplier;
            }
        }
        else
        {
            Debug.Log("Flower not found. " + flowerIndex);
        }
        LevelManager.main.CalculateIncome();
    }

    private void Shoot()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        GameObject projectileObj = Instantiate(projectilePrefab, firingPoint.position, targetRotation);
        Projectile projectileScript = projectileObj.GetComponent<Projectile>();
        if (effect == "Ramping")
        {
            projectileScript.SetTarget(target, (damage + damage * ((float)rampCount / GlobalValues.main.rampRatio)), armorPierce, canHit, ignoreTerrain, effect, effectRatio, effectDuration);
            if (rampCount < 10)
            {
                rampCount++;
            }
        }
        else
        {
            projectileScript.SetTarget(target, damage, armorPierce, canHit, ignoreTerrain, effect, effectRatio, effectDuration);
        }
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        if (effect == "Ramping")
        {
            rampCount = 0;
        }
        //Adjust for allowed targets
        if (canHit != "All")
        {
            RaycastHit2D[] hitsNew = new RaycastHit2D[] { };
            if (canHit == "Ground")
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (!Library.main.GetWillFly(hits[i].transform.gameObject))
                    {
                        Array.Resize(ref hitsNew, hitsNew.Length + 1);
                        hitsNew[hitsNew.Length - 1] = hits[i];
                    }
                }
            }
            else if (canHit == "Flying")
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (Library.main.GetWillFly(hits[i].transform.gameObject))
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
            if (targetSetting == targetingOptions[0])
            {
                //Near
                target = hits[0].transform;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (ignoreTerrain == true || !Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))
                    {
                        if (Vector2.Distance(target.position, transform.position) > Vector2.Distance(hits[i].transform.position, transform.position))
                        {
                            target = hits[i].transform;
                        }
                    }
                }
            }
            else if (targetSetting == targetingOptions[1])
            {
                //Far
                target = hits[hits.Length - 1].transform;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (ignoreTerrain == true || !Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))
                    {
                        if (Vector2.Distance(target.position, transform.position) < Vector2.Distance(hits[i].transform.position, transform.position))
                        {
                            target = hits[i].transform;
                        }
                    }
                }
            }
            else if (targetSetting == targetingOptions[2])
            {
                //Weak
                target = hits[0].transform;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (Library.main.GetMaxHP(target.gameObject) > Library.main.GetMaxHP(hits[i].transform.gameObject) && (ignoreTerrain == true || (ignoreTerrain == false && !Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))))
                    {
                        target = hits[i].transform;
                    }
                }
            }
            else if (targetSetting == targetingOptions[3])
            {
                //Strong
                target = hits[0].transform;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (Library.main.GetMaxHP(target.gameObject) < Library.main.GetMaxHP(hits[i].transform.gameObject) && (ignoreTerrain == true || (ignoreTerrain == false && !Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))))
                    {
                        target = hits[i].transform;
                    }
                }
            }
            else if (targetSetting == targetingOptions[4])
            {
                //Ground
                target = hits[0].transform;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (Library.main.GetWillFly(hits[i].transform.gameObject) == false && (ignoreTerrain == true || (ignoreTerrain == false && !Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))))
                    {
                        target = hits[i].transform;
                        return;
                    }
                }
            }
            else if (targetSetting == targetingOptions[5])
            {
                //Flying
                target = hits[0].transform;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (Library.main.GetWillFly(hits[i].transform.gameObject) == true && (ignoreTerrain == true || (ignoreTerrain == false && !Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask))))
                    {
                        target = hits[i].transform;
                        return;
                    }
                }
            }
            else
            {
                target = hits[0].transform;
                Debug.Log("No Targeting Settings Found for " + gameObject.name);
            }
            
        }
    }

    private bool isTargetObstructed()
    {
        return target != null && (Physics2D.Linecast(transform.position, target.transform.position, GlobalValues.main.obstructionMask));
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime * LevelManager.main.timing);
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void Effect()
    {
        if (effect == "Slow Pulse")
        {
            SendSlowPulse();
            //Thread debuffThread = new Thread(new ThreadStart(SlowPulse(hits)));
            //debuffThread.Start();
            timeUntilEffect = GlobalValues.main.slowTimer;
        }
        else if (effect == "Slow Pulse with Freeze")
        {
            SendSlowPulseWithFreeze();
            //Thread debuffThread = new Thread(SlowPulseWithFreeze());
            //debuffThread.Start();
            timeUntilEffect = GlobalValues.main.slowTimer;
        }
        else if (effect == "Damage Buff")
        {
            SendDamageBuff();
            //Thread buffThread = new Thread(DamageBuff);
            //buffThread.Start();
            timeUntilEffect = GlobalValues.main.buffTimer;
        }
        else if (effect == "Damage and Speed Buff")
        {
            SendDamageBuff();
            SendAPSBuff();
            //Thread buffThread = new Thread(DamageAndSpeedBuff);
            //buffThread.Start();
            timeUntilEffect = GlobalValues.main.buffTimer;
        }
        else if (effect == "Heal Queen")
        {
            float missingHP = LevelManager.main.queenBeeMaxHP - LevelManager.main.queenBeeHP;
            if (missingHP > 0)
            {
                float heal = efficiency * GlobalValues.main.healRatio;
                if (heal < missingHP)
                {
                    LevelManager.main.queenBeeHP += heal;
                }
                else
                {
                    LevelManager.main.queenBeeHP = LevelManager.main.queenBeeMaxHP;
                }
            }
            timeUntilEffect = GlobalValues.main.healTimer;
        }
        else
        {
            timeUntilEffect = 10000;
        }
    }

    private void SendDamageBuff()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, towerMask);
        float buff = 1 + (efficiency * GlobalValues.main.damageBuffRatio);
        float duration = GlobalValues.main.buffTimer;
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

    private void SendAPSBuff()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, towerMask);
        float buff = 1 + (efficiency * GlobalValues.main.apsBuffRatio);
        float duration = GlobalValues.main.buffTimer;
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                Turret tur = hit.transform.GetComponent<Turret>();
                tur.ReceiveAPSBuff(buff, duration);
            }
        }
    }

    private void SendSlowPulse()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        float slowRatio = 1f + (effectRatio * GlobalValues.main.slowRatio);
        float slowDuration = effectDuration * GlobalValues.main.slowDurationRatio;
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                Library.main.UpdateSpeed(hits[i].transform.gameObject, slowRatio, slowDuration);
            }
        }
    }

    private void SendSlowPulseWithFreeze()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        float slowRatio = 1f + (effectRatio * GlobalValues.main.slowRatio);
        float slowDuration = effectDuration * GlobalValues.main.slowDurationRatio;
        float freezeRatio = efficiency * GlobalValues.main.freezeRatio;
        int freezeChance = GlobalValues.main.freezeChance;
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                System.Random RandomGen = new System.Random();
                int freezeRoll = RandomGen.Next(freezeChance);
                RaycastHit2D hit = hits[i];
                if (freezeRoll < freezeRatio)
                {
                    Library.main.Freeze(hit.transform.gameObject, slowDuration);
                }
                else
                {
                    Library.main.UpdateSpeed(hit.transform.gameObject, slowRatio, slowDuration);
                }
            }
        }
    }

    private IEnumerator RemoveDamageBuff(float buff, float duration)
    {
        yield return new WaitForSeconds(duration);
        damage = damage / buff;
    }

    private IEnumerator RemoveAPSBuff(float buff, float duration)
    {
        yield return new WaitForSeconds(duration);
        aps = aps / buff;
    }

    public void ReceiveDamageBuff(float buff, float duration)
    {
        damage = damage * buff;
        StartCoroutine(RemoveDamageBuff(buff, duration));           
    }

    public void ReceiveAPSBuff(float buff, float duration)
    {
        aps = aps * buff;      
        StartCoroutine(RemoveAPSBuff(buff, duration));
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

    public void SapFlower(float duration)
    {
        ApplyFlowerBuff(false);
    }

    public void UnsapFlower(float duration)
    {
        ApplyFlowerBuff(true);
    }

    public void IdentifyFlower()
    {
        float range = GlobalValues.main.flowerRange;
        RaycastHit2D[] flowers = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, flowerMask);
        if (flowers.Length > 0)
        {
            flowerIndex = flowers[0].transform.GetComponent<Identify>().ID;
            flowerMultiplier = GlobalValues.main.FLOWERModifier[flowerIndex];
            if (flowers[0].transform.gameObject.GetComponent<Plot>().isSapped == false)
            {
                ApplyFlowerBuff(true);
            }
        }
    }
}
