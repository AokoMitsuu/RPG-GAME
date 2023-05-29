using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;

public class EnemyClass : EntityClass
{
    public delegate void OnDeathDelegate(EnemyClass enemyClass);
    public OnDeathDelegate OnDeath;
    
    private EnemySo _enemySo;
    
    public EnemyClass(EnemySo enemySo, int level, Image image)
    {
        _enemySo = enemySo;
        _entity = enemySo;
        _level = level;
        _currentLifePoint = _entity.Stats.GetMaxLifePoint(_level);
        _currentManaPoint = _entity.Stats.GetMaxManaPoint(_level);
        
        GO = image.GameObject();
        RectTransform = image.rectTransform;
        Image = image;
    }
    
    public override void TakeDamage(int damage)
    {
        _currentLifePoint -= damage;

        if (_currentLifePoint <= 0)
        {
            OnDeath?.Invoke(this);
        }
        else
        {
            AppManager.Instance.FightManager.ShakeEntity(GO, 1, 12, 12);
        }
    }
    public int GetXpDrop()
    {
        return (_enemySo.XpDrop*Level)/7;
    }
}
