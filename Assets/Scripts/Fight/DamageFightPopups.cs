using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class DamageFightPopups : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
    [SerializeField] private float _dispertionX;
    [SerializeField] private float _dispertionY;
    private TMP_Text text;
    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void Init(int damage, Vector3 position)
    {
        text.text = damage.ToString();
        transform.position = position + new Vector3(Random.Range(-_dispertionX,_dispertionX), Random.Range(0,_dispertionY), 0);

        StartCoroutine(MoveAndDestroy());
    }

    private IEnumerator MoveAndDestroy()
    {
        float lifeTime = 0;
        while (_lifeTime > lifeTime)
        {
            lifeTime += Time.deltaTime;
            transform.position += Vector3.up * _speed * Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(1, 0, lifeTime / _lifeTime));
            yield return null;
        }
        Destroy(gameObject);
    }
}
