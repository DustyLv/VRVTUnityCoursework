using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionMeter : MonoBehaviour
{
    //public float m_AttentionIncreasePerSource = 0.1f;
    public float m_AttentionIncreaseValue = 0f;
    public float m_AttentionDecrease = 0.2f;
    [SerializeField] private List<AttentionSource> _attentionSources = new List<AttentionSource>();
    private float _currentAttention = 0f;
    public float CurrentAttention
    {
        get
        {
            return _currentAttention;
        }
    }


    private float _maxAttentionValue = 100f;
    public float MaxAttentionValue
    {

        get
        {
            return _maxAttentionValue;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.GameRunning) { return; }

        if (m_AttentionIncreaseValue > 0)
        {
            _currentAttention += m_AttentionIncreaseValue * Time.deltaTime;
        }
        else
        {
            _currentAttention -= m_AttentionDecrease * Time.deltaTime;
        }

        _currentAttention = Mathf.Clamp(_currentAttention, 0f, _maxAttentionValue);

        if (_currentAttention >= _maxAttentionValue)
        {
            GameManager.Instance.GameEnd(GameEndType.Saved);
        }

        UIController.Instance.UpdateAttentionSlider(_currentAttention / _maxAttentionValue);
    }

    public void AddAttentionSource(AttentionSource _source, float _addAttentionValue)
    {
        m_AttentionIncreaseValue += _addAttentionValue;
    }

    public void RemoveAttentionSource(AttentionSource _source, float _addAttentionValue)
    {
        m_AttentionIncreaseValue -= _addAttentionValue;
    }
}
