using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumable/Revive", fileName = "Revive")]
public class ReviveItemSo : ItemSo
{
    public int Heal => _heal;
    [SerializeField] private int _heal;
}