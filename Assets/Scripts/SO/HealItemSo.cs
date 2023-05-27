using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumable/Heal", fileName = "Heal")]
public class HealItemSo : ItemSo
{
    public int Heal => _heal;
    [SerializeField] private int _heal;
}
