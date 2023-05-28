using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private RectTransform _enemyTargetTransform;
    [SerializeField] private FightPlayerPreview[] _fightPlayerPreview;
    [SerializeField] private FightPlayerPreview _fightPlayerAction;
    [SerializeField] private GameObject _actionBox;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private RectTransform _itemContentTransform;
    [SerializeField] private GridLayoutGroup _itemGridLayout;
    [SerializeField] private GameObject _itemToUseGameObject;
    [SerializeField] private GameObject _backgroundObjectsPanel;
    [SerializeField] private HeroFightUI[] _heroesFightUI;
    [SerializeField] private FightTransition _fightTransition;
    [SerializeField] private GameObject _damageFightPopups;
    [SerializeField] private Animator _attackAnimator;

    private List<HeroClass> _heroes;
    private List<HeroClass> _heroesDead;
    private List<EnemyClass> _enemies;

    private EnemyClass _enemyTarget;
    private EnemyClass _enemyAction;
    private GameObject _enemyActionGameObject;
    private Vector3 _enemyActionGameObjectInitialPosition;
    
    private HeroClass _heroTarget;
    private HeroClass _heroAction;
    private GameObject _heroActionGameObject;
    private Vector3 _heroActionGameObjectInitialPosition;

    private FightAction _action;
    private int _totalXp;

    private FightStateMachine _fightStateMachine;
    private void Awake()
    {
        _heroes = new List<HeroClass>();
        _heroesDead = new List<HeroClass>();
        _enemies = new List<EnemyClass>();
        
        _enemyAction = null;
        _heroAction = null;

        _fightStateMachine = new FightStateMachine();
    }

    private void Update()
    {
        _fightStateMachine.UpdateMachine();
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

        _totalXp = 0;
        _enemyTarget = null;
        
        _fightBackground.sprite = background;

        int maxSpeed = 0;
        
        for (int i = 0; i < _heroesImage.Length; i++)
        {
            if (i < heroes.Count)
            {
                _heroesImage[i].gameObject.SetActive(true);
                _heroesImage[i].sprite = heroes[i].GetHeroSprite();
                heroes[i].SetObject(_heroesImage[i], _heroesFightUI[i]); 
                heroes[i].OnDeath += UpdateHeroOnDeath;
                
                if(heroes[i].CurrentLifePoint > 0)
                    _heroes.Add(heroes[i]);
                else
                    _heroesDead.Add(heroes[i]);
                
                _heroesFightUI[i].Init(heroes[i]);
                _fightPlayerPreview[i].InitPlayerPreview(heroes[i]);
                heroes[i].ResetCharge();

                if (maxSpeed < heroes[i].GetHeroSpeed())
                    maxSpeed = heroes[i].GetHeroSpeed();
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

                enemyTmp.OnDeath += UpdateEnemyOnDeath;
                _enemies.Add(enemyTmp);
                enemyTmp.GO.GetComponent<EnemyFightUI>().SetEnemyClass(enemyTmp);
                
                if (_enemyTarget == null)
                {
                    SetEnemyTarget(enemyTmp, _enemiesImage[i].GetComponent<RectTransform>());
                }
                
                if (maxSpeed < enemyTmp.GetEnemySpeed())
                    maxSpeed = enemyTmp.GetEnemySpeed();
            }
            else
            { 
                _enemiesImage[i].gameObject.GetComponent<EnemyFightUI>().SetEnemyClass(null);
                _enemiesImage[i].color = new Color(1, 1, 1, 0);
            }
        }

        UpdateObjectPanel();
        
        _fightUIGameObject.SetActive(true);
        
        _fightStateMachine.SetBlackboardVariable("heroes",_heroes);
        _fightStateMachine.SetBlackboardVariable("heroesDead",_heroesDead);
        _fightStateMachine.SetBlackboardVariable("enemies",_enemies);
        
        _fightStateMachine.SetBlackboardVariable("fightUIGameObject",_fightUIGameObject);
        _fightStateMachine.SetBlackboardVariable("actionBox",_actionBox);
        _fightStateMachine.SetBlackboardVariable("fightPlayerAction",_fightPlayerAction);

        _fightStateMachine.SetBlackboardVariable("itemToUseGameObject", _itemToUseGameObject);
        
        _fightStateMachine.SetBlackboardVariable("Charge",maxSpeed);
        
        _fightStateMachine.SetBlackboardVariable("attackAnimator",_attackAnimator);
    }

    public void StartFight()
    {
        _fightStateMachine.SwitchState(_fightStateMachine.ChargingFightState);
    }

    public void HeroAttack()
    {
        if (_fightStateMachine.CurrentState == _fightStateMachine.SprtieMovingFightState) return;
        
        _fightStateMachine.SetBlackboardVariable("endPos", _enemyTargetTransform.transform.position - new Vector3(125,50,0));
        _fightStateMachine.SetBlackboardVariable("stateAfterMove", _fightStateMachine.HeroAttackFightState);
        
        HeroClass heroTmp = _fightStateMachine.GetBlackboardVariable<HeroClass>("heroAction");
        _action.SetAction(heroTmp.GetHeroAttack(), 0, 0, heroTmp.GetBaseAttackAnimatorController());
        Debug.Log(_action.Damage);
        _fightStateMachine.SetBlackboardVariable("action", _action);
        
        _fightStateMachine.SwitchState(_fightStateMachine.SprtieMovingFightState);
        
    }
    public void SetEnemyTarget(EnemyClass enemyTarget, RectTransform enemyGameObject)
    {
        _enemyTarget = enemyTarget;
        _enemyTargetTransform.SetParent(enemyGameObject, false);
        _enemyTargetTransform.localScale = new Vector3(1, 1, 1);
        _fightStateMachine.SetBlackboardVariable("enemyTarget", enemyTarget);
    }

    public void ItemOnClick(ItemClass item)
    {
        switch (item.Target)
        {
            case ItemTarget.ALIVEHERO:
                _backgroundObjectsPanel.SetActive(false);
                _fightStateMachine.SetBlackboardVariable("itemToUse", item);
                _fightStateMachine.SwitchState(_fightStateMachine.UseItemOnAliveHeroFightState);
                break;
            case ItemTarget.DEADHERO:
                _backgroundObjectsPanel.SetActive(false);
                _fightStateMachine.SetBlackboardVariable("itemToUse", item);
                _fightStateMachine.SwitchState(_fightStateMachine.UseItemOnDeadHeroFightState);
                break;
        }
    }

    public void EntitySelectForItem(HeroClass hero)
    {
        _fightStateMachine.GetBlackboardVariable<ItemClass>("itemToUse").UseEffect(hero);
        UpdateObjectPanel();
        _fightStateMachine.SwitchState(_fightStateMachine.HeroEndAttackFightState);
    }
    
    public void EntitySelectForItem(EnemyClass enemy)
    {
        UpdateObjectPanel();
        _fightStateMachine.SwitchState(_fightStateMachine.EnemyAttackFightState);
    }


    public void UpdateObjectPanel()
    {
        foreach (Transform child in _itemContentTransform.transform)
        {
            Destroy(child.gameObject);
        }
        
        _itemContentTransform.sizeDelta = new Vector2(_itemContentTransform.sizeDelta.x,_itemGridLayout.padding.top + _itemGridLayout.cellSize.y * AppManager.Instance.PlayerManager.PlayerSo.Inventory.Count + _itemGridLayout.spacing.y * AppManager.Instance.PlayerManager.PlayerSo.Inventory.Count);
        foreach (ItemClass item in AppManager.Instance.PlayerManager.PlayerSo.Inventory)
        {
            GameObject itemTmp = Instantiate(_itemPrefab, _itemContentTransform);
            itemTmp.GetComponent<ItemsFightButton>().Init(item);
        }
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
        bool changeTarget = _enemyTarget == enemyClass;
        
        _totalXp += enemyClass.GetXpDrop();
        _fightStateMachine.SetBlackboardVariable("totalXp",_totalXp);
        
        enemyClass.GO.GetComponent<EnemyFightUI>().SetEnemyClass(null);
        enemyClass.Image.color = new Color(1, 1, 1, 0);
        _enemies.Remove(enemyClass);
        
        if (_enemies.Count == 0)
        {
            _fightStateMachine.SwitchState(_fightStateMachine.WinFightState);
            return;
        }
        
        if(changeTarget)
            SetEnemyTarget(_enemies[0],_enemies[0].GO.GetComponent<RectTransform>());
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

public struct FightAction
{
    public int Damage;
    public int Heal;
    public int Cost;
    public AnimatorController AnimatorController;

    public void SetAction(int damage, int heal, int cost, AnimatorController animatorController)
    {
        Damage = damage;
        Heal = heal;
        Cost = cost;
        AnimatorController = animatorController;
    }
}
