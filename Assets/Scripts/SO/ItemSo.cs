using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSo : ScriptableObject
{
    public Sprite Sprite => _sprite;
    [SerializeField] private Sprite _sprite;
    
    public ItemTarget Target => _target;
    [SerializeField] private ItemTarget _target;
}
