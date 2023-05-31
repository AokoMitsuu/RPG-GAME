using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private float _durationBetweenLetter;
    [SerializeField] private float _durationBetweenSentence;
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private TMP_Text _namePNJText;
    [SerializeField] private Image _spritePNJ;
    
    private Coroutine _dialogueCoroutine;
    private bool _skip;

    private void Update()
    {
        if (_dialogueCoroutine == null) return;

        if (Input.GetKeyDown(KeyCode.X))
        {
            Skip();
        }
    }

    public void StartDialogue(DialogueSo dialogue, System.Action endDialogueAction = null)
    {
        if (_dialogueCoroutine != null) return;

        AppManager.Instance.PlayerManager.SetPlayerInteractable(false);
        _dialoguePanel.SetActive(true);
        
        _dialogueCoroutine = _dialogueCoroutine = StartCoroutine(StartDialogueCoroutine(dialogue, endDialogueAction));
    }

    private IEnumerator StartDialogueCoroutine(DialogueSo dialogue, System.Action endDialogueAction = null)
    {
        foreach (SentenceClass sentence in dialogue.Dialogue)
        {
            _spritePNJ.sprite = sentence.PnjSprite;
            _namePNJText.text = sentence.PNJName.GetLocalizedString() + " :";
            _dialogueText.text = "";
            
            foreach (char letter in sentence.Sentence.GetLocalizedString())
            {
                _dialogueText.text += letter;

                float timerLetter = 0;
                while (timerLetter < _durationBetweenLetter)
                {
                    yield return null;
                    
                    timerLetter += Time.deltaTime;

                    if (_skip)
                    {
                        _skip = false;
                        _dialogueText.text = sentence.Sentence.GetLocalizedString();
                        
                        goto SentenceEnd;
                    }
                }
            }
            SentenceEnd:
            
            float timerSentence = 0;
            while (timerSentence < _durationBetweenSentence)
            {
                yield return null; 
                timerSentence += Time.deltaTime;

                if (_skip)
                {
                    _skip = false;
                    break;
                }
            }
        }

        _dialoguePanel.SetActive(false);
        AppManager.Instance.PlayerManager.SetPlayerInteractable(true);
        endDialogueAction?.Invoke();
        _dialogueCoroutine = null;
    }

    public void Skip()
    {
        _skip = true;
    }
}
