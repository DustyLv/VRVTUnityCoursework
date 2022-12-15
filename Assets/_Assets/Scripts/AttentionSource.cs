using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttentionSource : MonoBehaviour
{
    [SerializeField] private float _sourceActiveLength = 5f;
    [SerializeField] private float _sourceCooldownTime = 5f;
    [SerializeField] private float _attentionValue = 0.1f;
    [SerializeField] private UnityEvent OnActivate;
    [SerializeField] private UnityEvent OnDeactivate;
    private AttentionMeter _meter;
    private bool _isActive = false;
    private bool _onCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        _meter = FindObjectOfType<AttentionMeter>();
    }

    public void TryActivateSource()
    {
        if(_isActive || _onCooldown) { return; }

        StartCoroutine(PerformSourceActions());
    }

    private IEnumerator PerformSourceActions()
    {
        _isActive = true;
        _meter.AddAttentionSource(this, _attentionValue);
        if (OnActivate != null)
        {
            OnActivate.Invoke();
        }
        yield return new WaitForSeconds(_sourceActiveLength);
        DeactivateSource();
    }

    private void DeactivateSource()
    {
        _isActive = false;
        _meter.RemoveAttentionSource(this, _attentionValue);
        StartCoroutine(Cooldown());
        if (OnDeactivate != null)
        {
            OnDeactivate.Invoke();
        }
    }

    private IEnumerator Cooldown()
    {
        _onCooldown = true;
        yield return new WaitForSeconds(_sourceCooldownTime);
        _onCooldown = false;
    }
}
