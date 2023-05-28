using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class ItemSo : ScriptableObject
{
    public Sprite Sprite => _sprite;
    [SerializeField] private Sprite _sprite;
    
    public ItemType Itemtype => _itemType;
    [SerializeField] private ItemType _itemType;
    
    public AnimatorController AnimatorController => _animatorController;
    [SerializeField] private AnimatorController _animatorController;
}

public enum ItemType
{
    Heal,
    Revive
}