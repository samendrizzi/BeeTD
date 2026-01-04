using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using System.Threading;
using System;
using Unity.VisualScripting;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] public Transform firingPoint;
    [SerializeField] public GameObject[] upgradeMatrix;

    [Header("Attribute")]
    private StructureUIHandler UI;
    private Attributes attributes;
    private Plot plot;

    private void Start()
    {
        attributes = gameObject.GetComponent<Attributes>();
        UI = gameObject.GetComponent<StructureUIHandler>();
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 0.1f, (Vector2)transform.position, 0f, GlobalValues.main.plotMask | GlobalValues.main.honeyCombMask | GlobalValues.main.flowerMask);
        plot = hits[0].transform.gameObject.GetComponent<Plot>();
        StartCoroutine(plot.RevealFog(attributes.targetingRange, attributes.ignoreTerrain));
        //Set starting rotation
        if (turretRotationPoint != null)
        {
            Transform start = LevelManager.main.pathsStart[0];
            if (LevelManager.main.pathsStart.Length > 1)
            {
                foreach (Transform nextPath in LevelManager.main.pathsStart)
                { 
                    {
                        if (Vector2.Distance(start.position, transform.position) < Vector2.Distance(nextPath.position, transform.position))    
                        {
                            start = nextPath;
                        }
                    }
                }
            }
            float angle = Mathf.Atan2(start.position.y - transform.position.y, start.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
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
        CheckTarget(attributes.targetingRange);
        RotateTowardsTarget();
    }

    private bool isTargetObstructed()
    {
        return attributes.target != null && (Physics2D.Linecast(transform.position, attributes.target.transform.position, GlobalValues.main.obstructionMask));
    }

    private bool CheckTargetIsInRange(float range)
    {
        return Vector2.Distance(attributes.target.position, transform.position) <= range;
    }

    public void CheckTarget(float range)
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
            attributes.target = hits[0].transform;
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

    private void RotateTowardsTarget()
    {
        if (attributes.target != null && turretRotationPoint != null)
        {
            float angle = Mathf.Atan2(attributes.target.position.y - transform.position.y, attributes.target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, attributes.rotationSpeed * GlobalValues.main.deltaTime);
        }
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
