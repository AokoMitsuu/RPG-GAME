using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.UI;

public class EntityClass
{
    public GameObject GO;
    public Image Image;
    public RectTransform RectTransform;
    public EntityFightUI EntityFightUI;
    
    protected int _currentLifePoint;
    protected int _currentManaPoint;
    public int level => _level;
    protected int _level;
    
    protected EntitySO _entity;
    protected float _chargeAction;
    
    #region Getter
    public string GetName()
    {
        return _entity.Name.GetLocalizedString();
    }
    
    public Sprite GetSprite()
    {
        return _entity.Sprite;
    }
    
    public int GetMaxLifePoint()
    {
        return _entity.Stats.GetMaxLifePoint(_level);
    }
    
    public float GetCurrentLifePointRatio()
    {
        return _currentLifePoint / (float)_entity.Stats.GetMaxLifePoint(_level);
    }
    
    public int GetMaxManaPoint()
    {
        return _entity.Stats.GetMaxManaPoint(_level);
    }
    
    public float GetCurrentManaPointRatio()
    {
        return _currentManaPoint / (float)_entity.Stats.GetMaxManaPoint(_level);
    }
    
    public int GetAttack()
    {
        return _entity.Stats.GetAttack(_level);
    }
    
    public int GetDefense()
    {
        return _entity.Stats.GetDeffense(_level);
    }
    
    public int GetSpeed()
    {
        return _entity.Stats.GetSpeed(_level);
    }

    public AnimatorController GetBaseAttackAnimatorController()
    {
        return _entity.BaseAttackAnimationController;
    }
    #endregion

    public virtual bool ChargeAction(int chargeTo)
    {
        _chargeAction += GetSpeed() * Time.deltaTime;
        return _chargeAction >= chargeTo;
    }
    
    public virtual void ResetCharge()
    {
        _chargeAction = 0;
    }
    
    public virtual void TakeDamage(int damage){}
    
}
