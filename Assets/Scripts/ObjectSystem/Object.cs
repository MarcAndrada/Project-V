using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Object : ScriptableObject
{
    [Header("Model")]
    public Mesh model;
    
    [Header("Object variables")]
    public int _throwForce;
}
