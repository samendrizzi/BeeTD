using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Slider healthBar;
    private GameObject prefab;
    private GameObject prefab2;
    private GameObject prefab3;

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
    public float armor = 0f;
    public float maxHP;
    private float bounty = 0;
    private bool hasHealthBar = false;
    public string effect;
    public float effectModifier;
    public bool willStealHoney = true;


    public bool isDestroyed = false;

    private Transform target;
    private LayerMask flowerMask;
    private int pathIndex = 0;
    private float baseSpeed;
    public bool inventoryFull = false;
    public bool isAttacking = false;
    private float timeUntilAttack;
    private bool frozen = false;
    private float timeUntilEffect = 5f;
    //private bool arrived = false;

    public int onPath = 1;
    private Transform[] path;

    // Start is called before the first frame update
    void Start()
    {
        flowerMask = GlobalValues.main.flowerMask;
        int index = GetComponent<Identify>().ID;
        prefab = GlobalValues.main.BOSSprefab[index];
        prefab2 = GlobalValues.main.BOSSprefab2[index];
        prefab3 = GlobalValues.main.BOSSprefab3[index];
        eName = GlobalValues.main.BOSSname[index];
        moveSpeed = GlobalValues.main.BOSSmoveSpeed[index] * GlobalValues.main.difficultyMultiplier;
        willFly = GlobalValues.main.BOSSwillFly[index];
        carryCapacity = GlobalValues.main.BOSScarryCapacity[index];
        willStealNectar = GlobalValues.main.BOSSwillStealNectar[index];
        willAttack = GlobalValues.main.BOSSwillAttack[index];
        attackDamage = GlobalValues.main.BOSSattackDamage[index];
        attackRate = GlobalValues.main.BOSSattackRate[index];
        effect = GlobalValues.main.BOSSeffect[index];
        effectModifier = GlobalValues.main.BOSSeffectModifier[index] * GlobalValues.main.difficultyMultiplier;
        willStealHoney = GlobalValues.main.BOSSwillStealHoney[index];
        baseSpeed = moveSpeed;
        hitPoints = GlobalValues.main.BOSShitPoints[index] * GlobalValues.main.difficultyMultiplier;
        armor = GlobalValues.main.BOSSarmor[index];
        maxHP = hitPoints;
        bounty = GlobalValues.main.BOSSbounty[index];
        hasHealthBar = GlobalValues.main.BOSShealthBar[index];
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
        if (effect != "Hummingbird")
        {
            target = path[pathIndex];
        }
        else
        {
            timeUntilEffect = 0f;
            Hummingbird();
        }
        WaveSpawner.main.EnemySpawned();
    }

    // Update is called once per frame
    void Update()
    {
        
        timeUntilEffect -= Time.deltaTime;
        timeUntilAttack += Time.deltaTime;
        if (effect == "Hummingbird" && timeUntilEffect <= 0f)
        {
            Move();
        }
        else if (effect == "Skunk")
        {
            if (timeUntilEffect < 0f)
            {
                timeUntilEffect = GlobalValues.main.skunkSprayTimer;
                StartCoroutine(SkunkSpray());
            }
            if (timeUntilAttack >= 1f / attackRate)
            {
                timeUntilAttack = 0f;
                Mite();
            }
            Move();
        }
        else if (effect != "Hummingbird")
        {
            Move();
        }   
    }

    private void Move()
    {
        if (Vector2.Distance(target.position, transform.position) >= 0.1f)
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
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 150 * moveSpeed * Time.deltaTime);
            }
        }
        else if (target != null)
        {
            AtTarget();
        }
    }

    private void AtTarget()
    {
        //Stop Moving
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * 0;

        if (frozen == true)
        {
            return;
        }

        if (effect == "Hummingbird")
        {
            Hummingbird();
        }
        else if (isAttacking == false)
        {
            if (inventoryFull == false)
            {
                if (pathIndex == path.Length - 1)
                {
                    isAttacking = true;
                }
                else
                {
                    pathIndex++;
                    target = path[pathIndex];
                }
            }
            else
            {
                if (pathIndex == 0)
                {
                    inventoryFull = false;
                    Heal(carryCapacity * GlobalValues.main.honeyHealRatio);
                }
                else
                {
                    pathIndex--;
                    target = path[pathIndex];
                }
            }

        }
        else if (isAttacking == true)
        {
            if (effect == "Skunk" && timeUntilAttack == 0f)
            {
                StealHoney();
            }
            else if (effect == "Chicken" && timeUntilAttack >= 1f / attackRate)
            {
                timeUntilAttack = 0f;
                StealHoney();
            }
        }
    }

    public void HealthBar()
    {
        if (hasHealthBar == true)
        {
            healthBar.value = hitPoints;
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

    //private void ActivateEffect()
    //{
        //if (effect == "Lay Ant Egg")
        //{
            //LayEgg(GlobalValues.main.eggHatchingTimer);
            //timeUntilEffect = GlobalValues.main.eggLayingTimer / effectModifier;
        //}
        //else if (effect == "Lay Wasp Egg")
        //{
            //LayEgg(GlobalValues.main.eggHatchingTimer);
           //timeUntilEffect = GlobalValues.main.eggLayingTimer / effectModifier;
        //}
        //else if (effect == "Heal Aura")
        //{
            //HealAura();
            //timeUntilEffect = GlobalValues.main.healAuraTimer / effectModifier;
        //}
        //else
        //{
            //Debug.Log(name + " does not have an effect.");
            //timeUntilEffect = 10000f;
        //}
    //}

    private void Hummingbird()
    {
        if (target == null)
        {
            FindRandomFlower();
        }
        else if (Vector2.Distance(target.position, transform.position) <= 0.1f && isAttacking == false && frozen == false)
        {
            timeUntilEffect = GlobalValues.main.hummingbirdWaitTime;
            StartCoroutine(InspectFlower(target));
            FindRandomFlower();
        }
    }

    private void FindRandomFlower()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, GlobalValues.main.hummingbirdRange, (Vector2)transform.position, 0f, flowerMask);
        Transform[] unsappedHits = new Transform[] {};
        int counter = 0;
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.gameObject.GetComponent<Plot>().isSapped == false && hits[i].transform.gameObject.GetComponent<Plot>().hasBloomed == true)
                {
                    counter++;
                    Array.Resize(ref unsappedHits, counter);
                    unsappedHits[counter - 1] = hits[i].transform;
                }
            }
            if (unsappedHits.Length > 0)
            {
                System.Random RandomGen = new System.Random();
                int randompick = RandomGen.Next(unsappedHits.Length - 1);
                target = unsappedHits[randompick];
            }
        }
        if (unsappedHits.Length == 0)
        {
            timeUntilEffect = GlobalValues.main.hummingbirdWaitTime;
        }    
    }

    private void SapFlower(Transform targetFlower)
    {
        if ((((1 << target.gameObject.layer) & flowerMask) != 0) && target.gameObject.GetComponent<Plot>().isSapped == false)
        {
            targetFlower.GetComponent<Plot>().SapFlower(GlobalValues.main.hummingbirdSapTime);
        }
    }

    private IEnumerator InspectFlower(Transform targetFlower)
    {
        yield return new WaitForSeconds(GlobalValues.main.hummingbirdWaitTime);
        //Check if still alive
        if (gameObject != null)
        {
            SapFlower(targetFlower);
        }
    }

    private void StealHoney()
    {
        if (LevelManager.main.honey > carryCapacity)
        {
            isAttacking = false;
            LevelManager.main.honey -= carryCapacity;
            inventoryFull = true;
        }
        else
        {
            AttackQueen();
        }
    }

    private IEnumerator SkunkSpray()
    {
        GameObject prefabToSpawn = prefab2;
        GameObject spray = Instantiate(prefabToSpawn, gameObject.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(GlobalValues.main.skunkSprayDuration);
        Destroy(spray);
    }

    private void Mite()
    {
        Transform[] mitePath = path;
        Transform start = mitePath[0];
        int index = 0;
        for (int i = 0; i < mitePath.Length; i++)
        {
            if (Vector2.Distance(mitePath[i].position, transform.position) < Vector2.Distance(start.position, transform.position))
            {
                start = mitePath[i];
                index = i;
            }
        }
        GameObject prefabToSpawn = prefab3;
        GameObject mite = Instantiate(prefabToSpawn, start.position, Quaternion.identity);
        mite.GetComponent<Enemy>().target = start;
        mite.GetComponent<Enemy>().pathIndex = index;
    }

    public void Die()
    {
        if (isDestroyed == false)
        {
            isDestroyed = true;
            LevelManager.main.IncreaseNectar(bounty);
            if (inventoryFull == true)
            {
                LevelManager.main.honey += carryCapacity * GlobalValues.main.honeyDropModifier;
            }
            WaveSpawner.main.EnemyDestroyed();
            Destroy(gameObject);
        }
    }

}
