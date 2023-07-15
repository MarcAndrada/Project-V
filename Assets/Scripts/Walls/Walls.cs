using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wall", menuName = "Wall")]
public class Walls : ScriptableObject
{
    private enum wallTypes { Normal, Wooden, Ventilation, Concrete, UpStairs};

    [SerializeField]
    private wallTypes walls;

    public string GetWalls()
    {
        return walls.ToString();    
    }
}
