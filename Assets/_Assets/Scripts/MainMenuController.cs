using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Material _fullscreenEffectMaterial;


    public RectTransform m_HelpTransform;
    public Vector2 m_Help_VerticalPositionOnOff = Vector2.zero;
    //public Vector2 m_Help_OffScreenPosition = Vector2.zero;

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
        m_HelpTransform.DOAnchorPosY(m_Help_VerticalPositionOnOff.x, 0.5f);
    }

    public void HideHelp()
    {
        m_HelpTransform.DOAnchorPosY(m_Help_VerticalPositionOnOff.y, 0.5f);
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