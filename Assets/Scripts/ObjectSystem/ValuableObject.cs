using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuableObject : Object
{
    [SerializeField]
    protected int Score;
    
    [Range(0.1f, 5f)]
    [SerializeField]
    protected float SpeedReduce;


    protected int ReturnScore()
    {
        return Score;
    }

    protected float ReturnSpeedAmount()
    {
        return SpeedReduce;
    }
}
