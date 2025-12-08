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
}
