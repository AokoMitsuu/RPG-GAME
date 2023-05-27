using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HeroDataBase", fileName = "HeroDataBase")]
public class HeroDataBase : ScriptableObject
{
    public List<HeroSo> HeroDatabase => _heroDatabase;
    [SerializeField] private List<HeroSo> _heroDatabase;
}
