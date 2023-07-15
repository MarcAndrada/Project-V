using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Object")]
public class MovingItems : ScriptableObject
{
   
    public enum ItemType {light, heavy, key };
    [SerializeField]
    private ItemType type;
    public enum Item { Kebab, PosterFiddle, Laptop, Plant, Dog, Cat, CoffeeMug, Chair, Acuarium, Guitar, Pan, Ball, Book, Microwave, Sofa, TV, OfficeTable, LockedSafe, Lamp, Axe, Ladder, ScrewDriver, Hammer };
    [SerializeField]
    private Item items;

    [SerializeField]
    private int score;

    public ItemType GetType()
    {
        return type;
    }
    public Item GetItem()
    {
        return items;
    }
    public int GetScore()
    {
        return score;
    }
}
