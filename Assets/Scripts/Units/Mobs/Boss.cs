using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public static Boss main;


    // Start is called before the first frame update
    void Start()
    {
        WaveSpawner.main.EnemySpawned();
    }

    void Update()
    {
 
    }

    private void Move()
    {

    }
}
