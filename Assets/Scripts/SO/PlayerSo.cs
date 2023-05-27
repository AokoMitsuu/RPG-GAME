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
    
    public List<ItemClass> Inventory => _inventory;
    [SerializeField] private List<ItemClass> _inventory;
    
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
    public void AddItem(ItemClass item)
    {
        _inventory.Add(item);
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
