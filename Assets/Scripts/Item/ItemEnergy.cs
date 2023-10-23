using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEnergy
{
    public float min;
    public float max;
    public float current;

    public bool IsDefault()
    {
        return min == 0 && max == 0 && current == 0;
    }
}
