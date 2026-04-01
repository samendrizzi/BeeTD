using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public struct MobStruct
{
    public GameObject prefab;
    public VariantType variant;
    public Prest[] prestiges;
    public int path;

    public MobStruct(GameObject arg1, VariantType arg2, Prest[] arg3, int arg4)
    {
        prefab = arg1;
        variant = arg2;
        prestiges = arg3;
        path = arg4;
    }

}

