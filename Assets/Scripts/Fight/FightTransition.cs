using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightTransition : MonoBehaviour
{
    [SerializeField] private Image _transitionPanel;

    public void StartTransition(Material material, float duration, System.Action midCallback = null, System.Action endCallback = null)
    {
        StartCoroutine(TransitionCoroutine(material, duration, midCallback, endCallback));
    }

    private IEnumerator TransitionCoroutine(Material material, float duration, System.Action midCallback = null, System.Action endCallback = null)
    {
        _transitionPanel.material = material;
        _transitionPanel.gameObject.SetActive(true);

        int progress = Shader.PropertyToID("_Progress");
        material.SetFloat(progress, 1);

        float step = 1;

        while (step > -0.5f)
        {
            step -= Time.deltaTime / duration;
            material.SetFloat(progress, step);
            yield return null;
        }

        midCallback?.Invoke();

        while (step < 1)
        {
            step += Time.deltaTime / duration;
            material.SetFloat(progress, step);
            yield return null;
        }
        
        _transitionPanel.gameObject.SetActive(false);
        endCallback?.Invoke();
    }
}
