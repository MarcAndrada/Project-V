using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KeyObject", menuName = "KeyObject")]
public class KeyItems : ScriptableObject
{
    public enum keyItem { Axe, Ladder, ScrewDriver, Hammer};

    [SerializeField]
    private keyItem items;



    public keyItem GetItem()
    {
        return items;
    }
}
