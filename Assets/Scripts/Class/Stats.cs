using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stats
{
    [SerializeField] private int _baseAttack;
    
    [SerializeField] private int _baseDefense;
    
    [SerializeField] private int _baseSpeed;
    
    [SerializeField] private int _baseLifePoints;
    
    [SerializeField] private int _baseManaPoints;

    public int GetAttack(int level)
    {
        return Mathf.FloorToInt(0.01f * (2 * _baseAttack) * level) + 5;
    }
    
    public int GetDeffense(int level)
    {
        return Mathf.FloorToInt(0.01f * (2 * _baseDefense) * level) + 5;
    }
    
    public int GetSpeed(int level)
    {
        return Mathf.FloorToInt(0.01f * (2 * _baseSpeed) * level) + 5;
    }
    
    public int GetMaxLifePoint(int level)
    {
        return Mathf.FloorToInt(0.01f * (2 * _baseLifePoints) * level) + level + 10;
    }
    
    public int GetMaxManaPoint(int level)
    {
        return Mathf.FloorToInt(0.01f * (2 * _baseManaPoints) * level) + level + 10;
    }
}
