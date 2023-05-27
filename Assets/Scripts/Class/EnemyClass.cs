using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyClass
{
    public delegate void OnDeathDelegate(EnemyClass enemyClass);
    public OnDeathDelegate OnDeath;
    
    public GameObject GO;
    public Image Image;
    public RectTransform RectTransform;
    
    private int _currentLifePoint;
    private int _currentManaPoint;
    public int level => _level;
    private int _level;
    
    private EnemySo _enemySo;
    private float _chargeAction;
    
    public EnemyClass(EnemySo enemySo, int level, Image image)
    {
        _enemySo = enemySo;
        _level = level;
        _currentLifePoint = _enemySo.Stats.GetMaxLifePoint(_level);
        _currentManaPoint = _enemySo.Stats.GetMaxManaPoint(_level);

        GO = image.GameObject();
        RectTransform = image.rectTransform;
        Image = image;
    }

    #region Getter
    
    public int GetEnemyAttack()
    {
        return _enemySo.Stats.GetAttack(_level);
    }
    
    public int GetEnemyDefense()
    {
        return _enemySo.Stats.GetDeffense(_level);
    }
    
    public int GetEnemySpeed()
    {
        return _enemySo.Stats.GetSpeed(_level);
    }
    #endregion

    public bool ChargeAction(int chargeTo)
    {
        _chargeAction += GetEnemySpeed() * Time.deltaTime;
        return _chargeAction >= chargeTo;
    }
    
    public void ResetCharge()
    {
        _chargeAction = 0;
    }
    
    public void TakeDamage(int damage)
    {
        _currentLifePoint -= damage;

        if (_currentLifePoint <= 0)
        {
            OnDeath?.Invoke(this);
        }
    }
}
