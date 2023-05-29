using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ : MonoBehaviour
{
    [SerializeField] private DialogueSo _dialogueSo;
    
    public void StartInteraction()
    {
        AppManager.Instance.DialogueManager.StartDialogue(_dialogueSo);
    }
}
