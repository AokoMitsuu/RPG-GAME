using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Player", fileName = "Player")]
public class PlayerSo : ScriptableObject
{
    public List<HeroClass> HeroesTeam => _heroesTeam;
    [SerializeField] private List<HeroClass> _heroesTeam;
    
    public List<HealItemClass> HealInventory => _healInventory;
    [SerializeField] private List<HealItemClass> _healInventory;
    
    public List<ReviveItemClass> ReviveInventory => _reviveInInventory;
    [SerializeField] private List<ReviveItemClass> _reviveInInventory;
    
    public Vector3 LastPosition => _lastPosition;
    [SerializeField] private Vector3 _lastPosition;
    
    public string LastSceneName => _lastSceneName;
    [SerializeField] private string _lastSceneName;

    public void Init()
    {
        foreach (HeroClass hero in _heroesTeam)
        {
            hero.Init();
        }
    }
    
    public void Load()
    {
        foreach (HeroClass hero in _heroesTeam)
        {
            hero.Load();
        }
    }
    public void AddItem(HealItemClass item)
    {
        _healInventory.Add(item);
    }
    
    public void AddItem(ReviveItemClass item)
    {
        _reviveInInventory.Add(item);
    }
    
    public void RemoveItem(HealItemClass item)
    {
        _healInventory.Remove(item);
    }
    
    public void RemoveItem(ReviveItemClass item)
    {
        _reviveInInventory.Remove(item);
    }

    public void SetLastPosition(Vector3 lastPosition)
    {
        _lastPosition = lastPosition;
    }

    public void SetLastSceneName(string lastSceneName)
    {
        _lastSceneName = lastSceneName;
    }
}
