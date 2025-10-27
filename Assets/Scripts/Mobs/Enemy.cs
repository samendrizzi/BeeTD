using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static Enemy main;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Slider healthBar;
    private GameObject prefab;
    private GameObject prefab2;

    [Header("Attributes")]
    public string eName;
    public float moveSpeed;
    public bool willFly;
    public int carryCapacity;
    public bool willStealNectar;
    public bool willAttack;
    public float attackDamage;
    public float attackRate;
    public float hitPoints = 2;
    public float maxHP;
    public float armor = 0f;
    private float bounty = 0;
    private bool hasHealthBar = false;
    public string effect;
    public float effectModifier;
    public bool willStealHoney = false;


    public bool isDestroyed = false;

    public Transform target;
    public int pathIndex = 0;
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
        rotationSpeed = GlobalValues.main.enemyRotationSpeed;
        waypointDistance = GlobalValues.main.enemyWaypointDistance;
        int index = GetComponent<Identify>().ID;
        prefab = GlobalValues.main.ENEMYprefab[index];
        prefab2 = GlobalValues.main.ENEMYprefab2[index];
        eName = GlobalValues.main.ENEMYname[index];
        moveSpeed = GlobalValues.main.ENEMYmoveSpeed[index] * GlobalValues.main.difficultyMultiplier;
        willFly = GlobalValues.main.ENEMYwillFly[index];
        carryCapacity = GlobalValues.main.ENEMYcarryCapacity[index];
        willStealNectar = GlobalValues.main.ENEMYwillStealNectar[index];
        willAttack = GlobalValues.main.ENEMYwillAttack[index];
        attackDamage = GlobalValues.main.ENEMYattackDamage[index];
        attackRate = GlobalValues.main.ENEMYattackRate[index];
        effect = GlobalValues.main.ENEMYeffect[index];
        effectModifier = GlobalValues.main.ENEMYeffectModifier[index] * GlobalValues.main.difficultyMultiplier;
        willStealHoney = GlobalValues.main.ENEMYwillStealHoney[index];
        baseSpeed = moveSpeed;
        hitPoints = GlobalValues.main.ENEMYhitPoints[index] * GlobalValues.main.difficultyMultiplier;
        armor = GlobalValues.main.ENEMYarmor[index];
        maxHP = hitPoints;
        bounty = GlobalValues.main.ENEMYbounty[index];
        hasHealthBar = GlobalValues.main.ENEMYhealthBar[index];
        if (hasHealthBar == true)
        {
            healthBar.maxValue = hitPoints;
            healthBar.value = hitPoints;
            healthBar.gameObject.SetActive(true);
        }
        if (willFly == true)
        {
            if (onPath == 1)
            {
                path = LevelManager.main.flyingPath1;
            }
            else
            {
                path = LevelManager.main.flyingPath2;
            }

        }
        else
        {
            if (onPath == 1)
            {
                path = LevelManager.main.path1;
            }
            else
            {
                path = LevelManager.main.path2;
            }

        }
        WaveSpawner.main.EnemySpawned();
        if (path != null)
        {
            target = path[pathIndex];
        }
    }

    private void Update()
    {
        
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * moveSpeed * Time.deltaTime);
        }
        if (isAttacking == true)
        {
            rb.velocity = rb.velocity / 5 * LevelManager.main.timing;
        }

        if (Vector2.Distance(target.position, transform.position) <= waypointDistance && isAttacking == false && frozen == false)
        {
            if (inventoryFull == true && pathIndex == 0)
            {
                Heal(carryCapacity * GlobalValues.main.honeyHealRatio);
                inventoryFull = false;
                return;
            }
            else if (pathIndex == path.Length - 1)
            {
                if ((willStealHoney == false && willAttack == true) || (willAttack == true && LevelManager.main.honey < carryCapacity))
                {
                    isAttacking = true;
                    target = LevelManager.main.queenBee;
                    return;
                }
                else if (willStealHoney == true && LevelManager.main.honey >= carryCapacity)
                {
                    inventoryFull = true;
                    if (LevelManager.main.honey >= carryCapacity)
                    {
                        LevelManager.main.honey = LevelManager.main.honey - carryCapacity;
                    }
                    else
                    {
                        //Will not steal nectar atm
                        //LevelManager.main.honey -= carryCapacity - LevelManager.main.nectar;
                        //LevelManager.main.nectar = 0;
                    }

                }
            }
            if (isAttacking == false)
            {
                if (inventoryFull == false)
                {
                    pathIndex++;
                }
                else
                {
                    pathIndex--;
                }
                target = path[pathIndex];
            }
        }

        if (isAttacking == true && frozen == false)
        {
            timeUntilAttack += Time.deltaTime;

            if (timeUntilAttack >= 1f / attackRate)
            {
                AttackQueen();
            }
        }
        if (effect != "none")
        {
            timeUntilEffect -= Time.deltaTime;
            if (timeUntilEffect <= 0)
            {
                ActivateEffect();
            }
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
        StartCoroutine(ResetEnemySpeed(slowEffect, duration));
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

    private IEnumerator ResetEnemySpeed(float slowEffect, float duration)
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

    private void LayEgg(float duration)
    {
        Transform start = path[pathIndex];
        Transform nextPoint = target;
        GameObject prefabToSpawn = prefab2;
        GameObject egg = Instantiate(prefabToSpawn, gameObject.transform.position, Quaternion.identity);
        egg.GetComponent<Enemy>().HatchEgg(onPath, pathIndex, duration);
    }

    public void HatchEgg(int ONPATH, int PATHINDEX, float duration)
    {
        StartCoroutine(Hatching(ONPATH, PATHINDEX, duration));
    }

    private IEnumerator Hatching(int ONPATH, int PATHINDEX, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (isDestroyed == false)
        {
            GameObject prefabToSpawn = prefab2;
            GameObject hatch = Instantiate(prefabToSpawn, gameObject.transform.position, Quaternion.identity);
            hatch.GetComponent<Enemy>().onPath = ONPATH;
            hatch.GetComponent<Enemy>().pathIndex = PATHINDEX;
            hatch.GetComponent<Enemy>().target = path[PATHINDEX];
            Die();
        }
    }

    private void HealAura()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, GlobalValues.main.enemyHealRange, (Vector2)transform.position, 0f, GlobalValues.main.enemyMask);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                Library.main.Heal(hits[i].transform.gameObject, GlobalValues.main.healAuraRatio * effectModifier);
            }
        }
    }

    private void ActivateEffect()
    {
        if (effect == "Lay Ant Egg")
        {
            LayEgg(GlobalValues.main.eggHatchingTimer);
            timeUntilEffect = GlobalValues.main.eggLayingTimer / effectModifier;
        }
        else if (effect == "Lay Wasp Egg")
        {
            LayEgg(GlobalValues.main.eggHatchingTimer);
            timeUntilEffect = GlobalValues.main.eggLayingTimer / effectModifier;
        }
        else if (effect == "Heal Aura")
        {
            HealAura();
            timeUntilEffect = GlobalValues.main.healAuraTimer / effectModifier;
        }
        else
        {
            Debug.Log(name + " does not have an effect.");
            timeUntilEffect = 10000f;
        }
    }

    public void Die()
    {
        if (isDestroyed == false)
        {
            isDestroyed = true;
            LevelManager.main.IncreaseNectar(bounty);
            if (inventoryFull)
            {
                LevelManager.main.honey += carryCapacity * GlobalValues.main.honeyDropModifier;
            }
            WaveSpawner.main.EnemyDestroyed();
            Destroy(gameObject);
        }
    }
}
