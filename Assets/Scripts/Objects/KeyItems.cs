using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KeyObject", menuName = "KeyObject")]
public class KeyItems : ScriptableObject
{
    private enum keyItem { Axe, Ladder, ScrewDriver, Hammer};

    [SerializeField]
    private keyItem items;



    public string GetItem()
    {
        return items.ToString();
    }
}
