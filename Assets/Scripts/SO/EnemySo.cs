using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entities/Enemy", fileName = "Enemy")]
public class EnemySo : ScriptableObject
{
    public string Name => _name;
    [SerializeField] private string _name;
    
    public Sprite Sprite => _sprite;
    [SerializeField] private Sprite _sprite;
    
    public Stats Stats => _stats;
    [SerializeField] private Stats _stats;
}
