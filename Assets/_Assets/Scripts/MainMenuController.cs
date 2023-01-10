using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Material _fullscreenEffectMaterial;


    [SerializeField] private RectTransform m_HelpTransform;
    [SerializeField] private Vector2 m_Help_VerticalPositionOnOff = Vector2.zero;
    [SerializeField] private Ease m_Ease;
    [SerializeField] private float m_HelpTweenLength = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        SetFullscreenEffect();
        HideHelpInstant();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        ResetFullscreenEffect();
    }



    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void ShowHelp()
    {
        m_HelpTransform.DOAnchorPosY(m_Help_VerticalPositionOnOff.x, m_HelpTweenLength).SetEase(m_Ease);
    }

    public void HideHelp()
    {
        m_HelpTransform.DOAnchorPosY(m_Help_VerticalPositionOnOff.y, m_HelpTweenLength).SetEase(m_Ease);
    }

    private void HideHelpInstant()
    {
        m_HelpTransform.anchoredPosition = new Vector2(0, m_Help_VerticalPositionOnOff.y);
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
