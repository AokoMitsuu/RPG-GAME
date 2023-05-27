using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsFightButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    private ItemClass _item;
    
    public void Init(ItemClass item)
    {
        _item = item;
        _image.sprite = item.Sprite;
    }

    public void Onclick()
    {
        AppManager.Instance.FightManager.ItemOnClick(_item);
    } 
}
