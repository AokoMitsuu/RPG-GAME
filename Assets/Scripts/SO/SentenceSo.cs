using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[Serializable]
public class SentenceClass
{
    public LocalizedString Sentence => _sentence;
    [SerializeField] private LocalizedString _sentence;
    
    public LocalizedString PNJName => _pnjName;
    [SerializeField] private LocalizedString _pnjName;
    
    public Sprite PnjSprite => _pnjSprite;
    [SerializeField] private Sprite _pnjSprite;
}
