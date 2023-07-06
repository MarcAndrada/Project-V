using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuableObject : Object
{
    [SerializeField]
    protected int Score;
    
    [Range(0.1f, 0.99f)]
    [SerializeField]
    protected float SpeedReduce;


    protected int GetScore()
    {
        return Score;
    }

    protected float GetSpeedAmount()
    {
        return SpeedReduce;
    }
}
