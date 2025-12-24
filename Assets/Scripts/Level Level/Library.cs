using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class Library : MonoBehaviour
{

    public static Library main;

    // Start is called before the first frame update
    void Awake()
    {
        main = this;
    }

    public int FindLayerNumber(LayerMask layerMask)
    {
        int layerNumber = -1;
        int layer = layerMask.value;
        while (layer > 0)
        {
            layer = layer >> 1;
            layerNumber++;
        }
        return layerNumber;
    }

    public Vector3 FindPositionBetweenPoints(Transform x, Transform y, float distance)
    {
        Vector3 direction = y.position - x.position;
        return x.position + direction.normalized * distance;
    }

    public GameObject Spawn(GameObject prefab, Transform transform)
    {
        GameObject newSpawn = Instantiate(prefab, transform.position, transform.rotation);
        return newSpawn;
    }

    public Transform ShiftTransform(Transform transform, float angleDegrees, float distance)
    {
        // Convert angle from degrees to radians
        float angleRadians = angleDegrees * Mathf.Deg2Rad; // If using Unity's Mathf class
        // Or using System.Math:
        // double angleRadians = Math.PI * angleDegrees / 180.0;

        // Calculate the new coordinates using trigonometry
        float x = transform.position.x + (float)Math.Cos(angleRadians) * distance;
        float y = transform.position.y + (float)Math.Sin(angleRadians) * distance;
        Transform transform2 = transform;
        transform2.position = new Vector2(x,y);
        return transform2;
    }

        public Vector2 ShiftPosition(Vector2 position, float angleDegrees, float distance)
    {
        // Convert angle from degrees to radians
        float angleRadians = angleDegrees * Mathf.Deg2Rad; // If using Unity's Mathf class
        // Or using System.Math:
        // double angleRadians = Math.PI * angleDegrees / 180.0;

        // Calculate the new coordinates using trigonometry
        float x = position.x + (float)Math.Cos(angleRadians) * distance;
        float y = position.y + (float)Math.Sin(angleRadians) * distance;
        return new Vector2(x,y);
    }
}
