using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static Enemy main;

    [Header("References")]

    [Header("Attributes")]

    //trackers
    private Attributes attributes;

    private void Start()
    {
        //setup
        WaveSpawner.main.EnemySpawned();
        attributes = gameObject.GetComponent<Attributes>();
        SetPathSettings();
    }

    private void Update()
    {
        if (attributes.frozen == true || attributes.pausing > 0f)
        {
            return;
        }
        if (attributes.sName == "Hummingbird")
        {
            UniqueMove();
        }
        else if (attributes.sName != "Skunk Spray")
        {
            Move();
        }
    }

    private void Move()
    {
        //check pathing
        if (Vector2.Distance(attributes.target.position, transform.position) <= attributes.wayPointDistance)
        {
            if ((attributes.inventoryFull == false) && (attributes.pathIndex != (attributes.path.Length - 1)))
            {
                attributes.pathIndex++;
            }
            else if (attributes.inventoryFull == true && attributes.pathIndex != 0)
            {
                attributes.pathIndex--;
            }
            attributes.target = attributes.path[attributes.pathIndex];
        }
        //set velocity
        Vector2 direction = (attributes.target.position - transform.position).normalized;
        attributes.rb.linearVelocity = direction * attributes.moveSpeed;
        //set rotation
        if (attributes.rotationSpeed > 0)
        {
        float angle = Mathf.Atan2(attributes.target.position.y - transform.position.y, attributes.target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, attributes.rotationSpeed * attributes.moveSpeed * Time.deltaTime);
        }
    }

    private void UniqueMove()
    {
        Hummingbird();
    }

    private void Hummingbird()
    {
        if (attributes.target == null)
        {
            //find new flower
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, GlobalValues.main.hummingbirdRange, (Vector2)transform.position, 0f, GlobalValues.main.flowerMask);
            System.Random RandomGen = new System.Random();
            int randompick = RandomGen.Next(hits.Length - 1);
            attributes.target = hits[randompick].transform;
        }
        if (attributes.target != null)
        {
            //move towards flower
            Vector2 direction = (attributes.target.position - transform.position).normalized;
            attributes.rb.linearVelocity = direction * attributes.moveSpeed;
            float angle = Mathf.Atan2(attributes.target.position.y - transform.position.y, attributes.target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 150 * attributes.moveSpeed * Time.deltaTime);
        }
        else
        {
            attributes.rb.linearVelocity = attributes.rb.linearVelocity * 0;
            Debug.Log("Hummingbird unable to find flower.");
        }
    }

    public void SetPathSettings()
    {
        if (attributes.willFly == true)
        {
            attributes.path = LevelManager.main.flyingPaths[attributes.onPath];
        }
        else
        {
            attributes.path = LevelManager.main.paths[attributes.onPath];
        }
        if (attributes.path != null)
        {
            attributes.target = attributes.path[attributes.pathIndex];
        }
        if (attributes.sName == "Hummingbird")
        {
            attributes.target = null;
        }
    }
}
