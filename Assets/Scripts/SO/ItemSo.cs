using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSo : ScriptableObject
{
    public Sprite Sprite => _sprite;
    [SerializeField] private Sprite _sprite;
    
    public ItemType Itemtype => _itemType;
    [SerializeField] private ItemType _itemType;
    
    public RuntimeAnimatorController  AnimatorController => _animatorController;
    [SerializeField] private RuntimeAnimatorController  _animatorController;
}

public enum ItemType
{
    Heal,
    Revive
}