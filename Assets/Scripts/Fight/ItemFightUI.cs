using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemFightUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    private ItemClass _item;
    
    public void Init(HealItemClass item)
    {
        _item = item;
        _image.sprite = item.Sprite;
    }
    
    public void Init(ReviveItemClass item)
    {
        _item = item;
        _image.sprite = item.Sprite;
    }

    public void Onclick()
    {
        AppManager.Instance.FightManager.HeroUseItem(_item);
    } 
}