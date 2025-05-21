using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public ActionType actionType;
    public bool stackable = true;
    public Sprite image;

    [Header("Optional Value")]
    public int healAmount;
}

public enum ItemType
{
    Weapon,
    Potion,
    Key
}

public enum ActionType
{
    Attack,
    Heal,
    Unlock
}
