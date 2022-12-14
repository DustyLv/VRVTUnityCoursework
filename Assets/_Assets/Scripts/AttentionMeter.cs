using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionMeter : MonoBehaviour
{
    [SerializeField] private float m_AttentionIncreaseValue = 0f;
    [SerializeField] private float m_AttentionDecrease = 0.2f;
    //[SerializeField] private List<AttentionSource> _attentionSources = new List<AttentionSource>();
    private float _currentAttention = 0f;
    public float CurrentAttention
    {
        get => _currentAttention;
    }


    private float _maxAttentionValue = 100f;
    public float MaxAttentionValue
    {
        get => _maxAttentionValue;

    }

    private bool _attentionUpdateEnabled = false;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGamePause += DisableAttentionUpdate;
        GameManager.Instance.OnGameResume += EnableAttentionUpdate;
        GameManager.Instance.OnGameEnd += DisableAttentionUpdate;
        EnableAttentionUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_attentionUpdateEnabled) { return; }

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

    private void EnableAttentionUpdate()
    {
        _attentionUpdateEnabled = true;
    }
    private void DisableAttentionUpdate()
    {
        _attentionUpdateEnabled = false;
    }
}
