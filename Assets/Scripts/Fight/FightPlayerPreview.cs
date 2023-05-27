using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class FightPlayerPreview : MonoBehaviour
{
    [SerializeField] private GameObject _playerPreview;
    [SerializeField] private Image _playerImage;
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private Image _lifeBar;
    [SerializeField] private Image _manaBar;
    [SerializeField] private Image _chargeBar;

    private HeroClass _hero;

    public void InitPlayerPreview(HeroClass hero)
    {
        if (hero == null)
        {
            _playerPreview.SetActive(false);
        }
        else
        {
            _hero = hero;
            UpdatePreview();
            UpdateCharge(_hero.Charge);
            
            _playerImage.sprite = _hero.GetHeroSprite();
            _playerName.text = _hero.GetHeroName();
            
            _hero.OnStatsChange += UpdatePreview;
            _hero.OnCharge += UpdateCharge;
            
            _playerPreview.SetActive(true);
        }
    }
    
    public void InitPlayerPreviewAction(HeroClass hero)
    {
        if (hero == null)
        {
            _playerPreview.SetActive(false);
        }
        else
        {
            _hero = hero;
            UpdatePreview();
            
            UpdateCharge(_hero.Charge);
            _playerImage.sprite = _hero.GetHeroSprite();
            _playerName.text = _hero.GetHeroName();
            
            _playerPreview.SetActive(true);
        }
    }
    

    private void UpdatePreview()
    {
        _lifeBar.fillAmount = _hero.GetHeroCurrentLifePointRatio();
        _manaBar.fillAmount = _hero.GetHeroCurrentManaPointRatio();
    }
    
    private void UpdateCharge(float chargeRatio)
    {
        _chargeBar.fillAmount = chargeRatio;
    }
}
