using System.Collections;
using System.Collections.Generic;
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CrosshairPOIActivate(bool _state)
    {
        if (_currentCrosshairActiveState == _state) return;
        m_CrosshairImage.rectTransform.localScale = _state ? Vector3.one * 1.5f : Vector3.one * 1f;
        m_CrosshairImage.color = _state ? m_Crosshair_ActiveColor : m_Crosshair_NormalColor;
        _currentCrosshairActiveState = _state;
    }

    public void UpdateAttentionSlider(float _value)
    {
        m_AttentionSlider.value = _value;
        UpdateAttentionSliderColor(_value);
    }

    private void UpdateAttentionSliderColor(float _value)
    {
        m_AttentionSliderFillImage.color = m_AttentionGradient.Evaluate(_value);
    }
}
