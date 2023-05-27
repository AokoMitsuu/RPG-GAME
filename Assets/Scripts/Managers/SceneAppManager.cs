using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneAppManager : MonoBehaviour
{
    [SerializeField] private Image _fade;
    [SerializeField] private string _currentScene;

    public void Init(string startScene)
    {
        _currentScene = startScene;
    }
    public void SwitchScene(string newScene, float fadeDuration, System.Action action = null)
    {
        StartCoroutine(FadeAndSwitchScene(newScene,fadeDuration, action));
    }

    private IEnumerator FadeAndSwitchScene(string newScene,float fadeDuration, System.Action action = null)
    {
        _fade.gameObject.SetActive(true);
        
        _fade.color = new Color(0, 0, 0, 0);
        
        while (_fade.color.a < 1f)
        {
            _fade.color += new Color(0, 0, 0, Time.deltaTime / fadeDuration);
            yield return null;
        }

        _fade.color = new Color(0, 0, 0, 1);
        
        var asyncUnLoadLevel = SceneManager.UnloadSceneAsync(_currentScene);
        while (!asyncUnLoadLevel.isDone){
            yield return null;
        }

        var asyncLoadLevel = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        while (!asyncLoadLevel.isDone){
            yield return null;
        }

        action?.Invoke();
        _currentScene = newScene;
        
        while (_fade.color.a > 0.1f)
        {
            _fade.color -= new Color(0, 0, 0,  Time.deltaTime / fadeDuration);
            yield return null;
        }
        
        _fade.color = new Color(0, 0, 0, 0);
        _fade.gameObject.SetActive(false);
    }
}
