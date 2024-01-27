using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/NewItem", order = 0)]
public class Item : ScriptableObject
{
    public Sprite Texture;

    public string Name;

    public StringStringDictionary Combinations;

    public bool Combine(string CombineTo, out string OutResult) { return Combinations.TryGetValue(CombineTo, out OutResult); }
}
