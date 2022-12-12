using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttentionSource : MonoBehaviour
{
    [SerializeField] private float _sourceActiveLength = 5f;
    [SerializeField] private UnityEvent OnActivate;
    [SerializeField] private UnityEvent OnDeactivate;
    private AttentionMeter _meter;

    // Start is called before the first frame update
    void Start()
    {
        _meter = FindObjectOfType<AttentionMeter>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateSource()
    {
        StartCoroutine(PerformSourceActions());
    }

    private IEnumerator PerformSourceActions()
    {
        _meter.AddAttentionSource(this);
        if (OnActivate != null)
        {
            OnActivate.Invoke();
        }
        yield return new WaitForSeconds(_sourceActiveLength);
        DeactivateSource();
    }

    private void DeactivateSource()
    {
        _meter.RemoveAttentionSource(this);
        if (OnDeactivate != null)
        {
            OnDeactivate.Invoke();
        }
    }
}
