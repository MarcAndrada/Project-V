using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "Object/LightObject")]
public class LightObject : ValuableObject
{
    private enum _itemType
    { 
        Kebab,
        PosterFiddle,
        Laptop,
        Plant,
        Dog,
        Cat,
        CoffeeMug,
        Chair,
        Acuarium,
        Guitar,
        Pan,
        Ball,
        Book,
        Microwave,
        Sofa,
        TV,
        OfficeTable,
        LockedSafe,
        Lamp 
    }

    [SerializeField] private _itemType _item;
}
