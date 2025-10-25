using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    private LayerMask enemyMask;
    private LayerMask obstructionMask;

    [Header("Attributes")]
    private float projectileSpeed;
    private float projectileDamage;
    private float armorPierce;
    private float AoE = 0f;
    private float slow = 0f;
    private float slowDuration = 0f;
    private float ricochet = 0f;
    private int numberRicochet = 0;
    private bool ignoreTerrain = false;
    //private float freeze = 0f;
    private string effect;
    private string canHit = "All";
    private bool AoEDropOff = false;

    private Transform target;
    private bool isDestroyed = false;

    public void Start()
    {
        int index = GetComponent<Identify>().ID;
        enemyMask = GlobalValues.main.PROJECTILEenemyMask[index];
        obstructionMask = GlobalValues.main.obstructionMask;
        projectileSpeed = GlobalValues.main.PROJECTILEprojectileSpeed[index];
        AoE = GlobalValues.main.PROJECTILEAoE[index];
        AoEDropOff = GlobalValues.main.PROJECTILEAoEDropOff[index];
    }

    public void SetTarget(Transform _target, float damage, float armor, string canHiter, bool igTerrain, string effecter, float effectRatio, float effectDuration)
    {
        target = _target;
        projectileDamage = damage;
        armorPierce = armor;
        canHit = canHiter;
        ignoreTerrain = igTerrain;
        effect = effecter;
        if (effect == "Slow Projectile" || effect == "Freeze & Slow Projectile")
        {
            slow = 1 + (effectRatio * GlobalValues.main.slowRatio);
            slowDuration = effectDuration * GlobalValues.main.slowDurationRatio;
        }
        else if (effect == "Ricochet")
        {
            ricochet = effectRatio;
            numberRicochet = (int)effectDuration;
        }
    }

    private void Update()
    {
        
        if (!target)
        {
            isDestroyed = true;
            Destroy(gameObject);
            return;
        }
        else if (Vector2.Distance(gameObject.transform.position, target.position) <= 0.1f)
        {
            Hit(target.gameObject);
        }

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * projectileSpeed * LevelManager.main.timing;

        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 500 * Time.deltaTime * LevelManager.main.timing);
          
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Hit(other.gameObject);
    }

    private void Hit(GameObject other)
    {
        if (((1 << other.layer) & enemyMask) == 0 || (ricochet > 0 && other.transform != target) || (((1 << other.layer) & obstructionMask) != 0 && ignoreTerrain == false))
        {
            if (((1 << other.layer) & obstructionMask) != 0 && ignoreTerrain == false)
            {
                HitObstruction(other);
            }
            return;
        }
        else if (canHit != "All")
        {
            if ((canHit == "Flying" && !Library.main.GetWillFly(other)) || ((canHit == "Ground" && Library.main.GetWillFly(other)))) 
            {
                return;
            }
        }
        if (AoE == 0 && isDestroyed == false && Library.main.IsDestroyed(other) == false)
        {
            Library.main.TakeDamage(other, projectileDamage, armorPierce);
            if (effect == "Slow Projectile")
            {
                Library.main.UpdateSpeed(other, slow, slowDuration);
            }
            else if (effect == "Freeze & Slow Projectile")
            {
                System.Random RandomGen = new System.Random();
                int freezeRoll = RandomGen.Next(GlobalValues.main.freezeChance);
                if (slow > freezeRoll)
                {
                    Library.main.Freeze(other, slowDuration);
                }
                else
                {
                    Library.main.UpdateSpeed(other, slow, slowDuration);
                }
                
            }
            else if (effect == "Ricochet")
            {
                Ricochet();
                return;
            }
        }
        else if (isDestroyed == false)
        {
            Explode();
        }
        isDestroyed = true;
        Destroy(gameObject);
    }

    private void Ricochet()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, GlobalValues.main.ricochetRange, (Vector2)transform.position, 0f, enemyMask);
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
            if (target != hits[0].transform)
            {
                target = hits[0].transform;
            }
            else if (hits.Length > 1)
            {
                target = hits[1].transform;
            }
            else
            {
                isDestroyed = true;
                Destroy(gameObject);
            }
        }
        else
        {
            isDestroyed = true;
            Destroy(gameObject);
        }
        numberRicochet--;
        if (numberRicochet <= 0)
        {
            isDestroyed = true;
            Destroy(gameObject);
        }
        projectileDamage = projectileDamage * ricochet;
        projectileSpeed = projectileSpeed * 0.8f;
        if (projectileSpeed < 5f)
        {
            projectileSpeed = 5f;
        }
    }

    private void Explode()
    {
        if (isDestroyed == false)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, AoE, (Vector2)transform.position, 0f, enemyMask);
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
                System.Random RandomGen = new System.Random();
                int freezeRoll = RandomGen.Next(GlobalValues.main.freezeChance);
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit2D hit = hits[i];
                    float damageDrop = projectileDamage;
                    float slowDrop = slow;
                    if (AoEDropOff == true)
                    {
                        float distance = Vector2.Distance(hit.transform.position, transform.position);
                        damageDrop = (AoE - distance * (1 - GlobalValues.main.AoeDropOffFloor)) * projectileDamage;
                        slowDrop = (AoE - distance * (1 - GlobalValues.main.AoeDropOffFloor)) * slow;
                    }
                    Library.main.TakeDamage(hit.transform.gameObject, damageDrop, armorPierce);
                    if (effect == "Slow Projectile")
                    {
                        Library.main.UpdateSpeed(hit.transform.gameObject, slowDrop, slowDuration);
                    }
                    else if (effect == "Freeze & Slow Projectile")
                    {
                        if (i == 0 && slow > (freezeRoll / 2))
                        {
                            Library.main.Freeze(hit.transform.gameObject, slowDuration);
                        }
                        else
                        {
                            Library.main.UpdateSpeed(hit.transform.gameObject, slowDrop, slowDuration);
                        }              
                    }
                }
            }
        }
    }

    private void HitObstruction(GameObject other)
    {
        if (isDestroyed == false)
        {
            if (other.GetComponent<Plot>().isDestructible == true)
            {
                other.GetComponent<Plot>().Hit(projectileDamage, armorPierce);
            }
            if (AoE > 0)
            {
                Explode();
            }
            if (numberRicochet > 0)
            {
                Ricochet();
            }
            else
            {
                isDestroyed = true;
                Destroy(gameObject);
            }
        }
    }

}
