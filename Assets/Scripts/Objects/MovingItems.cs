using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Object")]
public class MovingItems : ScriptableObject
{
   
    private enum weightType { light, heavy };
    [SerializeField]
    private weightType type;
    private enum item { Kebab, PosterFiddle, Laptop, Plant, Dog, Cat, CoffeeMug, Chair, Acuarium, Guitar, Pan, Ball, Book, Microwave, Sofa, TV, OfficeTable, LockedSafe, Lamp };
    [SerializeField]
    private item items;

    [SerializeField]
    private int score;

    public string GetWeight()
    {
        return type.ToString();
    }
    public string GetItem()
    {
        return items.ToString();
    }
    public int GetScore()
    {
        return score;
    }
}
