using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

[Serializable]
public class HeroClass
{
    public delegate void OnStatsChangeDelegate();
    public OnStatsChangeDelegate OnStatsChange;
    
    public delegate void OnChargeDelegate(float charge);
    public OnChargeDelegate OnCharge;
    
    public delegate void OnDeathDelegate(HeroClass enemyClass);
    public OnDeathDelegate OnDeath;

    public GameObject GO;
    public Image Image;
    public RectTransform RectTransform;
    public HeroFightUI HeroFightUI;

    public int CurrentLifePoint => _currentLifePoint;
    [SerializeField] private int _currentLifePoint;
    
    public int CurrentManaPoint => _currentManaPoint;
    [SerializeField] private int _currentManaPoint;
    
    public int Level => _level;
    [SerializeField] private int _level;
    
    public int XP => _xp;
    [SerializeField] private int _xp;
    
    [SerializeField] private int _heroClassIndex;
    
    public float Charge => _chargeAction;
    private float _chargeAction;
    private HeroSo _hero;

    public void Init()
    {
        _hero = AppManager.Instance.PlayerManager.HeroDataBase.HeroDatabase[_heroClassIndex];
        _currentLifePoint = _hero.Stats.GetMaxLifePoint(_level);
        _currentManaPoint = _hero.Stats.GetMaxManaPoint(_level);
    }
    
    public void Load()
    {
        _hero = AppManager.Instance.PlayerManager.HeroDataBase.HeroDatabase[_heroClassIndex];
    }
    
    public HeroClass()
    {
        _level = 1;
        _xp = 0;
    }

    public void SetObject(Image image, HeroFightUI heroFightUI)
    {
        GO = image.gameObject;
        Image = image;
        RectTransform = image.rectTransform;
        HeroFightUI = heroFightUI;
    }

    #region Getter
    public string GetHeroName()
    {
        return _hero.Name.GetLocalizedString();
    }
    
    public Sprite GetHeroSprite()
    {
        return _hero.FightSprite;
    }
    
    public int GetHeroMaxLifePoint()
    {
        return _hero.Stats.GetMaxLifePoint(_level);
    }
    
    public float GetHeroCurrentLifePointRatio()
    {
        return _currentLifePoint / (float)_hero.Stats.GetMaxLifePoint(_level);
    }
    
    public int GetHeroMaxManaPoint()
    {
        return _hero.Stats.GetMaxManaPoint(_level);
    }
    
    public float GetHeroCurrentManaPointRatio()
    {
        return _currentManaPoint / (float)_hero.Stats.GetMaxManaPoint(_level);
    }
    
    public int GetHeroAttack()
    {
        return _hero.Stats.GetAttack(_level);
    }
    
    public int GetHeroDefense()
    {
        return _hero.Stats.GetDeffense(_level);
    }
    
    public int GetHeroSpeed()
    {
        return _hero.Stats.GetSpeed(_level);
    }

    public AnimatorController GetAnimatorController()
    {
        return _hero.AnimatorController;
    }
    #endregion

    public bool ChargeAction(int chargeTo)
    {
        _chargeAction += GetHeroSpeed() * Time.deltaTime;
        OnCharge?.Invoke(_chargeAction/(float)chargeTo);
        return _chargeAction >= chargeTo;
    }
    
    public void ResetCharge()
    {
        _chargeAction = 0;
        OnCharge?.Invoke(_chargeAction);
    }
    
    public void TakeDamage(int damage)
    {
        _currentLifePoint -= damage;
        
        OnStatsChange?.Invoke();
        
        if (_currentLifePoint <= 0)
        {
            OnDeath?.Invoke(this);
        }
    }

    public void Heal(int heal)
    {
        _currentLifePoint = Mathf.Min(_currentLifePoint + heal, GetHeroMaxLifePoint());
        OnStatsChange?.Invoke();
    }
    public void FullHeal()
    {
        _currentLifePoint = _hero.Stats.GetMaxLifePoint(_level);
        _currentManaPoint = _hero.Stats.GetMaxManaPoint(_level);
    }
}
