using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[Serializable]
public class ItemClass
{
    public Sprite Sprite => _sprite;
    [SerializeField] private Sprite _sprite;
    
    public ItemType Itemtype => _itemType;
    [SerializeField] private ItemType _itemType;
    
    public AnimatorController AnimatorController => _animatorController;
    [SerializeField] private AnimatorController _animatorController;
    public ItemClass(ItemSo itemSo)
    {
        _sprite = itemSo.Sprite;
        _itemType = itemSo.Itemtype;
        _animatorController = itemSo.AnimatorController;
    }

    public virtual void UseEffect(HeroClass hero)
    {
        AppManager.Instance.FightManager.UpdateItemsPanel();
    }

    public virtual void UseEffect(EnemyClass enemy)
    {
        AppManager.Instance.FightManager.UpdateItemsPanel();
    }
}
