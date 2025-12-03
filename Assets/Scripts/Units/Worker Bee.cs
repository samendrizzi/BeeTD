using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class WorkerBee : MonoBehaviour
{
    public static Enemy main;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Slider healthBar;
    private GameObject prefab;
    private GameObject prefab2;

    [Header("Attributes")]
    public string uName;
    public float moveSpeed;
    public bool willFly;
    public int carryCapacity;
    public bool willAttack;
    public float attackDamage;
    public float attackRate;
    public float hitPoints = 2;
    public float maxHP;
    public float armor = 0f;
    private bool hasHealthBar = false;
    public string effect;
    public float effectModifier;
    public bool willStealNectar;
    public bool willStealHoney = false;
    public float nectar = 0f;


    public bool isDestroyed = false;
    private GameObject queenBee;
    public GameObject flower;
    public Transform target;
    private Plot F;
    public float baseSpeed;
    public bool inventoryFull = false;
    public bool isAttacking = false;
    private float timeUntilAttack;
    private bool frozen = false;
    private float timeUntilEffect = 0f;
    private float waypointDistance;
    private float rotationSpeed;
    public string work = "unassigned";
    private float armorPierce = 15f;
    private LayerMask enemyMask;

    public int onPath = 1;
    private Transform[] path;


    private void Start()
    {
        //setup
        rotationSpeed = GlobalValues.main.unitRotationSpeed;
        waypointDistance = GlobalValues.main.unitWaypointDistance;
        int index = GetComponent<Identify>().ID;
        prefab = GlobalValues.main.UNITprefab[index];
        prefab2 = GlobalValues.main.UNITprefab2[index];
        uName = GlobalValues.main.UNITname[index];
        moveSpeed = GlobalValues.main.UNITmoveSpeed[index];
        willFly = GlobalValues.main.UNITwillFly[index];
        carryCapacity = GlobalValues.main.UNITcarryCapacity[index];
        willAttack = GlobalValues.main.UNITwillAttack[index];
        attackDamage = GlobalValues.main.UNITattackDamage[index];
        attackRate = GlobalValues.main.UNITattackRate[index];
        effect = GlobalValues.main.UNITeffect[index];
        effectModifier = GlobalValues.main.UNITeffectModifier[index] * GlobalValues.main.difficultyMultiplier;
        baseSpeed = moveSpeed;
        hitPoints = GlobalValues.main.UNIThitPoints[index] * GlobalValues.main.difficultyMultiplier;
        armor = GlobalValues.main.UNITarmor[index];
        maxHP = hitPoints;
        hasHealthBar = GlobalValues.main.UNIThealthBar[index];
        willStealHoney = GlobalValues.main.UNITwillStealHoney[index];
        willStealNectar = GlobalValues.main.UNITwillStealNectar[index];
        enemyMask = GlobalValues.main.enemyMask;
        if (hasHealthBar == true)
        {
            healthBar.maxValue = hitPoints;
            healthBar.value = hitPoints;
            healthBar.gameObject.SetActive(true);
        }
        queenBee = LevelManager.main.queenBee.gameObject;
        //track bees
        LevelManager.main.OrganizeBees();
    }

    private void Update()
    {

        if (LevelManager.main.levelStarted == false || work == "unassigned")
        {
            return;
        }
        
        //check if frozen
        if (frozen == true)
        {
            ResetBee();
            return;
        }

        //Check if bee has target
        if (work == "soldier" && target == null)
        {
            ResetBee();
            //prevent constant looping when no enemies are spawned
            if (timeUntilAttack > 0f)
            {
                timeUntilAttack -= Time.deltaTime;
                timeUntilEffect -= Time.deltaTime;
                return;
            }
            //find enemy nearest to queen
            RaycastHit2D[] hits = Physics2D.CircleCastAll(LevelManager.main.queenBee.transform.position, 300f, (Vector2)LevelManager.main.queenBee.transform.position, 0f, enemyMask);
            if (hits.Length > 0)
            {
                target = hits[0].transform;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (Vector2.Distance(target.position, LevelManager.main.queenBee.transform.position) > Vector2.Distance(hits[i].transform.position, LevelManager.main.queenBee.transform.position))
                    {
                        target = hits[i].transform;
                    }
                }
            }
            else
            {
                //pause checking for targets
                timeUntilAttack = 3f;
                return;
            }
        }

        Vector2 direction = (target.position - transform.position).normalized;

        if (timeUntilAttack > 0 || timeUntilEffect > 0 )
        {
            ResetBee();
            timeUntilAttack -= Time.deltaTime;
            timeUntilEffect -= Time.deltaTime;
            return;
        }
        else
        {
            rb.velocity = direction * moveSpeed;
            float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * moveSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(target.position, transform.position) <= waypointDistance)
        {
            if (work == "nectar")
            {
                if (inventoryFull == true && target == queenBee.transform)
                {
                    DepositNectar();
                    return;
                }
                else if (inventoryFull == false && target == flower.transform)
                {
                    CollectNectar(flower);
                }
                else
                {
                    Debug.Log("Worker Bee done be confused where to go.");
                }
            }
            else if (work == "honey")
            {
                if (timeUntilEffect <= 0)
                {
                    timeUntilEffect = 1f;
                    CreateHoney();
                }
                else
                {
                    timeUntilEffect -= Time.deltaTime;
                }
            }
            else if (work == "soldier")
            {
                AttackTarget();
            }
        }

        //if (effect != "none")
        //{
            //
        //}
        
    }

    public void TakeDamage(float dmg, float AP)
    {
        float armorBlock = (armor - AP) / 100f;
        if (armorBlock < 0)
        {
            armorBlock = 0f;
        }
        hitPoints -= (1f - armorBlock) * dmg;
        HealthBar();
        if (hitPoints <= 0 && !isDestroyed)
        {
            Die();
        }
    }

    public void HealthBar()
    {
        if (hasHealthBar == true)
        {
            healthBar.value = hitPoints;
        }
    }

    public void Heal(float heal)
    {
        if (heal == 0f || (heal + hitPoints) >= maxHP)
        {
            hitPoints = maxHP;
        }
        else
        {
            hitPoints += heal;
        }
        HealthBar();
    }

    public void UpdateSpeed(float slowEffect, float duration)
    {
        moveSpeed = moveSpeed / slowEffect;
        if (moveSpeed < GlobalValues.main.maxSlowDebuff * baseSpeed)
        {
            moveSpeed = GlobalValues.main.maxSlowDebuff * baseSpeed;
        }
        StartCoroutine(ResetUnitSpeed(slowEffect, duration));
    }

    public void Freeze(float duration)
    {
        frozen = true;
        StartCoroutine(Unfreeze(duration));
    }

    public void ReturnNectar()
    {
        if (inventoryFull == true)
        {
            LevelManager.main.nectar = LevelManager.main.nectar + carryCapacity;
        }         
    }

    private void AttackTarget()
    {
        if (timeUntilAttack <= 0)
        {
            timeUntilAttack = 1 / attackRate;
            Library.main.TakeDamage(target.gameObject, attackDamage, armorPierce);
        }
    }

    private IEnumerator ResetUnitSpeed(float slowEffect, float duration)
    {
        yield return new WaitForSeconds(duration);
        moveSpeed = moveSpeed * slowEffect;
        if (moveSpeed > baseSpeed)
        {
            moveSpeed = baseSpeed;
        }
    }

    private IEnumerator Unfreeze(float duration)
    {
        yield return new WaitForSeconds(duration);
        frozen = false;
    }

    public void Die()
    {
        if (isDestroyed == false)
        {
            isDestroyed = true;
            if (inventoryFull)
            {
                //drop nectar
            }
            Destroy(gameObject);
        }
    }

    public void DepositNectar()
    {
        Heal(carryCapacity * GlobalValues.main.workerHealRatio);
        inventoryFull = false;
        LevelManager.main.IncreaseNectar(nectar);
        nectar = 0f;
        target = flower.transform;
    }

    public void CollectNectar(GameObject f)
    {
        if (f.GetComponent<Plot>().isSapped == false)
        {
            nectar = carryCapacity;
            f.GetComponent<Plot>().SapFlower(carryCapacity / LevelManager.main.nectarGenerationRate);
            inventoryFull = true;
            target = queenBee.transform;
        }
        else
        {
            timeUntilAttack = 0.25f;
        }
    }

    public void CreateHoney()
    {
        if (LevelManager.main.finalWave == false)
        {
            LevelManager.main.IncreaseHoney(effectModifier);
        }
        timeUntilEffect = 1f;
    }

    public void ResetBee() 
    {
        rb.velocity = Vector2.zero;
    }

}
