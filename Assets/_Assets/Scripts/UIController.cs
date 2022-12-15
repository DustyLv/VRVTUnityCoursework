using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image m_CrosshairImage;
    public Color m_Crosshair_NormalColor = Color.white;
    public Color m_Crosshair_ActiveColor = Color.green;
    private bool _currentCrosshairActiveState = false;

    public Slider m_AttentionSlider;
    private Image m_AttentionSliderFillImage;
    public Gradient m_AttentionGradient;

    public Slider m_TemperatureSlider;
    private Image m_TemperatureSliderFillImage;
    public Gradient m_TemperatureGradient;


    public static UIController Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        m_AttentionSliderFillImage = m_AttentionSlider.fillRect.gameObject.GetComponent<Image>();
        m_TemperatureSliderFillImage = m_TemperatureSlider.fillRect.gameObject.GetComponent<Image>();

        GameManager.Instance.OnGameEnd += CrosshairDisable;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CrosshairEnable()
    {
        if(_currentCrosshairActiveState) { return; }
        m_CrosshairImage.rectTransform.localScale = Vector3.one * 1.5f;
        m_CrosshairImage.color = m_Crosshair_ActiveColor;
        _currentCrosshairActiveState = true;
    }

    public void CrosshairDisable()
    {
        if (!_currentCrosshairActiveState) { return; }
        m_CrosshairImage.rectTransform.localScale = Vector3.one * 1f;
        m_CrosshairImage.color = m_Crosshair_NormalColor;
        _currentCrosshairActiveState = false;
    }

    public void UpdateAttentionSlider(float _value)
    {
        UpdateSliderValue(m_AttentionSlider, _value);
        UpdateSliderColor(m_AttentionSliderFillImage, m_AttentionGradient, _value);
    }
    public void UpdateTemperatureSlider(float _value)
    {
        UpdateSliderValue(m_TemperatureSlider, _value);
        UpdateSliderColor(m_TemperatureSliderFillImage, m_TemperatureGradient, _value);
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
