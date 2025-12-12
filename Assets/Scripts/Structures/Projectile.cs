using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float projectileSpeed = 5f;
    private LayerMask enemyMask;
    private LayerMask obstructionMask;

    [Header("Attributes")]
    private float projectileDamage = 1f;
    private float armorPierce = 0f;
    private float resistancePierce = 0f;
    private float effectPower = 0f;
    private float AoE = 0f;
    private float slow = 0f;
    private float slowDuration = 0f;
    private int numberRicochet = 0;
    private bool ignoreTerrain = false;
    //private float freeze = 0f;
    private string action;
    private float actionPowerModifier;
    private float actionDuration;
    private string canHit = "All";
    private bool AoEDropOff = false;
    private Transform target;
    private bool isDestroyed = false;

    public void Start()
    {
        enemyMask = GlobalValues.main.enemyMask;
        obstructionMask = GlobalValues.main.obstructionMask;
    }

    public void SetTarget(Transform _target, float damage, float effPower, float armor, float resistance, string canHiter, bool igTerrain, string actioner, float actionDur, float actionAoE)
    {
        target = _target;
        projectileDamage = damage;
        armorPierce = armor;
        resistancePierce = resistance;
        canHit = canHiter;
        ignoreTerrain = igTerrain;
        action = actioner;
        actionDuration = actionDur;
        AoE = actionAoE;
        effectPower = effPower;
        if (action == "Shoot Slowing" || action == "Shoot Freezing" || action == "Shoot AoE Freezing" || action == "Shoot AoE Slowing")
        {
            slow = 1 + (effectPower * GlobalValues.main.slowPowerModifier);
            slowDuration = actionDuration * GlobalValues.main.slowDurationModifier;
        }
        else if (action == "Shoot Ricochet")
        {
            numberRicochet = (int)actionDuration;
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
        else if (Vector2.Distance(gameObject.transform.position, target.position) <= GlobalValues.main.projectileCollisonDistance)
        {
            Hit(target.gameObject);
        }
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * projectileSpeed;
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 500 * Time.deltaTime);   
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Hit(other.gameObject);
    }

    private void Hit(GameObject other)
    {
        if (((1 << other.layer) & enemyMask) == 0 || other.transform != target || (((1 << other.layer) & obstructionMask) != 0 && ignoreTerrain == false))
        {
            if (((1 << other.layer) & obstructionMask) != 0 && ignoreTerrain == false)
            {
                HitObstruction(other);
            }
            return;
        }
        else if (canHit != "All")
        {
            if ((canHit == "Flying" && !other.GetComponent<Attributes>().willFly) || ((canHit == "Ground" && other.GetComponent<Attributes>().willFly))) 
            {
                return;
            }
        }
        if (AoE == 0 && isDestroyed == false)
        {
           other.GetComponent<Attributes>().TakeDamage(projectileDamage, armorPierce);
            if (action == "Shoot Slowing")
            {
                other.GetComponent<Attributes>().SlowSpeed(slow, resistancePierce, slowDuration);
            }
            else if (action == "Shoot Freezing")
            {
                other.GetComponent<Attributes>().Freeze(slow, resistancePierce, slowDuration);
            }
            else if (action == "Shoot Ricochet")
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
                    if (!hits[i].transform.gameObject.GetComponent<Attributes>().willFly)
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
                        if (!hits[i].transform.gameObject.GetComponent<Attributes>().willFly)
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
                    hit.transform.gameObject.GetComponent<Attributes>().TakeDamage(damageDrop, armorPierce);
                    if (action == "Shoot AoE Slowing")
                    {
                        hit.transform.gameObject.GetComponent<Attributes>().SlowSpeed(slowDrop, resistancePierce, slowDuration);
                    }
                    else if (action == "Shoot AoE Freezing")
                    {
                        hit.transform.gameObject.GetComponent<Attributes>().Freeze(slowDrop, resistancePierce, slowDuration);
                    }
                }
            }
        }
    }

    private void HitObstruction(GameObject other)
    {
        if (isDestroyed == false)
        {
            if (other.GetComponent<Plot>() != null)
            {
                if (other.GetComponent<Plot>().isDestructible == true)
                {
                    other.GetComponent<Plot>().Hit(projectileDamage, armorPierce);
                }
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
