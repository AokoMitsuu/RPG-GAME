using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Quest", fileName = "Quest")]
public class QuestSo : ScriptableObject
{
    public string Description => _description;
    [SerializeField] private string _description;
}
