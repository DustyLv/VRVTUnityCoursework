using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string _gameSceneName = "GameScene";
    [SerializeField] private string _menuSceneName = "MainMenu";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadGameScene()
    {
        StartCoroutine(LoadSceneAsync(_gameSceneName));
    }

    public void LoadMenuScene()
    {
        StartCoroutine(LoadSceneAsync(_menuSceneName));
    }

    private IEnumerator LoadSceneAsync(string _sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


}
