using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionMeter : MonoBehaviour
{
    public float m_AttentionIncreasePerSource = 0.1f;
    public float m_AttentionDecrease = 0.2f;
    [SerializeField] private List<AttentionSource> _attentionSources = new List<AttentionSource>();
    [SerializeField] private float _currentAttention = 0f;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_attentionSources.Count > 0)
        {
            _currentAttention += (m_AttentionIncreasePerSource * _attentionSources.Count) * Time.deltaTime;
        }
        else
        {
            _currentAttention -= m_AttentionDecrease * Time.deltaTime;
        }
        _currentAttention = Mathf.Clamp(_currentAttention, 0f, 100f);
    }

    public void AddAttentionSource(AttentionSource _source)
    {
        if (!_attentionSources.Contains(_source))
        {
            _attentionSources.Add(_source);
        }
    }

    public void RemoveAttentionSource(AttentionSource _source)
    {
        if (_attentionSources.Contains(_source))
        {
            _attentionSources.Remove(_source);
        }
    }
}
