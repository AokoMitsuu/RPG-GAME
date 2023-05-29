using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class SkillFightUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _textName;
    [SerializeField] private TMP_Text _textCost;
    [SerializeField] private LocalizedString _localizeString;
    [SerializeField] private GameObject _lockPanel;

    private SkillSo _skill;
    public void Init(HeroClass heroAction, EntitySkill skill)
    {
        _skill = skill.Skill;
        _image.sprite = _skill.Sprite;
        _textName.text = _skill.Name.GetLocalizedString();
        _textCost.text = _localizeString.GetLocalizedString() + _skill.Cost;
        if (heroAction.CurrentManaPoint < _skill.Cost)
        {
            _lockPanel.SetActive(true);
        }
    }

    public void SelectSkill()
    {
        AppManager.Instance.FightManager.HeroUseSkill(_skill);
    }
}
