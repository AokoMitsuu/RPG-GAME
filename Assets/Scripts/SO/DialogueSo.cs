using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue", fileName = "Dialogue")]
public class DialogueSo : ScriptableObject
{
     public List<SentenceClass> Dialogue => _dialogue;
     [SerializeField] private List<SentenceClass> _dialogue;
}
