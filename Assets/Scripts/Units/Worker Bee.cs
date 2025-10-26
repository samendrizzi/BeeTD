using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

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


    public bool isDestroyed = false;
    private GameObject queenBee;
    public GameObject tower;
    public Transform target;
    private Turret turret;
    private float baseSpeed;
    public bool inventoryFull = false;
    public bool isAttacking = false;
    private float timeUntilAttack;
    private bool frozen = false;
    private float timeUntilEffect = 5f;
    private float waypointDistance;
    private float rotationSpeed;


    public int onPath = 1;
    private Transform[] path;


    private void Start()
    {
        rotationSpeed = GlobalValues.main.unitRotationSpeed;
        waypointDistance = GlobalValues.main.unitWaypointDistance;
        int index = GetComponent<Identify>().ID;
        prefab = GlobalValues.main.UNITprefab[index];
        prefab2 = GlobalValues.main.UNITprefab2[index];
        uName = GlobalValues.main.UNITname[index];
        moveSpeed = GlobalValues.main.UNITmoveSpeed[index] * GlobalValues.main.difficultyMultiplier;
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
        RaycastHit2D[] towers = Physics2D.CircleCastAll(transform.position, 0.5f, (Vector2)transform.position, 0f, GlobalValues.main.incomeMask);
        if (towers.Length > 0)
        {
            tower = towers[0].transform.gameObject;
            turret = tower.GetComponent<Turret>();
        }
        else
        {
            Debug.Log("Worker Bee Tower not found.");
        }
        if (hasHealthBar == true)
        {
            healthBar.maxValue = hitPoints;
            healthBar.value = hitPoints;
            healthBar.gameObject.SetActive(true);
        }
        queenBee = LevelManager.main.queenBee.gameObject;
        if (willFly == true)
        {

        }
        target = tower.transform;
    }

    private void Update()
    {

        if (LevelManager.main.levelStarted == false)
        {
            return;
        }
        
        Vector2 direction = (target.position - transform.position).normalized;

        if (frozen == true)
        {
            rb.velocity = direction * 0;
        }
        else
        {
            rb.velocity = direction * moveSpeed * LevelManager.main.timing;
            float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * moveSpeed * Time.deltaTime * LevelManager.main.timing);
        }
        if (isAttacking == true)
        {
            rb.velocity = rb.velocity / 5 * LevelManager.main.timing;
        }

        if (Vector2.Distance(target.position, transform.position) <= waypointDistance && isAttacking == false && frozen == false)
        {
            if (inventoryFull == true && target == queenBee.transform)
            {
                DepositNectar();
                return;
            }
            else if (inventoryFull == false && target == tower.transform)
            {
                CollectNectar();
            }
            else
            {
                Debug.Log("Worker Bee done be confused where to go.");
            }
        }

        if (effect != "none")
        {
            //
        }
        
    }

    public void TakeDamage(float dmg, float armorPierce)
    {
        float armorBlock = (armor - armorPierce) / 100f;
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

    private void AttackQueen()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f && isAttacking == true)
        {
            timeUntilAttack = 0f;
            LevelManager.main.HitQueen(attackDamage);
            TakeDamage(attackDamage * GlobalValues.main.queenThornRatio, 100f);
            isAttacking = false;
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
        LevelManager.main.IncreaseNectar(carryCapacity);
        target = tower.transform;
    }

    public void CollectNectar()
    {
        if (turret.nectar >= carryCapacity)
        {
            turret.nectar -= carryCapacity;
            inventoryFull = true;
            target = queenBee.transform;
        }
    }
}
