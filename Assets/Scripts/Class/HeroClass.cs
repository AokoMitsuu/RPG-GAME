using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Animations;

[Serializable]
public class HeroClass : EntityClass
{
    public delegate void OnDeathDelegate(HeroClass enemyClass);
    public OnDeathDelegate OnDeath;
    public delegate void OnStatsChangeDelegate();
    public OnStatsChangeDelegate OnStatsChange;
    public delegate void OnChargeDelegate(float charge);
    public OnChargeDelegate OnCharge;

    public int CurrentLifePoint => _currentLifePoint;
    public int CurrentManaPoint => _currentManaPoint;
    public int XP => _xp;
    [SerializeField] private int _xp;
    
    [SerializeField] private int _heroClassIndex;
    
    public float Charge => _chargeAction;


    public void Init()
    {
        _level = 1;
        _xp = 0;
        _entity = AppManager.Instance.PlayerManager.HeroDataBase.HeroDatabase[_heroClassIndex];
        _currentLifePoint = _entity.Stats.GetMaxLifePoint(_level);
        _currentManaPoint = _entity.Stats.GetMaxManaPoint(_level);
    }
    
    public void Load()
    {
        _entity = AppManager.Instance.PlayerManager.HeroDataBase.HeroDatabase[_heroClassIndex];
    }

    public void SetObject(Image image, EntityFightUI heroFightUI)
    {
        GO = image.gameObject;
        Image = image;
        RectTransform = image.rectTransform;
        EntityFightUI = heroFightUI;
    }
    
    public AnimatorController GetAnimatorController()
    {
        return ((HeroSo)_entity).AnimatorController;
    }
    
    public override bool ChargeAction(int chargeTo)
    {
        _chargeAction += GetSpeed() * Time.deltaTime;
        OnCharge?.Invoke(_chargeAction/(float)chargeTo);
        return _chargeAction >= chargeTo;
    }
    
    public override void ResetCharge()
    {
        base.ResetCharge();
        OnCharge?.Invoke(_chargeAction);
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
        OnStatsChange?.Invoke();
    }

    public override void ConsumeMana(int manaConsume)
    {
        base.ConsumeMana(manaConsume);
        OnStatsChange?.Invoke();
    }

    public void Heal(int heal)
    {
        _currentLifePoint = Mathf.Min(_currentLifePoint + heal, GetMaxLifePoint());
        OnStatsChange?.Invoke();
    }
    public void FullHeal()
    {
        _currentLifePoint = GetMaxLifePoint();
        _currentManaPoint = GetMaxLifePoint();
    }

    public void AddXp(int xp)
    {
        _xp += xp;
        CheckLevel();
    }

    private void CheckLevel()
    {
        for (int i = _level + 1; i <= 100; i++)
        {
            if (_xp >= i * i * i)
            {
                Debug.Log("level up");
                _level = i;
            }
            else
            {
                return;
            }
        }
    }
}
