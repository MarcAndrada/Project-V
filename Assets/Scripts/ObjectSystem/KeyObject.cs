using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Object/KeyObject")]
public class KeyObject : Object
{
    
    public enum _itemType
    {
        Axe,
        Ladder,
        Screwdriver, 
        Hammer
    }
    

    public _itemType _item;
}
