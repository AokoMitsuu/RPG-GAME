using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemClass
{
    public Sprite Sprite => _sprite;
    [SerializeField] private Sprite _sprite;
    
    public ItemTarget Target => _target;
    [SerializeField] private ItemTarget _target;

    public ItemClass(ItemSo itemSo)
    {
        _sprite = itemSo.Sprite;
        _target = itemSo.Target;
    }

    public virtual void UseEffect(HeroClass hero)
    {
        AppManager.Instance.PlayerManager.PlayerSo.Inventory.Remove(this);
        
    }

    public virtual void UseEffect(EnemyClass enemy)
    {
        AppManager.Instance.PlayerManager.PlayerSo.Inventory.Remove(this);
    }
}

public enum ItemTarget
{
    NONE,
    ENEMY,
    ALIVEHERO,
    DEADHERO,
}
