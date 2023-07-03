using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Object/HeavyObject")]
public class HeavyObject : ValuableObject
{
    private enum _itemType
    {
        Axe,
        Ladder,
        Screwdriver, 
        Hammer
    }

    [SerializeField] private _itemType _item;
}
