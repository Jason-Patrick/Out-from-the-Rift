﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "Scriptable Objects/Tile")]
public class Tile : ScriptableObject
{
    public GameObject tileObj;
    public GameObject GetTile() { return tileObj; }
}
