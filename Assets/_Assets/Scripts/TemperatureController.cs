using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TemperatureController : MonoBehaviour
{
    private float _currentTemperature = 0f;
    private float _temperatureMaxValue = 1f;

    [SerializeField] private Material _fullscreenEffectMaterial;


    // Start is called before the first frame update
    void Start()
    {
        UpdateTemperature();
        GameManager.Instance.OnGameEnd += DisableTemperatureUpdate;
        GameManager.Instance.OnGameEnd += ResetFullscreenEffect;
        GameManager.Instance.OnGamePause += PauseTemperatureUpdate;
        GameManager.Instance.OnGameResume += UnpauseTemperatureUpdate;
    }

    private void OnEnable()
    {
        ResetFullscreenEffect();
    }

    private void OnDisable()
    {
        ResetFullscreenEffect();
    }

    private void UpdateTemperature()
    {
        DOTween.To(() => _currentTemperature, x => _currentTemperature = x, _temperatureMaxValue, GameManager.Instance.GameLength).SetEase(Ease.Linear).SetId("tempTween")
            .OnUpdate(() =>
            {
                UIController.Instance.UpdateTemperatureSlider(_currentTemperature / _temperatureMaxValue);
                UpdateFullscreenEffect();
            }).OnComplete(() =>
            {
                GameManager.Instance.GameEnd(GameEndType.Died);
            });
    }

    private void DisableTemperatureUpdate()
    {
        DOTween.Kill("tempTween");
    }

    private void PauseTemperatureUpdate()
    {
        DOTween.Pause("tempTween");
    }

    private void UnpauseTemperatureUpdate()
    {
        DOTween.Play("tempTween");
    }

    private void ResetFullscreenEffect()
    {
        _fullscreenEffectMaterial.SetFloat("_Noise_Opacity", 0f);
        _fullscreenEffectMaterial.SetFloat("_Overlay_Color_Opacity", 0f);
    }

    private void UpdateFullscreenEffect()
    {
        _fullscreenEffectMaterial.SetFloat("_Noise_Opacity", _currentTemperature / _temperatureMaxValue);
        _fullscreenEffectMaterial.SetFloat("_Overlay_Color_Opacity", _currentTemperature / _temperatureMaxValue);
    }


}
