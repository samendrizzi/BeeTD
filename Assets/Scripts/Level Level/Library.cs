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

    public void TakeDamage(GameObject other, float damage, float armorPierce)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().TakeDamage(damage, armorPierce);
        }
        else if (other.GetComponent<Boss>() != null)
        {
            other.GetComponent<Boss>().TakeDamage(damage, armorPierce);
        }
    }

    public void Heal(GameObject other, float heal)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().Heal(heal);
        }
        else if (other.GetComponent<Boss>() != null)
        {
            other.GetComponent<Boss>().Heal(heal);
        }
    }

    public void UpdateSpeed(GameObject other, float slowEffect, float duration)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().UpdateSpeed(slowEffect, duration);
        }
        else if (other.GetComponent<Boss>() != null)
        {
            other.GetComponent<Boss>().UpdateSpeed(slowEffect, duration);
        }
    }

    public void Freeze(GameObject other, float duration)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().Freeze(duration);
        }
        else if (other.GetComponent<Boss>() != null)
        {
            other.GetComponent<Boss>().Freeze(duration);
        }
    }

    public float GetMaxHP(GameObject other)
    {
        float ret = 0f;
        if (other.GetComponent<Enemy>() != null)
        {
            ret = other.GetComponent<Enemy>().maxHP;
        }
        else if (other.GetComponent<Boss>() != null)
        {
            ret = other.GetComponent<Boss>().maxHP;
        }
        return ret;
    }

    public bool GetWillFly(GameObject other)
    {
        bool ret = false;
        if (other.GetComponent<Enemy>() != null)
        {
            ret = other.GetComponent<Enemy>().willFly;
        }
        else if (other.GetComponent<Boss>() != null)
        {
            ret = other.GetComponent<Boss>().willFly;
        }
        return ret;
    }

    public bool IsDestroyed(GameObject other)
    {
        bool ret = false;
        if (other.GetComponent<Enemy>() != null)
        {
            ret = other.GetComponent<Enemy>().isDestroyed;
        }
        else if (other.GetComponent<Boss>() != null)
        {
            ret = other.GetComponent<Boss>().isDestroyed;
        }
        else if (other.GetComponent<Turret>() != null)
        {
            ret = other.GetComponent<Turret>().isDestroyed;
        }
        return ret;
    }

    public void SetPath(GameObject other, int path)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().onPath = path;
        }
        else if (other.GetComponent<Boss>() != null)
        {
            other.GetComponent<Boss>().onPath = path;
        }
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

    //Failed Attempt
    public GameObject[] FindObjectsInLayer(LayerMask layer)
    {
        GameObject[] array = new GameObject[] { };
        GameObject[] root = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in root)
        {
            if (obj.layer == layer)
            {
                array = new GameObject[] { obj};
            }
        }
        return array;
    }
}
