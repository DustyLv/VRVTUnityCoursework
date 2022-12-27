using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public Image m_CrosshairImage;
    public Color m_Crosshair_NormalColor = Color.white;
    public Color m_Crosshair_ActiveColor = Color.green;
    private bool _currentCrosshairActiveState = false;

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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CrosshairEnable()
    {
        if (_currentCrosshairActiveState) { return; }
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
        //UpdateSliderColor(_attentionSliderFillImage, m_AttentionGradient, _value);
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

    public void ShowEndScreen(GameEndType _gameEndType)
    {
        m_GameEndScreen.SetActive(true);

        switch (_gameEndType)
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
