using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    public Image m_CrosshairImage;
    public Image m_CrosshairOutlineImage;
    public Color m_Crosshair_NormalColor = Color.white;
    public Color m_Crosshair_ActiveColor = Color.green;
    private bool _currentCrosshairActiveState = false;

    public CanvasGroup m_ScreenFader;

    public GameObject m_PauseScreen;

    public Slider m_AttentionSlider;
    private Image _attentionSliderFillImage;
    public Gradient m_AttentionGradient;

    public Slider m_TemperatureSlider;
    private Image _temperatureSliderFillImage;
    public Gradient m_TemperatureGradient;

    public GameObject m_GameEndScreen;
    public GameObject m_Dog_Win;
    public GameObject m_Dog_Lose;
    public TextMeshProUGUI m_GameEndScreenTitle;
    [TextArea] public string m_WinText = "You survived!!! This time....";
    [TextArea] public string m_LoseText = "Congratulations! You are a dehydrated sausage!";

    public static UIController Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _attentionSliderFillImage = m_AttentionSlider.fillRect.gameObject.GetComponent<Image>();
        _temperatureSliderFillImage = m_TemperatureSlider.fillRect.gameObject.GetComponent<Image>();

        SetUpEndScreen();

        GameManager.Instance.OnGameEnd += CrosshairDisable;
        GameManager.Instance.OnGameEnd += FadeScreen_HideScreen;

        GameManager.Instance.OnGamePause += ShowPauseScreen;
        GameManager.Instance.OnGameResume += HidePauseScreen;

        HidePauseScreen();

        m_ScreenFader.alpha = 1f;
        FadeScreen_ShowScreen();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CrosshairEnable()
    {
        if (_currentCrosshairActiveState) { return; }

        m_CrosshairImage.rectTransform.DOComplete();
        m_CrosshairOutlineImage.rectTransform.DOComplete();

        m_CrosshairImage.rectTransform.DOScale(Vector3.one * 1.5f, 0.3f).SetEase(Ease.InOutBack).OnStart(() =>
        {
            m_CrosshairImage.rectTransform.DORotate(Vector3.forward * 90f, 0.3f, RotateMode.LocalAxisAdd);
            m_CrosshairOutlineImage.rectTransform.DOScale(Vector3.one * 2f, 0.1f);
            m_CrosshairImage.color = m_Crosshair_ActiveColor;
        }).OnComplete(() =>
        {

            _currentCrosshairActiveState = true;
        });
    }

    public void CrosshairDisable()
    {
        if (!_currentCrosshairActiveState) { return; }

        m_CrosshairImage.rectTransform.DOComplete();
        m_CrosshairOutlineImage.rectTransform.DOComplete();

        m_CrosshairImage.rectTransform.DOScale(Vector3.one, 0.4f).SetEase(Ease.InOutBack).OnStart(() =>
        {
            m_CrosshairImage.rectTransform.DORotate(Vector3.forward * 90f, 0.2f, RotateMode.LocalAxisAdd);
            m_CrosshairOutlineImage.rectTransform.DOScale(Vector3.one, 0.2f);
            m_CrosshairImage.color = m_Crosshair_NormalColor;
        }).OnComplete(() =>
        {

            _currentCrosshairActiveState = false;
        });
    }

    public void FadeScreen_ShowScreen()
    {
        m_ScreenFader.DOFade(0f, 2f);
    }

    public void FadeScreen_HideScreen()
    {
        m_ScreenFader.DOFade(1f, 2f);
    }

    public void ShowPauseScreen()
    {
        m_PauseScreen.SetActive(true);
    }

    public void HidePauseScreen()
    {
        m_PauseScreen.SetActive(false);
    }

    public void UpdateAttentionSlider(float _value)
    {
        UpdateSliderValue(m_AttentionSlider, _value);
    }
    public void UpdateTemperatureSlider(float _value)
    {
        UpdateSliderValue(m_TemperatureSlider, _value);
        UpdateSliderColor(_temperatureSliderFillImage, m_TemperatureGradient, _value);
    }

    private void SetUpEndScreen()
    {
        m_Dog_Win.SetActive(false);
        m_Dog_Lose.SetActive(false);
        m_GameEndScreen.SetActive(false);
    }

    public void ShowEndScreen()
    {
        m_GameEndScreen.SetActive(true);
        FadeScreen_ShowScreen();
        switch (GameManager.Instance._gameEndType)
        {
            case GameEndType.Saved:
                m_GameEndScreenTitle.text = m_WinText;
                m_Dog_Win.SetActive(true);
                break;
            case GameEndType.Died:
                m_GameEndScreenTitle.text = m_LoseText;
                m_Dog_Lose.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void UpdateSliderValue(Slider _slider, float _value)
    {
        _slider.value = _value;
    }

    private void UpdateSliderColor(Image _sliderImage, Gradient _gradient, float _value)
    {
        _sliderImage.color = _gradient.Evaluate(_value);
    }
}
