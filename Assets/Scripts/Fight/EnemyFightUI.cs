using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightUI : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    
    private EnemyClass _enemyClass;
    public void SetEnemyClass(EnemyClass enemyClass)
    {
        _enemyClass = enemyClass;
    }

    public void SetEnemyTargert()
    {
        if(_enemyClass == null) return;
        
        AppManager.Instance.FightManager.SetEnemyTarget(_enemyClass, _rectTransform);
    }
}
