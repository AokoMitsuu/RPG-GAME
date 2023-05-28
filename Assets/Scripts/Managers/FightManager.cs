using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FightManager : MonoBehaviour
{
    [SerializeField] private GameObject _fightUIGameObject;
    [SerializeField] private Image _fightBackground;
    [SerializeField] private Image[] _heroesImage;
    [SerializeField] private Image[] _enemiesImage;
    [SerializeField] private FightPlayerPreview[] _fightPlayerPreview;
    [SerializeField] private FightPlayerPreview _fightPlayerAction;
    [SerializeField] private GameObject _actionBox;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private RectTransform _itemContentTransform;
    [SerializeField] private GridLayoutGroup _itemGridLayout;
    [SerializeField] private GameObject _itemToUseGameObject;
    [SerializeField] private GameObject _backgroundObjectsPanel;
    [SerializeField] private EntityFightUI[] _heroesFightUI;
    [SerializeField] private EntityFightUI[] _enemiesFightUI;
    [SerializeField] private FightTransition _fightTransition;
    [SerializeField] private GameObject _damageFightPopups;
    [SerializeField] private Animator _attackAnimator;
    [SerializeField] private float _chargeMaxSeconds;

    private List<EntityClass> _heroes;
    private List<EntityClass> _heroesDead;
    private List<EntityClass> _enemies;


    private FightAction _fightAction;
    private int _totalXp;

    private FightStateMachine _fightStateMachine;
    private void Awake()
    {
        _heroes = new List<EntityClass>();
        _heroesDead = new List<EntityClass>();
        _enemies = new List<EntityClass>();
        
        _fightAction = new FightAction();
        
        _fightStateMachine = new FightStateMachine();
    }
    
    public void EnemyEncounter(Sprite background, List<HeroClass> heroes, ZoneSo.EnemyDataZone enemyDataZone)
    {
        System.Action midCallback = () =>
        {
            SetupFight(background, heroes, enemyDataZone);
        };
        System.Action endCallback = () =>
        {
            StartFight();
        };
        _fightTransition.StartTransition(enemyDataZone.FightTransition.Material, enemyDataZone.FightTransition.Duration, midCallback, endCallback);
    }
    
    public void SetupFight(Sprite background, List<HeroClass> heroes, ZoneSo.EnemyDataZone enemyDataZone)
    {
        _heroes.Clear();
        _heroesDead.Clear();
        _enemies.Clear();
        
        _fightAction = new FightAction();
        
        _totalXp = 0;
        
        _fightBackground.sprite = background;

        int maxSpeed = 0;
        
        for (int i = 0; i < _heroesImage.Length; i++)
        {
            if (i < heroes.Count)
            {
                _heroesImage[i].gameObject.SetActive(true);
                _heroesImage[i].sprite = heroes[i].GetSprite();
                heroes[i].SetObject(_heroesImage[i], _heroesFightUI[i]); 
                heroes[i].OnDeath += UpdateHeroOnDeath;
                
                if(heroes[i].CurrentLifePoint > 0)
                    _heroes.Add(heroes[i]);
                else
                    _heroesDead.Add(heroes[i]);
                
                _heroesFightUI[i].Init(heroes[i]);
                _fightPlayerPreview[i].InitPlayerPreview(heroes[i]);
                heroes[i].ResetCharge();

                if (maxSpeed < heroes[i].GetSpeed())
                    maxSpeed = heroes[i].GetSpeed();
            }
            else
            {
                _heroesImage[i].gameObject.SetActive(false);
                _fightPlayerPreview[i].InitPlayerPreview(null);
            }
        }
        
        for (int i = 0; i < _enemiesImage.Length; i++)
        {
            if (enemyDataZone.EnemyGroup.Enemy[i].Enemy != null)
            {
                _enemiesImage[i].color = new Color(1, 1, 1, 1);
                _enemiesImage[i].sprite = enemyDataZone.EnemyGroup.Enemy[i].Enemy.Sprite;
                
                EnemyClass enemyTmp = new EnemyClass(enemyDataZone.EnemyGroup.Enemy[i].Enemy,Random.Range(enemyDataZone.EnemyGroup.Enemy[i].MinLevel, enemyDataZone.EnemyGroup.Enemy[i].MaxLevel), _enemiesImage[i]);
                enemyTmp.EntityFightUI = _enemiesFightUI[i];
                _enemiesFightUI[i].Init(enemyTmp);
                
                enemyTmp.OnDeath += UpdateEnemyOnDeath;
                _enemies.Add(enemyTmp);
                
                if (maxSpeed < enemyTmp.GetSpeed())
                    maxSpeed = enemyTmp.GetSpeed();
            }
            else
            {
                _enemiesImage[i].color = new Color(1, 1, 1, 0);
            }
        }
        
        _fightUIGameObject.SetActive(true);
        
        _fightStateMachine.SetBlackboardVariable("heroes",_heroes);
        _fightStateMachine.SetBlackboardVariable("heroesDead",_heroesDead);
        _fightStateMachine.SetBlackboardVariable("enemies",_enemies);
        
        _fightStateMachine.SetBlackboardVariable("fightUIGameObject",_fightUIGameObject);
        _fightStateMachine.SetBlackboardVariable("actionBox",_actionBox);
        _fightStateMachine.SetBlackboardVariable("fightPlayerAction",_fightPlayerAction);

        _fightStateMachine.SetBlackboardVariable("itemToUseGameObject", _itemToUseGameObject);
        
        _fightStateMachine.SetBlackboardVariable("charge",maxSpeed);
        _fightStateMachine.SetBlackboardVariable("chargeMaxSecond",_chargeMaxSeconds);
        
        _fightStateMachine.SetBlackboardVariable("attackAnimator",_attackAnimator);
        
        _fightStateMachine.SetBlackboardVariable("fightAction",_fightAction);
    }

    public void StartFight()
    {
        _fightStateMachine.SwitchState(_fightStateMachine.ChargingFightState);
    }
    
    private void Update()
    {
        _fightStateMachine.UpdateMachine();
    }
    
    public void HeroAttack()
    {
        if (_fightStateMachine.CurrentState != _fightStateMachine.WaitActionFightState) return;
        
        _fightStateMachine.SetBlackboardVariable("entitiesTarget", _enemies);
        _fightStateMachine.SwitchState(_fightStateMachine.WaitTargetFightState);
    }

    public void SelectTarget(EntityClass entity)
    {
        if (_fightStateMachine.CurrentState != _fightStateMachine.WaitTargetFightState) return;
        
        _fightAction = _fightStateMachine.GetBlackboardVariable<FightAction>("fightAction");

        _fightAction.TargetPos = entity.GO.transform.position - new Vector3(125, 0, 0);
        _fightAction.Damage = _fightAction.EntityAction.GetAttack();
        _fightAction.EntityTarget = entity;
        
        _fightStateMachine.SetBlackboardVariable("stateAfterMove", _fightStateMachine.EntityAttackFightState);
        _fightStateMachine.SetBlackboardVariable("fightAction", _fightAction);

        _fightStateMachine.SwitchState(_fightStateMachine.SprtieMovingFightState);
    }

    public void ReviveHero(HeroClass hero)
    {
        _heroesDead.Remove(hero);
        _heroes.Add(hero);
    }

    public void InstantiateDamagePopups(int damage, Vector3 position)
    {
        GameObject damagePopupsTmp = Instantiate(_damageFightPopups, _fightUIGameObject.transform);
        damagePopupsTmp.GetComponent<DamageFightPopups>().Init(damage, position);
    }

    public void ShakeEntity(GameObject entity, float duration, float speed, float amount)
    {
        StartCoroutine(ShakeEntityCoroutine(entity, duration, speed, amount));
    }
    private IEnumerator ShakeEntityCoroutine(GameObject entity, float duration, float speed, float amount)
    {
        float time = 0;
        Vector3 oldPos = entity.transform.position;
        while (time < duration)
        {
            time += Time.deltaTime;
            entity.transform.position = new Vector3(oldPos.x + Mathf.Sin(Time.time * speed) * amount,entity.transform.position.y,entity.transform.position.z);
            yield return null;
        }

        entity.transform.position = oldPos;
    }
    
    private void UpdateEnemyOnDeath(EnemyClass enemyClass)
    {
        _totalXp += enemyClass.GetXpDrop();
        _fightStateMachine.SetBlackboardVariable("totalXp",_totalXp);
        
        enemyClass.Image.color = new Color(1, 1, 1, 0);
        _enemies.Remove(enemyClass);
        
        if (_enemies.Count == 0)
        {
            _fightStateMachine.SwitchState(_fightStateMachine.WinFightState);
        }
    }

    private void UpdateHeroOnDeath(HeroClass heroClass)
    {
        _heroesDead.Add(heroClass);
        _heroes.Remove(heroClass);
        if (_heroes.Count == 0)
        {
            _fightStateMachine.SwitchState(_fightStateMachine.LostFightState);
        }
    }
}