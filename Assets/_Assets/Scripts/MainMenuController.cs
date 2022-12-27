using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string _gameSceneName = "GameScene";

    [SerializeField] private Material _fullscreenEffectMaterial;

    // Start is called before the first frame update
    void Start()
    {
        SetFullscreenEffect();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDisable()
    {
        ResetFullscreenEffect();
    }

    public void LoadGameScene()
    {
        StartCoroutine(LoadSceneAsync(_gameSceneName));
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
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


    private void SetFullscreenEffect()
    {
        _fullscreenEffectMaterial.SetFloat("_Noise_Opacity", 0.1f);
        _fullscreenEffectMaterial.SetFloat("_Overlay_Color_Opacity", 0.4f);
    }

    private void ResetFullscreenEffect()
    {
        _fullscreenEffectMaterial.SetFloat("_Noise_Opacity", 0f);
        _fullscreenEffectMaterial.SetFloat("_Overlay_Color_Opacity", 0f);
    }
}
