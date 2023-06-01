using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/DataBase",fileName = "DataBase")]
public class QuestDataBase : ScriptableObject
{
    public List<QuestSo> Quests => _quests;
    [SerializeField] private List<QuestSo> _quests;
}
